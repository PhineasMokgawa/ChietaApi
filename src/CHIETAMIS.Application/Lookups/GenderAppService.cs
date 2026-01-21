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
    public class GenderAppService : CHIETAMISAppServiceBase
    {
        private readonly IRepository<Gender> _GenderRepository;
        public GenderAppService(IRepository<Gender> GenderRepository)
        {
            _GenderRepository = GenderRepository;
        }

        public async Task<PagedResultDto<GenderDtoForView>> Genders()
        {
            var tasks = _GenderRepository.GetAll();

            var query = (from o in tasks
                         select new GenderDtoForView()
                         {
                             Gender = new GenderDto
                             {
                                 Gender_Name = o.Description,
                                 Gender_Code = o.Gender_Code
                             }
                         });

            var GendersDtos = await query.ToListAsync();
            var totalCount = await query.CountAsync();


            return new PagedResultDto<GenderDtoForView>(totalCount, GendersDtos);
        }
    }
}
