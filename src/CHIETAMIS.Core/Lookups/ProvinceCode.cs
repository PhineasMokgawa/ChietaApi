using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Lookups
{
    [Table("Ikp_Province_Code")]
    public class ProvinceCode: Entity
    {
        public int Province_Code { get; set; }
        public string Description { get; set; }
    }
}
