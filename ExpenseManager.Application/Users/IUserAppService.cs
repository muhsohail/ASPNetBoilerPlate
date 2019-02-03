using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ExpenseManager.Roles.Dto;
using ExpenseManager.Users.Dto;

namespace ExpenseManager.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedResultRequestDto, CreateUserDto, UpdateUserDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();
    }
}