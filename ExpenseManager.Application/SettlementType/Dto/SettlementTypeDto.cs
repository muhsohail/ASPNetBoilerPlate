using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ExpenseManager.Model;

namespace ExpenseManager.SettlementType.Dto
{
    [AutoMapFrom(typeof(SettlementCategory))]
    public class SettlementTypeDto : EntityDto
    {
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
    }
}
