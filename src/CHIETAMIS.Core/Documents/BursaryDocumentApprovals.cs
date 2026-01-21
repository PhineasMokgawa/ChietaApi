using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Documents
{
    [Table("tbl_Bursary_DocumentApprovals")]
    public class BursaryDocumentApprovals: Entity
    {
        public int ApplicationId { get; set; }
        public int ApprovalTypeId { get; set; }
        public int ApprovalStatusId { get; set; }
        public int DocumentTypeId { get; set; }
        public string? Comments { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DteUpd { get; set; }
        public int? UsrUpd { get; set; }
        public int UserId { get; set; }
    }
}
