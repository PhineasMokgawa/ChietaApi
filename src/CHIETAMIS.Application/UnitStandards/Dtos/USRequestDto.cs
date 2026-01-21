using Abp.Application.Services.Dto;
using System;

namespace CHIETAMIS.UnitStandards.Dtos
{
    public class USRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public bool? IsActive { get; set; }
    }
}
