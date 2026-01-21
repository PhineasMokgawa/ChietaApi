using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using CHIETAMIS.Organisations.Dtos;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Application.Services.Dto;
using CHIETAMIS.Sdfs;
using CHIETAMIS.DiscretionaryProjects;
using CHIETAMIS.Counters;
using CHIETAMIS.UnitStandards.Dtos;

namespace CHIETAMIS.UnitStandards
{
    public class UnitStandardAppService: CHIETAMISAppServiceBase
    {
        private readonly IRepository<UnitStandard> _usRepository;
        public UnitStandardAppService(IRepository<UnitStandard> usRepository)
        {
            _usRepository = usRepository;
        }

        public async Task<PagedResultDto<GetUnitStandardsForViewDto>> GetAll(USRequestDto input)
        {
            var uss = _usRepository.GetAll()
                .Where(x => x.UNIT_STANDARD_ID.ToString().StartsWith(input.Keyword))
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);

            if (uss.Count() == 0)
            {
                uss = _usRepository.GetAll()
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.UNIT_STD_TITLE.Contains(input.Keyword))
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);
            }

            var unitstandards = from o in uss
                             select new GetUnitStandardsForViewDto()
                             {
                                 UnitStandard = new UnitStandardDto
                                 {
                                     UNIT_STANDARD_ID = o.UNIT_STANDARD_ID,
                                     UNIT_STD_TITLE = o.UNIT_STD_TITLE,
                                     Amount2 = o.Amount2,
                                     Amount1 = o.Amount1,
                                     Id = o.Id
                                 }
                             };

            var totalCount = await uss.CountAsync();

            return new PagedResultDto<GetUnitStandardsForViewDto>(
                totalCount,
                await unitstandards.ToListAsync()
            );
        }
    }
}
