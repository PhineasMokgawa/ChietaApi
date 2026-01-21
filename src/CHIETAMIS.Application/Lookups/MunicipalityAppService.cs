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
    public class MunicipalityAppService : CHIETAMISAppServiceBase
    {
        private readonly IRepository<Area> _MunicipalityRepository;
        private readonly IRepository<MainPlace> _mainPlace;
        private readonly IRepository<SubPlace> _subRepository;


        public MunicipalityAppService(IRepository<Area> MunicipalityRepository,
            IRepository<MainPlace> mainPlace,
            IRepository<SubPlace> subRepository)
        {
            _MunicipalityRepository = MunicipalityRepository;
            _mainPlace = mainPlace;
            _subRepository = subRepository;
        }

        public async Task<PagedResultDto<MunicipalityDtoForView>> Municipalitys(int postcode)
        {
            var tasks = _MunicipalityRepository.GetAll();

            var query = (from o in tasks
                         select new MunicipalityDtoForView()
                         {
                             Municipality = new MunicipalityDto
                             {
                                 Municipality_Name = o.Municipality_Name,
                                 Municipality_Code = o.CodeId
                             }
                         });

            var MunicipalitysDtos = await query.Distinct().ToListAsync();
            var totalCount = await query.CountAsync();


            return new PagedResultDto<MunicipalityDtoForView>(totalCount, MunicipalitysDtos);
        }

        public async Task<PagedResultDto<MunicipalityDtoForView>> Municipalities(string province)
        {
            var tasks = _MunicipalityRepository.GetAll();

            var query = (from o in tasks
                         select new MunicipalityDtoForView()
                         {
                             Municipality = new MunicipalityDto
                             {
                                 Municipality_Name = o.Municipality_Name,
                             }
                         });

            var MunicipalitysDtos = await query.Distinct().ToListAsync();
            var totalCount = await query.CountAsync();


            return new PagedResultDto<MunicipalityDtoForView>(totalCount, MunicipalitysDtos);
        }

        public async Task<PagedResultDto<MunicipalityDtoForView>> MunicipalitysProvince(string province)
        {
            var tasks = _subRepository.GetAll().Where(e => e.PR_NAME == province);

            var query = (from o in tasks
                         select new MunicipalityDtoForView()
                         {
                             Municipality = new MunicipalityDto
                             {
                                 Municipality_Name = o.MN_NAME,
                                 Municipality_Code = o.MN_CODE,
                             }
                         });

            var MunicipalitysDtos = await query.Distinct().ToListAsync();
            var totalCount = await query.CountAsync();


            return new PagedResultDto<MunicipalityDtoForView>(totalCount, MunicipalitysDtos);
        }
    }
}
