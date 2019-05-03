using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MGS.BoilerplateDemo.MultiTenancy.Dto;

namespace MGS.BoilerplateDemo.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

