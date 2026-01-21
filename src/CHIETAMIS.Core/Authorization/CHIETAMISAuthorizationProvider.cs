using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace CHIETAMIS.Authorization
{
    public class CHIETAMISAuthorizationProvider : AuthorizationProvider
    {

        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            context.CreatePermission(PermissionNames.Pages_Users, L("Users"));
            context.CreatePermission(PermissionNames.Pages_Users_Activation, L("UsersActivation"));
            context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
            context.CreatePermission(PermissionNames.Pages_DgApproval, L("DgApproval"));
            context.CreatePermission(PermissionNames.Pages_MgApproval, L("MgApproval"));
            context.CreatePermission(PermissionNames.Pages_DgManager, L("DgManager"));
            context.CreatePermission(PermissionNames.Pages_GEC_Committee, L("GECCommittee"));
            context.CreatePermission(PermissionNames.Pages_GAC_Committee, L("GACCommittee"));
            context.CreatePermission(PermissionNames.Pages_GC_Committee, L("GCCommittee"));
            context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, CHIETAMISConsts.LocalizationSourceName);
        }
    }
}
