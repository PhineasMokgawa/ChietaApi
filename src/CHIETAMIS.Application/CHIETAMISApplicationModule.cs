using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using CHIETAMIS.Authorization;

namespace CHIETAMIS
{
    [DependsOn(
        typeof(CHIETAMISCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class CHIETAMISApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<CHIETAMISAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(CHIETAMISApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
