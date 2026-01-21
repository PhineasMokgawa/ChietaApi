using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CHIETAMIS.Lookups.DTOs
{
    public class EquityDto : EntityDto<int>
    {
        public string Equity_Name { get; set; }
        public string Equity_Code { get; set; }
    }
}
