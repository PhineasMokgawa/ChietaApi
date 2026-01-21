using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Lookups
{
    [Table("lkp_Mandatory_ExtensionStatus")]
    public class Mandatory_Extension_Status: Entity
    {
        public string StatusDescription { get; set; }
    }
}
