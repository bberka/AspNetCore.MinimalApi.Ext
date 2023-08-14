using System.Reflection;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.MinimalApi.Ext.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class EndpointRouteAttribute : Attribute
{
  /// <summary>
  ///   Initializes rout with RouteAttribute
  /// </summary>
  /// <param name="routeAttribute"></param>
  internal EndpointRouteAttribute(RouteAttribute routeAttribute) {
    Template = routeAttribute.Template;
    Name = routeAttribute.Name;
  }

  /// <summary>
  ///   Initializes Route with the given template
  /// </summary>
  /// <param name="template"></param>
  public EndpointRouteAttribute(string template) {
    Template = template;
  }

  /// <summary>
  ///   Initializes Route with the name of the class and containing folder.
  ///   <br /> <br />
  ///   It uses namespace last part as containing folder and class name as action name.
  ///   <br /> <br />
  ///   If the class is in the root of the project, it uses the class name as the template.
  ///   <br /> <br />
  ///   If the class is in a folder, it uses the folder name as the template.
  ///   <br /> <br />
  ///   If the class is inside multiple folders it joins folder names with '/' as the template and adds class name as action
  ///   name.
  ///   <br /><br />
  ///   Make sure namespaces of the classes are correct and unique in same folder.
  /// </summary>
  /// <param name="type">Endpoint class type</param>
  /// <param name="removeEndpointsStringInFolderName">Removes "Endpoints" and "Endpoint" string in folder names</param>
  /// <param name="removeEndpointStringInClassName">Removes "Endpoint" string in class name</param>
  public EndpointRouteAttribute(Type type, bool removeEndpointsStringInFolderName = true, bool removeEndpointStringInClassName = true) {
    var entryAssembly = Assembly.GetEntryAssembly();
    if (entryAssembly == null) throw new InvalidOperationException("Entry assembly not found");

    var baseAssemblyNameSpace = entryAssembly.GetName().Name;
    if (baseAssemblyNameSpace is null) throw new InvalidOperationException("Entry assembly name not found");

    var className = type.Name;

    var cleanNameSpace = type.Namespace?.Replace(baseAssemblyNameSpace, "")?.Trim('.');
    var split = cleanNameSpace?.Split('.');
    if (removeEndpointStringInClassName) className = className.Replace("Endpoint", "");
    if (split == null || split.Length == 0) {
      Template = className;
      return;
    }

    var joined = string.Join("/", split);
    if (removeEndpointsStringInFolderName) joined = joined.Replace("Endpoints", "").Replace("Endpoint", "");
    Template = $"{joined}/{className}";
  }

  public string Template { get; }
  public string? Name { get; set; }
}