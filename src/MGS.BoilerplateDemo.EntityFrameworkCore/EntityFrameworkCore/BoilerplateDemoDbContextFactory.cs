using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using MGS.BoilerplateDemo.Configuration;
using MGS.BoilerplateDemo.Web;

namespace MGS.BoilerplateDemo.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class BoilerplateDemoDbContextFactory : IDesignTimeDbContextFactory<BoilerplateDemoDbContext>
    {
        public BoilerplateDemoDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<BoilerplateDemoDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            BoilerplateDemoDbContextConfigurer.Configure(builder, configuration.GetConnectionString(BoilerplateDemoConsts.ConnectionStringName));

            return new BoilerplateDemoDbContext(builder.Options);
        }
    }
}
