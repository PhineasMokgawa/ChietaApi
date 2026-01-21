using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CHIETAMIS.Dto;
using CHIETAMIS.MandatoryGrantPayments.Dtos;

namespace CHIETAMIS.MandatoryGrantPayments
{
    public interface IMandatoryGrantPaymentsAppService : IApplicationService
    {
        Task<PagedResultDto<GetMandatoryGrantPaymentsForViewDto>> GetMandatoryGrantPayments(string sdl);
    }
}
