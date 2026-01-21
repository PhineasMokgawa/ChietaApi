using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace CHIETAMIS.DiscretionaryProjects.Dtos
{
    public class DiscretionaryProjectDetailsApprovalPagedDto: EntityDto
    {
        public DiscretionaryProjectDetailsApprovalDto projectDetailsApproval { get; set; }
    }
}
