using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace CHIETAMIS.DiscretionaryStratRess.Dtos
{
    public class DiscretionaryStratResDetailsView: EntityDto
    {
		public int Id { get; set; }
		public string ProjectType { get; set; }
		public string FocusArea { get; set; }
		public string SubCategory { get; set; }
		public string Intervention { get; set; }
		public string Province { get; set; }
		public string Municipality { get; set; }
		public string Status { get; set; }
		public DateTime DateCreated { get; set; }
	}
}
