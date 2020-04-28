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

        public EmployeeController(IConfiguration config, IDistributedCache distributedCache)
        {
            _config = config;
            cache = distributedCache;
        }

        [HttpGet]
        public string Get(string name = null)
        {
            if (string.IsNullOrEmpty(name))
                name = "Reddy 123 " + DateTime.Now.ToString("HH:mm:sss");
            cache.SetString("Name", name);
            var data = cache.GetString("Name");
            return data;
        }

    }
}
