using System.Threading.Tasks;
using Abp.Application.Services;
using ExpenseManager.Sessions.Dto;

namespace ExpenseManager.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
