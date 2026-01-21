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
    public class DesignationAppService : CHIETAMISAppServiceBase
    {
        private readonly IRepository<Designation> _designationRepository;
        public DesignationAppService(IRepository<Designation> designationRepository)
        {
            _designationRepository = designationRepository;
        }

        public async Task<PagedResultDto<DesignationDtoForView>> Designations()
        {

            var tasks = _designationRepository.GetAll();



            var query = (from o in tasks
                         select new DesignationDtoForView()
                         {
                             Designation = new DesignationDto
                             {
                                 Designation_Name = o.Designation_Name,
                                 Designation_Code = o.Designation_Code
                             }
                         });

                var DesignationsDtos = await query.ToListAsync();

                var totalCount = await query.CountAsync();


                return new PagedResultDto<DesignationDtoForView>(totalCount, DesignationsDtos);
        }
    }
}
