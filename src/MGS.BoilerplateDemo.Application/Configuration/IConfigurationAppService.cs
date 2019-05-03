using System.Threading.Tasks;
using MGS.BoilerplateDemo.Configuration.Dto;

namespace MGS.BoilerplateDemo.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
