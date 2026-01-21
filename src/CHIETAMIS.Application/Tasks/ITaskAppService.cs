using Abp.Application.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CHIETAMIS.Tasks.Dto;
using Abp.Linq.Extensions;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Abp.Application.Services.Dto;

namespace CHIETAMIS.Tasks
{
    public interface ITaskAppService : IApplicationService
    {
        Task<ListResultDto<TaskListDto>> GetAll(GetAllTasksInput input);
    }
}
