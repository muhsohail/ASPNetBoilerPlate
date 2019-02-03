using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ExpenseManager.Model;

namespace ExpenseManager.Stakeholder.Dto
{
    [AutoMapFrom(typeof(StakeholderDetail))]
    public class StakeholderDto : EntityDto
    {
        public string Name { get; set; }
        public string Relationship { get; set; }
    }
}
