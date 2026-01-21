using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace CHIETAMIS.DiscretionaryProjects.Dtos
{
    public class DiscretionaryProjectDetailsDto : EntityDto
	{
		public int ProjectId { get; set; }
		public int ProjectTypeId { get; set; }
		public int FocusAreaId { get; set; }
		public int SubCategoryId { get; set; }
		public int InterventionId { get; set; }
		public string? OtherIntervention { get; set; }
		public int FocusCritEvalId { get; set; }
		public int Number_Continuing { get; set; }
		public int Number_New { get; set; }
		public decimal CostPerLearner { get; set; }
		public int HDI { get; set; }
		public int Female { get; set; }
		public int Youth { get; set; }
		public int Number_Disabled { get; set; }
		public int Rural { get; set; }
		public string Province { get; set; }
		public string Municipality { get; set; }
		public string District { get; set; }
		public DateTime DateCreated { get; set; }
		public int UserId { get; set; }
	}
}
