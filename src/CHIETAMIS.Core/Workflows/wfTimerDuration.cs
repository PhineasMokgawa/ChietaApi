using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Workflows
{
    [Table("wf_TimerDuration")]
    public class wfTimerDuration: Entity
    {
        public string DurationType { get; set; }
    }
}
