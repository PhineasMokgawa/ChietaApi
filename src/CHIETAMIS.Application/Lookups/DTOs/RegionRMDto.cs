using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Lookups.DTOs
{
    public class RegionRMDto: EntityDto
    {
        public int UserId { get; set; }
        public int RegionId { get; set; }
        public string ManagerName { get; set; }
    }
}
