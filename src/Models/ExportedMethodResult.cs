using System.Reflection;

namespace AspNetCore.MinimalApi.Ext.Models;

internal sealed class ExportedMethodResult
{
  public ExportedMethodResult(MethodInfo method) {
    Method = method;
  }

  public MethodInfo Method { get; set; }
}