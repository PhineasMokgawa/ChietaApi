using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Workflows.Dto
{
    public class wfTimerDto: EntityDto
    {
        public int ProcessId { get; set; }
        public int RequestId { get; set; }
        public int TransitionId { get; set; }
        public DateTime StartDate { get; set; }
        public int TimerDurationId { get; set; }
        public string DurationType { get; set; }
        public int Duration { get; set; }
        public int TimerResultId { get; set; }
        public int? UserId { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
