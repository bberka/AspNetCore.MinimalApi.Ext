﻿using Microsoft.Extensions.Caching.Memory;

namespace AspNetCore.MinimalApi.Ext.Sample.Endpoints.Product;
[Endpoint]

public class GetAll 
{

  public List<Classes.Product> Handle(HttpContext context) {
    var _memoryCache = context.RequestServices.GetRequiredService<MemoryCache>();
    var products = _memoryCache.Get<List<Classes.Product>>("Products");
    return products ?? new List<Classes.Product>();
  }
}