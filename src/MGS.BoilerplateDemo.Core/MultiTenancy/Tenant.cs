using Abp.MultiTenancy;
using MGS.BoilerplateDemo.Authorization.Users;

namespace MGS.BoilerplateDemo.MultiTenancy
{
    public class Tenant : AbpTenant<User>
    {
        public Tenant()
        {            
        }

        public Tenant(string tenancyName, string name)
            : base(tenancyName, name)
        {
        }
    }
}
