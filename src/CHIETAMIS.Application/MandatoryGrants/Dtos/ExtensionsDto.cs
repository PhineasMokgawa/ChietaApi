using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.MandatoryGrants.Dtos
{
    public class ExtensionsDto: EntityDto
    {
        public int ApplicationId { get; set; }
        public int RequestStatus { get; set; }
        public DateTime DateRequested { get; set; }
        public string ReasonForRequest { get; set; }
        public DateTime DateCreated { get; set; }
        public int UserId { get; set; }
        public DateTime? DteUpd { get; set; }
        public int? UsrUpd { get; set; }
    }
}
