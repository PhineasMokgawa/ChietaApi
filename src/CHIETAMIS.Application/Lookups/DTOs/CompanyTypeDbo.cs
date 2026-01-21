using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Lookups.DTOs
{
    public class CompanyTypeDbo: EntityDto
    {
        public string Description { get; set; }
        public int Score { get; set; }
    }
}
