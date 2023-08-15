namespace AspNetCore.MinimalApi.Ext.Exceptions;

internal sealed class MustHaveEndpointMethodException : Exception
{
  public const string ErrorMessage = "Type {0} must have a method named {1}";
  public MustHaveEndpointMethodException(Type type) : base(string.Format(ErrorMessage, type.Name, EndpointOptions.Options.DefaultEndpointMethodName)) { }
}