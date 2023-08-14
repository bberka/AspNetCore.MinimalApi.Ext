namespace AspNetCore.MinimalApi.Ext;

/// <summary>
///   Base abstract class for defining endpoint handlers.
///   Inheritor of this class must have Handle method.
///   Rest of the methods will be ignored and will not be hosted as endpoint.
/// </summary>
public abstract class BaseEndpoint { }