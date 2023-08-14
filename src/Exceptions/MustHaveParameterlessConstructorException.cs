namespace AspNetCore.MinimalApi.Ext.Exceptions;

internal class MustHaveParameterlessConstructorException : Exception
{
  private const string ErrorMessage = "Class of {0} type must have a parameterless constructor.";

  public MustHaveParameterlessConstructorException(Type type) : base(string.Format(ErrorMessage, type.Name)) { }
}