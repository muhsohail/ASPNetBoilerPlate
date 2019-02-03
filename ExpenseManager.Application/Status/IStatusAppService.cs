using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ExpenseManager.Status.Dto;

namespace ExpenseManager.Status
{
    public interface IStatusAppService : IAsyncCrudAppService<StatusDto, int, PagedResultRequestDto, CreateStatusDto, UpdateStatusDto>
    {
        string GetOnusStatusName(int OnusStatusId);
    }
}
