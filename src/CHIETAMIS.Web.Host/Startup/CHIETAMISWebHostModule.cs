using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using CHIETAMIS.Configuration;

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
    }
}
