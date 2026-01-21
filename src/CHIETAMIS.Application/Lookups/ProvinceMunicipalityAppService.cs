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
    public class ProvinceMunicipalityAppService : CHIETAMISAppServiceBase
    {
        private readonly IRepository<PostalCodeMapping> _postcodeRepository;

        public ProvinceMunicipalityAppService(IRepository<PostalCodeMapping> postcodeRepository)
        {
            _postcodeRepository = postcodeRepository;
        }

        public async Task<PagedResultDto<ProvinceMunicipalitiesDtoForView>> ProvinceMunicipalities(string district)
        {
            var tasks = _postcodeRepository.GetAll().Where(a => a.District == district);

            var query = (from o in tasks
                         select new ProvinceMunicipalitiesDtoForView()
                         {
                             Municipalities = new ProvinceMunicipalitiesDto
                             {
                                 Municipality = o.Municipality
                             }
                         }).Distinct();

            var MunicipalitiesDtos = await query.ToListAsync();
            var totalCount = await query.CountAsync();


            return new PagedResultDto<ProvinceMunicipalitiesDtoForView>(totalCount, MunicipalitiesDtos);
        }
    }
}
