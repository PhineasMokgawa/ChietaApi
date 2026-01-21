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
    public class CitizenshipAppService : CHIETAMISAppServiceBase
    {
        private readonly IRepository<Citizenship> _CitizenshipRepository;
        public CitizenshipAppService(IRepository<Citizenship> CitizenshipRepository)
        {
            _CitizenshipRepository = CitizenshipRepository;
        }

        public async Task<PagedResultDto<CitizenshipDtoForView>> Citizenships()
        {
            var tasks = _CitizenshipRepository.GetAll();

            var query = (from o in tasks
                         select new CitizenshipDtoForView()
                         {
                             Citizenship = new CitizenshipDto
                             {
                                 Citizenship_Name = o.Description,
                                 Citizenship_Code = o.Citizen_Resident_Status_Code
                             }
                         });

            var CitizenshipsDtos = await query.ToListAsync();
            var totalCount = await query.CountAsync();


            return new PagedResultDto<CitizenshipDtoForView>(totalCount, CitizenshipsDtos);
        }
    }
}
