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
    public class ProvinceDistrictAppService : CHIETAMISAppServiceBase
    {
        private readonly IRepository<PostalCodeMapping> _postcodeRepository;

        public ProvinceDistrictAppService(IRepository<PostalCodeMapping> postcodeRepository)
        {
            _postcodeRepository = postcodeRepository;
        }

        public async Task<PagedResultDto<ProvinceDistrictForViewDto>> ProvinceDistricts(string province)
        {
            var tasks = _postcodeRepository.GetAll().Where(a => a.Province == province);

            var query = (from o in tasks
                         select new ProvinceDistrictForViewDto()
                         {
                             District = new ProvinceDistrictsDto
                             {
                                 District = o.District
                             }
                         }).Distinct();

            var dist = await query.ToListAsync();
            var totalCount = await query.CountAsync();


            return new PagedResultDto<ProvinceDistrictForViewDto>(totalCount, dist);
        }

    }
}
