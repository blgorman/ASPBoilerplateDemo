using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace MGS.BoilerplateDemo.EntityFrameworkCore
{
    public static class BoilerplateDemoDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<BoilerplateDemoDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<BoilerplateDemoDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
