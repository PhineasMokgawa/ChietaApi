using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio.Jwt.Taskrouter;

namespace CHIETAMIS.Schedules
{
    [Table("tbl_Discretionary_Learner_Schedule")]
    public class DiscretionaryLearnerSchedule: Entity
    {
        public string TrancheType { get; set; }
        public int ProjectLearnerId { get; set; }
    }
}
