using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.DiscretionaryStratRess
{
	[Table("tbl_Discretionary_StratResObjectives")]
	public class DiscretionaryStratResObjectives: Entity
    {
		public int DetailsId { get; set; }
		public string Objectiv { get; set; }
		public decimal Cost { get; set; }
		public int? Learners { get; set; }
		public DateTime DateCreated { get; set; }
		public int UserId { get; set; }
	}
}
