using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.PaymentPortals
{
    [Table("tbl_PaymentMessages")]
    public class PaymentMessages : Entity<int>
    {
        public string BatchNum { get; set; }
        public DateTime CreDtTm { get; set; }
        public int NbOfTxs { get; set; }
        public double CtrlSum { get; set; }
        public string Nm { get; set; }
        public DateTime DateCreated { get; set; }
        public int UserId { get; set; }

    }
}
