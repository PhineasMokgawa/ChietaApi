using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Lookups.DTOs
{
    public class MainPlaceDto: EntityDto
    {
        public string MP_NAME { get; set; }
        public string MN_NAME { get; set; }
        public string DC_NAME { get; set; }
        public string PR_NAME { get; set; }
    }
}
