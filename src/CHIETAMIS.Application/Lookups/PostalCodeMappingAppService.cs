using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
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
    public class PostalCodeMappingAppService : CHIETAMISAppServiceBase
    {
        private readonly IRepository<PostalCodeMapping> _postalCodeMappingRepository;
        public PostalCodeMappingAppService(IRepository<PostalCodeMapping> postalCodeMappingRepository)
        {
            _postalCodeMappingRepository = postalCodeMappingRepository;
        }

        public async Task<PagedResultDto<PostalCodeMappingDtoForView>> PostalCodeMappings(string postcode)
        {
            var tasks = _postalCodeMappingRepository.GetAll()
                .Where(e=>e.PostalCode == postcode);

            var query = (from o in tasks
                         select new PostalCodeMappingDtoForView()
                         {
                             PostalCodeMapping = new PostalCodeMappingDto
                             {
                                 PostalCode = o.PostalCode,
                                 Suburb = o.Suburb,
                                 Area = o.Area,
                                 District = o.District,
                                 Municipality = o.Municipality,
                                 Province = o.Province,
                                 RuralUrban = o.RuralUrban
                             }
                         });

            try
            {
                var postalCodeMappingsDtos = await query.ToListAsync();
                var totalCount = await query.CountAsync();

                return new PagedResultDto<PostalCodeMappingDtoForView>(totalCount, postalCodeMappingsDtos);
            } 
            catch (Exception ex)
            {
                throw ex;
            };  
        }
    }
}
