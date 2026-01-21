using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Lesedis.Dtos
{
    public class BursarydocumentApprovalsView
    {
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public string ApprovalType { get; set; }
        public string ApprovalStatus { get; set; }
        public string DocumentType { get; set; }
        public string? Comments { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DteUpd { get; set; }
        public int? UsrUpd { get; set; }
        public int UserId { get; set; }
    }
}
