using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.MandatoryGrants.Dtos
{
    public class MandatoryExtensionsView
    {
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public string RequestStatus { get; set; }
        public DateTime DateRequested { get; set; }
        public string ReasonForRequest { get; set; }
        public DateTime? ExtensionDate { get; set; }
        public DateTime DateCreated { get; set; }
        public int UserId { get; set; }
        public DateTime DteUpd { get; set; }
        public int UsrUpd { get; set; }
    }
}
