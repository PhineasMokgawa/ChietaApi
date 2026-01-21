using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Finance
{
    [Table("tbl_LeviesRecon")]
    public class LeviesRecon: Entity
    {
        public int ZipFileId { get; set; }
        public string ZipFileName { get; set; }
        public DateTime TransactionDate { get; set; }
        public int LevyYear { get; set; }
        public decimal GrantAmount { get; set; }
        public decimal GrantAmount2 { get; set; }
        public decimal AdminAmount { get; set; }
        public decimal InterestAmount { get; set;}
        public decimal InterestAmount2 { get;set;}
        public decimal PenaltyAmount { get; set; }
    }
}
