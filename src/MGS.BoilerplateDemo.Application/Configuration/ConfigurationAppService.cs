using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using MGS.BoilerplateDemo.Configuration.Dto;

namespace MGS.BoilerplateDemo.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : BoilerplateDemoAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
