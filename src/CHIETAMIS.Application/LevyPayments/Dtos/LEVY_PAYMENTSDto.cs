
using System;
using Abp.Application.Services.Dto;

namespace CHIETAMIS.LEVYPAYMENTS.Dtos
{
    public class LEVY_PAYMENTSDto : EntityDto
    {
        public decimal PERIOD { get; set; }

        public string SDL_NO { get; set; }

        public DateTime? RECEIPT_DATE_SARS { get; set; }

        public decimal LEVY_AMOUNT { get; set; }

        public decimal PENALTY_AMOUNT { get; set; }

        public decimal INTEREST_AMOUNT { get; set; }

        public decimal TOTAL_AMOUNT { get; set; }

        public decimal NO_SDL201_OUTSTANDING { get; set; }

        public decimal DEBT_OUTSTANDING_AMOUNT { get; set; }

        public decimal SARS_LEVY { get; set; }

        public decimal SARS_INTEREST { get; set; }

        public decimal SARS_PENALTY { get; set; }

        public decimal NSF_LEVY { get; set; }

        public decimal NSF_INTEREST { get; set; }

        public decimal NSF_PENALTY { get; set; }

        public decimal SETA_SETUP_LEVY { get; set; }

        public decimal SETA_SETUP_INTEREST { get; set; }

        public decimal SETA_SETUP_PENALTY { get; set; }

        public decimal SETA_ADMIN_LEVY { get; set; }

        public decimal SETA_ADMIN_INTEREST { get; set; }

        public decimal SETA_ADMIN_PENALTY { get; set; }

        public decimal UNAPPORTIONED_LEVY { get; set; }

        public decimal UNAPPORTIONED_INTEREST { get; set; }

        public decimal UNAPPORTIONED_PENALTY { get; set; }

        public decimal GRANT_A { get; set; }

        public decimal GRANT_B { get; set; }

        public decimal GRANT_C { get; set; }

        public decimal GRANT_D { get; set; }

        public decimal FINANCIAL_YEAR { get; set; }

        public string LEVY_TYPE { get; set; }

        public decimal? SETA_CODE { get; set; }

        public string GRANT_A_STATUS { get; set; }

        public string GRANT_A_STATUS_COMMENT { get; set; }

        public DateTime? GRANT_A_DISBURSED_DATE { get; set; }

        public string GRANT_A_DISBURSED_BY { get; set; }

        public decimal? GRANT_A_APPROVED { get; set; }

        public decimal? GRANT_A_DECLINED { get; set; }

        public decimal? GRANT_A_SWEPT { get; set; }

        public string GRANT_B_STATUS { get; set; }

        public string GRANT_B_STATUS_COMMENT { get; set; }

        public DateTime? GRANT_B_DISBURSED_DATE { get; set; }

        public string GRANT_B_DISBURSED_BY { get; set; }

        public decimal? GRANT_B_APPROVED { get; set; }

        public decimal? GRANT_B_DECLINED { get; set; }

        public decimal? GRANT_B_SWEPT { get; set; }

        public string GRANT_C_STATUS { get; set; }

        public string GRANT_C_STATUS_COMMENT { get; set; }

        public DateTime? GRANT_C_DISBURSED_DATE { get; set; }

        public string GRANT_C_DISBURSED_BY { get; set; }

        public decimal? GRANT_C_APPROVED { get; set; }

        public decimal? GRANT_C_DECLINED { get; set; }

        public decimal? GRANT_C_SWEPT { get; set; }

        public string GRANT_D_STATUS { get; set; }

        public string GRANT_D_STATUS_COMMENT { get; set; }

        public DateTime? GRANT_D_DISBURSED_DATE { get; set; }

        public string GRANT_D_DISBURSED_BY { get; set; }

        public decimal? GRANT_D_APPROVED { get; set; }

        public decimal? GRANT_D_DECLINED { get; set; }

        public decimal? GRANT_D_SWEPT { get; set; }

        public string GRANT_E_STATUS { get; set; }

        public string GRANT_E_STATUS_COMMENT { get; set; }

        public DateTime? GRANT_E_DISBURSED_DATE { get; set; }

        public string GRANT_E_DISBURSED_BY { get; set; }

        public decimal? GRANT_E_APPROVED { get; set; }

        public decimal? GRANT_E_DECLINED { get; set; }

        public decimal? GRANT_E_SWEPT { get; set; }

        public decimal? GRANT_A_OVERRIDDEN_AMOUNT_DIFF { get; set; }

        public decimal? GRANT_B_OVERRIDDEN_AMOUNT_DIFF { get; set; }

        public decimal? GRANT_C_OVERRIDDEN_AMOUNT_DIFF { get; set; }

        public decimal? GRANT_D_OVERRIDDEN_AMOUNT_DIFF { get; set; }

        public decimal? GRANT_E_OVERRIDDEN_AMOUNT_DIFF { get; set; }

        public string PROOF_OF_PAYMENT_RECEIVED { get; set; }

        public decimal? TOTAL_GRANT_APPROVED { get; set; }

        public string GRANT_A_PROCESSED { get; set; }

        public string GRANT_B_PROCESSED { get; set; }

        public string GRANT_C_PROCESSED { get; set; }

        public string GRANT_D_PROCESSED { get; set; }

        public string GRANT_E_PROCESSED { get; set; }

        public string GRANT_A_PAYMENT_STATUS { get; set; }

        public string GRANT_B_PAYMENT_STATUS { get; set; }

        public string GRANT_C_PAYMENT_STATUS { get; set; }

        public string GRANT_D_PAYMENT_STATUS { get; set; }

        public string GRANT_E_PAYMENT_STATUS { get; set; }

        public string GRANT_A_CHEQUE_EFT_NO { get; set; }

        public string GRANT_B_CHEQUE_EFT_NO { get; set; }

        public string GRANT_C_CHEQUE_EFT_NO { get; set; }

        public string GRANT_D_CHEQUE_EFT_NO { get; set; }

        public string GRANT_E_CHEQUE_EFT_NO { get; set; }

        public decimal? GRANT_E { get; set; }

        public string GRANT_A_BATCH_NO { get; set; }

        public string GRANT_B_BATCH_NO { get; set; }

        public string GRANT_C_BATCH_NO { get; set; }

        public string GRANT_D_BATCH_NO { get; set; }

        public string GRANT_E_BATCH_NO { get; set; }

        public string GRANT_B_USER_COMMENT { get; set; }

        public string GRANT_C_USER_COMMENT { get; set; }

        public decimal? LEVY_AMOUNT_RECEIVED { get; set; }

        public decimal? INTEREST_AMOUNT_RECEIVED { get; set; }

        public decimal? PENALTY_AMOUNT_RECEIVED { get; set; }

        public decimal? TOTAL_AMOUNT_RECEIVED { get; set; }

        public decimal? SETA_COMPLETE_ADMIN_LEVY { get; set; }

        public decimal? SETA_COMPLETE_ADMIN_INTEREST { get; set; }

        public decimal? SETA_COMPLETE_ADMIN_PENALTY { get; set; }

        public decimal? SETA_COMPLETE_ADMIN_TOTAL { get; set; }

        public decimal? GRANT_MG { get; set; }

        public decimal? GRANT_DG { get; set; }

        public string GRANT_MG_STATUS { get; set; }

        public string GRANT_MG_STATUS_COMMENT { get; set; }

        public DateTime? GRANT_MG_DISBURSED_DATE { get; set; }

        public string GRANT_MG_DISBURSED_BY { get; set; }

        public decimal? GRANT_MG_APPROVED { get; set; }

        public decimal? GRANT_MG_DECLINED { get; set; }

        public decimal? GRANT_MG_SWEPT { get; set; }

        public decimal? GRANT_MG_OVERRIDDEN_AMT_DIFF { get; set; }

        public string GRANT_MG_PROCESSED { get; set; }

        public string GRANT_MG_PAYMENT_STATUS { get; set; }

        public string GRANT_MG_CHEQUE_EFT_NO { get; set; }

        public string GRANT_MG_BATCH_NO { get; set; }

        public string GRANT_MG_USER_COMMENT { get; set; }

        public string GRANT_DG_STATUS { get; set; }

        public string GRANT_DG_STATUS_COMMENT { get; set; }

        public DateTime? GRANT_DG_DISBURSED_DATE { get; set; }

        public string GRANT_DG_DISBURSED_BY { get; set; }

        public decimal? GRANT_DG_APPROVED { get; set; }

        public decimal? GRANT_DG_DECLINED { get; set; }

        public decimal? GRANT_DG_SWEPT { get; set; }

        public decimal? GRANT_DG_OVERRIDDEN_AMT_DIFF { get; set; }

        public string GRANT_DG_PROCESSED { get; set; }

        public string GRANT_DG_PAYMENT_STATUS { get; set; }

        public string GRANT_DG_CHEQUE_EFT_NO { get; set; }

        public string GRANT_DG_BATCH_NO { get; set; }

        public string GRANT_DG_USER_COMMENT { get; set; }

        public string statusOne { get; set; }

        public string statusTwo { get; set; }



    }
}