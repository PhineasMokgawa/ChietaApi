using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.DiscretionaryStratRess
{
	[Table("tbl_Discretionary_StratRes_Details_Approval")]
	public class DiscretionaryStratResDetailsApproval : Entity
	{
		public int ProjectId { get; set; }
		public int ProjectTypeId { get; set; }
		public int FocusAreaId { get; set; }
		public int SubCategoryId { get; set; }
		public int InterventionId { get; set; }
		public int FocusCritEvalId { get; set; }
		public decimal? Cost { get; set; }
		public decimal? GEC_Amount { get; set; }
		public string Province { get; set; }
		public string District { get; set; }
		public string Municipality { get; set; }
		public string? vision2025goal { get; set; }
		public string? SqmrAppIndicator { get; set; }
		public string Leviesuptodate { get; set; }
		public string PreviousWSP { get; set; }
		public string PreviousParticipation { get; set; }
		public DateTime DateCreated { get; set; }
		public int UserId { get; set; }
		public int? UsrUpd { get; set; }
		public DateTime? DteUpd { get; set; }
	}
}
