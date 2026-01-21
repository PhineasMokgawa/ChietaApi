using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace CHIETAMIS.DiscretionaryStratRess.Dtos
{
    public class DiscretionaryStratResObjectsApprovalForViewDto: EntityDto
    {
        public DiscretionaryStratResObjectivesApprovalDto DiscretionaryStratResObjectivesApproval { get; set; }
    }
}
