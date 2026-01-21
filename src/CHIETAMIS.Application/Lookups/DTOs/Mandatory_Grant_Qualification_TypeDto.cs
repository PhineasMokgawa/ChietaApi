using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Lookups.DTOs
{
    public class Mandatory_Grant_Qualification_TypeDto: EntityDto
    {
        public string Qualification_Type { get; set; }
        public string NQF_Level { get; set; }
        public string Band { get; set; }
    }
}
