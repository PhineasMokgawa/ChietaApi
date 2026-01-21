using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.MandatoryGrants
{
	[Table("tbl_Mandatory_SkillGaps")]
	public class SkillGab : Entity
	{
		public virtual int ApplicationId { get; set; }
		public virtual string OCCUPATION_OR_SPECIALISATION_TITLE { get; set; }

		public virtual string Code { get; set; }

		public virtual string SKILL_GAB { get; set; }

		public virtual string REASON_FOR_THE_SKILLS_GAP { get; set; }

		public virtual string ADDITIONAL_COMMENTS { get; set; }
        public virtual string Comment { get; set; }
        public virtual string Status { get; set; }

        public virtual int UserID { get; set; }
		public virtual DateTime DateCreated { get; set; }
		public virtual int? UsrUpd { get; set; }
		public virtual int? DteUpd { get; set; }

	}
}
