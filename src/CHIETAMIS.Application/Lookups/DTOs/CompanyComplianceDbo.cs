using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.ookups.DTOs
{
    public class CompanyComplianceDbo: EntityDto
    {
        public string Description { get; set; }
        public int Score { get; set; }
    }
}
