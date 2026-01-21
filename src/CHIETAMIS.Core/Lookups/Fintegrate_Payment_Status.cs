using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Lookups
{
    [Table("lkp_Fintegrate_Payment_Status")]
    public class Fintegrate_Payment_Status : Entity<int>
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }

    }
}
