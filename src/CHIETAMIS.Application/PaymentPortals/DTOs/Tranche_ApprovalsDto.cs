using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.PaymentPortals.DTOs
{
    public class Tranche_ApprovalsDto: EntityDto
    {
        public int ApplicationId { get; set; }
        public int TrancheId { get; set; }
        public string ApprovalLevel { get; set; }
        public string Approva_Status { get; set; }
        public string Comment { get; set; }
        public int UserId { get; set; }
        public DateTime DateApproved { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
