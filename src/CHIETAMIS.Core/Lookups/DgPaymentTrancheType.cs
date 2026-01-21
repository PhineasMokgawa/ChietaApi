using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Lookups
{
    [Table("lkp_Dg_Payment_Tranche_Type")]
    public class DgPaymentTrancheType: Entity
    {
        public string TrancheCode { get; set; }
        public string Tranche_Description { get; set; }
    }
}
