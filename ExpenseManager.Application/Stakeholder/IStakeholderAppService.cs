using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ExpenseManager.Stakeholder.Dto;

namespace ExpenseManager.Stakeholder
{
    public interface IStakeholderAppService : IAsyncCrudAppService<StakeholderDto, int, PagedResultRequestDto, CreateStakeholderDto, UpdateStakeholderDto>
    {
        string GetStakeholderName(int CategoryTypeId);
    }
}
