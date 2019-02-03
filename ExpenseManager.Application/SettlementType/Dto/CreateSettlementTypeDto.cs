using Abp.AutoMapper;
using ExpenseManager.Model;

namespace ExpenseManager.SettlementType.Dto
{
    [AutoMapFrom(typeof(SettlementCategory))]
    public class CreateSettlementTypeDto
    {
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
    }
}
