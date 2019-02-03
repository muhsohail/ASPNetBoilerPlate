using Abp.AutoMapper;
using ExpenseManager.Model;

namespace ExpenseManager.Status.Dto
{
    [AutoMapFrom(typeof(OnusStatus))]
    public class CreateStatusDto
    {
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
    }
}
