using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Documents.Dtos
{
    public class MandatoryDocumentApprovalForView
    {
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public string ApprovalType { get; set; }
        public string ApprovalStatus { get; set; }
        public string? Comments { get; set; }
    }
}
