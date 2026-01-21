using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.MandatoryGrants.Dtos
{
    public class MandatoryWindowForView
    {
        public int Id { get; set; }
        public string ReferenceNo { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime? ExtensionDate { get; set; }
        public bool ActiveYN { get; set; }
        public bool ExtenstionActive { get; set; }
        public DateTime DateCreated { get; set; }
        public int UserId { get; set; }
        public DateTime? DteUpd { get; set; }
        public int? UsrUpd { get; set; }
    }
}
