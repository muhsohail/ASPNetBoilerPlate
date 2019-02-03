using Abp.AutoMapper;
using ExpenseManager.Model;

namespace ExpenseManager.Stakeholder.Dto
{
    [AutoMapFrom(typeof(StakeholderDetail))]
    public class CreateStakeholderDto
    {
        public string Name { get; set; }
        public string Relationship { get; set; }
    }
}
