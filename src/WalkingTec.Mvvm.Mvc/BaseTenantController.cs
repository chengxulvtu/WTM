using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace WalkingTec.Mvvm.Mvc
{
    public abstract class BaseTenantController : BaseController
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
                if (loginUserInfo != null && loginUserInfo.TenantId != TenantContext?.Tenant?.Id)
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

        protected override T ReadFromCache<T>(string key, Func<T> setFunc, int? timeout = null)
        {
            var tenantKey = $"{TenantContext.Tenant.Id}_{key}";
            return base.ReadFromCache(tenantKey, setFunc, timeout);
        }

    }
}
