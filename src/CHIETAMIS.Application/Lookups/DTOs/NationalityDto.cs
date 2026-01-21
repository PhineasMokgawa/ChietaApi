using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CHIETAMIS.Lookups.DTOs
{
    public class NationalityDto : EntityDto<int>
    {
        public string Nationality_Name { get; set; }
        public string Nationality_Code { get; set; }
    }
}
