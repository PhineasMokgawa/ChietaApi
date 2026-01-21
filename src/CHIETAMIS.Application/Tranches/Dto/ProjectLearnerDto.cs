using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Tranches.Dto
{
    public class ProjectLearnerDto : EntityDto
    {
        public int ApplicationId { get; set; }
        public int LearnerId { get; set; }
        public int WorkplaceId { get; set; }
        public int ProviderId { get; set; }
        public int UserId { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
