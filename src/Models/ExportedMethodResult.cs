using System.Reflection;

namespace AspNetCore.MinimalApi.Ext.Models;

internal class ExportedMethodResult
{
  public ExportedMethodResult(MethodInfo method) {
    Method = method;
  }

  public MethodInfo Method { get; set; }
}