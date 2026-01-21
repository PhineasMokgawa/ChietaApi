using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Workflows.Dto
{
    public class wfTimerResultDto: EntityDto
    {
        public string TimerResult { get; set; }
        public int? UserId { get; set; }
    }
}
