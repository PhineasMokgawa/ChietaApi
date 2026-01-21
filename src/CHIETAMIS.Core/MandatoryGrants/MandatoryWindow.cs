using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.MandatoryGrants
{
    [Table("tbl_Mandatory_Grant_Window")]
    public class MandatoryWindow: Entity
    {
        public int Id { get; set; }
        //public string MG_Window { get; set; }
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
