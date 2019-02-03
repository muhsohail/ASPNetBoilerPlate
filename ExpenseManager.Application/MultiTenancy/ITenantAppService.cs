using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ExpenseManager.MultiTenancy.Dto;

namespace ExpenseManager.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}
