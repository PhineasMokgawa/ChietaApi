using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CHIETAMIS.Lookups.DTOs
{
    public class GenderDto : EntityDto<int>
    {
        public string Gender_Name { get; set; }
        public string Gender_Code { get; set; }
    }
}
