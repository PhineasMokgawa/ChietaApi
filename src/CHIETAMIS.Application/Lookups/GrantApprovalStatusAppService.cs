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
    public class GrantApprovalStatusAppService: CHIETAMISAppServiceBase
    {
        private readonly IRepository<GrantApprovalStatus> _grantApprovalStatus;
        public GrantApprovalStatusAppService(IRepository<GrantApprovalStatus> grantApprovalStatus)
        {
            _grantApprovalStatus = grantApprovalStatus;
        }

        public async Task<PagedResultDto<GrantApprovalStatusForView>> GrantApprovalStatuss()
        {
            var tasks = _grantApprovalStatus.GetAll();

            var query = (from o in tasks
                         select new GrantApprovalStatusForView()
                         {
                             GrantApprovalStatus = new GrantApprovalStatusDto
                             {
                                 Id = o.Id,
                                 GrantStatusDescription = o.GrantStatusDescription
                             }
                         });

            var res = query.ToList();
            var totalCount = query.Count();


            return new PagedResultDto<GrantApprovalStatusForView>(totalCount, res);
        }
    }
}
