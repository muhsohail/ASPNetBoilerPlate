using Abp.Application.Services.Dto;

namespace ExpenseManager.Onus.Dto
{
    public class UpdateOnusDto : OnusDto, IEntityDto
    {
        public long? UserId { get; set; }
    }
}
