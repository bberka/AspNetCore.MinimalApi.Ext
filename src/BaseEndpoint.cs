using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.MinimalApi.Ext;

public abstract class BaseEndpoint
{
}

public static class BaseEndpointSync
{
  public static class WithRequest<TRequest>
  {
    public abstract class WithResult<TResponse> : BaseEndpoint
    {
      public abstract TResponse Handle(HttpContext context, TRequest request);
    }

    public abstract class WithoutResult : BaseEndpoint
    {
      public abstract void Handle(HttpContext context, TRequest request);
    }

    public abstract class WithActionResult<TResponse> : BaseEndpoint
    {
      public abstract ActionResult<TResponse> Handle(HttpContext context, TRequest request);
    }

    public abstract class WithActionResult : BaseEndpoint
    {
      public abstract ActionResult Handle(HttpContext context, TRequest request);
    }
  }

  public static class WithoutRequest
  {
    public abstract class WithResult<TResponse> : BaseEndpoint
    {
      public abstract TResponse Handle(HttpContext context);
    }

    public abstract class WithoutResult : BaseEndpoint
    {
      public abstract void Handle(HttpContext context);
    }

    public abstract class WithActionResult<TResponse> : BaseEndpoint
    {
      public abstract ActionResult<TResponse> Handle(HttpContext context);
    }

    public abstract class WithActionResult : BaseEndpoint
    {
      public abstract ActionResult Handle(HttpContext context);
    }
  }

  public static class WithManyRequest<TRequest, TRequest2, TRequest3, TRequest4, TRequest5, TRequest6, TRequest7,
    TRequest8, TRequest9>
  {
    public abstract class WithResult<TResponse> : BaseEndpoint
    {
      public abstract TResponse Handle(HttpContext context, TRequest request, TRequest2 request2, TRequest3 request3,
        TRequest4 request4, TRequest5 request5, TRequest6 request6, TRequest7 request7, TRequest8 request8,
        TRequest9 request9);
    }

    public abstract class WithoutResult : BaseEndpoint
    {
      public abstract void Handle(HttpContext context, TRequest request, TRequest2 request2, TRequest3 request3,
        TRequest4 request4, TRequest5 request5, TRequest6 request6, TRequest7 request7, TRequest8 request8,
        TRequest9 request9);
    }

    public abstract class WithActionResult<TResponse> : BaseEndpoint
    {
      public abstract ActionResult<TResponse> Handle(HttpContext context, TRequest request, TRequest2 request2,
        TRequest3 request3, TRequest4 request4, TRequest5 request5, TRequest6 request6, TRequest7 request7,
        TRequest8 request8, TRequest9 request9);
    }

    public abstract class WithActionResult : BaseEndpoint
    {
      public abstract ActionResult Handle(HttpContext context, TRequest request, TRequest2 request2, TRequest3 request3,
        TRequest4 request4, TRequest5 request5, TRequest6 request6, TRequest7 request7, TRequest8 request8,
        TRequest9 request9);
    }
  }

  public static class WithManyRequest<TRequest, TRequest2, TRequest3, TRequest4, TRequest5, TRequest6, TRequest7,
    TRequest8>
  {
    public abstract class WithResult<TResponse> : BaseEndpoint
    {
      public abstract TResponse Handle(HttpContext context, TRequest request, TRequest2 request2, TRequest3 request3,
        TRequest4 request4, TRequest5 request5, TRequest6 request6, TRequest7 request7, TRequest8 request8);
    }

    public abstract class WithoutResult : BaseEndpoint
    {
      public abstract void Handle(HttpContext context, TRequest request, TRequest2 request2, TRequest3 request3,
        TRequest4 request4, TRequest5 request5, TRequest6 request6, TRequest7 request7, TRequest8 request8);
    }

    public abstract class WithActionResult<TResponse> : BaseEndpoint
    {
      public abstract ActionResult<TResponse> Handle(HttpContext context, TRequest request, TRequest2 request2,
        TRequest3 request3, TRequest4 request4, TRequest5 request5, TRequest6 request6, TRequest7 request7,
        TRequest8 request8);
    }

    public abstract class WithActionResult : BaseEndpoint
    {
      public abstract ActionResult Handle(HttpContext context, TRequest request, TRequest2 request2, TRequest3 request3,
        TRequest4 request4, TRequest5 request5, TRequest6 request6, TRequest7 request7, TRequest8 request8);
    }
  }

  public static class WithManyRequest<TRequest, TRequest2, TRequest3, TRequest4, TRequest5, TRequest6, TRequest7>
  {
    public abstract class WithResult<TResponse> : BaseEndpoint
    {
      public abstract TResponse Handle(HttpContext context, TRequest request, TRequest2 request2, TRequest3 request3,
        TRequest4 request4, TRequest5 request5, TRequest6 request6, TRequest7 request7);
    }

    public abstract class WithoutResult : BaseEndpoint
    {
      public abstract void Handle(HttpContext context, TRequest request, TRequest2 request2, TRequest3 request3,
        TRequest4 request4, TRequest5 request5, TRequest6 request6, TRequest7 request7);
    }

    public abstract class WithActionResult<TResponse> : BaseEndpoint
    {
      public abstract ActionResult<TResponse> Handle(HttpContext context, TRequest request, TRequest2 request2,
        TRequest3 request3, TRequest4 request4, TRequest5 request5, TRequest6 request6, TRequest7 request7);
    }

    public abstract class WithActionResult : BaseEndpoint
    {
      public abstract ActionResult Handle(HttpContext context, TRequest request, TRequest2 request2, TRequest3 request3,
        TRequest4 request4, TRequest5 request5, TRequest6 request6, TRequest7 request7);
    }
  }

  public static class WithManyRequest<TRequest, TRequest2, TRequest3, TRequest4, TRequest5, TRequest6>
  {
    public abstract class WithResult<TResponse> : BaseEndpoint
    {
      public abstract TResponse Handle(HttpContext context, TRequest request, TRequest2 request2, TRequest3 request3,
        TRequest4 request4, TRequest5 request5, TRequest6 request6);
    }

    public abstract class WithoutResult : BaseEndpoint
    {
      public abstract void Handle(HttpContext context, TRequest request, TRequest2 request2, TRequest3 request3,
        TRequest4 request4, TRequest5 request5, TRequest6 request6);
    }

    public abstract class WithActionResult<TResponse> : BaseEndpoint
    {
      public abstract ActionResult<TResponse> Handle(HttpContext context, TRequest request, TRequest2 request2,
        TRequest3 request3, TRequest4 request4, TRequest5 request5, TRequest6 request6);
    }

    public abstract class WithActionResult : BaseEndpoint
    {
      public abstract ActionResult Handle(HttpContext context, TRequest request, TRequest2 request2, TRequest3 request3,
        TRequest4 request4, TRequest5 request5, TRequest6 request6);
    }
  }

  public static class WithManyRequest<TRequest, TRequest2, TRequest3, TRequest4, TRequest5>
  {
    public abstract class WithResult<TResponse> : BaseEndpoint
    {
      public abstract TResponse Handle(HttpContext context, TRequest request, TRequest2 request2, TRequest3 request3,
        TRequest4 request4, TRequest5 request5);
    }

    public abstract class WithoutResult : BaseEndpoint
    {
      public abstract void Handle(HttpContext context, TRequest request, TRequest2 request2, TRequest3 request3,
        TRequest4 request4, TRequest5 request5);
    }

    public abstract class WithActionResult<TResponse> : BaseEndpoint
    {
      public abstract ActionResult<TResponse> Handle(HttpContext context, TRequest request, TRequest2 request2,
        TRequest3 request3, TRequest4 request4, TRequest5 request5);
    }

    public abstract class WithActionResult : BaseEndpoint
    {
      public abstract ActionResult Handle(HttpContext context, TRequest request, TRequest2 request2, TRequest3 request3,
        TRequest4 request4, TRequest5 request5);
    }
  }

  public static class WithManyRequest<TRequest, TRequest2, TRequest3, TRequest4>
  {
    public abstract class WithResult<TResponse> : BaseEndpoint
    {
      public abstract TResponse Handle(HttpContext context, TRequest request, TRequest2 request2, TRequest3 request3,
        TRequest4 request4);
    }

    public abstract class WithoutResult : BaseEndpoint
    {
      public abstract void Handle(HttpContext context, TRequest request, TRequest2 request2, TRequest3 request3,
        TRequest4 request4);
    }

    public abstract class WithActionResult<TResponse> : BaseEndpoint
    {
      public abstract ActionResult<TResponse> Handle(HttpContext context, TRequest request, TRequest2 request2,
        TRequest3 request3, TRequest4 request4);
    }

    public abstract class WithActionResult : BaseEndpoint
    {
      public abstract ActionResult Handle(HttpContext context, TRequest request, TRequest2 request2, TRequest3 request3,
        TRequest4 request4);
    }
  }

  public static class WithManyRequest<TRequest, TRequest2, TRequest3>
  {
    public abstract class WithResult<TResponse> : BaseEndpoint
    {
      public abstract TResponse Handle(HttpContext context, TRequest request, TRequest2 request2, TRequest3 request3);
    }

    public abstract class WithoutResult : BaseEndpoint
    {
      public abstract void Handle(HttpContext context, TRequest request, TRequest2 request2, TRequest3 request3);
    }

    public abstract class WithActionResult<TResponse> : BaseEndpoint
    {
      public abstract ActionResult<TResponse> Handle(HttpContext context, TRequest request, TRequest2 request2,
        TRequest3 request3);
    }

    public abstract class WithActionResult : BaseEndpoint
    {
      public abstract ActionResult Handle(HttpContext context, TRequest request, TRequest2 request2,
        TRequest3 request3);
    }
  }

  public static class WithManyRequest<TRequest, TRequest2>
  {
    public abstract class WithResult<TResponse> : BaseEndpoint
    {
      public abstract TResponse Handle(HttpContext context, TRequest request, TRequest2 request2);
    }

    public abstract class WithoutResult : BaseEndpoint
    {
      public abstract void Handle(HttpContext context, TRequest request, TRequest2 request2);
    }

    public abstract class WithActionResult<TResponse> : BaseEndpoint
    {
      public abstract ActionResult<TResponse> Handle(HttpContext context, TRequest request, TRequest2 request2);
    }

    public abstract class WithActionResult : BaseEndpoint
    {
      public abstract ActionResult Handle(HttpContext context, TRequest request, TRequest2 request2);
    }
  }
}
