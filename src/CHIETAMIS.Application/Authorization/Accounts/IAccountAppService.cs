using System.Threading.Tasks;
using Abp.Application.Services;
using CHIETAMIS.Authorization.Accounts.Dto;

namespace CHIETAMIS.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
