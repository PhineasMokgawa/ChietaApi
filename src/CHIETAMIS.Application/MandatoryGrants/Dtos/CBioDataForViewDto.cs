using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.MandatoryGrants.Dtos
{
    public class CBioDataForViewDto: EntityDto
    {
		public virtual int ApplicationId { get; set; }
		public virtual string SA_Id_Number { get; set; }
		public virtual string Passport_Number { get; set; }
		public virtual string Firstname { get; set; }
		public virtual string Middlename { get; set; }
		public virtual string Surname { get; set; }
		public virtual string Birth_Year { get; set; }
		public virtual string Gender { get; set; }
		public virtual string Race { get; set; }
		public virtual string Disability { get; set; }
		public virtual string Nationality { get; set; }
		public virtual string Province { get; set; }
		public virtual string Municipality { get; set; }
		public virtual string Highest_Qualification_Type { get; set; }
		public virtual string Employment_Status { get; set; }
		public virtual string Occupation_Level_For_Equity_Reporting { get; set; }
		public virtual string Organisational_Structure_Filter { get; set; }
		public virtual string Post_Reference { get; set; }
		public virtual string Job_Title { get; set; }
		public virtual string OFO_Occupation_Code { get; set; }
		public virtual string OFO_Specialisation { get; set; }
		public virtual string OFO_Occupation { get; set; }
		public virtual string Status { get; set; }
		public virtual string Comment { get; set; }
		public virtual int UserId { get; set; }
		public virtual DateTime DateCreated { get; set; }
		public virtual int? UsrUpd { get; set; }
		public virtual DateTime? DteUpd { get; set; }
	}
}
