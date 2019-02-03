using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using ExpenseManager.Stakeholder.Dto;
using ExpenseManager.Model;

namespace ExpenseManager.Stakeholder
{
    public class StakeholderAppService : AsyncCrudAppService<StakeholderDetail, StakeholderDto, int, PagedResultRequestDto, CreateStakeholderDto, UpdateStakeholderDto>, IStakeholderAppService
    {
        private IObjectMapper _objectMapper;
        public StakeholderAppService(IObjectMapper objectMapper, IRepository<StakeholderDetail, int> repository) : base(repository)
        {
            _objectMapper = objectMapper;

        }

        public string GetStakeholderName(int StakeholderId)
        {
            return _objectMapper.Map<string>(Repository.Get(StakeholderId).Relationship);
        }
    }
}
