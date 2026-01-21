using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Lookups
{
    [Table("lkp_Tranche_Approval_Status")]
    public class Tranche_Approval_Status : Entity<int>
    {
        public string ApprovalStatus { get; set; }
        public string ApprovalGroup { get; set; }
        public string DateCreated { get; set; }
    }
}
