using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using CHIETAMIS.Configuration;
using Microsoft.EntityFrameworkCore;
using CHIETAMIS.EntityFrameworkCore;
using System.Linq;

namespace CHIETAMIS.Web.Host.Startup
{
    [DependsOn(
       typeof(CHIETAMISWebCoreModule))]
    public class CHIETAMISWebHostModule: AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public CHIETAMISWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(CHIETAMISWebHostModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            // Automatically apply pending migrations on application startup
            MigrateDatabase();
        }

        /// <summary>
        /// Automatically apply pending migrations to the database on application startup.
        /// This ensures new tables (like tbl_Notifications) are created without manual intervention.
        /// Uses robust error handling to prevent application crash on migration failures.
        /// </summary>
        private void MigrateDatabase()
        {
            try
            {
                using (var uow = IocManager.Resolve<Abp.Domain.Uow.IUnitOfWorkManager>().Begin())
                {
                    var dbContext = IocManager.Resolve<CHIETAMISDbContext>();

                    try
                    {
                        // Check database connection first
                        if (!dbContext.Database.CanConnect())
                        {
                            Logger.Warn("Cannot connect to database. Skipping migration check.");
                            return;
                        }

                        var pendingMigrations = dbContext.Database.GetPendingMigrations().ToList();

                        if (pendingMigrations.Any())
                        {
                            Logger.Info($"Found {pendingMigrations.Count} pending migrations. Starting automatic database migration...");

                            foreach (var migration in pendingMigrations)
                            {
                                Logger.Info($"  - Pending migration: {migration}");
                            }

                            dbContext.Database.Migrate();
                            Logger.Info("✓ Database migration completed successfully. All tables are now up to date.");
                        }
                        else
                        {
                            Logger.Info("✓ Database is up to date. No pending migrations detected.");
                        }

                        uow.Complete();
                    }
                    catch (Microsoft.Data.SqlClient.SqlException sqlEx)
                    {
                        // Handle SQL-specific errors gracefully
                        Logger.Error($"SQL error during database migration: {sqlEx.Message}", sqlEx);
                        Logger.Warn("Application will continue to run, but some features may not work correctly if tables are missing.");
                        // Don't throw - let the application start even if migration fails
                    }
                    catch (System.Exception ex)
                    {
                        Logger.Error("An unexpected error occurred during automatic database migration.", ex);
                        Logger.Warn("Application will continue to run, but some features may not work correctly if tables are missing.");
                        // Don't throw - let the application start even if migration fails
                    }
                    finally
                    {
                        IocManager.Release(dbContext);
                    }
                }
            }
            catch (System.Exception outerEx)
            {
                Logger.Error("Critical error in migration process.", outerEx);
                // Don't throw - let the application start
            }
        }
    }
}
