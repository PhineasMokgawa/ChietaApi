using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CHIETAMIS.DiscretionaryWindows
{
    [Table("lkp_EvaluationMethod")]
    public class EvaluationMethod: Entity
    {
        public string EvalMthdCd { get; set; }
        public string EvalMthdDesc { get; set; }
        public bool ActiveYN { get; set; }
    }
}
