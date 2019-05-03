using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace MGS.BoilerplateDemo.Controllers
{
    public abstract class BoilerplateDemoControllerBase: AbpController
    {
        protected BoilerplateDemoControllerBase()
        {
            LocalizationSourceName = BoilerplateDemoConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
