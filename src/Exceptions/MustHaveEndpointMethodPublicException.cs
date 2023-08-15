namespace AspNetCore.MinimalApi.Ext.Exceptions;

internal sealed class MustHaveEndpointMethodPublicException : Exception
{
  
  public const string ErrorMessage = "Type {0} must have a public method named {1}";
  public MustHaveEndpointMethodPublicException(Type type) : base(string.Format(ErrorMessage, type.Name, EndpointOptions.Options.DefaultEndpointMethodName)) { }
}