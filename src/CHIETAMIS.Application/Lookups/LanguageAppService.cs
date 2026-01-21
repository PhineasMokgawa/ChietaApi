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
    public class LanguageAppService : CHIETAMISAppServiceBase
    {
        private readonly IRepository<Language> _LanguageRepository;
        public LanguageAppService(IRepository<Language> LanguageRepository)
        {
            _LanguageRepository = LanguageRepository;
        }

        public async Task<PagedResultDto<LanguageDtoForView>> Languages()
        {
            var tasks = _LanguageRepository.GetAll();

            var query = (from o in tasks
                         select new LanguageDtoForView()
                         {
                             Language = new LanguageDto
                             {
                                 Language_Name = o.Description,
                                 Language_Code = o.Home_Language_Code
                             }
                         });

            var LanguagesDtos = await query.ToListAsync();
            var totalCount = await query.CountAsync();


            return new PagedResultDto<LanguageDtoForView>(totalCount, LanguagesDtos);
        }
    }
}
