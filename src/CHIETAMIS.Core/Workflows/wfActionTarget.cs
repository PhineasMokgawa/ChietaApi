using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Workflows
{
    [Table("wf_ActionTarget")]
    public class wfActionTarget: Entity
    {
        public int ActionId { get; set; }
        public int TargetId { get; set; }
        public int? GroupId { get; set; }
        public DateTime DateCreated { get; set; }
        public int? UserId { get; set; }
    }
}
