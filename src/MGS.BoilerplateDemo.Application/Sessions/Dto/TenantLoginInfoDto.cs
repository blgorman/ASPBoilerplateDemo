using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using MGS.BoilerplateDemo.MultiTenancy;

namespace MGS.BoilerplateDemo.Sessions.Dto
{
    [AutoMapFrom(typeof(Tenant))]
    public class TenantLoginInfoDto : EntityDto
    {
        public string TenancyName { get; set; }

        public string Name { get; set; }
    }
}
