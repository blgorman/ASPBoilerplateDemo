using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using MGS.BoilerplateDemo.Authorization.Roles;
using MGS.BoilerplateDemo.Authorization.Users;
using MGS.BoilerplateDemo.MultiTenancy;
using MGS.BoilerplateDemo.BoilerplateDemo.OptionListsAndListItems;

namespace MGS.BoilerplateDemo.EntityFrameworkCore
{
    public class BoilerplateDemoDbContext : AbpZeroDbContext<Tenant, Role, User, BoilerplateDemoDbContext>
    {
        /* Define a DbSet for each entity of the application */
        public virtual DbSet<OptionList> OptionLists { get; set; }
        public virtual DbSet<OptionListItem> OptionListItems { get; set; }

        public BoilerplateDemoDbContext(DbContextOptions<BoilerplateDemoDbContext> options)
            : base(options)
        {
        }
    }
}
