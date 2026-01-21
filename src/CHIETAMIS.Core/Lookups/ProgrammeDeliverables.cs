using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Lookups
{
    [Table("lkp_Discretionary_ProgrammeDeliverables")]
    public class ProgrammeDeliverables: Entity
    {
        public int ProjectTypeId { get; set; }
        public int? FocusAreaId { get; set; }
        public int? SubCategoryId { get; set; }
        public string TrancheTypeId { get; set; }
        public int DeliverableId { get; set; }
        public string Deliverable { get; set; }
        public string DocumentType { get; set; }
        public string AppliesTo { get; set; }
        public decimal TranchePercent { get; set; }
    }
}
