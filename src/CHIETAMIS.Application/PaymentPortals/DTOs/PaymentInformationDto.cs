using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.PaymentPortals.DTOs
{
    public class PaymentInformationDto: EntityDto  
    {
        public string PmtInfId { get; set; }
        public string MessageId { get; set; }
        public string PmtMtd { get; set; }
        public decimal Total_Amount { get; set; }
        public bool BtchBookg { get; set; }
        public int NbOfTxs { get; set; }
        public decimal CtrlSum { get; set; }
        public string SvcLvl_Cd { get; set; }
        public DateOnly ReqdExctnDt { get; set; }
        public string Dbtr { get; set; }
        public string DbtrAcct_Id { get; set; }
        public string DbtrAcct_TP_Cd { get; set; }
        public string DbtrAgt_ClrSysMmbId_MmbId { get; set; }
        public string DbtrAgt_BrnchId { get; set; }
        public string CdtTrfTxInf_PmtId_EndToEndId { get; set; }
        public decimal CdtTrfTxInf_Amt_InstdAmt { get; set; }
        public string CdtTrfTxInf_CdtrAgt_FinInstnId_MmbId { get; set; }
        public string CdtTrfTxInf_CdtrAgt_BrnchId { get; set; }
        public string CdtTrfTxInf_CdtTrfTxInfCdtr_Nm { get; set; }
        public string CdtTrfTxInf_CdtrAcct_Id { get; set; }
        public string CdtTrfTxInf_CdtrAcct_Tp_Prtry { get; set; }
        public string CdtTrfTxInf_RmtInf_Ustrd { get; set; }
        public DateTime DateCreated { get; set; }
        public int UserId { get; set; }
    }
}
