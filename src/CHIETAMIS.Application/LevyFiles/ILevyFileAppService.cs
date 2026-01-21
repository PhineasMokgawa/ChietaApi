using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CHIETAMIS.LevyFiles;
using CHIETAMIS.LevyFiles.Dtos;

namespace CHIETAMIS.LevyFiles
{
    public interface ILevyFileAppService : IApplicationService
    {
        Task<PagedResultDto<GetLevyFileForViewDto>> GetAll(GetAllLevyFileInput input);

        Task<GetLevyFileForViewDto> GetLevyFileForView(long id);

        Task<GetLevyFileForEditOutput> GetLevyFileForEdit(EntityDto<long> input);

        Task CreateOrEdit(CreateOrEditLevyFileDto input);

        Task Delete(EntityDto<long> input);
    }
}
