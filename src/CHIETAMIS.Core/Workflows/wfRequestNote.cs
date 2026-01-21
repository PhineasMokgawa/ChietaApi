using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Workflows
{
    [Table("wf_RequestNote")]
    public class wfRequestNote: Entity
    {
        public int RequestId { get; set; }
        public int UserId { get; set; }
        public string Note { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
