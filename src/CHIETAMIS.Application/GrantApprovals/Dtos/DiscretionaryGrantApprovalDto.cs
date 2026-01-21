using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace CHIETAMIS.GrantApprovals.Dtos
{
    public class DiscretionaryGrantApprovalDto: EntityDto
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
