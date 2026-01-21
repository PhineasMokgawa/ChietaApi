using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using CHIETAMIS.Lookups.DTOs;

namespace CHIETAMIS.Lookups
{
    public class GrantApprovalTypeAppService: CHIETAMISAppServiceBase
    {
        private readonly IRepository<GrantApprovalType> _grantApprovalType;
        public GrantApprovalTypeAppService(IRepository<GrantApprovalType> grantApprovalType)
        {
            _grantApprovalType = grantApprovalType;
        }

        public async Task<PagedResultDto<GrantApprovalTypeForView>> GrantApprovalTypes()
        {
            var tasks = _grantApprovalType.GetAll();

            var query = (from o in tasks
                         select new GrantApprovalTypeForView()
                         {
                             GrantApprovalType = new GrantApprovalTypeDto
                             {
                                 Id = o.Id,
                                 ApprovalDescription = o.ApprovalDescription
                             }
                         });

            var res = query.ToList();
            var totalCount = query.Count();


            return new PagedResultDto<GrantApprovalTypeForView>(totalCount, res);
        }
    }
}
