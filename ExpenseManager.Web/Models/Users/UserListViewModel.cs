using System.Collections.Generic;
using ExpenseManager.Roles.Dto;
using ExpenseManager.Users.Dto;

namespace ExpenseManager.Web.Models.Users
{
    public class UserListViewModel
    {
        public IReadOnlyList<UserDto> Users { get; set; }

        public IReadOnlyList<RoleDto> Roles { get; set; }
    }
}