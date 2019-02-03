using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ExpenseManager.Model;

namespace ExpenseManager.Status.Dto
{
    [AutoMapFrom(typeof(OnusStatus))]
    public class StatusDto : EntityDto
    {
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
    }
}
