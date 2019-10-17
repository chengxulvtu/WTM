using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using WalkingTec.Mvvm.Core;

namespace WalkingTec.Mvvm.Mvc
{
    public abstract class BaseTenantApiController : BaseApiController
    {
        private readonly TenantContext _tenantContext;

        public BaseTenantApiController()
        {
            _tenantContext = HttpContext.RequestServices.GetService<TenantContext>();
        }


        public override LoginUserInfo LoginUserInfo
        {
            get
            {
                var loginUserInfo = base.LoginUserInfo;
                if (loginUserInfo.TenantId != _tenantContext.Tenant?.Id)
                {
                    loginUserInfo = null;
                }
                return loginUserInfo;
            }
            set
            {
                if (_tenantContext.Tenant == null) throw new NullReferenceException(nameof(_tenantContext.Tenant));
                value.TenantId = _tenantContext.Tenant.Id;
                base.LoginUserInfo = value;
            }
        }

    }
}
