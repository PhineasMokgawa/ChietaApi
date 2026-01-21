using Abp.Application.Services;
using CHIETAMIS.MultiTenancy.Dto;

namespace CHIETAMIS.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

