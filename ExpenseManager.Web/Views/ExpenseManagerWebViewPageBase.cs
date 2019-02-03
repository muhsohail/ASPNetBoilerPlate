using Abp.Web.Mvc.Views;

namespace ExpenseManager.Web.Views
{
    public abstract class ExpenseManagerWebViewPageBase : ExpenseManagerWebViewPageBase<dynamic>
    {

    }

    public abstract class ExpenseManagerWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected ExpenseManagerWebViewPageBase()
        {
            LocalizationSourceName = ExpenseManagerConsts.LocalizationSourceName;
        }
    }
}