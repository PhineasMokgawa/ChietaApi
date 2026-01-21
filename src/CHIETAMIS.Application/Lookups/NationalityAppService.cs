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
    public class NationalityAppService : CHIETAMISAppServiceBase
    {
        private readonly IRepository<Nationality> _NationalityRepository;
        public NationalityAppService(IRepository<Nationality> NationalityRepository)
        {
            _NationalityRepository = NationalityRepository;
        }

        public async Task<PagedResultDto<NationalityDtoForView>> Nationalitys()
        {
            var tasks = _NationalityRepository.GetAll();

            var query = (from o in tasks
                         select new NationalityDtoForView()
                         {
                             Nationality = new NationalityDto
                             {
                                 Nationality_Name = o.Description,
                                 Nationality_Code = o.Nationality_Code
                             }
                         });

            var NationalitysDtos = await query.ToListAsync();
            var totalCount = await query.CountAsync();


            return new PagedResultDto<NationalityDtoForView>(totalCount, NationalitysDtos);
        }
    }
}
