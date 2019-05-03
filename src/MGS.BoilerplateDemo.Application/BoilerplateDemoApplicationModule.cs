using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using MGS.BoilerplateDemo.Authorization;

namespace MGS.BoilerplateDemo
{
    [DependsOn(
        typeof(BoilerplateDemoCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class BoilerplateDemoApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<BoilerplateDemoAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(BoilerplateDemoApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddProfiles(thisAssembly)
            );
        }
    }
}
