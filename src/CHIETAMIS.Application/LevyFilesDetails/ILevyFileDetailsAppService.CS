using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CHIETAMIS.Dto;
using CHIETAMIS.LevyFilesDetails.Dtos;

namespace MIS.LevyFilesDetails
{
    public interface ILevyFileDetailsAppService : IApplicationService
    {
        Task<PagedResultDto<GetLevyFileDetailsForViewDto>> GetAll(GetAllLevyFileDetailsInput input);

        Task<GetLevyFileDetailsForViewDto> GetLevyFileDetailsForView(int id);

        Task<GetLevyFileDetailsForEditOutput> GetLevyFileDetailsForEdit(EntityDto<int> input);

        Task CreateOrEdit(CreateOrEditLevyFileDetailsDto input);

        Task Delete(EntityDto<int> input);
    }
}
