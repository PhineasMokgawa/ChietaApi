using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace CHIETAMIS.DiscretionaryProjects.Dtos
{
    public class DiscretionaryProjectDetailsView: EntityDto
    {
		public int Id { get; set; }
		public string ProjectType { get; set; }
        public int ProjectId { get; set; }
        public int? ApplicationStatusId { get; set; }
		public string? Contract_Number { get; set; }
		public int? SubmittedBy { get; set; }
		public DateTime? SubmissionDte { get; set; }
		public string Current_Approver { get; set; }
		public string Reason { get; set; }
		public string ApprovalStatus { get; set; }
		public string FocusArea { get; set; }
		public string SubCategory { get; set; }
		public string Intervention { get; set; }
		public string? OtherIntervention { get; set; }
		public int Number_Continuing { get; set; }
		public int Number_New { get; set; }
		public decimal CostPerLearner { get; set; }
		public int? GC_Continuing { get; set; }
		public int? GC_New { get; set; }
		public decimal? GC_CostPerLearner { get; set; }
		public int HDI { get; set; }
		public int Female { get; set; }
		public int Youth { get; set; }
		public int Number_Disabled { get; set; }
		public int Rural { get; set; }
		public string Province { get; set; }
		public string Municipality { get; set; }
		public string Status { get; set; }
		public int? OrganisationId { get; set; }
		public DiscretionaryGCApproval GCStatus { get; set; }
		public DateTime DateCreated { get; set; }
	}
}
