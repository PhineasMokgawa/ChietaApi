using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using CHIETAMIS.Lookups.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Lookups
{
    public class AreaAppService : CHIETAMISAppServiceBase
    {
        private readonly IRepository<Area> _AreaRepository;
        public AreaAppService(IRepository<Area> AreaRepository)
        {
            _AreaRepository = AreaRepository;
        }

        public async Task<PagedResultDto<AreaDtoForView>> Areas()
        {
            var tasks = _AreaRepository.GetAll();

            var query = (from o in tasks
                         select new AreaDtoForView()
                         {
                             Area = new AreaDto
                             {
                                 Area_Name = o.TownCity,
                                 Area_Code = o.CodeId
                             }
                         });

            var AreasDtos = await query.ToListAsync();
            var totalCount = await query.CountAsync();


            return new PagedResultDto<AreaDtoForView>(totalCount, AreasDtos);
        }


    }
}
