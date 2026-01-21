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
    public class IdTypeAppService : CHIETAMISAppServiceBase
    {
        private readonly IRepository<IdType> _IdTypeRepository;
        public IdTypeAppService(IRepository<IdType> IdTypeRepository)
        {
            _IdTypeRepository = IdTypeRepository;
        }

        public async Task<PagedResultDto<IdTypeDtoForView>> IdTypes()
        {
            var tasks = _IdTypeRepository.GetAll().Where(a=>a.DGInd == true);

            var query = (from o in tasks
                         select new IdTypeDtoForView()
                         {
                             IdType = new IdTypeDto
                             {
                                 IdType_Name = o.Description,
                                 IdType_Code = o.Alternate_Id_Type_Id
                             }
                         });

            var IdTypesDtos = await query.ToListAsync();
            var totalCount = await query.CountAsync();


            return new PagedResultDto<IdTypeDtoForView>(totalCount, IdTypesDtos);
        }
    }
}
