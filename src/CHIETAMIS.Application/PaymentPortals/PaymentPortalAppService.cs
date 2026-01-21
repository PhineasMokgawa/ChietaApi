using Abp.Domain.Repositories;
using CHIETAMIS.PaymentPortals.DTOs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using CHIETAMIS.Lookups;
using CHIETAMIS.DiscretionaryWindows;
using Abp.Application.Services.Dto;
using CHIETAMIS.DiscretionaryProjects;
using CHIETAMIS.Workflows;
using CHIETAMIS.Organisations;

namespace CHIETAMIS.PaymentPortals
{
    public class PaymentPortalAppService : CHIETAMISAppServiceBase
    {

        private readonly IRepository<GrantDeliverableSchedule> _grantDeliverableSchedule;
        private readonly IRepository<DiscretionaryTrancheBatch> _trancheBatch;
        private readonly IRepository<DiscrationaryTrancheBatchRequests> _tbRequests;
        private readonly IRepository<wfRequest> _request;
        private readonly IRepository<wfRequestData> _requestData;
        private readonly IRepository<ApplicationTranche> _appTranche;
        private readonly IRepository<DiscretionaryProjectDetailsApproval> _discProjDetails;
        private readonly IRepository<DiscretionaryProject> _discProj;
        private readonly IRepository<Organisation> _org;
        private readonly IRepository<BankDetails> _bank;
        public PaymentPortalAppService(IRepository<GrantDeliverableSchedule> grantDeliverableSchedule,
                                        IRepository<DiscretionaryTrancheBatch> trancheBatch,
                                        IRepository<DiscrationaryTrancheBatchRequests> tbRequests,
                                        IRepository<wfRequest> request,
                                        IRepository<wfRequestData> requestData,
                                        IRepository<ApplicationTranche> appTranche,
                                        IRepository<DiscretionaryProjectDetailsApproval> discProjDetails,
                                        IRepository<DiscretionaryProject> discProj,
                                        IRepository<Organisation> org,
                                        IRepository<BankDetails> bank)
        {
            _grantDeliverableSchedule = grantDeliverableSchedule;
            _trancheBatch = trancheBatch;
            _tbRequests = tbRequests;
            _request = request;
            _requestData = requestData;
            _appTranche = appTranche;
            _discProjDetails = discProjDetails;
            _discProj = discProj;
            _org = org;
            _bank = bank;
        }

        public async Task GeneratePaymentInformation(int BatchId, int userid)
        {
            var payments = (from at in _appTranche.GetAll()
                            join rd in _requestData.GetAll().Where(a => a.Name == "ApplicationId") on at.ApplicationId.ToString() equals rd.Value
                            join r in _request.GetAll() on rd.RequestId equals r.Id
                            join dpd in _discProjDetails.GetAll() on at.ApplicationId equals dpd.Id
                            join batch in _trancheBatch.GetAll() on at.BatchId equals batch.Id
                            join tbr in _tbRequests.GetAll() on batch.Id equals tbr.TrancheBatchId
                            join p in _discProj.GetAll() on dpd.ProjectId equals p.Id
                            join org in _org.GetAll() on p.OrganisationId equals org.Id
                            join bank in _bank.GetAll() on org.Id equals bank.OrganisationId

                            select new
                            {
                                Id = batch.Id,
                                BatchNum = "M" + batch.Id.ToString(),
                                Amount = at.TrancheAmount,
                                Dbtr_Nm = "Debtor One Inc",
                                DbtrAcct_Id = "62006676772",
                                DbtrAgt_ClrSysMmbId_MmbId = "250655",
                                DbtrAgt_BrnchId = "250655",
                                CdtTrfTxInf_PmtId_EndToEndId = "T" + at.Id,
                                CdtTrfTxInf_ClrSysMmbId = bank.Branch_Code,
                                CdtTrfTxInf_CdtrAgt_BrnchId = bank.Branch_Code,
                                CdtTrfTxInf_CdtTrfTxInfCdtr_Nm = bank.Account_Holder,
                                CdtTrfTxInf_CdtrAcct_Id = bank.Account_Number,
                                CdtTrfTxInf_CdtrAcct_Tp_Prtry = "BOND",
                                CdtTrfTxInf_RmtInf_Ustrd = "Inv: " + at.Id,
                                BatchId = batch.Id,
                                ProgType =  p.ProjectTypeId,
                                TBRRequestId = tbr.RequestId,
                                TRDescription = at.Description,
                                RPath = r.RequestPath,
                                RequestId = r.Id,
                                pmtid = at.Id
                            })
                    .Where(a => a.Id == BatchId && a.TBRRequestId == a.RequestId &&
                            ((a.TRDescription == "Tranche 1a" && a.RPath == "tranche1aapproval") ||
                            (a.TRDescription == "Tranche 1b" && a.RPath == "tranche1bapproval")))
                    .ToList();

            var HeadInfo = payments.FirstOrDefault();
            var ntrans = payments.Count();
            var TotalSum = payments.Sum(a => a.Amount);
            var DBAccNumber = "";
            var DBBranchCode = "250655";

            if (HeadInfo.ProgType == 2)
            {
                DBAccNumber = "62910739749";
            }
            if (HeadInfo.ProgType == 4)
            {
                DBAccNumber = "62910739773";
            }


            XmlTextWriter writer = new XmlTextWriter("C:\\temp\\DGPayment B" + BatchId.ToString() + ".xml", System.Text.Encoding.UTF8);
            writer.WriteStartDocument();
            writer.Formatting = Formatting.Indented;
            writer.Indentation = 4;
            writer.WriteStartElement("Document");
            writer.WriteAttributeString("xmlns", "urn:iso:std:iso:20022:tech:xsd:pain.001.001.03");
            writer.WriteAttributeString("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
            writer.WriteStartElement("CstmrCdtTrfInitn");
                writer.WriteStartElement("GrpHdr");
                    writer.WriteStartElement("MsgId");
                        writer.WriteString(HeadInfo.BatchNum);  //BatchNo
                    writer.WriteEndElement();
                    writer.WriteStartElement("CreDtTm");
                        writer.WriteString(DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"));  //Current Timestamp
                    writer.WriteEndElement();
                    writer.WriteStartElement("NbOfTxs");
                        writer.WriteString(ntrans.ToString());    //Count of Transactions in batch
                    writer.WriteEndElement();
                    writer.WriteStartElement("CtrlSum");
                        writer.WriteString(Math.Round(TotalSum, 2).ToString());  //Sum of transactions in batch
                    writer.WriteEndElement();
                    writer.WriteStartElement("InitgPty");
                        writer.WriteStartElement("Nm");
                            writer.WriteString("CHIETA"); //CHIETA( It think)
                        writer.WriteEndElement();
                    writer.WriteEndElement();
                writer.WriteEndElement();


            foreach (var pmt in payments)
            {
                //Add each payment information here
                writer.WriteStartElement("PmtInf");
                    writer.WriteStartElement("PmtInfId");
                        writer.WriteString(pmt.pmtid.ToString());
                    writer.WriteEndElement();
                    writer.WriteStartElement("PmtMtd");
                        writer.WriteString("TRF");
                    writer.WriteEndElement();
                    writer.WriteStartElement("BtchBookg");
                        writer.WriteString("false");
                    writer.WriteEndElement();
                    writer.WriteStartElement("NbOfTxs");
                        writer.WriteString("1");
                    writer.WriteEndElement();
                    writer.WriteStartElement("CtrlSum");
                        writer.WriteString(Math.Round(pmt.Amount,2).ToString());
                    writer.WriteEndElement();

                    writer.WriteStartElement("PmtTpInf");
                        writer.WriteStartElement("SvcLvl");
                            writer.WriteStartElement("Cd");
                                writer.WriteString("SDVA");
                            writer.WriteEndElement();
                        writer.WriteEndElement();
                    writer.WriteEndElement();

                    writer.WriteStartElement("ReqdExctnDt");
                        writer.WriteString(DateTime.Now.ToString("yyyy-MM-dd"));
                    writer.WriteEndElement();

                    writer.WriteStartElement("Dbtr");
                        writer.WriteStartElement("Nm");
                            writer.WriteString("CHIETA");
                        writer.WriteEndElement();
                    writer.WriteEndElement();

                    writer.WriteStartElement("DbtrAcct");
                        writer.WriteStartElement("Id");
                            writer.WriteStartElement("Othr");
                                writer.WriteStartElement("Id");
                                    writer.WriteString(DBAccNumber);
                                writer.WriteEndElement();
                            writer.WriteEndElement();
                        writer.WriteEndElement();
                        writer.WriteStartElement("Tp");
                            writer.WriteStartElement("Cd");
                                writer.WriteString("CACC");
                            writer.WriteEndElement();
                        writer.WriteEndElement();
                    writer.WriteEndElement();

                    writer.WriteStartElement("DbtrAgt");
                        writer.WriteStartElement("FinInstnId");
                            writer.WriteStartElement("ClrSysMmbId");
                                writer.WriteStartElement("MmbId");
                                    writer.WriteString(DBBranchCode);
                                writer.WriteEndElement();
                            writer.WriteEndElement();
                        writer.WriteEndElement();
                        writer.WriteStartElement("BrnchId");
                            writer.WriteStartElement("Id");
                                writer.WriteString(DBBranchCode);
                            writer.WriteEndElement();
                        writer.WriteEndElement();
                    writer.WriteEndElement();

                    writer.WriteStartElement("CdtTrfTxInf");
                        writer.WriteStartElement("PmtId");
                            writer.WriteStartElement("EndToEndId");
                                writer.WriteString(pmt.CdtTrfTxInf_PmtId_EndToEndId);
                            writer.WriteEndElement();
                        writer.WriteEndElement();
                        writer.WriteStartElement("Amt");
                            writer.WriteStartElement("InstdAmt");
                                writer.WriteAttributeString("Ccy", "ZAR");
                                writer.WriteString(Math.Round(pmt.Amount,2).ToString());
                            writer.WriteEndElement();
                        writer.WriteEndElement();
                        writer.WriteStartElement("CdtrAgt");
                            writer.WriteStartElement("FinInstnId");
                                writer.WriteStartElement("ClrSysMmbId");
                                    writer.WriteStartElement("MmbId");
                                        writer.WriteString(pmt.CdtTrfTxInf_ClrSysMmbId.ToString());
                                    writer.WriteEndElement();
                                writer.WriteEndElement();
                            writer.WriteEndElement();
                            writer.WriteStartElement("BrnchId");
                                writer.WriteStartElement("Id");
                                    writer.WriteString(pmt.CdtTrfTxInf_CdtrAgt_BrnchId);
                                writer.WriteEndElement();
                            writer.WriteEndElement();
                        writer.WriteEndElement();
                        writer.WriteStartElement("Cdtr");
                            writer.WriteStartElement("Nm");
                                writer.WriteString(pmt.CdtTrfTxInf_CdtTrfTxInfCdtr_Nm);
                            writer.WriteEndElement();
                        writer.WriteEndElement();
                        writer.WriteStartElement("CdtrAcct");
                            writer.WriteStartElement("Id");
                                writer.WriteStartElement("Othr");
                                    writer.WriteStartElement("Id");
                                        writer.WriteString(pmt.CdtTrfTxInf_CdtrAcct_Id);
                                    writer.WriteEndElement();
                                writer.WriteEndElement();
                            writer.WriteEndElement();
                            writer.WriteStartElement("Tp");
                                writer.WriteStartElement("Prtry");
                                    writer.WriteString("GRCP");
                                writer.WriteEndElement();
                            writer.WriteEndElement();
                        writer.WriteEndElement();
                        writer.WriteStartElement("RmtInf");
                            writer.WriteStartElement("Ustrd");
                                writer.WriteString(pmt.CdtTrfTxInf_RmtInf_Ustrd);
                            writer.WriteEndElement();
                        writer.WriteEndElement();
                    writer.WriteEndElement();
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();
        }


    }
}
