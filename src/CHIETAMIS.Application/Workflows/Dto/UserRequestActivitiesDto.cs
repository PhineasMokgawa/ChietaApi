using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Workflows.Dto
{
    public class UserRequestActivitiesDto
    {
        public int ProcessId { get; set; }
        public string ProcessName { get; set; }
        public int ActivityId { get; set; }
        public string ActivityName { get; set; }
        public int RequestId { get; set; }
        public string RequestTitle { get; set; }
        public string RequestPath { get; set; }
        public int CurrentStateId { get; set; }
        public string CurrentState { get; set; }
        public string NextState { get; set; }
        public string? Username { get; set; }
        public int? GroupId { get; set; }
        public DateTime DateRequested { get; set; }
        public int ApplicationId { get; set; }

    }
}
