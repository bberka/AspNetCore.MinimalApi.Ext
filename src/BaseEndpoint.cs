using System.Collections.Concurrent;
using AspNetCore.MinimalApi.Ext.Models;
using Microsoft.AspNetCore.Routing;

namespace AspNetCore.MinimalApi.Ext;

/// <summary>
///   Base abstract class for defining endpoint handlers.
///   Inheritor of this class must have Handle method.
///   Rest of the methods will be ignored and will not be hosted as endpoint.
/// </summary>
public abstract class BaseEndpoint
{
  /// <summary>
  /// Whether to add containing folder name as controller to the route.
  /// Default value is true.
  ///<br/>
  ///<br/>
  /// If false the given action name will be like this: GLOBAL_PREFIX/ACTION_NAME
  ///<br/>
  ///<br/>
  /// If true the given action name will be like this: GLOBAL_PREFIX/CONTAINING_FOLDER_NAME/ACTION_NAME
  /// </summary>
  public bool AddContainingFolderNameToRoute { get; protected init; } = true;
}