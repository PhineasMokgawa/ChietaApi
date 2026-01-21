using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Finance.Dto
{
    public class LevyFileDetailsDto: EntityDto
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
