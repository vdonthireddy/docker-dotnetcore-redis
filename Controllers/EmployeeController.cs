using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace docker_dotnetcore_redis.Controllers
{
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        private readonly IMemoryCache _memoryCache;
        public EmployeeController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            string cacheKey = "EmployeeCacheData";
            IEnumerable<string> data = null;

            if (!_memoryCache.TryGetValue(cacheKey, out string cachedData))
            {
                var cacheExpirationOptions = new MemoryCacheEntryOptions()
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(1),
                    Priority = CacheItemPriority.Normal
                };
                data = new string[] { "cache", "value1", "value2" };
                _memoryCache.Set(cacheKey, data, cacheExpirationOptions);
            } else
            {
                return _memoryCache.Get(cacheKey) as IEnumerable<string>;
            }
            return new string[] { "value1", "value2" };
        }

    }
}
