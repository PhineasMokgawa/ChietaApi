using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace CHIETAMIS.DiscretionaryStratRess.Dtos
{
    public class DiscretionaryStratResDetailsDto: EntityDto
    {
		public int ProjectId { get; set; }
		public int ProjectTypeId { get; set; }
		public int FocusAreaId { get; set; }
		public int SubCategoryId { get; set; }
		public int InterventionId { get; set; }
		public int FocusCritEvalId { get; set; }
		public string Province { get; set; }
		public string Municipality { get; set; }
		public string District { get; set; }
		public DateTime DateCreated { get; set; }
		public int UserId { get; set; }
	}
}
