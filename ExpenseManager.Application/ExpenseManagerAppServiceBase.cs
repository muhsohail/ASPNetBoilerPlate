using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.IdentityFramework;
using Abp.Runtime.Session;
using ExpenseManager.Authorization.Users;
using ExpenseManager.MultiTenancy;
using ExpenseManager.Users;
using Microsoft.AspNet.Identity;

namespace ExpenseManager
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class ExpenseManagerAppServiceBase : ApplicationService
    {
        public TenantManager TenantManager { get; set; }

        public UserManager UserManager { get; set; }

        protected ExpenseManagerAppServiceBase()
        {
            LocalizationSourceName = ExpenseManagerConsts.LocalizationSourceName;
        }

        protected virtual Task<User> GetCurrentUserAsync()
        {
            var user = UserManager.FindByIdAsync(AbpSession.GetUserId());
            if (user == null)
            {
                throw new ApplicationException("There is no current user!");
            }

            return user;
        }

        protected virtual Task<Tenant> GetCurrentTenantAsync()
        {
            return TenantManager.GetByIdAsync(AbpSession.GetTenantId());
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}