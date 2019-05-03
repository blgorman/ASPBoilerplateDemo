using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using MGS.BoilerplateDemo.Configuration;

namespace MGS.BoilerplateDemo.Web.Host.Startup
{
    [DependsOn(
       typeof(BoilerplateDemoWebCoreModule))]
    public class BoilerplateDemoWebHostModule: AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public BoilerplateDemoWebHostModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(BoilerplateDemoWebHostModule).GetAssembly());
        }
    }
}
