using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CHIETAMIS.Dto;
using CHIETAMIS.LevyFileLists.Dtos;

namespace CHIETAMIS.LevyFileLists
{
    public interface ILevyFileListAppService : IApplicationService
    {
        Task<PagedResultDto<GetLevyFileListForViewDto>> GetAll(GetAllLevyFileListInput input);

        Task<GetLevyFileListForViewDto> GetLevyFileListForView(long id);

        Task<GetLevyFileListForEditOutput> GetLevyFileListForEdit(EntityDto<long> input);

        Task CreateOrEdit(CreateOrEditLevyFileListDto input);

        Task Delete(EntityDto<long> input);
    }
}
