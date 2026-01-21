using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.PaymentPortals.DTOs
{
    public class PaymentTranches
    {
        public string TrancheType { get; set; }
        public decimal Amount { get; set; }

    }
}
