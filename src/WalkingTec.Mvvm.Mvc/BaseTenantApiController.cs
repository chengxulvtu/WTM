using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using WalkingTec.Mvvm.Core;

namespace WalkingTec.Mvvm.Mvc
{
    public abstract class BaseTenantApiController : BaseApiController
    {
        private TenantContext _tenantContext;

        public TenantContext TenantContext
        {
            get
            {
                if (_tenantContext == null)
                {
                    _tenantContext = HttpContext.RequestServices.GetService<TenantContext>();
                }
                return _tenantContext;
            }
        }

        public override LoginUserInfo LoginUserInfo
        {
            get
            {
                var loginUserInfo = base.LoginUserInfo;
                if (loginUserInfo?.TenantId != TenantContext.Tenant?.Id)
                {
                    loginUserInfo = null;
                }
                return loginUserInfo;
            }
            set
            {
                if (TenantContext.Tenant == null) throw new NullReferenceException(nameof(TenantContext.Tenant));
                value.TenantId = TenantContext.Tenant.Id;
                base.LoginUserInfo = value;
            }
        }

        public override IDataContext CreateDC(bool isLog = false)
        {
            return (IDataContext)GlobaInfo?.DataContextCI?.Invoke(new object[] { CurrentCS, CurrentDbType ?? ConfigInfo.DbType });
        }
    }
}
