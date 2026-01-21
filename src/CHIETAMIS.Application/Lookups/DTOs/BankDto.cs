using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace CHIETAMIS.Lookups.DTOs
{
    public class BankDto:EntityDto
    {
        public string Bank_Name { get; set; }
        public string Branch_Code { get; set; }
    }
}
