using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.DiscretionaryStratRess
{
	[Table("tbl_Discretionary_StratRes_Details")]
	public class DiscretionaryStratResDetails: Entity
    {
		public int ProjectId { get; set; }
		public int ProjectTypeId { get; set; }
		public int FocusAreaId { get; set; }
		public int SubCategoryId { get; set; }
		public int InterventionId { get; set; }
		public int FocusCritEvalId { get; set; }
		public string Province { get; set; }
		public string District { get; set; }
		public string Municipality { get; set; }
		public DateTime DateCreated { get; set; }
		public int UserId { get; set; }
	}
}
