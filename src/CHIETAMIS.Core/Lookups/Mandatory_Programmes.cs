using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Lookups
{
    [Table("lkp_Mandatory_Programmes")]
    public class Mandatory_Programmes: Entity
    {
        public string Programme_Type { get; set; }
        public string Programme { get;set; }
    }
}
