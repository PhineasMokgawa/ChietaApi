using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using CHIETAMIS.Lookups.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CHIETAMIS.Lookups
{
    public class SqmrAppIndicatorsAppService: CHIETAMISAppServiceBase
    {
        private readonly IRepository<SqmrAppIndicators> _sqmrRepository;
        public SqmrAppIndicatorsAppService(IRepository<SqmrAppIndicators> sqmrRepository)
        {
            _sqmrRepository = sqmrRepository;
        }

        public async Task<PagedResultDto<SqmrAppIndicatorsDtoForView>> SqmrAppIndicators()
        {
            var tasks = _sqmrRepository.GetAll();

            var query = (from o in tasks
                         select new SqmrAppIndicatorsDtoForView()
                         {
                             SqmrAppIndicators = new SqmrAppIndicatorsDto()
                             {
                                 Indicator = o.Indicator,
                                 Id = o.Id
                             }
                         });

            var ind = await query.ToListAsync();
            var totalCount = await query.CountAsync();


            return new PagedResultDto<SqmrAppIndicatorsDtoForView>(totalCount, ind);
        }
    }
}
