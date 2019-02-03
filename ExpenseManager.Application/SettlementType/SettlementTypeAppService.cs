using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using ExpenseManager.SettlementType.Dto;
using ExpenseManager.Model;

namespace ExpenseManager.SettlementType
{
    public class SettlementTypeAppService : AsyncCrudAppService<SettlementCategory, SettlementTypeDto, int, PagedResultRequestDto, CreateSettlementTypeDto, UpdateSettlementTypeDto>, ISettlementTypeAppService
    {
        private IObjectMapper _objectMapper;
        public SettlementTypeAppService(IObjectMapper objectMapper, IRepository<SettlementCategory, int> repository) : base(repository)
        {
            _objectMapper = objectMapper;

        }

        public string GetCategoryName(int CategoryTypeId)
        {
            return _objectMapper.Map<string>(Repository.Get(CategoryTypeId).Name);
        }
    }
}
