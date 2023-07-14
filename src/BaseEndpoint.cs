using Microsoft.AspNetCore.Mvc;

namespace Selfrated.Middleware;

//public abstract class BaseEndpoint<TOut> : IBaseEndpoint
//{
//  public abstract TOut Handle();
//}
//public abstract class BaseEndpoint<TIn, TOut> : IBaseEndpoint
//{
//  public abstract TOut Handle(TIn @in);
//}

//public abstract class BaseEndpoint : IBaseEndpoint
//{
//  public abstract class WithoutRequest
//  {

//    public abstract object Handle();
//  }
//  public abstract class WithRequest<T>
//  {
//    public abstract object Handle(T request);
//  }
//  public abstract class WithResult<TResult>
//  {
//    public abstract TResult Handle();
//  }

//}

public abstract class BaseEndpoint
{

}


public static class BaseEndpointSync
{
  public static class WithRequest<TRequest>
  {
    public abstract class WithResult<TResponse> : BaseEndpoint
    {
      public abstract TResponse Handle(TRequest request);
    }

    public abstract class WithoutResult : BaseEndpoint
    {
      public abstract void Handle(TRequest request);
    }

    public abstract class WithActionResult<TResponse> : BaseEndpoint
    {
      public abstract ActionResult<TResponse> Handle(TRequest request);
    }

    public abstract class WithActionResult : BaseEndpoint
    {
      public abstract ActionResult Handle(TRequest request);
    }
  }

  public static class WithoutRequest
  {
    public abstract class WithResult<TResponse> : BaseEndpoint
    {
      public abstract TResponse Handle();
    }

    public abstract class WithoutResult : BaseEndpoint
    {
      public abstract void Handle();
    }

    public abstract class WithActionResult<TResponse> : BaseEndpoint
    {
      public abstract ActionResult<TResponse> Handle();
    }

    public abstract class WithActionResult : BaseEndpoint
    {
      public abstract ActionResult Handle();
    }
  }
}
