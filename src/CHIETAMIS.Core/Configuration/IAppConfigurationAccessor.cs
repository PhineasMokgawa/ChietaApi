using Microsoft.Extensions.Configuration;

namespace CHIETAMIS.Configuration
{
    public interface IAppConfigurationAccessor
    {
        IConfigurationRoot Configuration { get; }
    }
}
