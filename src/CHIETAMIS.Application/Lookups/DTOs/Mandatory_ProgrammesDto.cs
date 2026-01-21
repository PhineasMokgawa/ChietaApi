using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Lookups.DTOs
{
    public class Mandatory_ProgrammesDto: EntityDto
    {
        public string Programme_Type { get; set; }
        public string Programme { get; set; }
    }
}
