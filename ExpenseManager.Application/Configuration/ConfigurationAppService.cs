using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using ExpenseManager.Configuration.Dto;

namespace ExpenseManager.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : ExpenseManagerAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
