
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
using CHIETAMIS.LEVYPAYMENTS;

namespace CHIETAMIS.LEVYPAYMENTS.Dtos
{
    public class CreateOrEditLEVY_PAYMENTSDto : EntityDto<int?>
    {

        [Required]
        public decimal PERIOD { get; set; }


        [Required]
        [StringLength(LEVY_PAYMENTSConsts.MaxSDL_NOLength, MinimumLength = LEVY_PAYMENTSConsts.MinSDL_NOLength)]
        public string SDL_NO { get; set; }


        public DateTime? RECEIPT_DATE_SARS { get; set; }


        [Required]
        public decimal LEVY_AMOUNT { get; set; }


        [Required]
        public decimal PENALTY_AMOUNT { get; set; }


        [Required]
        public decimal INTEREST_AMOUNT { get; set; }


        [Required]
        public decimal TOTAL_AMOUNT { get; set; }


        [Required]
        public decimal NO_SDL201_OUTSTANDING { get; set; }


        [Required]
        public decimal DEBT_OUTSTANDING_AMOUNT { get; set; }


        [Required]
        public decimal SARS_LEVY { get; set; }


        [Required]
        public decimal SARS_INTEREST { get; set; }


        [Required]
        public decimal SARS_PENALTY { get; set; }


        [Required]
        public decimal NSF_LEVY { get; set; }


        [Required]
        public decimal NSF_INTEREST { get; set; }


        [Required]
        public decimal NSF_PENALTY { get; set; }


        [Required]
        public decimal SETA_SETUP_LEVY { get; set; }


        [Required]
        public decimal SETA_SETUP_INTEREST { get; set; }


        [Required]
        public decimal SETA_SETUP_PENALTY { get; set; }


        [Required]
        public decimal SETA_ADMIN_LEVY { get; set; }


        [Required]
        public decimal SETA_ADMIN_INTEREST { get; set; }


        [Required]
        public decimal SETA_ADMIN_PENALTY { get; set; }


        [Required]
        public decimal UNAPPORTIONED_LEVY { get; set; }


        [Required]
        public decimal UNAPPORTIONED_INTEREST { get; set; }


        [Required]
        public decimal UNAPPORTIONED_PENALTY { get; set; }


        [Required]
        public decimal GRANT_A { get; set; }


        [Required]
        public decimal GRANT_B { get; set; }


        [Required]
        public decimal GRANT_C { get; set; }


        [Required]
        public decimal GRANT_D { get; set; }


        [Required]
        public decimal FINANCIAL_YEAR { get; set; }


        [StringLength(LEVY_PAYMENTSConsts.MaxLEVY_TYPELength, MinimumLength = LEVY_PAYMENTSConsts.MinLEVY_TYPELength)]
        public string LEVY_TYPE { get; set; }


        public decimal? SETA_CODE { get; set; }


        [StringLength(LEVY_PAYMENTSConsts.MaxGRANT_A_STATUSLength, MinimumLength = LEVY_PAYMENTSConsts.MinGRANT_A_STATUSLength)]
        public string GRANT_A_STATUS { get; set; }


        [StringLength(LEVY_PAYMENTSConsts.MaxGRANT_A_STATUS_COMMENTLength, MinimumLength = LEVY_PAYMENTSConsts.MinGRANT_A_STATUS_COMMENTLength)]
        public string GRANT_A_STATUS_COMMENT { get; set; }


        public DateTime? GRANT_A_DISBURSED_DATE { get; set; }


        [StringLength(LEVY_PAYMENTSConsts.MaxGRANT_A_DISBURSED_BYLength, MinimumLength = LEVY_PAYMENTSConsts.MinGRANT_A_DISBURSED_BYLength)]
        public string GRANT_A_DISBURSED_BY { get; set; }


        public decimal? GRANT_A_APPROVED { get; set; }


        public decimal? GRANT_A_DECLINED { get; set; }


        public decimal? GRANT_A_SWEPT { get; set; }


        [StringLength(LEVY_PAYMENTSConsts.MaxGRANT_B_STATUSLength, MinimumLength = LEVY_PAYMENTSConsts.MinGRANT_B_STATUSLength)]
        public string GRANT_B_STATUS { get; set; }


        [StringLength(LEVY_PAYMENTSConsts.MaxGRANT_B_STATUS_COMMENTLength, MinimumLength = LEVY_PAYMENTSConsts.MinGRANT_B_STATUS_COMMENTLength)]
        public string GRANT_B_STATUS_COMMENT { get; set; }


        public DateTime? GRANT_B_DISBURSED_DATE { get; set; }


        [StringLength(LEVY_PAYMENTSConsts.MaxGRANT_B_DISBURSED_BYLength, MinimumLength = LEVY_PAYMENTSConsts.MinGRANT_B_DISBURSED_BYLength)]
        public string GRANT_B_DISBURSED_BY { get; set; }


        public decimal? GRANT_B_APPROVED { get; set; }


        public decimal? GRANT_B_DECLINED { get; set; }


        public decimal? GRANT_B_SWEPT { get; set; }


        [StringLength(LEVY_PAYMENTSConsts.MaxGRANT_C_STATUSLength, MinimumLength = LEVY_PAYMENTSConsts.MinGRANT_C_STATUSLength)]
        public string GRANT_C_STATUS { get; set; }


        [StringLength(LEVY_PAYMENTSConsts.MaxGRANT_C_STATUS_COMMENTLength, MinimumLength = LEVY_PAYMENTSConsts.MinGRANT_C_STATUS_COMMENTLength)]
        public string GRANT_C_STATUS_COMMENT { get; set; }


        public DateTime? GRANT_C_DISBURSED_DATE { get; set; }


        [StringLength(LEVY_PAYMENTSConsts.MaxGRANT_C_DISBURSED_BYLength, MinimumLength = LEVY_PAYMENTSConsts.MinGRANT_C_DISBURSED_BYLength)]
        public string GRANT_C_DISBURSED_BY { get; set; }


        public decimal? GRANT_C_APPROVED { get; set; }


        public decimal? GRANT_C_DECLINED { get; set; }


        public decimal? GRANT_C_SWEPT { get; set; }


        [StringLength(LEVY_PAYMENTSConsts.MaxGRANT_D_STATUSLength, MinimumLength = LEVY_PAYMENTSConsts.MinGRANT_D_STATUSLength)]
        public string GRANT_D_STATUS { get; set; }


        [StringLength(LEVY_PAYMENTSConsts.MaxGRANT_D_STATUS_COMMENTLength, MinimumLength = LEVY_PAYMENTSConsts.MinGRANT_D_STATUS_COMMENTLength)]
        public string GRANT_D_STATUS_COMMENT { get; set; }


        public DateTime? GRANT_D_DISBURSED_DATE { get; set; }


        [StringLength(LEVY_PAYMENTSConsts.MaxGRANT_D_DISBURSED_BYLength, MinimumLength = LEVY_PAYMENTSConsts.MinGRANT_D_DISBURSED_BYLength)]
        public string GRANT_D_DISBURSED_BY { get; set; }


        public decimal? GRANT_D_APPROVED { get; set; }


        public decimal? GRANT_D_DECLINED { get; set; }


        public decimal? GRANT_D_SWEPT { get; set; }


        [StringLength(LEVY_PAYMENTSConsts.MaxGRANT_E_STATUSLength, MinimumLength = LEVY_PAYMENTSConsts.MinGRANT_E_STATUSLength)]
        public string GRANT_E_STATUS { get; set; }


        [StringLength(LEVY_PAYMENTSConsts.MaxGRANT_E_STATUS_COMMENTLength, MinimumLength = LEVY_PAYMENTSConsts.MinGRANT_E_STATUS_COMMENTLength)]
        public string GRANT_E_STATUS_COMMENT { get; set; }


        public DateTime? GRANT_E_DISBURSED_DATE { get; set; }


        [StringLength(LEVY_PAYMENTSConsts.MaxGRANT_E_DISBURSED_BYLength, MinimumLength = LEVY_PAYMENTSConsts.MinGRANT_E_DISBURSED_BYLength)]
        public string GRANT_E_DISBURSED_BY { get; set; }


        public decimal? GRANT_E_APPROVED { get; set; }


        public decimal? GRANT_E_DECLINED { get; set; }


        public decimal? GRANT_E_SWEPT { get; set; }


        public decimal? GRANT_A_OVERRIDDEN_AMOUNT_DIFF { get; set; }


        public decimal? GRANT_B_OVERRIDDEN_AMOUNT_DIFF { get; set; }


        public decimal? GRANT_C_OVERRIDDEN_AMOUNT_DIFF { get; set; }


        public decimal? GRANT_D_OVERRIDDEN_AMOUNT_DIFF { get; set; }


        public decimal? GRANT_E_OVERRIDDEN_AMOUNT_DIFF { get; set; }


        [StringLength(LEVY_PAYMENTSConsts.MaxPROOF_OF_PAYMENT_RECEIVEDLength, MinimumLength = LEVY_PAYMENTSConsts.MinPROOF_OF_PAYMENT_RECEIVEDLength)]
        public string PROOF_OF_PAYMENT_RECEIVED { get; set; }


        public decimal? TOTAL_GRANT_APPROVED { get; set; }


        [StringLength(LEVY_PAYMENTSConsts.MaxGRANT_A_PROCESSEDLength, MinimumLength = LEVY_PAYMENTSConsts.MinGRANT_A_PROCESSEDLength)]
        public string GRANT_A_PROCESSED { get; set; }


        [StringLength(LEVY_PAYMENTSConsts.MaxGRANT_B_PROCESSEDLength, MinimumLength = LEVY_PAYMENTSConsts.MinGRANT_B_PROCESSEDLength)]
        public string GRANT_B_PROCESSED { get; set; }


        [StringLength(LEVY_PAYMENTSConsts.MaxGRANT_C_PROCESSEDLength, MinimumLength = LEVY_PAYMENTSConsts.MinGRANT_C_PROCESSEDLength)]
        public string GRANT_C_PROCESSED { get; set; }


        [StringLength(LEVY_PAYMENTSConsts.MaxGRANT_D_PROCESSEDLength, MinimumLength = LEVY_PAYMENTSConsts.MinGRANT_D_PROCESSEDLength)]
        public string GRANT_D_PROCESSED { get; set; }


        [StringLength(LEVY_PAYMENTSConsts.MaxGRANT_E_PROCESSEDLength, MinimumLength = LEVY_PAYMENTSConsts.MinGRANT_E_PROCESSEDLength)]
        public string GRANT_E_PROCESSED { get; set; }


        [StringLength(LEVY_PAYMENTSConsts.MaxGRANT_A_PAYMENT_STATUSLength, MinimumLength = LEVY_PAYMENTSConsts.MinGRANT_A_PAYMENT_STATUSLength)]
        public string GRANT_A_PAYMENT_STATUS { get; set; }


        [StringLength(LEVY_PAYMENTSConsts.MaxGRANT_B_PAYMENT_STATUSLength, MinimumLength = LEVY_PAYMENTSConsts.MinGRANT_B_PAYMENT_STATUSLength)]
        public string GRANT_B_PAYMENT_STATUS { get; set; }


        [StringLength(LEVY_PAYMENTSConsts.MaxGRANT_C_PAYMENT_STATUSLength, MinimumLength = LEVY_PAYMENTSConsts.MinGRANT_C_PAYMENT_STATUSLength)]
        public string GRANT_C_PAYMENT_STATUS { get; set; }


        [StringLength(LEVY_PAYMENTSConsts.MaxGRANT_D_PAYMENT_STATUSLength, MinimumLength = LEVY_PAYMENTSConsts.MinGRANT_D_PAYMENT_STATUSLength)]
        public string GRANT_D_PAYMENT_STATUS { get; set; }


        [StringLength(LEVY_PAYMENTSConsts.MaxGRANT_E_PAYMENT_STATUSLength, MinimumLength = LEVY_PAYMENTSConsts.MinGRANT_E_PAYMENT_STATUSLength)]
        public string GRANT_E_PAYMENT_STATUS { get; set; }


        [StringLength(LEVY_PAYMENTSConsts.MaxGRANT_A_CHEQUE_EFT_NOLength, MinimumLength = LEVY_PAYMENTSConsts.MinGRANT_A_CHEQUE_EFT_NOLength)]
        public string GRANT_A_CHEQUE_EFT_NO { get; set; }


        [StringLength(LEVY_PAYMENTSConsts.MaxGRANT_B_CHEQUE_EFT_NOLength, MinimumLength = LEVY_PAYMENTSConsts.MinGRANT_B_CHEQUE_EFT_NOLength)]
        public string GRANT_B_CHEQUE_EFT_NO { get; set; }


        [StringLength(LEVY_PAYMENTSConsts.MaxGRANT_C_CHEQUE_EFT_NOLength, MinimumLength = LEVY_PAYMENTSConsts.MinGRANT_C_CHEQUE_EFT_NOLength)]
        public string GRANT_C_CHEQUE_EFT_NO { get; set; }


        [StringLength(LEVY_PAYMENTSConsts.MaxGRANT_D_CHEQUE_EFT_NOLength, MinimumLength = LEVY_PAYMENTSConsts.MinGRANT_D_CHEQUE_EFT_NOLength)]
        public string GRANT_D_CHEQUE_EFT_NO { get; set; }


        [StringLength(LEVY_PAYMENTSConsts.MaxGRANT_E_CHEQUE_EFT_NOLength, MinimumLength = LEVY_PAYMENTSConsts.MinGRANT_E_CHEQUE_EFT_NOLength)]
        public string GRANT_E_CHEQUE_EFT_NO { get; set; }


        public decimal? GRANT_E { get; set; }


        [StringLength(LEVY_PAYMENTSConsts.MaxGRANT_A_BATCH_NOLength, MinimumLength = LEVY_PAYMENTSConsts.MinGRANT_A_BATCH_NOLength)]
        public string GRANT_A_BATCH_NO { get; set; }


        [StringLength(LEVY_PAYMENTSConsts.MaxGRANT_B_BATCH_NOLength, MinimumLength = LEVY_PAYMENTSConsts.MinGRANT_B_BATCH_NOLength)]
        public string GRANT_B_BATCH_NO { get; set; }


        [StringLength(LEVY_PAYMENTSConsts.MaxGRANT_C_BATCH_NOLength, MinimumLength = LEVY_PAYMENTSConsts.MinGRANT_C_BATCH_NOLength)]
        public string GRANT_C_BATCH_NO { get; set; }


        [StringLength(LEVY_PAYMENTSConsts.MaxGRANT_D_BATCH_NOLength, MinimumLength = LEVY_PAYMENTSConsts.MinGRANT_D_BATCH_NOLength)]
        public string GRANT_D_BATCH_NO { get; set; }


        [StringLength(LEVY_PAYMENTSConsts.MaxGRANT_E_BATCH_NOLength, MinimumLength = LEVY_PAYMENTSConsts.MinGRANT_E_BATCH_NOLength)]
        public string GRANT_E_BATCH_NO { get; set; }


        [StringLength(LEVY_PAYMENTSConsts.MaxGRANT_B_USER_COMMENTLength, MinimumLength = LEVY_PAYMENTSConsts.MinGRANT_B_USER_COMMENTLength)]
        public string GRANT_B_USER_COMMENT { get; set; }


        [StringLength(LEVY_PAYMENTSConsts.MaxGRANT_C_USER_COMMENTLength, MinimumLength = LEVY_PAYMENTSConsts.MinGRANT_C_USER_COMMENTLength)]
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


        [StringLength(LEVY_PAYMENTSConsts.MaxGRANT_MG_STATUSLength, MinimumLength = LEVY_PAYMENTSConsts.MinGRANT_MG_STATUSLength)]
        public string GRANT_MG_STATUS { get; set; }


        [StringLength(LEVY_PAYMENTSConsts.MaxGRANT_MG_STATUS_COMMENTLength, MinimumLength = LEVY_PAYMENTSConsts.MinGRANT_MG_STATUS_COMMENTLength)]
        public string GRANT_MG_STATUS_COMMENT { get; set; }


        public DateTime? GRANT_MG_DISBURSED_DATE { get; set; }


        [StringLength(LEVY_PAYMENTSConsts.MaxGRANT_MG_DISBURSED_BYLength, MinimumLength = LEVY_PAYMENTSConsts.MinGRANT_MG_DISBURSED_BYLength)]
        public string GRANT_MG_DISBURSED_BY { get; set; }


        public decimal? GRANT_MG_APPROVED { get; set; }


        public decimal? GRANT_MG_DECLINED { get; set; }


        public decimal? GRANT_MG_SWEPT { get; set; }


        public decimal? GRANT_MG_OVERRIDDEN_AMT_DIFF { get; set; }


        [StringLength(LEVY_PAYMENTSConsts.MaxGRANT_MG_PROCESSEDLength, MinimumLength = LEVY_PAYMENTSConsts.MinGRANT_MG_PROCESSEDLength)]
        public string GRANT_MG_PROCESSED { get; set; }


        [StringLength(LEVY_PAYMENTSConsts.MaxGRANT_MG_PAYMENT_STATUSLength, MinimumLength = LEVY_PAYMENTSConsts.MinGRANT_MG_PAYMENT_STATUSLength)]
        public string GRANT_MG_PAYMENT_STATUS { get; set; }


        [StringLength(LEVY_PAYMENTSConsts.MaxGRANT_MG_CHEQUE_EFT_NOLength, MinimumLength = LEVY_PAYMENTSConsts.MinGRANT_MG_CHEQUE_EFT_NOLength)]
        public string GRANT_MG_CHEQUE_EFT_NO { get; set; }


        [StringLength(LEVY_PAYMENTSConsts.MaxGRANT_MG_BATCH_NOLength, MinimumLength = LEVY_PAYMENTSConsts.MinGRANT_MG_BATCH_NOLength)]
        public string GRANT_MG_BATCH_NO { get; set; }


        [StringLength(LEVY_PAYMENTSConsts.MaxGRANT_MG_USER_COMMENTLength, MinimumLength = LEVY_PAYMENTSConsts.MinGRANT_MG_USER_COMMENTLength)]
        public string GRANT_MG_USER_COMMENT { get; set; }


        [StringLength(LEVY_PAYMENTSConsts.MaxGRANT_DG_STATUSLength, MinimumLength = LEVY_PAYMENTSConsts.MinGRANT_DG_STATUSLength)]
        public string GRANT_DG_STATUS { get; set; }


        [StringLength(LEVY_PAYMENTSConsts.MaxGRANT_DG_STATUS_COMMENTLength, MinimumLength = LEVY_PAYMENTSConsts.MinGRANT_DG_STATUS_COMMENTLength)]
        public string GRANT_DG_STATUS_COMMENT { get; set; }


        public DateTime? GRANT_DG_DISBURSED_DATE { get; set; }


        [StringLength(LEVY_PAYMENTSConsts.MaxGRANT_DG_DISBURSED_BYLength, MinimumLength = LEVY_PAYMENTSConsts.MinGRANT_DG_DISBURSED_BYLength)]
        public string GRANT_DG_DISBURSED_BY { get; set; }


        public decimal? GRANT_DG_APPROVED { get; set; }


        public decimal? GRANT_DG_DECLINED { get; set; }


        public decimal? GRANT_DG_SWEPT { get; set; }


        public decimal? GRANT_DG_OVERRIDDEN_AMT_DIFF { get; set; }


        [StringLength(LEVY_PAYMENTSConsts.MaxGRANT_DG_PROCESSEDLength, MinimumLength = LEVY_PAYMENTSConsts.MinGRANT_DG_PROCESSEDLength)]
        public string GRANT_DG_PROCESSED { get; set; }


        [StringLength(LEVY_PAYMENTSConsts.MaxGRANT_DG_PAYMENT_STATUSLength, MinimumLength = LEVY_PAYMENTSConsts.MinGRANT_DG_PAYMENT_STATUSLength)]
        public string GRANT_DG_PAYMENT_STATUS { get; set; }


        [StringLength(LEVY_PAYMENTSConsts.MaxGRANT_DG_CHEQUE_EFT_NOLength, MinimumLength = LEVY_PAYMENTSConsts.MinGRANT_DG_CHEQUE_EFT_NOLength)]
        public string GRANT_DG_CHEQUE_EFT_NO { get; set; }


        [StringLength(LEVY_PAYMENTSConsts.MaxGRANT_DG_BATCH_NOLength, MinimumLength = LEVY_PAYMENTSConsts.MinGRANT_DG_BATCH_NOLength)]
        public string GRANT_DG_BATCH_NO { get; set; }


        [StringLength(LEVY_PAYMENTSConsts.MaxGRANT_DG_USER_COMMENTLength, MinimumLength = LEVY_PAYMENTSConsts.MinGRANT_DG_USER_COMMENTLength)]
        public string GRANT_DG_USER_COMMENT { get; set; }


        public string statusOne { get; set; }


        public string statusTwo { get; set; }



    }
}