using Abp.Application.Services.Dto;
using System;

namespace CHIETAMIS.MandatoryGrantPayments.Dtos
{
    public class GetMandatoryGrantPaymentsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
        public string SDLNumberFilter { get; set; }
        public int LevyYearFilter { get; set; }
    }
}
