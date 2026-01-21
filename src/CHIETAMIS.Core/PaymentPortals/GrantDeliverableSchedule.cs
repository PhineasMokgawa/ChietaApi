using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.PaymentPortals
{
    [Table("lkp_Grant_Deliverable_Schedule")]
    public class GrantDeliverableSchedule: Entity
    {
        public string Delivertable { get; set; }
        public string Obligation { get; set; }
        public int TrancheTypeId { get; set; }
        public int Percentage { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
