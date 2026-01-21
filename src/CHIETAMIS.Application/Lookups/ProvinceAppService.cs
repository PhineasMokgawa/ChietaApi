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
    public class ProvinceAppService : CHIETAMISAppServiceBase
    {
        private readonly IRepository<Province> _ProvinceRepository;
        public ProvinceAppService(IRepository<Province> ProvinceRepository)
        {
            _ProvinceRepository = ProvinceRepository;
        }

        public async Task<PagedResultDto<ProvinceDtoForView>> Provinces()
        {
            var tasks = _ProvinceRepository.GetAll();

            var query = (from o in tasks
                         select new ProvinceDtoForView()
                         {
                             Province = new ProvinceDto
                             {
                                 Province_Name = o.Province_Name,
                                 Province_Code = o.Province_Code
                             }
                         });

            var ProvincesDtos = await query.ToListAsync();
            var totalCount = await query.CountAsync();


            return new PagedResultDto<ProvinceDtoForView>(totalCount, ProvincesDtos);
        }
    }
}
