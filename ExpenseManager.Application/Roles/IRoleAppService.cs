using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ExpenseManager.Roles.Dto;

namespace ExpenseManager.Roles
{
    public interface IRoleAppService : IAsyncCrudAppService<RoleDto, int, PagedResultRequestDto, CreateRoleDto, RoleDto>
    {
        Task<ListResultDto<PermissionDto>> GetAllPermissions();
    }
}
