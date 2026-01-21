using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace CHIETAMIS.Finance
{
    [Table("tbl_LevyFileDetails")]
    public class LevyFileDetails : Entity
    {
        public int LevyFileId { get; set; }

        public DateTime LevyDate { get; set; }

        public int SETA { get; set; }

        public string SDLNumber { get; set; }

        public DateTime TransactionDate { get; set; }

        public decimal GrantAmount { get; set; }

        public decimal GrantAmount2 { get; set; }

        public decimal AdminAmount { get; set; }

        public decimal InterestAmount { get; set; }
        public decimal PenaltyAmount { get; set; }

        public decimal DhetAmount { get; set; }

        public int ReturnsOutstanding { get; set; }

        public decimal OutstandingDebt { get; set; }

        public int LevyYear { get; set; }
    }
}
