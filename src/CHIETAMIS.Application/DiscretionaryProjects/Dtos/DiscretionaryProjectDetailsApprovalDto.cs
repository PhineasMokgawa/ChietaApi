using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace CHIETAMIS.DiscretionaryProjects.Dtos
{
	public class DiscretionaryProjectDetailsApprovalDto : EntityDto
	{
		public int ProjectId { get; set; }
		public int ProjectTypeId { get; set; }
		public int? SubmittedBy { get; set; }
		public DateTime? SubmissionDte { get; set; }
		public int? ApplicationStatusId { get; set; }
        public string? Contract_Number { get; set; }
        public string? Current_Approver { get; set; }
		public string? Reason { get; set; }
		public string? ApprovalStatus { get; set; }
		public int FocusAreaId { get; set; }
		public int SubCategoryId { get; set; }
		public int InterventionId { get; set; }
		public string? OtherIntervention { get; set; }
		public int FocusCritEvalId { get; set; }
		public int Number_Continuing { get; set; }
		public int Number_New { get; set; }
		public decimal CostPerLearner { get; set; }
		public int? GEC_Continuing { get; set; }
		public int? GEC_New { get; set; }
		public decimal? GEC_CostPerLearner { get; set; }
		public int? GAC_Continuing { get; set; }
		public int? GAC_New { get; set; }
		public decimal? GAC_CostPerLearner { get; set; }
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
		public string District { get; set; }
		public string? SqmrAppIndicator { get; set; }
		public string? vision2025goal { get; set; }
		public string Leviesuptodate { get; set; }
		public string PreviousWSP { get; set; }
		public string PreviousParticipation { get; set; }
		public DateTime DateCreated { get; set; }
		public int UserId { get; set; }
		public int? UsrUpd {get; set;}
		public DateTime? DteUpd { get; set; }
	}
}
