using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Lookups
{
    [Table("lkp_Mandatory_Grants_Scarce_Reason")]
    public class Mandatory_Grants_Scarce_Reason: Entity
    {
        public string Scarce_Reason { get; set; }
    }
}
