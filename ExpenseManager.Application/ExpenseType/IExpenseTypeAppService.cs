using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ExpenseManager.ExpenseType.Dto;

namespace ExpenseManager.ExpenseType
{
    public interface IExpenseTypeAppService : IAsyncCrudAppService<ExpenseTypeDto, int, PagedResultRequestDto, CreateExpenseTypeDto, UpdateExpenseTypeDto>
    {
        string GetCategoryName(int CategoryTypeId);
    }
}
