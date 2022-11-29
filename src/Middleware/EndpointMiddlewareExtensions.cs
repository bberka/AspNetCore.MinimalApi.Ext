using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Selfrated.MinimalAPI.Middleware.Attributes;
using System.Linq.Expressions;
using System.Reflection;

namespace Selfrated.MinimalAPI.Middleware;

public static class EndpointMiddlewareExtensions
{
    public class EndpointMiddlewareOptions
    {
        public RouteType RouteType { get; set; } = RouteType.POST;
        public bool AuthenticationRequired { get; set; } = false;
        public string[] Roles { get; set; } = Array.Empty<string>();
        public Type[] EndpointFilters { get; set; } = Array.Empty<Type>();
    }

    public static void UseEndpoints(this WebApplication app)
    {
        var options = new EndpointMiddlewareOptions();
        UseEndpoints(app, options);
    }

    public static void UseEndpoints(this WebApplication app, EndpointMiddlewareOptions options)
    {
        //get all assemblies that have the EndpointAPIAttribute attribute
        var results = Assembly.GetEntryAssembly().ExportedTypes
                                    .Select(itemType => new
                                    {
                                        itemType,
                                        attribute = itemType.GetCustomAttributes<EndpointAPIAttribute>(false).FirstOrDefault()
                                    }
                                    ).ToList();

        foreach (var classType in results.Where(e => e.attribute != null))
        {
            //instantiate the class
            var instance = Activator.CreateInstance(classType.itemType);

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
                var methodDelegate = method.method.CreateDelegate(
                                        GetDelegateType(method.method),
                                        instance);

                var path = GetUrlPath(options, classType.attribute, classType.itemType.Name, method.attribute, method.method.Name);

                var methodTypes = GetMethodTypes(options, classType.attribute, method.attribute);

                foreach (RouteType enumValue in Enum.GetValues(typeof(RouteType)))
                {
                    RouteHandlerBuilder? call = null;

                    switch (methodTypes & enumValue)
                    {
                        case RouteType.POST:
                            call = app.MapPost(path, methodDelegate);
                            break;
                        case RouteType.GET:
                            call = app.MapGet(path, methodDelegate);
                            break;
                        case RouteType.PUT:
                            call = app.MapPut(path, methodDelegate);
                            break;
                        case RouteType.DELETE:
                            call = app.MapDelete(path, methodDelegate);
                            break;
                    }

                    if (call != null)
                    {
                        if (GetAuthRequired(options, classType.attribute, method.attribute))
                            call.RequireAuthorization();

                        foreach (var role in GetAuthenticationRoles(options, classType.attribute, method.attribute) ?? Array.Empty<string>())
                            call.RequireAuthorization(role);

                        foreach (var filterItem in GetEndpointFilters(options,classType.attribute,method.attribute) ?? Array.Empty<Type>())
                        {
                            if (typeof(IEndpointFilter).IsAssignableFrom(filterItem))
                            {
                                //instantiate the filter
                                var filter = Activator.CreateInstance(filterItem) as IEndpointFilter;

                                if (filter != null)
                                    call.AddEndpointFilter(filter);
                            }
                        }

                    }
                }
            }

        }
    }

    static Type[]? GetEndpointFilters(EndpointMiddlewareOptions options, EndpointAPIAttribute? classAttribute, EndpointMethodAttribute? methodAttribute)
    {
        if (methodAttribute?.EndpointFilters != null)
            return methodAttribute?.EndpointFilters;

        if (classAttribute?.EndpointFilters != null)
            return classAttribute?.EndpointFilters;

        return options.EndpointFilters;
    }

    static string[]? GetAuthenticationRoles(EndpointMiddlewareOptions options, EndpointAPIAttribute? classAttribute, EndpointMethodAttribute? methodAttribute)
    {
        if (methodAttribute?.Roles != null)
            return methodAttribute?.Roles;

        if (classAttribute?.Roles != null)
            return classAttribute?.Roles;

        return options.Roles;
    }

    static RouteType? GetMethodTypes(EndpointMiddlewareOptions options, EndpointAPIAttribute? classAttribute, EndpointMethodAttribute? methodAttribute)
    {
        if (methodAttribute?.RouteType != RouteType.Inherit)
            return methodAttribute?.RouteType;

        if (classAttribute?.RouteType != RouteType.Inherit)
            return classAttribute?.RouteType;

        //make sure not set to inherit
        return options.RouteType != RouteType.Inherit ? options.RouteType : RouteType.POST;
    }

    static bool GetAuthRequired(EndpointMiddlewareOptions options, EndpointAPIAttribute? classAttribute, EndpointMethodAttribute? methodAttribute)
    {
        if (methodAttribute?.AuthenticationRequired != AuthenticationRequired.Inherit)
            return methodAttribute?.AuthenticationRequired == AuthenticationRequired.Yes;

        if (classAttribute?.AuthenticationRequired != AuthenticationRequired.Inherit)
            return classAttribute?.AuthenticationRequired == AuthenticationRequired.Yes;

        return options.AuthenticationRequired;
    }

    static string GetUrlPath(EndpointMiddlewareOptions options, EndpointAPIAttribute? classAttribute, string className, EndpointMethodAttribute? methodAttribute, string methodName)
    {
        var urlPrefix = methodAttribute?.UrlPrefixOverride ?? classAttribute?.Name;

        //blank string means no prefix
        if (urlPrefix != "")
        {
            if (urlPrefix != null)
            {
                if (string.IsNullOrEmpty(urlPrefix))
                {
                    //empty is className
                    urlPrefix = $"{className.Replace("Endpoint", "")}/";
                }
                else
                {
                    urlPrefix = $"{urlPrefix}/";
                }
            }
            else //null is blank
            {
                urlPrefix = "";
            }
        }
        

        var actionName = methodAttribute?.Name;

        if (string.IsNullOrEmpty(actionName))
        {
            actionName = methodName;
        }

        return $"/{urlPrefix}{actionName}";
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


