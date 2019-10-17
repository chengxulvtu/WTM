using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalkingTec.Mvvm.Mvc
{
    public class HttpTenantResolveMiddleware
    {
        private readonly RequestDelegate _next;

        public HttpTenantResolveMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, TenantResolver tenantResolver, TenantContext tenantContext)
        {
            var tenant = tenantResolver.ResolveByHost(context.Request.Host.ToString());
            tenantContext.Tenant = tenant;
            await _next.Invoke(context);
        }
    }
}
