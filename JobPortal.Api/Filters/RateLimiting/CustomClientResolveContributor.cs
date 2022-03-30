using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobPortal.Api.Filters.RateLimiting
{
    public class CustomClientResolveContributor : IClientResolveContributor
    {
        public Task<string> ResolveClientAsync(HttpContext httpContext)
        {
            var clientHeaderValue = string.Empty;
            if(httpContext.Request.Headers.TryGetValue("First-Client-Header", out var values))
            {
                clientHeaderValue = values.First();
            }

            return Task.FromResult(clientHeaderValue);
        }
    }
}
