using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Workflows
{
    [Table("wf_ProcessAdmins")]
    public class wfProcessAdmins : Entity
    {
        public int ProcessId { get; set; }
        public int UserId { get; set; }
        public DateTime DateCreated { get; set; }
        public int? UserCreated { get; set; }

    }
}
