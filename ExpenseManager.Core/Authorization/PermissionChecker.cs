using Abp.Authorization;
using ExpenseManager.Authorization.Roles;
using ExpenseManager.Authorization.Users;

namespace ExpenseManager.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
