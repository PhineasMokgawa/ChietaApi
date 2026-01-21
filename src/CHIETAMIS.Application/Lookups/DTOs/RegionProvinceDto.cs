using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Lookups.DTOs
{
    public class RegionProvinceDto: EntityDto
    {
        public int RegionId { get; set; }
        public int ProvinceId { get; set; }
    }
}
