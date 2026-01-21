using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.PaymentPortals.DTOs
{
    public class Payment_Message_TransactionsDto: EntityDto
    {
        public int PaymentMessageId { get; set; }
        public int ApplicationTrancheDetailsId { get; set; }
        public decimal CtrlSum { get; set; }
        public string PmtMtd { get; set; }
        public int NbOfTxs { get; set; }
        public string Cd { get; set; }
        public DateTime ReqdExctnDt { get; set; }
        public string Nm { get; set; }
        public string DbtrAcct_Id { get; set; }
        public string DbtrAcct_TP { get; set; }
        public string MmbId { get; set; }
        public string EndtoEndId { get; set; }
        public decimal InstdAmt { get; set; }
        public string ClrSysMmbId { get; set; }
        public string CdtrAgt_BrnchId { get; set; }
        public string Cdtr_Nm { get; set; }
        public string CdtrAcct_Id { get; set; }
        public string CdtrAcct_Tp { get; set; }
        public string Ustrd { get; set; }
        public DateTime DateCreated { get; set; }
        public int UserId { get; set; }
        public int UsrUpd { get; set; }
        public DateTime DteUpd { get; set; }

    }
}
