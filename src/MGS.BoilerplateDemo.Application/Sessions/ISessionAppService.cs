using System.Threading.Tasks;
using Abp.Application.Services;
using MGS.BoilerplateDemo.Sessions.Dto;

namespace MGS.BoilerplateDemo.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
