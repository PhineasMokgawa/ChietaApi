using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using CHIETAMIS.EntityFrameworkCore;
using CHIETAMIS.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace CHIETAMIS.Web.Tests
{
    [DependsOn(
        typeof(CHIETAMISWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class CHIETAMISWebTestModule : AbpModule
    {
        public CHIETAMISWebTestModule(CHIETAMISEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(CHIETAMISWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(CHIETAMISWebMvcModule).Assembly);
        }
    }
}