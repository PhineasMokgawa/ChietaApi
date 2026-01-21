using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Lookups
{
    [Table("lkp_Mandatory_Status")]
    public class MandatoryStatus: Entity
    {
        public string StatusDesc { get; set; }
        public string Typ { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
