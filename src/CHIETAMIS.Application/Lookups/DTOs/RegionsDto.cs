using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Lookups.DTOs
{
    public class RegionsDto: EntityDto
    {
        public string RegionCode { get; set; }
        public string RegionName { get; set; }
    }
}
