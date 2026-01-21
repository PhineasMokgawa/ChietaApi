using Abp.Application.Services.Dto;
using System;

namespace CHIETAMIS.LEVYPAYMENTS.Dtos
{
    public class GetAllLEVY_PAYMENTSsForExcelInput
    {
        public string Filter { get; set; }

        public decimal? MaxPERIODFilter { get; set; }
        public decimal? MinPERIODFilter { get; set; }

        public string SDL_NOFilter { get; set; }

        public DateTime? MaxRECEIPT_DATE_SARSFilter { get; set; }
        public DateTime? MinRECEIPT_DATE_SARSFilter { get; set; }

        public decimal? MaxLEVY_AMOUNTFilter { get; set; }
        public decimal? MinLEVY_AMOUNTFilter { get; set; }

        public decimal? MaxPENALTY_AMOUNTFilter { get; set; }
        public decimal? MinPENALTY_AMOUNTFilter { get; set; }

        public decimal? MaxINTEREST_AMOUNTFilter { get; set; }
        public decimal? MinINTEREST_AMOUNTFilter { get; set; }

        public decimal? MaxTOTAL_AMOUNTFilter { get; set; }
        public decimal? MinTOTAL_AMOUNTFilter { get; set; }

        public decimal? MaxNO_SDL201_OUTSTANDINGFilter { get; set; }
        public decimal? MinNO_SDL201_OUTSTANDINGFilter { get; set; }

        public decimal? MaxDEBT_OUTSTANDING_AMOUNTFilter { get; set; }
        public decimal? MinDEBT_OUTSTANDING_AMOUNTFilter { get; set; }

        public decimal? MaxSARS_LEVYFilter { get; set; }
        public decimal? MinSARS_LEVYFilter { get; set; }

        public decimal? MaxSARS_INTERESTFilter { get; set; }
        public decimal? MinSARS_INTERESTFilter { get; set; }

        public decimal? MaxSARS_PENALTYFilter { get; set; }
        public decimal? MinSARS_PENALTYFilter { get; set; }

        public decimal? MaxNSF_LEVYFilter { get; set; }
        public decimal? MinNSF_LEVYFilter { get; set; }

        public decimal? MaxNSF_INTERESTFilter { get; set; }
        public decimal? MinNSF_INTERESTFilter { get; set; }

        public decimal? MaxNSF_PENALTYFilter { get; set; }
        public decimal? MinNSF_PENALTYFilter { get; set; }

        public decimal? MaxSETA_SETUP_LEVYFilter { get; set; }
        public decimal? MinSETA_SETUP_LEVYFilter { get; set; }

        public decimal? MaxSETA_SETUP_INTERESTFilter { get; set; }
        public decimal? MinSETA_SETUP_INTERESTFilter { get; set; }

        public decimal? MaxSETA_SETUP_PENALTYFilter { get; set; }
        public decimal? MinSETA_SETUP_PENALTYFilter { get; set; }

        public decimal? MaxSETA_ADMIN_LEVYFilter { get; set; }
        public decimal? MinSETA_ADMIN_LEVYFilter { get; set; }

        public decimal? MaxSETA_ADMIN_INTERESTFilter { get; set; }
        public decimal? MinSETA_ADMIN_INTERESTFilter { get; set; }

        public decimal? MaxSETA_ADMIN_PENALTYFilter { get; set; }
        public decimal? MinSETA_ADMIN_PENALTYFilter { get; set; }

        public decimal? MaxUNAPPORTIONED_LEVYFilter { get; set; }
        public decimal? MinUNAPPORTIONED_LEVYFilter { get; set; }

        public decimal? MaxUNAPPORTIONED_INTERESTFilter { get; set; }
        public decimal? MinUNAPPORTIONED_INTERESTFilter { get; set; }

        public decimal? MaxUNAPPORTIONED_PENALTYFilter { get; set; }
        public decimal? MinUNAPPORTIONED_PENALTYFilter { get; set; }

        public decimal? MaxGRANT_AFilter { get; set; }
        public decimal? MinGRANT_AFilter { get; set; }

        public decimal? MaxGRANT_BFilter { get; set; }
        public decimal? MinGRANT_BFilter { get; set; }

        public decimal? MaxGRANT_CFilter { get; set; }
        public decimal? MinGRANT_CFilter { get; set; }

        public decimal? MaxGRANT_DFilter { get; set; }
        public decimal? MinGRANT_DFilter { get; set; }

        public decimal? MaxFINANCIAL_YEARFilter { get; set; }
        public decimal? MinFINANCIAL_YEARFilter { get; set; }

        public string LEVY_TYPEFilter { get; set; }

        public decimal? MaxSETA_CODEFilter { get; set; }
        public decimal? MinSETA_CODEFilter { get; set; }

        public string GRANT_A_STATUSFilter { get; set; }

        public string GRANT_A_STATUS_COMMENTFilter { get; set; }

        public DateTime? MaxGRANT_A_DISBURSED_DATEFilter { get; set; }
        public DateTime? MinGRANT_A_DISBURSED_DATEFilter { get; set; }

        public string GRANT_A_DISBURSED_BYFilter { get; set; }

        public decimal? MaxGRANT_A_APPROVEDFilter { get; set; }
        public decimal? MinGRANT_A_APPROVEDFilter { get; set; }

        public decimal? MaxGRANT_A_DECLINEDFilter { get; set; }
        public decimal? MinGRANT_A_DECLINEDFilter { get; set; }

        public decimal? MaxGRANT_A_SWEPTFilter { get; set; }
        public decimal? MinGRANT_A_SWEPTFilter { get; set; }

        public string GRANT_B_STATUSFilter { get; set; }

        public string GRANT_B_STATUS_COMMENTFilter { get; set; }

        public DateTime? MaxGRANT_B_DISBURSED_DATEFilter { get; set; }
        public DateTime? MinGRANT_B_DISBURSED_DATEFilter { get; set; }

        public string GRANT_B_DISBURSED_BYFilter { get; set; }

        public decimal? MaxGRANT_B_APPROVEDFilter { get; set; }
        public decimal? MinGRANT_B_APPROVEDFilter { get; set; }

        public decimal? MaxGRANT_B_DECLINEDFilter { get; set; }
        public decimal? MinGRANT_B_DECLINEDFilter { get; set; }

        public decimal? MaxGRANT_B_SWEPTFilter { get; set; }
        public decimal? MinGRANT_B_SWEPTFilter { get; set; }

        public string GRANT_C_STATUSFilter { get; set; }

        public string GRANT_C_STATUS_COMMENTFilter { get; set; }

        public DateTime? MaxGRANT_C_DISBURSED_DATEFilter { get; set; }
        public DateTime? MinGRANT_C_DISBURSED_DATEFilter { get; set; }

        public string GRANT_C_DISBURSED_BYFilter { get; set; }

        public decimal? MaxGRANT_C_APPROVEDFilter { get; set; }
        public decimal? MinGRANT_C_APPROVEDFilter { get; set; }

        public decimal? MaxGRANT_C_DECLINEDFilter { get; set; }
        public decimal? MinGRANT_C_DECLINEDFilter { get; set; }

        public decimal? MaxGRANT_C_SWEPTFilter { get; set; }
        public decimal? MinGRANT_C_SWEPTFilter { get; set; }

        public string GRANT_D_STATUSFilter { get; set; }

        public string GRANT_D_STATUS_COMMENTFilter { get; set; }

        public DateTime? MaxGRANT_D_DISBURSED_DATEFilter { get; set; }
        public DateTime? MinGRANT_D_DISBURSED_DATEFilter { get; set; }

        public string GRANT_D_DISBURSED_BYFilter { get; set; }

        public decimal? MaxGRANT_D_APPROVEDFilter { get; set; }
        public decimal? MinGRANT_D_APPROVEDFilter { get; set; }

        public decimal? MaxGRANT_D_DECLINEDFilter { get; set; }
        public decimal? MinGRANT_D_DECLINEDFilter { get; set; }

        public decimal? MaxGRANT_D_SWEPTFilter { get; set; }
        public decimal? MinGRANT_D_SWEPTFilter { get; set; }

        public string GRANT_E_STATUSFilter { get; set; }

        public string GRANT_E_STATUS_COMMENTFilter { get; set; }

        public DateTime? MaxGRANT_E_DISBURSED_DATEFilter { get; set; }
        public DateTime? MinGRANT_E_DISBURSED_DATEFilter { get; set; }

        public string GRANT_E_DISBURSED_BYFilter { get; set; }

        public decimal? MaxGRANT_E_APPROVEDFilter { get; set; }
        public decimal? MinGRANT_E_APPROVEDFilter { get; set; }

        public decimal? MaxGRANT_E_DECLINEDFilter { get; set; }
        public decimal? MinGRANT_E_DECLINEDFilter { get; set; }

        public decimal? MaxGRANT_E_SWEPTFilter { get; set; }
        public decimal? MinGRANT_E_SWEPTFilter { get; set; }

        public decimal? MaxGRANT_A_OVERRIDDEN_AMOUNT_DIFFFilter { get; set; }
        public decimal? MinGRANT_A_OVERRIDDEN_AMOUNT_DIFFFilter { get; set; }

        public decimal? MaxGRANT_B_OVERRIDDEN_AMOUNT_DIFFFilter { get; set; }
        public decimal? MinGRANT_B_OVERRIDDEN_AMOUNT_DIFFFilter { get; set; }

        public decimal? MaxGRANT_C_OVERRIDDEN_AMOUNT_DIFFFilter { get; set; }
        public decimal? MinGRANT_C_OVERRIDDEN_AMOUNT_DIFFFilter { get; set; }

        public decimal? MaxGRANT_D_OVERRIDDEN_AMOUNT_DIFFFilter { get; set; }
        public decimal? MinGRANT_D_OVERRIDDEN_AMOUNT_DIFFFilter { get; set; }

        public decimal? MaxGRANT_E_OVERRIDDEN_AMOUNT_DIFFFilter { get; set; }
        public decimal? MinGRANT_E_OVERRIDDEN_AMOUNT_DIFFFilter { get; set; }

        public string PROOF_OF_PAYMENT_RECEIVEDFilter { get; set; }

        public decimal? MaxTOTAL_GRANT_APPROVEDFilter { get; set; }
        public decimal? MinTOTAL_GRANT_APPROVEDFilter { get; set; }

        public string GRANT_A_PROCESSEDFilter { get; set; }

        public string GRANT_B_PROCESSEDFilter { get; set; }

        public string GRANT_C_PROCESSEDFilter { get; set; }

        public string GRANT_D_PROCESSEDFilter { get; set; }

        public string GRANT_E_PROCESSEDFilter { get; set; }

        public string GRANT_A_PAYMENT_STATUSFilter { get; set; }

        public string GRANT_B_PAYMENT_STATUSFilter { get; set; }

        public string GRANT_C_PAYMENT_STATUSFilter { get; set; }

        public string GRANT_D_PAYMENT_STATUSFilter { get; set; }

        public string GRANT_E_PAYMENT_STATUSFilter { get; set; }

        public string GRANT_A_CHEQUE_EFT_NOFilter { get; set; }

        public string GRANT_B_CHEQUE_EFT_NOFilter { get; set; }

        public string GRANT_C_CHEQUE_EFT_NOFilter { get; set; }

        public string GRANT_D_CHEQUE_EFT_NOFilter { get; set; }

        public string GRANT_E_CHEQUE_EFT_NOFilter { get; set; }

        public decimal? MaxGRANT_EFilter { get; set; }
        public decimal? MinGRANT_EFilter { get; set; }

        public string GRANT_A_BATCH_NOFilter { get; set; }

        public string GRANT_B_BATCH_NOFilter { get; set; }

        public string GRANT_C_BATCH_NOFilter { get; set; }

        public string GRANT_D_BATCH_NOFilter { get; set; }

        public string GRANT_E_BATCH_NOFilter { get; set; }

        public string GRANT_B_USER_COMMENTFilter { get; set; }

        public string GRANT_C_USER_COMMENTFilter { get; set; }

        public decimal? MaxLEVY_AMOUNT_RECEIVEDFilter { get; set; }
        public decimal? MinLEVY_AMOUNT_RECEIVEDFilter { get; set; }

        public decimal? MaxINTEREST_AMOUNT_RECEIVEDFilter { get; set; }
        public decimal? MinINTEREST_AMOUNT_RECEIVEDFilter { get; set; }

        public decimal? MaxPENALTY_AMOUNT_RECEIVEDFilter { get; set; }
        public decimal? MinPENALTY_AMOUNT_RECEIVEDFilter { get; set; }

        public decimal? MaxTOTAL_AMOUNT_RECEIVEDFilter { get; set; }
        public decimal? MinTOTAL_AMOUNT_RECEIVEDFilter { get; set; }

        public decimal? MaxSETA_COMPLETE_ADMIN_LEVYFilter { get; set; }
        public decimal? MinSETA_COMPLETE_ADMIN_LEVYFilter { get; set; }

        public decimal? MaxSETA_COMPLETE_ADMIN_INTERESTFilter { get; set; }
        public decimal? MinSETA_COMPLETE_ADMIN_INTERESTFilter { get; set; }

        public decimal? MaxSETA_COMPLETE_ADMIN_PENALTYFilter { get; set; }
        public decimal? MinSETA_COMPLETE_ADMIN_PENALTYFilter { get; set; }

        public decimal? MaxSETA_COMPLETE_ADMIN_TOTALFilter { get; set; }
        public decimal? MinSETA_COMPLETE_ADMIN_TOTALFilter { get; set; }

        public decimal? MaxGRANT_MGFilter { get; set; }
        public decimal? MinGRANT_MGFilter { get; set; }

        public decimal? MaxGRANT_DGFilter { get; set; }
        public decimal? MinGRANT_DGFilter { get; set; }

        public string GRANT_MG_STATUSFilter { get; set; }

        public string GRANT_MG_STATUS_COMMENTFilter { get; set; }

        public DateTime? MaxGRANT_MG_DISBURSED_DATEFilter { get; set; }
        public DateTime? MinGRANT_MG_DISBURSED_DATEFilter { get; set; }

        public string GRANT_MG_DISBURSED_BYFilter { get; set; }

        public decimal? MaxGRANT_MG_APPROVEDFilter { get; set; }
        public decimal? MinGRANT_MG_APPROVEDFilter { get; set; }

        public decimal? MaxGRANT_MG_DECLINEDFilter { get; set; }
        public decimal? MinGRANT_MG_DECLINEDFilter { get; set; }

        public decimal? MaxGRANT_MG_SWEPTFilter { get; set; }
        public decimal? MinGRANT_MG_SWEPTFilter { get; set; }

        public decimal? MaxGRANT_MG_OVERRIDDEN_AMT_DIFFFilter { get; set; }
        public decimal? MinGRANT_MG_OVERRIDDEN_AMT_DIFFFilter { get; set; }

        public string GRANT_MG_PROCESSEDFilter { get; set; }

        public string GRANT_MG_PAYMENT_STATUSFilter { get; set; }

        public string GRANT_MG_CHEQUE_EFT_NOFilter { get; set; }

        public string GRANT_MG_BATCH_NOFilter { get; set; }

        public string GRANT_MG_USER_COMMENTFilter { get; set; }

        public string GRANT_DG_STATUSFilter { get; set; }

        public string GRANT_DG_STATUS_COMMENTFilter { get; set; }

        public DateTime? MaxGRANT_DG_DISBURSED_DATEFilter { get; set; }
        public DateTime? MinGRANT_DG_DISBURSED_DATEFilter { get; set; }

        public string GRANT_DG_DISBURSED_BYFilter { get; set; }

        public decimal? MaxGRANT_DG_APPROVEDFilter { get; set; }
        public decimal? MinGRANT_DG_APPROVEDFilter { get; set; }

        public decimal? MaxGRANT_DG_DECLINEDFilter { get; set; }
        public decimal? MinGRANT_DG_DECLINEDFilter { get; set; }

        public decimal? MaxGRANT_DG_SWEPTFilter { get; set; }
        public decimal? MinGRANT_DG_SWEPTFilter { get; set; }

        public decimal? MaxGRANT_DG_OVERRIDDEN_AMT_DIFFFilter { get; set; }
        public decimal? MinGRANT_DG_OVERRIDDEN_AMT_DIFFFilter { get; set; }

        public string GRANT_DG_PROCESSEDFilter { get; set; }

        public string GRANT_DG_PAYMENT_STATUSFilter { get; set; }

        public string GRANT_DG_CHEQUE_EFT_NOFilter { get; set; }

        public string GRANT_DG_BATCH_NOFilter { get; set; }

        public string GRANT_DG_USER_COMMENTFilter { get; set; }

        public string statusOneFilter { get; set; }

        public string statusTwoFilter { get; set; }



    }
}