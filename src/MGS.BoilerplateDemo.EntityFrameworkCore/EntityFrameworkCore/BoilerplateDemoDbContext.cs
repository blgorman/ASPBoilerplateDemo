using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using MGS.BoilerplateDemo.Authorization.Roles;
using MGS.BoilerplateDemo.Authorization.Users;
using MGS.BoilerplateDemo.MultiTenancy;

namespace MGS.BoilerplateDemo.EntityFrameworkCore
{
    public class BoilerplateDemoDbContext : AbpZeroDbContext<Tenant, Role, User, BoilerplateDemoDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public BoilerplateDemoDbContext(DbContextOptions<BoilerplateDemoDbContext> options)
            : base(options)
        {
        }
    }
}
