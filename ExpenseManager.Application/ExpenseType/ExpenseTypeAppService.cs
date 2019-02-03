using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using ExpenseManager.ExpenseType.Dto;
using ExpenseManager.Model;

namespace ExpenseManager.ExpenseType
{
    public class ExpenseTypeAppService : AsyncCrudAppService<ExpenseCategory, ExpenseTypeDto, int, PagedResultRequestDto, CreateExpenseTypeDto, UpdateExpenseTypeDto>, IExpenseTypeAppService
    {
        private IObjectMapper _objectMapper;
        public ExpenseTypeAppService(IObjectMapper objectMapper, IRepository<ExpenseCategory, int> repository) : base(repository)
        {
            _objectMapper = objectMapper;

        }

        public string GetCategoryName(int CategoryTypeId)
        {
            return _objectMapper.Map<string>(Repository.Get(CategoryTypeId).Name);
        }
    }
}
