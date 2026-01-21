using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Workflows
{
    [Table("wf_TimerResult")]
    public class wfTimerResult: Entity
    {
        public string TimerResult { get; set; }
        public int? UserId { get; set; }
    }
}
