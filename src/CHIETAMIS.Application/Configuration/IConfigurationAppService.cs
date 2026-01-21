using System.Threading.Tasks;
using CHIETAMIS.Configuration.Dto;

namespace CHIETAMIS.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
