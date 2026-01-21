using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.PaymentPortals.DTOs
{
    public class ApplicationTrancheView: EntityDto
    {
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public string Contract_Number { get; set; }
        public int ProjectId { get; set; }
        public string TrancheType { get; set; }
        public string Description { get; set; }
        public string TrancheStatus { get; set; }
        public string FocusArea { get; set; }
        public string? SubCategory { get; set; }
        public decimal TrancheAmount { get; set; }
        public int? GC_New_Learners { get; set; }
        public int? GC_Continuing { get; set; }
        public decimal? GC_CostPerLearner { get; set; }
        public DateTime DateCreated { get; set; }
        public int UserId { get; set; }
        public int? Usrupd { get; set; }
        public DateTime? DteUpd { get; set; }
    }
}
