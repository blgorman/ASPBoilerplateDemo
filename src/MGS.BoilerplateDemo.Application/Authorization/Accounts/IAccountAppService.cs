using System.Threading.Tasks;
using Abp.Application.Services;
using MGS.BoilerplateDemo.Authorization.Accounts.Dto;

namespace MGS.BoilerplateDemo.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
