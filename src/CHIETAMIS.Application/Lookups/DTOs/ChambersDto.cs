using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Lookups.DTOs
{
    public class ChambersDto: EntityDto
    {
        public int Code { get; set; }
        public string Chamber { get; set; }
    }
}
