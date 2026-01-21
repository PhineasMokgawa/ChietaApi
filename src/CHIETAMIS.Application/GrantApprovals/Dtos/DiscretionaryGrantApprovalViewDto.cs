using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.GrantApprovals.Dtos
{
    public class DiscretionaryGrantApprovalViewDto
    {
        public int ProjectId { get; set; }
        public string StatusDescription { get; set; }
        public string ApprovalDescription { get; set; }
        public bool BBBEE { get; set; }
        public bool TaxClearance { get; set; }
        public bool BankLetter { get; set; }
        public bool DeclarationOfInterest { get; set; }
        public bool ProjectProposal { get; set; }
        public string? Comments { get; set; }
        public string? Outcome { get; set; }
        public DateTime? MeetingDate { get; set; }
        public DateTime? OutcomeDate { get; set; }
        public int Id { get; set; }
    }
}
