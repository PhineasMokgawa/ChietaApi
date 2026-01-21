using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace CHIETAMIS.DiscretionaryStratRess.Dtos
{
    public class DiscretionaryStratResObjectivesApprovalDto: EntityDto
    {
		public int DetailsId { get; set; }
		public string Objectiv { get; set; }
		public decimal Cost { get; set; }
		public int? Learners { get; set; }
		public DateTime DateCreated { get; set; }
		public int UserId { get; set; }
	}
}
