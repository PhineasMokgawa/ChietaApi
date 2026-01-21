using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Workflows
{
    [Table("wf_TransitionActivity")]
    public class wfTransitionActivity: Entity
    {
        public int ActivityId { get; set; }
        public int TransitionId { get; set; }
        public DateTime DateCreated { get; set; }
        public int? UserId { get; set; }
    }
}
