using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Finance
{
    [Table("tbl_ImportBatch")]
    public class ImportBatch:Entity
    {
        public int ZipFileId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int LevyYear { get; set; }
        public int SETA { get; set; }
        public string BatchType { get; set; }
        public string BatchName { get; set; }
        public decimal MandatoryCollected { get; set; }
        public decimal DiscretionaryCollected { get; set;}
        public decimal AdminCollected { get;set; }
        public decimal InterestCollected { get; set; }
        public decimal PenaltyCollected { get; set; }
        public decimal RebateCollected { get; set; }
    }
}
