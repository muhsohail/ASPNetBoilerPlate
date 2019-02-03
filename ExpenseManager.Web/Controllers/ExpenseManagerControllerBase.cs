using Abp.IdentityFramework;
using Abp.UI;
using Abp.Web.Mvc.Controllers;
using ExpenseManager.IO.Helper;
using Microsoft.AspNet.Identity;

namespace ExpenseManager.Web.Controllers
{
    /// <summary>
    /// Derive all Controllers from this class.
    /// </summary>
    public abstract class ExpenseManagerControllerBase : AbpController
    {
        public IHttpCallingAppService _httpCallingAppService;
        public ExpenseManagerControllerBase()
        {
            LocalizationSourceName = ExpenseManagerConsts.LocalizationSourceName;
        }
        public ExpenseManagerControllerBase(IHttpCallingAppService httpCallingAppService)
        {
            this._httpCallingAppService = httpCallingAppService;
        }

        protected virtual void CheckModelState()
        {
            if (!ModelState.IsValid)
            {
                throw new UserFriendlyException(L("FormIsNotValidMessage"));
            }
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}