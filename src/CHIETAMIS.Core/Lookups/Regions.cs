using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Lookups
{
    [Table("lkp_Regions")]
    public class Regions: Entity
    {
        public string RegionCode { get; set; }
        public string RegionName { get; set; }
    }
}
