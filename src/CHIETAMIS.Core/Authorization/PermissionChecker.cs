using Abp.Authorization;
using CHIETAMIS.Authorization.Roles;
using CHIETAMIS.Authorization.Users;

namespace CHIETAMIS.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
