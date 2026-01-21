using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.DiscretionaryProjects.Dtos
{
    public class DiscrationaryTrancheBatchRequestsDto: EntityDto
    {
        public int TrancheBatchId { get; set; }
        public int RequestId { get; set; }
        public DateTime DateCreated { get; set; }
        public int UserId { get; set; }
    }
}
