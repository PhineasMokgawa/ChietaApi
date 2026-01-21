using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Workflows
{
    [Table("wf_TransitionTimer")]
    public class wfTransitionTimer: Entity
    {
        public int TransitionId { get; set; }
        public int TimeDurationId { get; set; }
        public int Duration { get; set; }
        public DateTime DateCreated { get; set; }
        public int UserId { get; set; }
    }
}
