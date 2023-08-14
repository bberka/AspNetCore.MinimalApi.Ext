namespace AspNetCore.MinimalApi.Ext.Exceptions;

internal class MustHavePublicConstructorException : Exception
{
  private const string ErrorMessage = "Class of {0} type must have a public constructor.";

  public MustHavePublicConstructorException(Type type) : base(string.Format(ErrorMessage, type.Name)) { }
}