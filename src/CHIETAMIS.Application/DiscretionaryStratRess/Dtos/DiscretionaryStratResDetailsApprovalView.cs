using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace CHIETAMIS.DiscretionaryStratRess.Dtos
{
    public class DiscretionaryStratResDetailsApprovalView:EntityDto
    {
		public int Id { get; set; }
		public string SDL_No { get; set; }
		public string Organisation_Name { get; set; }
		public string ProjectType { get; set; }
		public string FocusArea { get; set; }
		public string SubCategory { get; set; }
		public string Intervention { get; set; }
		public decimal? Cost { get; set; }
		public decimal? GEC_Amount { get; set; }
		public string Province { get; set; }
		public string Municipality { get; set; }
		public string Status { get; set; }
		public DiscretionaryResearchApproval ApprovalStatus { get; set; }
		public DiscretionaryGECRApproval GECStatus { get; set; }
		public string? vision2025goal { get; set; }
		public string? SqmrAppIndicator { get; set; }
		public string Leviesuptodate { get; set; }
		public string PreviousWSP { get; set; }
		public string PreviousParticipation { get; set; }
		public DateTime DateCreated { get; set; }
	}
}
