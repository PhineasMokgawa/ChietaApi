using System.Threading.Tasks;
using Abp.Application.Services;
using CHIETAMIS.Sessions.Dto;

namespace CHIETAMIS.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
