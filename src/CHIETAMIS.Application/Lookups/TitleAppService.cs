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
    public class TitleAppService : CHIETAMISAppServiceBase
    {
        private readonly IRepository<Title> _titleRepository;
        public TitleAppService(IRepository<Title> titleRepository)
        {
            _titleRepository = titleRepository;
        }

        public async Task<PagedResultDto<TitleDtoForView>> Titles()
        {
                var tasks = _titleRepository.GetAll();

                var query = (from o in tasks
                             select new TitleDtoForView()
                             {
                                 Title = new TitleDto
                                 {
                                     Title_Name = o.Title_Name,
                                     Title_Code = o.Title_Code
                                 }
                             });

                var TitlesDtos = await query.ToListAsync();
                var totalCount = await query.CountAsync();


            return new PagedResultDto<TitleDtoForView>(totalCount, TitlesDtos);
        }
    }
}
