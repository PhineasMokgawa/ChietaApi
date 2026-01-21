using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace CHIETAMIS.Controllers
{
    public abstract class CHIETAMISControllerBase: AbpController
    {
        protected CHIETAMISControllerBase()
        {
            LocalizationSourceName = CHIETAMISConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
