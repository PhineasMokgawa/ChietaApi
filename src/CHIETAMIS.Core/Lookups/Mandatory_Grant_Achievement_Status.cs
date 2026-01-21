using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Lookups
{
    [Table("lkp_Mandatory_Grant_Achievement_Status")]
    public class Mandatory_Grant_Achievement_Status: Entity
    {
        public string Achievement_Status { get; set; }
    }
}
