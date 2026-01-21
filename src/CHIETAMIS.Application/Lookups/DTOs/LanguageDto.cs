using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CHIETAMIS.Lookups.DTOs
{
    public class LanguageDto : EntityDto<int>
    {
        public string Language_Name { get; set; }
        public string Language_Code { get; set; }
    }
}
