using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CHIETAMIS.Lookups.DTOs
{
    public class AreaDto : EntityDto<int>
    {
        public string Area_Name { get; set; }
        public int Area_Code { get; set; }
    }
}
