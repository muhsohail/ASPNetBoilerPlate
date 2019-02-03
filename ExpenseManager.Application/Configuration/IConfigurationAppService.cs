using System.Threading.Tasks;
using Abp.Application.Services;
using ExpenseManager.Configuration.Dto;

namespace ExpenseManager.Configuration
{
    public interface IConfigurationAppService: IApplicationService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}