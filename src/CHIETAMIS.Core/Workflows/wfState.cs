using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Workflows
{
    [Table("wf_State")]
    public class wfState : Entity
    {
        public int StateTypeId { get; set; }
        public int ProcessId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public int? UserId { get; set; }

    }
}
