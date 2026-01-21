using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Lookups.DTOs
{
    public class Fintegrate_Payment_StatusDto: EntityDto
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
