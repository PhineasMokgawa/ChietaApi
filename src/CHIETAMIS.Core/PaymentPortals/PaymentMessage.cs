using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.PaymentPortals
{
    public class PaymentMessage: Entity
    {
        public string MessageId { get; set; }

        public int Number_Transactions { get; set; }

        public decimal Total_Amount { get; set; }

        public string Inititator_Name { get; set; }
	    public DateTime DateCreated { get; set; }

        public int UserId { get; set; }
    }
}
