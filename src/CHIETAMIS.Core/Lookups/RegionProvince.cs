using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Lookups
{
    [Table("lkp_RegionProvince")]
    public class RegionProvince: Entity
    {
        public int RegionId { get; set; }
        public int ProvinceId { get; set; }
    }
}
