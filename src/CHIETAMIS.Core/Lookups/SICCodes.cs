using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Lookups
{
    [Table("Ikp_SIC_Code")]
    public class SICCodes: Entity
    {
        public int? SIC_Code { get; set; }
        public string Description { get;set; }
    }
}
