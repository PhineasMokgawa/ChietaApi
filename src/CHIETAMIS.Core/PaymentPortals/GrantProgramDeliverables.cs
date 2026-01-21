using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.PaymentPortals
{
    [Table("lkp_ProgrammeDeliverables")]
    public class GrantProgramDeliverables: Entity
    {
        public int ProjectTypeId { get; set; }
        public int TrancheScheduleId { get; set; }
        public int FocusAreaId { get; set; }
        public int SubcategoryId { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
