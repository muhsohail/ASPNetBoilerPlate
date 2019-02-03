using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ExpenseManager.SettlementType.Dto;

namespace ExpenseManager.SettlementType
{
    public interface ISettlementTypeAppService : IAsyncCrudAppService<SettlementTypeDto, int, PagedResultRequestDto, CreateSettlementTypeDto, UpdateSettlementTypeDto>
    {
        string GetCategoryName(int CategoryTypeId);
    }
}
