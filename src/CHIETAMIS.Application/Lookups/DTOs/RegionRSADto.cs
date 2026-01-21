using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Lookups.DTOs
{
    public class RegionRSADto: EntityDto
    {
        public int UserID { get; set; }
        public int RegionID { get; set; }
        public string RSA_Name { get; set; }
    }
}
