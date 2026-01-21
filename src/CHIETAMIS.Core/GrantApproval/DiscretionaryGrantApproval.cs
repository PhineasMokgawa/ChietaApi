using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.GrantApprovals
{
    [Table("tbl_Discretionary_Grant_Approvals")]
    public class DiscretionaryGrantApproval : Entity
    {
        public int ProjectId { get; set; }
        public int ApprovalTypeId { get; set; }
        public int ApprovalStatusId { get; set; }
        public bool BBBEE { get; set; }
        public bool TaxClearance { get; set; }
        public bool BankLetter { get; set; }
        public bool DeclarationOfInterest { get; set; }
        public bool ProjectProposal { get; set; }
        public string? Comments { get; set; }
        public string? Outcome { get; set; }
        public DateTime? MeetingDate { get; set; }
        public DateTime? OutcomeDate { get; set; }
        public DateTime DateCreated { get; set; }
        public int UserId { get; set; }
        public DateTime? DteUpd { get; set; }
        public int? UsrUpd { get; set; }

    }
}
