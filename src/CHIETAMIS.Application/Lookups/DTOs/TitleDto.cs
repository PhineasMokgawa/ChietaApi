using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CHIETAMIS.Lookups.DTOs
{
    public class TitleDto: EntityDto<int>
    {
        public string Title_Name { get; set; }
        public string Title_Code { get; set; }
    }
}
