using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.PaymentPortals
{
    [Table("tbl_Tranche_Approvals")]
    public class Tranche_Approvals : Entity<int>
    {
        public int ApplicationId { get; set; }
        public int TrancheId { get; set; }
        public string ApprovalLevel { get; set; }
        public string Approval_Status { get; set; }
        public string Comment { get; set; }
        public int UserId { get; set; }
        public DateTime DateApproved { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
