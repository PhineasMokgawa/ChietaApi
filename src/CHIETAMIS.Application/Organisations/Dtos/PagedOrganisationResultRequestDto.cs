using Abp.Application.Services.Dto;
using System;


namespace CHIETAMIS.Organisations.Dtos
{
    public class PagedOrganisationResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public bool? IsActive { get; set; }
    }
}
