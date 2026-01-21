using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Lookups.DTOs
{
    public class Bank_Account_TypeDto: EntityDto
    {
        public string AccountType { get; set; }
        public string FIntegrateType { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
