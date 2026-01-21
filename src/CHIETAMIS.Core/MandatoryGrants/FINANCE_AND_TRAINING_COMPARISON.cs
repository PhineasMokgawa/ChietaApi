using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.MandatoryGrants
{
	[Table("tbl_Mandatory_Finance_Training")]
	public class FINANCE_AND_TRAINING_COMPARISON : Entity
	{
		public virtual int ApplicationId { get; set; }
		public virtual decimal? TOTAL_ACTUAL_PAYROLL_FOR_THE_YEAR { get; set; }

		public virtual decimal? TOTAL_ACTUAL_SKILLS_DEVELOPMENT_SPEND_FOR_THE_YEAR { get; set; }

		public virtual decimal? OF_PAYROLL_SPENT_ON_SKILLS_DEVELOPMENT { get; set; }

		public virtual decimal? TOTAL_PROJECTED_PAYROLL_FOR_THE_YEAR { get; set; }

		public virtual decimal? TOTAL_PROJECTED_SKILLS_DEVELOPMENT_BUDGET { get; set; }

		public virtual decimal PROJECTED_PAYROLL { get; set; }

		public virtual int? BENEFICIARIES_TRAIN { get; set; }

		public virtual int? TOTAL_BENEFICIARIES_ACTUALLY_TRAINED_IN_THE { get; set; }

		public virtual decimal? ACTUAL_TRAINING_VS_PLANNED_TRAINING { get; set; }

		public virtual string CONFIRMATION_OF_EMPLOYEES_HIGHEST_QUALIFICATIONS { get; set; }

		public virtual string LEARNING_OPPORTUNITIES_UNEMPLOYED_PEOPLE { get; set; }

		public virtual string LEARNING_AREAS_AND_OPPORTUNITIES_FOR_EMPLOYED_STAFF { get; set; }

		public virtual string ADDRESSING_EQUITY_AND_BBBEE_TARGETS { get; set; }

		public virtual string WORK_PLACEMENT { get; set; }

		public virtual string AREAS_FOR_RESEARCH_AND_INNOVATION { get; set; }

		public virtual string LEARNERS_RETAINED { get; set; }

		public virtual string PEOPLE_FOUND_EMPLOYMENT_DUE_TRAINING { get; set; }

		public virtual string General_Comments { get; set; }
		public virtual int UserId { get; set; }
		public virtual DateTime DateCreated { get; set; }
		public virtual int? UsrUpd { get; set; }
		public virtual DateTime? DteUpd { get; set; }
	}
}
