using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Lookups.DTOs
{
    public class MandatoryStatusDto: EntityDto
    {
        public string StatusDesc { get; set; }
        public string Typ { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
