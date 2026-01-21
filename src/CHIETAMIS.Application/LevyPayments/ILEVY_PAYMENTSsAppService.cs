using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CHIETAMIS.LEVYPAYMENTS.Dtos;
using CHIETAMIS.Dto;
using CHEITAMIS.Dto;
using CHIETAMIS.LEVYPAYMENTS.Dtos;


namespace CHIETAMIS.LEVYPAYMENTS
{
    public interface ILEVY_PAYMENTSsAppService : IApplicationService
    {
        Task<PagedResultDto<GetLEVY_PAYMENTSForViewDto>> GetAll(GetAllLEVY_PAYMENTSsInput input);

        Task<GetLEVY_PAYMENTSForViewDto> GetLEVY_PAYMENTSForView(int id);

        Task<GetLEVY_PAYMENTSForEditOutput> GetLEVY_PAYMENTSForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditLEVY_PAYMENTSDto input);

        Task Delete(EntityDto input);

        Task<FileDto> GetLEVY_PAYMENTSsToExcel(GetAllLEVY_PAYMENTSsForExcelInput input);


    }
}