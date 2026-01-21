using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace CHIETAMIS.Documents.Dtos
{
    public class DiscretionaryDocumentApprovalDto: EntityDto
    {
        public int ProjectId { get; set; }
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
