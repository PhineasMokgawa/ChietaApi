using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.DiscretionaryGrants
{
	public class FinancialGrant : Entity
	{
		public int ApplicationId { get; set; }
		public int ProjectId { get; set; }
		public int OrganisationId { get; set; }
		public string Grant_Description { get; set; }
		public string GrantCode { get; set; }
		public string GrantType { get; set; }
		public string GrantBase { get; set; }
		public float GrantPerc { get; set; }
		public int RelevantYearFrom { get; set; }
		public int RelevantMonthFrom { get; set; }
		public int RelevantYearTo { get; set; }
		public int RelevantMonthTo { get; set; }
		public int FinYr { get; set; }
		public double GrantAmount { get; set; }
		public int UsrUpd {get; set;}
		public DateTime DteUpd {get; set;}
		public DateTime DateCreated { get; set; }
		public int UsrCreated {get; set;}

}
}
