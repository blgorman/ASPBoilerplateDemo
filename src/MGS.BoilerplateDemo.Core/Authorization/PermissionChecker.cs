using Abp.Authorization;
using MGS.BoilerplateDemo.Authorization.Roles;
using MGS.BoilerplateDemo.Authorization.Users;

namespace MGS.BoilerplateDemo.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
