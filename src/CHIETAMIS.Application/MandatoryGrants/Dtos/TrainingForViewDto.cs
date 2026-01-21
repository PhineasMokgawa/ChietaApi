using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.MandatoryGrants.Dtos
{
    public class TrainingForViewDto: EntityDto
    {
		public virtual int ApplicationId { get; set; }
		public virtual string? SA_Id_Number { get; set; }
		public virtual string? Passport_Number { get; set; }
		public virtual string Qualification_Learning_Program_Type { get; set; }

		public virtual string Details_Of_Learning_Program { get; set; }

		public virtual string Study_Field_Or_Specialisation_Specification { get; set; }

		public virtual decimal Total_Training_Cost { get; set; }

		public virtual string Achievement_status { get; set; }
		public virtual int Year_enrolled_or_completed { get; set; }
		public virtual int? BiodataId { get; set; }
        public string? Status { get; set; }
        public string? Comment { get; set; }
        public virtual int? UserId { get; set; }
		public virtual DateTime DateCreated { get; set; }
		public virtual int? UsrUpd { get; set; }
		public virtual DateTime? DteUpd { get; set; }
	}
}
