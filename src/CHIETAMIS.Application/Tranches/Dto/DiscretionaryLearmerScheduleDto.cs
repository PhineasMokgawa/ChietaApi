using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Tranches.Dto
{
    public class DiscretionaryLearnerScheduleDto : EntityDto
    {
        public string TrancheType { get; set; }
        public int ProjectLearnerId { get; set; }
        public string Description { get; set; }
    }
}
