using Abp.Application.Services.Dto;
using System;

namespace CHIETAMIS.LevyFilesDetails.Dtos
{
    public class GetAllLevyFileDetailsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public DateTime LevyDateFilter { get; set; }

        public int SETAFilter { get; set; }

        public string SDLNumberFilter { get; set; }

        public DateTime TransactionDateFilter { get; set; }

        public Decimal GrantAmountFilter { get; set; }

        public Decimal GrantAmount2Filter { get; set; }

        public Decimal AdminAmountFilter { get; set; }

        public Decimal InterestAmountFilter { get; set; }
        public Decimal PenaltyAmountFilter { get; set; }

        public Decimal DhetAmountFilter { get; set; }

        public int ReturnsOutstandingFilter { get; set; }

        public Decimal OutstandingDebtFilter { get; set; }

        public int LevyYearFilter { get; set; }
    }
}
