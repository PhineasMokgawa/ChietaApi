using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.PaymentPortals.DTOs
{
    public class ApplicationTrancheDetailsDto: EntityDto
    {
        public int ApplicationTrancheId { get; set; }
        public int? LearnerDetailsId { get; set; }
        public decimal? Amount { get; set; }
        public string ApplicationTranceStatus { get; set; }
        public string? Current_Approver { get; set; }
        public int UserId { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
