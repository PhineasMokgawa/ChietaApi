using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Lookups
{
    [Table("lkp_CompanyCompliance")]
    public class CompanyCompliance: Entity
    {
        public string Description { get; set; }
        public int Score { get; set; }
    }
}
