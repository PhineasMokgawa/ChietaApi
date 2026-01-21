using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.MandatoryGrants
{
    [Table("tbl_Mandatory_Extensions")]
    public class Extensions: Entity
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
