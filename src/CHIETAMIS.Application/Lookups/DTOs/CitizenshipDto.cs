using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CHIETAMIS.Lookups.DTOs
{
    public class CitizenshipDto : EntityDto<int>
    {
        public string Citizenship_Name { get; set; }
        public string Citizenship_Code { get; set; }
    }
}
