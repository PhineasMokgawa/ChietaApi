using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Lookups
{
    [Table("lkp_OFO_Specialization")]
    public class OFO_Specialization: Entity
    {
        public string OFO_Code { get; set; }
        public string Specilization { get; set; }
    }
}
