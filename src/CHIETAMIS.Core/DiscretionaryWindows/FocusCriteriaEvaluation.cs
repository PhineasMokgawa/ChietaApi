using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CHIETAMIS.DiscretionaryWindows
{
    [Table("lkp_FocusCritEval")]
    public class FocusCriteriaEvaluation: Entity
    {
        public Int16 FocusAreaKey { get; set; }
        public Int16 AdminCritKey { get; set; }
        public Int16 EvalMthdCd { get; set; }
        public Int16 ProjTypCD { get; set; }
        public bool ActiveYN { get; set; }
        public bool AllowNew { get; set; }
        public bool AllowContinuing { get; set; }
    }
}
