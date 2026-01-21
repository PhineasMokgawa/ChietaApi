using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.PaymentPortals.DTOs
{
    public class PaymentMessagesDto: EntityDto
    {
        public string BatchNum { get; set; }
        public DateTime CreDtTm { get; set; }
        public int NbOfTxs { get; set; }
        public decimal CtrlSum { get; set; }
        public string Nm { get; set; }
        public DateTime DateCreated { get; set; }
        public int UserId { get; set; }
    }
}
