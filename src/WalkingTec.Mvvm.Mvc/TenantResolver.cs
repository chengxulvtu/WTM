using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Mvc.Model;

namespace WalkingTec.Mvvm.Mvc
{
    public class TenantResolver
    {
        private readonly IServiceProvider _serviceProvider;

        public TenantResolver(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        private IEnumerable<Tenant> _tenants;

        public IEnumerable<Tenant> Tenants
        {
            // TODO 加上缓存过期，考虑并发(但并不影响结果)
            get
            {
                if (_tenants == null)
                {
                    using (var serviceScope = _serviceProvider.CreateScope())
                    {
                        var dbContext = serviceScope.ServiceProvider.GetService<TenantDbContext>();
                        var tenants = dbContext.Tenants.ToList();
                        _tenants = tenants.Select(t => new Tenant
                        {
                            Id = t.Id,
                            Name = t.Name,
                            HostName = t.HostName,
                            ConnectionString = t.ConnectionString
                        });
                    }
                  
                }
                return _tenants;
            }
        }


        public Tenant ResolveByHost(string host)
        {
            return Tenants.FirstOrDefault(t => t.HostName == host);
        }


    }
}
