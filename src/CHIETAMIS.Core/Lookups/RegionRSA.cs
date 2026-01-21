using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Lookups
{
    [Table("lkp_Region_RSA")]
    public class RegionRSA: Entity
    {
        public int UserID { get; set; }
        public int RegionID { get; set; }
        public string RSA_Name { get; set; }
    }
}
