using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Lookups.DTOs
{
    public class DgPaymentTrancheTypeDto: EntityDto
    {
        public string TrancheCode { get; set; }
        public string Tranche_Description { get; set; }
    }
}
