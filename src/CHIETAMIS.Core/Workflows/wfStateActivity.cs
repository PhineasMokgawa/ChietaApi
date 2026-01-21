using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Workflows
{
    [Table("wf_StateActivity")]
    public class wfStateActivity: Entity
    {
        public int StateId { get; set; }
        public int ActivityId { get; set; }
        public DateTime DateCreated { get; set; }
        public int? Userid { get; set; }
    }
}
