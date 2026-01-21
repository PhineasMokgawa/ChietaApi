using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Lookups
{
    [Table("lkp_Mandatory_Grant_Qualification_Type")]
    public class Mandatory_Grant_Qualification_Type: Entity
    {
        public string Qualification_Type { get; set; }
        public string NQF_Level { get; set; }
        public string Band { get; set; }
    }
}
