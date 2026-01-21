using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Lookups.DTOs
{
    public class OFODto:EntityDto
    {
        public int OFO_Code { get; set; }
        public string Description { get; set; }
        public string Major { get; set; }
    }
}
