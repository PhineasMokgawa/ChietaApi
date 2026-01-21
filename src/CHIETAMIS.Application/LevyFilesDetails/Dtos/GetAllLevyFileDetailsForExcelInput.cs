using Abp.Application.Services.Dto;
using System;

namespace CHIETAMIS.LevyFilesDetails.Dtos
{
    public class GetAllLevyFileDetailsForExcelInput
    {
        public int Id { get; set; }

        public int LevyFileId { get; set; }

        public DateTime LevyDate { get; set; }

        public int SETA { get; set; }

        public string SDLNumber { get; set; }

        public DateTime TransactionDate { get; set; }

        public Decimal GrantAmount { get; set; }

        public Decimal GrantAmount2 { get; set; }

        public Decimal AdminAmount { get; set; }

        public Decimal InterestAmount { get; set; }
        public Decimal PenaltyAmount { get; set; }

        public Decimal DhetAmount { get; set; }

        public int ReturnsOutstanding { get; set; }

        public Decimal OutstandingDebt { get; set; }

        public int LevyYear { get; set; }
    }
}
