using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Lookups
{
    [Table("lkp_Mand_Pivotal_Programmes")]
    public class Mandatory_Pivotal_Programmes: Entity
    {
        public string Pivotal_Programme { get; set; }
    }
}
