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
    public class EquityAppService : CHIETAMISAppServiceBase
    {
        private readonly IRepository<Equity> _EquityRepository;
        public EquityAppService(IRepository<Equity> EquityRepository)
        {
            _EquityRepository = EquityRepository;
        }

        public async Task<PagedResultDto<EquityDtoForView>> Equitys()
        {
            var tasks = _EquityRepository.GetAll();

            var query = (from o in tasks
                         select new EquityDtoForView()
                         {
                             Equity = new EquityDto
                             {
                                 Equity_Name = o.Description,
                                 Equity_Code = o.Equity_Code
                             }
                         });

            var EquitysDtos = await query.ToListAsync();
            var totalCount = await query.CountAsync();


            return new PagedResultDto<EquityDtoForView>(totalCount, EquitysDtos);
        }
    }
}
