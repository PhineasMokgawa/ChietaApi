using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Lookups
{
    [Table("lkp_Payment_Tranches")]

    public class Payment_Tranches: Entity<int>
    {
        public string TrancheCode { get; set; }
        public int ProjectTypeId { get; set; }
        public string Tranche_Description { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
