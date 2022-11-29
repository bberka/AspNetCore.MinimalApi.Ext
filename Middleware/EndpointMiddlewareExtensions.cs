using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Selfrated.MinimalAPI.Middleware.Attributes;
using System.Linq.Expressions;
using System.Reflection;

namespace Selfrated.MinimalAPI.Middleware;

public static class EndpointMiddlewareExtensions
{
    public static void UseEndpoints(this WebApplication app)
    {
        //get all assemblies that have the EndpointAPIAttribute attribute
        var results = Assembly.GetCallingAssembly().ExportedTypes
                                    .Select(itemType => new
                                    {
                                        itemType,
                                        attribute = itemType.GetCustomAttribute<EndpointAPIAttribute>()
                                    }
                                    ).ToList();

        foreach (var match in results.Where(e => e.attribute != null))
        {
            //instantiate the class
            var instance = Activator.CreateInstance(match.itemType);

            if (instance == null)
                continue;

            //get methods that have the EndpointMethodAttribute
            var methods = instance.GetType().GetMethods().Select(method => new
                                                        {
                                                            method,
                                                            attribute = method.GetCustomAttribute<EndpointMethodAttribute>()
                                                        });

            foreach (var method in methods.Where(e => e.attribute != null))
            {
                var urlPrefix = method.attribute?.PrefixOverride ?? match.attribute?.Name;

                if (urlPrefix != null)
                {
                    if (string.IsNullOrEmpty(urlPrefix))
                    {
                        //not set the use the class name
                        urlPrefix = $"{match.itemType.Name.Replace("Endpoint", "")}/";
                    }
                    else
                    {
                        urlPrefix = $"{urlPrefix}/";
                    }
                }
                else
                {
                    urlPrefix = "";
                }

                var authRequired = match.attribute?.RequireAuthentication;

                //if explicitly set, then use that value
                if (method.attribute?.RequireAuthentication != null)
                    authRequired = method.attribute.RequireAuthentication.Value;

                var actionName = method.attribute?.Name;

                if (string.IsNullOrEmpty(actionName))
                {
                    actionName = method.method.Name;
                }

                var roles = new List<string>();

                if (method.attribute?.Roles != null)
                    roles = method.attribute.Roles.ToList();
                else if (match.attribute?.Roles != null)
                    roles = match.attribute.Roles.ToList();

                var path = $"/{urlPrefix}{actionName}";

                var methodDelegate = method.method.CreateDelegate(
                                        GetDelegateType(method.method),
                                        instance);

                foreach (MethodTypeEnum enumValue in Enum.GetValues(typeof(MethodTypeEnum)))
                {
                    RouteHandlerBuilder? call = null;

                    switch (method.attribute?.MethodType & enumValue)
                    {
                        case MethodTypeEnum.POST:
                            call = app.MapPost(path, methodDelegate);
                            break;
                        case MethodTypeEnum.GET:
                            call = app.MapGet(path, methodDelegate);
                            break;
                        case MethodTypeEnum.PUT:
                            call = app.MapPut(path, methodDelegate);
                            break;
                        case MethodTypeEnum.DELETE:
                            call = app.MapDelete(path, methodDelegate);
                            break;
                    }
                    
                    if (call != null)
                    {
                        if (authRequired == true)
                            call.RequireAuthorization();

                        foreach (var role in roles)
                            call.RequireAuthorization(role);

                        if (method.attribute?.EndpointFilter != null)
                        {
                            if (typeof(IEndpointFilter).IsAssignableFrom(method.attribute?.EndpointFilter))
                            {
                                //instantiate the filter
                                var filter = Activator.CreateInstance(method.attribute?.EndpointFilter) as IEndpointFilter;

                                if(filter != null)
                                    call.AddEndpointFilter(filter);
                            }
                        }
                    }
                }
            }

        }
    }

    static Type GetDelegateType(MethodInfo method)
    {
        List<Type> args = new List<Type>(
            method.GetParameters().Select(p => p.ParameterType));

        Type delegateType;

        if (method.ReturnType == typeof(void))
        {
            delegateType = Expression.GetActionType(args.ToArray());
        }
        else
        {
            args.Add(method.ReturnType);
            delegateType = Expression.GetFuncType(args.ToArray());
        }

        return delegateType;
    }
}


