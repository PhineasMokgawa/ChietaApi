using CHIETAMIS.PaymentPortals.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.DiscretionaryProjects.Dtos
{
    public class BatchPaymentDetailsDto
    {
        public int BatchId { get; set; }
        public string BatchNumber { get; set; }
        public List<PaymentTranches> paymentTranches { get; set; }
        public decimal BatchTotal { get; set; }
    }
}
