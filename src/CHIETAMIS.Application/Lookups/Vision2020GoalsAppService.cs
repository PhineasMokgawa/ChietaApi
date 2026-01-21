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
    public class Vision2020GoalsAppService: CHIETAMISAppServiceBase
    {
        private readonly IRepository<Vision2025Goals> _visRepository;
        public Vision2020GoalsAppService(IRepository<Vision2025Goals> visRepository)
        {
            _visRepository = visRepository;
        }

        public async Task<PagedResultDto<Vision2025GoalsDtoForView>> Vision2025Goals()
        {
            var tasks = _visRepository.GetAll();

            var query = (from o in tasks
                         select new Vision2025GoalsDtoForView()
                         {
                             Vision2025Goals = new Vision2025GoalsDto()
                             {
                                 Goal = o.Goal,
                                 Id = o.Id
                             }
                         });

            var vis = await query.ToListAsync();
            var totalCount = await query.CountAsync();


            return new PagedResultDto<Vision2025GoalsDtoForView>(totalCount, vis);
        }
    }
}
