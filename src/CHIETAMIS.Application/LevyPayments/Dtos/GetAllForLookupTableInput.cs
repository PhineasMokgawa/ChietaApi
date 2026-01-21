using Abp.Application.Services.Dto;

namespace CHIETAMIS.LEVYPAYMENTS.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}