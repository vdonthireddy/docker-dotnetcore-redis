using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;

namespace docker_dotnetcore_redis.Controllers
{
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IDistributedCache cache;
        private readonly string cacheKey = "vjName";

        public EmployeeController(IConfiguration config, IDistributedCache distributedCache)
        {
            _config = config;
            cache = distributedCache;
        }

        [HttpGet]
        public string Get()
        {
            var data = string.Empty;
            if ((data = cache.GetString(cacheKey)) == null)
            {
                data = DateTime.Now.ToString("HH:mm:sss");
                cache.SetString(cacheKey, data, new DistributedCacheEntryOptions {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(1)
                });
                return "Not Cached: " + data;
            } else
            {
                data = cache.GetString(cacheKey);
                return "Cached: " + data;
            }
        }
    }
}
