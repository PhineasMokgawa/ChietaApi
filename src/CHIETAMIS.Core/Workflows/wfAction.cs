using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Workflows
{
    [Table("wf_Action")]
    public class wfAction : Entity
    {
        public int ActionTypeId { get; set; }
        public int ProcessId { get; set; }
        public string Name { get; set; }
        public string Description {get; set; }
        public DateTime DateCreated { get; set; }
        public int UserId { get; set; }
    }
}
