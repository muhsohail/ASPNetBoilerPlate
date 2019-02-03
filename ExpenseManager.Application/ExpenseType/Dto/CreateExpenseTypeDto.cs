using Abp.AutoMapper;
using ExpenseManager.Model;

namespace ExpenseManager.ExpenseType.Dto
{
    [AutoMapFrom(typeof(ExpenseCategory))]
    public class CreateExpenseTypeDto
    {
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
    }
}
