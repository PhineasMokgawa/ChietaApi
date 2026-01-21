using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace CHIETAMIS.EntityFrameworkCore
{
    public static class CHIETAMISDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<CHIETAMISDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<CHIETAMISDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
