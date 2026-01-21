using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CHIETAMIS.Lookups.DTOs
{
    public class MunicipalityDto : EntityDto<int>
    {
        public string Municipality_Name { get; set; }
        public int Municipality_Code { get; set; }
    }
}
