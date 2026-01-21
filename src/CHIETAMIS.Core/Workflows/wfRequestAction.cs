using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Workflows
{
    [Table("wf_RequestAction")]
    public class wfRequestAction: Entity
    {
        public int RequestId { get; set; }
        public int ActionId { get; set; }
        public int TransitionId { get; set; }
        public bool IsActive { get; set; }
        public bool IsComplete { get; set; }
        public DateTime? DateActioned { get; set; }
        public int? UserActioned { get; set; }
        public DateTime DateCreated { get; set; }
        public int UserId { get; set; }
    }
}
