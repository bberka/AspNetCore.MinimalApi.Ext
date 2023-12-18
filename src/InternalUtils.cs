using System.Linq.Expressions;
using System.Reflection;
using AspNetCore.MinimalApi.Ext.Models;

namespace AspNetCore.MinimalApi.Ext;

internal static class InternalUtils
{
  internal static Type GetDelegateType(this MethodInfo method) {
    var args = new List<Type>(method.GetParameters().Select(p => p.ParameterType));
    Type delegateType;

    if (method.ReturnType == typeof(void)) {
      delegateType = Expression.GetActionType(args.ToArray());
    }
    else {
      args.Add(method.ReturnType);
      delegateType = Expression.GetFuncType(args.ToArray());
    }

    return delegateType;
  }

  internal static List<ExportedClassTypeResult> GetExportedTypeResults(this Assembly assembly) {
    var results = assembly.ExportedTypes
                          .Where(x => x.IsClass
                                      && x.IsSubclassOf(typeof(EndpointContainer))
                                      && !x.IsAbstract
                                      && x.IsPublic)
                          .Select(x => new {
                            Class = x,
                            Contructors = x.GetConstructors()
                          })
                          .Where(x => x.Contructors.Any(y => y.IsPublic && y.GetParameters().Length == 0))
                          .Select(itemType => new ExportedClassTypeResult(itemType.Class))
                          .Where(x => x.Endpoints.Count > 0)
                          .ToList();
    return results;
  }
}