

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using CHIETAMIS.LEVYPAYMENTS.Exporting;
using CHIETAMIS.LEVYPAYMENTS.Dtos;
using CHIETAMIS.Dto;
using Abp.Application.Services.Dto;
using CHIETAMIS.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.Runtime.Session;
using AutoMapper;
using CHEITAMIS.Dto;
using CHIETAMIS.LEVYPAYMENTS;
using CHIETAMIS.LEVYPAYMENTS.Dtos;
using CHIETAMIS.LEVYPAYMENTS.Exporting;
using CHIETAMIS;

namespace CHIETAMIS.LEVYPAYMENTS
{
    [AbpAuthorize(AppPermissions.Pages_LEVY_PAYMENTSs)]
    public class LEVY_PAYMENTSsAppService : CHIETAMISAppServiceBase, ILEVY_PAYMENTSsAppService
    {
        private readonly IRepository<LEVY_PAYMENTS> _levY_PAYMENTSRepository;
        private readonly ILEVY_PAYMENTSsExcelExporter _levY_PAYMENTSsExcelExporter;

        public LEVY_PAYMENTSsAppService(IRepository<LEVY_PAYMENTS> levY_PAYMENTSRepository, ILEVY_PAYMENTSsExcelExporter levY_PAYMENTSsExcelExporter)
        {
            _levY_PAYMENTSRepository = levY_PAYMENTSRepository;
            _levY_PAYMENTSsExcelExporter = levY_PAYMENTSsExcelExporter;

        }

        public async Task<PagedResultDto<GetLEVY_PAYMENTSForViewDto>> GetAll(GetAllLEVY_PAYMENTSsInput input)
        {

            var filteredLEVY_PAYMENTSs = _levY_PAYMENTSRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.SDL_NO.Contains(input.Filter) || e.LEVY_TYPE.Contains(input.Filter) || e.GRANT_A_STATUS.Contains(input.Filter) || e.GRANT_A_STATUS_COMMENT.Contains(input.Filter) || e.GRANT_A_DISBURSED_BY.Contains(input.Filter) || e.GRANT_B_STATUS.Contains(input.Filter) || e.GRANT_B_STATUS_COMMENT.Contains(input.Filter) || e.GRANT_B_DISBURSED_BY.Contains(input.Filter) || e.GRANT_C_STATUS.Contains(input.Filter) || e.GRANT_C_STATUS_COMMENT.Contains(input.Filter) || e.GRANT_C_DISBURSED_BY.Contains(input.Filter) || e.GRANT_D_STATUS.Contains(input.Filter) || e.GRANT_D_STATUS_COMMENT.Contains(input.Filter) || e.GRANT_D_DISBURSED_BY.Contains(input.Filter) || e.GRANT_E_STATUS.Contains(input.Filter) || e.GRANT_E_STATUS_COMMENT.Contains(input.Filter) || e.GRANT_E_DISBURSED_BY.Contains(input.Filter) || e.PROOF_OF_PAYMENT_RECEIVED.Contains(input.Filter) || e.GRANT_A_PROCESSED.Contains(input.Filter) || e.GRANT_B_PROCESSED.Contains(input.Filter) || e.GRANT_C_PROCESSED.Contains(input.Filter) || e.GRANT_D_PROCESSED.Contains(input.Filter) || e.GRANT_E_PROCESSED.Contains(input.Filter) || e.GRANT_A_PAYMENT_STATUS.Contains(input.Filter) || e.GRANT_B_PAYMENT_STATUS.Contains(input.Filter) || e.GRANT_C_PAYMENT_STATUS.Contains(input.Filter) || e.GRANT_D_PAYMENT_STATUS.Contains(input.Filter) || e.GRANT_E_PAYMENT_STATUS.Contains(input.Filter) || e.GRANT_A_CHEQUE_EFT_NO.Contains(input.Filter) || e.GRANT_B_CHEQUE_EFT_NO.Contains(input.Filter) || e.GRANT_C_CHEQUE_EFT_NO.Contains(input.Filter) || e.GRANT_D_CHEQUE_EFT_NO.Contains(input.Filter) || e.GRANT_E_CHEQUE_EFT_NO.Contains(input.Filter) || e.GRANT_A_BATCH_NO.Contains(input.Filter) || e.GRANT_B_BATCH_NO.Contains(input.Filter) || e.GRANT_C_BATCH_NO.Contains(input.Filter) || e.GRANT_D_BATCH_NO.Contains(input.Filter) || e.GRANT_E_BATCH_NO.Contains(input.Filter) || e.GRANT_B_USER_COMMENT.Contains(input.Filter) || e.GRANT_C_USER_COMMENT.Contains(input.Filter) || e.GRANT_MG_STATUS.Contains(input.Filter) || e.GRANT_MG_STATUS_COMMENT.Contains(input.Filter) || e.GRANT_MG_DISBURSED_BY.Contains(input.Filter) || e.GRANT_MG_PROCESSED.Contains(input.Filter) || e.GRANT_MG_PAYMENT_STATUS.Contains(input.Filter) || e.GRANT_MG_CHEQUE_EFT_NO.Contains(input.Filter) || e.GRANT_MG_BATCH_NO.Contains(input.Filter) || e.GRANT_MG_USER_COMMENT.Contains(input.Filter) || e.GRANT_DG_STATUS.Contains(input.Filter) || e.GRANT_DG_STATUS_COMMENT.Contains(input.Filter) || e.GRANT_DG_DISBURSED_BY.Contains(input.Filter) || e.GRANT_DG_PROCESSED.Contains(input.Filter) || e.GRANT_DG_PAYMENT_STATUS.Contains(input.Filter) || e.GRANT_DG_CHEQUE_EFT_NO.Contains(input.Filter) || e.GRANT_DG_BATCH_NO.Contains(input.Filter) || e.GRANT_DG_USER_COMMENT.Contains(input.Filter) || e.statusOne.Contains(input.Filter) || e.statusTwo.Contains(input.Filter))
                        .WhereIf(input.MinPERIODFilter != null, e => e.PERIOD >= input.MinPERIODFilter)
                        .WhereIf(input.MaxPERIODFilter != null, e => e.PERIOD <= input.MaxPERIODFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.SDL_NOFilter), e => e.SDL_NO == input.SDL_NOFilter)
                        .WhereIf(input.MinRECEIPT_DATE_SARSFilter != null, e => e.RECEIPT_DATE_SARS >= input.MinRECEIPT_DATE_SARSFilter)
                        .WhereIf(input.MaxRECEIPT_DATE_SARSFilter != null, e => e.RECEIPT_DATE_SARS <= input.MaxRECEIPT_DATE_SARSFilter)
                        .WhereIf(input.MinLEVY_AMOUNTFilter != null, e => e.LEVY_AMOUNT >= input.MinLEVY_AMOUNTFilter)
                        .WhereIf(input.MaxLEVY_AMOUNTFilter != null, e => e.LEVY_AMOUNT <= input.MaxLEVY_AMOUNTFilter)
                        .WhereIf(input.MinPENALTY_AMOUNTFilter != null, e => e.PENALTY_AMOUNT >= input.MinPENALTY_AMOUNTFilter)
                        .WhereIf(input.MaxPENALTY_AMOUNTFilter != null, e => e.PENALTY_AMOUNT <= input.MaxPENALTY_AMOUNTFilter)
                        .WhereIf(input.MinINTEREST_AMOUNTFilter != null, e => e.INTEREST_AMOUNT >= input.MinINTEREST_AMOUNTFilter)
                        .WhereIf(input.MaxINTEREST_AMOUNTFilter != null, e => e.INTEREST_AMOUNT <= input.MaxINTEREST_AMOUNTFilter)
                        .WhereIf(input.MinTOTAL_AMOUNTFilter != null, e => e.TOTAL_AMOUNT >= input.MinTOTAL_AMOUNTFilter)
                        .WhereIf(input.MaxTOTAL_AMOUNTFilter != null, e => e.TOTAL_AMOUNT <= input.MaxTOTAL_AMOUNTFilter)
                        .WhereIf(input.MinNO_SDL201_OUTSTANDINGFilter != null, e => e.NO_SDL201_OUTSTANDING >= input.MinNO_SDL201_OUTSTANDINGFilter)
                        .WhereIf(input.MaxNO_SDL201_OUTSTANDINGFilter != null, e => e.NO_SDL201_OUTSTANDING <= input.MaxNO_SDL201_OUTSTANDINGFilter)
                        .WhereIf(input.MinDEBT_OUTSTANDING_AMOUNTFilter != null, e => e.DEBT_OUTSTANDING_AMOUNT >= input.MinDEBT_OUTSTANDING_AMOUNTFilter)
                        .WhereIf(input.MaxDEBT_OUTSTANDING_AMOUNTFilter != null, e => e.DEBT_OUTSTANDING_AMOUNT <= input.MaxDEBT_OUTSTANDING_AMOUNTFilter)
                        .WhereIf(input.MinSARS_LEVYFilter != null, e => e.SARS_LEVY >= input.MinSARS_LEVYFilter)
                        .WhereIf(input.MaxSARS_LEVYFilter != null, e => e.SARS_LEVY <= input.MaxSARS_LEVYFilter)
                        .WhereIf(input.MinSARS_INTERESTFilter != null, e => e.SARS_INTEREST >= input.MinSARS_INTERESTFilter)
                        .WhereIf(input.MaxSARS_INTERESTFilter != null, e => e.SARS_INTEREST <= input.MaxSARS_INTERESTFilter)
                        .WhereIf(input.MinSARS_PENALTYFilter != null, e => e.SARS_PENALTY >= input.MinSARS_PENALTYFilter)
                        .WhereIf(input.MaxSARS_PENALTYFilter != null, e => e.SARS_PENALTY <= input.MaxSARS_PENALTYFilter)
                        .WhereIf(input.MinNSF_LEVYFilter != null, e => e.NSF_LEVY >= input.MinNSF_LEVYFilter)
                        .WhereIf(input.MaxNSF_LEVYFilter != null, e => e.NSF_LEVY <= input.MaxNSF_LEVYFilter)
                        .WhereIf(input.MinNSF_INTERESTFilter != null, e => e.NSF_INTEREST >= input.MinNSF_INTERESTFilter)
                        .WhereIf(input.MaxNSF_INTERESTFilter != null, e => e.NSF_INTEREST <= input.MaxNSF_INTERESTFilter)
                        .WhereIf(input.MinNSF_PENALTYFilter != null, e => e.NSF_PENALTY >= input.MinNSF_PENALTYFilter)
                        .WhereIf(input.MaxNSF_PENALTYFilter != null, e => e.NSF_PENALTY <= input.MaxNSF_PENALTYFilter)
                        .WhereIf(input.MinSETA_SETUP_LEVYFilter != null, e => e.SETA_SETUP_LEVY >= input.MinSETA_SETUP_LEVYFilter)
                        .WhereIf(input.MaxSETA_SETUP_LEVYFilter != null, e => e.SETA_SETUP_LEVY <= input.MaxSETA_SETUP_LEVYFilter)
                        .WhereIf(input.MinSETA_SETUP_INTERESTFilter != null, e => e.SETA_SETUP_INTEREST >= input.MinSETA_SETUP_INTERESTFilter)
                        .WhereIf(input.MaxSETA_SETUP_INTERESTFilter != null, e => e.SETA_SETUP_INTEREST <= input.MaxSETA_SETUP_INTERESTFilter)
                        .WhereIf(input.MinSETA_SETUP_PENALTYFilter != null, e => e.SETA_SETUP_PENALTY >= input.MinSETA_SETUP_PENALTYFilter)
                        .WhereIf(input.MaxSETA_SETUP_PENALTYFilter != null, e => e.SETA_SETUP_PENALTY <= input.MaxSETA_SETUP_PENALTYFilter)
                        .WhereIf(input.MinSETA_ADMIN_LEVYFilter != null, e => e.SETA_ADMIN_LEVY >= input.MinSETA_ADMIN_LEVYFilter)
                        .WhereIf(input.MaxSETA_ADMIN_LEVYFilter != null, e => e.SETA_ADMIN_LEVY <= input.MaxSETA_ADMIN_LEVYFilter)
                        .WhereIf(input.MinSETA_ADMIN_INTERESTFilter != null, e => e.SETA_ADMIN_INTEREST >= input.MinSETA_ADMIN_INTERESTFilter)
                        .WhereIf(input.MaxSETA_ADMIN_INTERESTFilter != null, e => e.SETA_ADMIN_INTEREST <= input.MaxSETA_ADMIN_INTERESTFilter)
                        .WhereIf(input.MinSETA_ADMIN_PENALTYFilter != null, e => e.SETA_ADMIN_PENALTY >= input.MinSETA_ADMIN_PENALTYFilter)
                        .WhereIf(input.MaxSETA_ADMIN_PENALTYFilter != null, e => e.SETA_ADMIN_PENALTY <= input.MaxSETA_ADMIN_PENALTYFilter)
                        .WhereIf(input.MinUNAPPORTIONED_LEVYFilter != null, e => e.UNAPPORTIONED_LEVY >= input.MinUNAPPORTIONED_LEVYFilter)
                        .WhereIf(input.MaxUNAPPORTIONED_LEVYFilter != null, e => e.UNAPPORTIONED_LEVY <= input.MaxUNAPPORTIONED_LEVYFilter)
                        .WhereIf(input.MinUNAPPORTIONED_INTERESTFilter != null, e => e.UNAPPORTIONED_INTEREST >= input.MinUNAPPORTIONED_INTERESTFilter)
                        .WhereIf(input.MaxUNAPPORTIONED_INTERESTFilter != null, e => e.UNAPPORTIONED_INTEREST <= input.MaxUNAPPORTIONED_INTERESTFilter)
                        .WhereIf(input.MinUNAPPORTIONED_PENALTYFilter != null, e => e.UNAPPORTIONED_PENALTY >= input.MinUNAPPORTIONED_PENALTYFilter)
                        .WhereIf(input.MaxUNAPPORTIONED_PENALTYFilter != null, e => e.UNAPPORTIONED_PENALTY <= input.MaxUNAPPORTIONED_PENALTYFilter)
                        .WhereIf(input.MinGRANT_AFilter != null, e => e.GRANT_A >= input.MinGRANT_AFilter)
                        .WhereIf(input.MaxGRANT_AFilter != null, e => e.GRANT_A <= input.MaxGRANT_AFilter)
                        .WhereIf(input.MinGRANT_BFilter != null, e => e.GRANT_B >= input.MinGRANT_BFilter)
                        .WhereIf(input.MaxGRANT_BFilter != null, e => e.GRANT_B <= input.MaxGRANT_BFilter)
                        .WhereIf(input.MinGRANT_CFilter != null, e => e.GRANT_C >= input.MinGRANT_CFilter)
                        .WhereIf(input.MaxGRANT_CFilter != null, e => e.GRANT_C <= input.MaxGRANT_CFilter)
                        .WhereIf(input.MinGRANT_DFilter != null, e => e.GRANT_D >= input.MinGRANT_DFilter)
                        .WhereIf(input.MaxGRANT_DFilter != null, e => e.GRANT_D <= input.MaxGRANT_DFilter)
                        .WhereIf(input.MinFINANCIAL_YEARFilter != null, e => e.FINANCIAL_YEAR >= input.MinFINANCIAL_YEARFilter)
                        .WhereIf(input.MaxFINANCIAL_YEARFilter != null, e => e.FINANCIAL_YEAR <= input.MaxFINANCIAL_YEARFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.LEVY_TYPEFilter), e => e.LEVY_TYPE == input.LEVY_TYPEFilter)
                        .WhereIf(input.MinSETA_CODEFilter != null, e => e.SETA_CODE >= input.MinSETA_CODEFilter)
                        .WhereIf(input.MaxSETA_CODEFilter != null, e => e.SETA_CODE <= input.MaxSETA_CODEFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_A_STATUSFilter), e => e.GRANT_A_STATUS == input.GRANT_A_STATUSFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_A_STATUS_COMMENTFilter), e => e.GRANT_A_STATUS_COMMENT == input.GRANT_A_STATUS_COMMENTFilter)
                        .WhereIf(input.MinGRANT_A_DISBURSED_DATEFilter != null, e => e.GRANT_A_DISBURSED_DATE >= input.MinGRANT_A_DISBURSED_DATEFilter)
                        .WhereIf(input.MaxGRANT_A_DISBURSED_DATEFilter != null, e => e.GRANT_A_DISBURSED_DATE <= input.MaxGRANT_A_DISBURSED_DATEFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_A_DISBURSED_BYFilter), e => e.GRANT_A_DISBURSED_BY == input.GRANT_A_DISBURSED_BYFilter)
                        .WhereIf(input.MinGRANT_A_APPROVEDFilter != null, e => e.GRANT_A_APPROVED >= input.MinGRANT_A_APPROVEDFilter)
                        .WhereIf(input.MaxGRANT_A_APPROVEDFilter != null, e => e.GRANT_A_APPROVED <= input.MaxGRANT_A_APPROVEDFilter)
                        .WhereIf(input.MinGRANT_A_DECLINEDFilter != null, e => e.GRANT_A_DECLINED >= input.MinGRANT_A_DECLINEDFilter)
                        .WhereIf(input.MaxGRANT_A_DECLINEDFilter != null, e => e.GRANT_A_DECLINED <= input.MaxGRANT_A_DECLINEDFilter)
                        .WhereIf(input.MinGRANT_A_SWEPTFilter != null, e => e.GRANT_A_SWEPT >= input.MinGRANT_A_SWEPTFilter)
                        .WhereIf(input.MaxGRANT_A_SWEPTFilter != null, e => e.GRANT_A_SWEPT <= input.MaxGRANT_A_SWEPTFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_B_STATUSFilter), e => e.GRANT_B_STATUS == input.GRANT_B_STATUSFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_B_STATUS_COMMENTFilter), e => e.GRANT_B_STATUS_COMMENT == input.GRANT_B_STATUS_COMMENTFilter)
                        .WhereIf(input.MinGRANT_B_DISBURSED_DATEFilter != null, e => e.GRANT_B_DISBURSED_DATE >= input.MinGRANT_B_DISBURSED_DATEFilter)
                        .WhereIf(input.MaxGRANT_B_DISBURSED_DATEFilter != null, e => e.GRANT_B_DISBURSED_DATE <= input.MaxGRANT_B_DISBURSED_DATEFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_B_DISBURSED_BYFilter), e => e.GRANT_B_DISBURSED_BY == input.GRANT_B_DISBURSED_BYFilter)
                        .WhereIf(input.MinGRANT_B_APPROVEDFilter != null, e => e.GRANT_B_APPROVED >= input.MinGRANT_B_APPROVEDFilter)
                        .WhereIf(input.MaxGRANT_B_APPROVEDFilter != null, e => e.GRANT_B_APPROVED <= input.MaxGRANT_B_APPROVEDFilter)
                        .WhereIf(input.MinGRANT_B_DECLINEDFilter != null, e => e.GRANT_B_DECLINED >= input.MinGRANT_B_DECLINEDFilter)
                        .WhereIf(input.MaxGRANT_B_DECLINEDFilter != null, e => e.GRANT_B_DECLINED <= input.MaxGRANT_B_DECLINEDFilter)
                        .WhereIf(input.MinGRANT_B_SWEPTFilter != null, e => e.GRANT_B_SWEPT >= input.MinGRANT_B_SWEPTFilter)
                        .WhereIf(input.MaxGRANT_B_SWEPTFilter != null, e => e.GRANT_B_SWEPT <= input.MaxGRANT_B_SWEPTFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_C_STATUSFilter), e => e.GRANT_C_STATUS == input.GRANT_C_STATUSFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_C_STATUS_COMMENTFilter), e => e.GRANT_C_STATUS_COMMENT == input.GRANT_C_STATUS_COMMENTFilter)
                        .WhereIf(input.MinGRANT_C_DISBURSED_DATEFilter != null, e => e.GRANT_C_DISBURSED_DATE >= input.MinGRANT_C_DISBURSED_DATEFilter)
                        .WhereIf(input.MaxGRANT_C_DISBURSED_DATEFilter != null, e => e.GRANT_C_DISBURSED_DATE <= input.MaxGRANT_C_DISBURSED_DATEFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_C_DISBURSED_BYFilter), e => e.GRANT_C_DISBURSED_BY == input.GRANT_C_DISBURSED_BYFilter)
                        .WhereIf(input.MinGRANT_C_APPROVEDFilter != null, e => e.GRANT_C_APPROVED >= input.MinGRANT_C_APPROVEDFilter)
                        .WhereIf(input.MaxGRANT_C_APPROVEDFilter != null, e => e.GRANT_C_APPROVED <= input.MaxGRANT_C_APPROVEDFilter)
                        .WhereIf(input.MinGRANT_C_DECLINEDFilter != null, e => e.GRANT_C_DECLINED >= input.MinGRANT_C_DECLINEDFilter)
                        .WhereIf(input.MaxGRANT_C_DECLINEDFilter != null, e => e.GRANT_C_DECLINED <= input.MaxGRANT_C_DECLINEDFilter)
                        .WhereIf(input.MinGRANT_C_SWEPTFilter != null, e => e.GRANT_C_SWEPT >= input.MinGRANT_C_SWEPTFilter)
                        .WhereIf(input.MaxGRANT_C_SWEPTFilter != null, e => e.GRANT_C_SWEPT <= input.MaxGRANT_C_SWEPTFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_D_STATUSFilter), e => e.GRANT_D_STATUS == input.GRANT_D_STATUSFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_D_STATUS_COMMENTFilter), e => e.GRANT_D_STATUS_COMMENT == input.GRANT_D_STATUS_COMMENTFilter)
                        .WhereIf(input.MinGRANT_D_DISBURSED_DATEFilter != null, e => e.GRANT_D_DISBURSED_DATE >= input.MinGRANT_D_DISBURSED_DATEFilter)
                        .WhereIf(input.MaxGRANT_D_DISBURSED_DATEFilter != null, e => e.GRANT_D_DISBURSED_DATE <= input.MaxGRANT_D_DISBURSED_DATEFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_D_DISBURSED_BYFilter), e => e.GRANT_D_DISBURSED_BY == input.GRANT_D_DISBURSED_BYFilter)
                        .WhereIf(input.MinGRANT_D_APPROVEDFilter != null, e => e.GRANT_D_APPROVED >= input.MinGRANT_D_APPROVEDFilter)
                        .WhereIf(input.MaxGRANT_D_APPROVEDFilter != null, e => e.GRANT_D_APPROVED <= input.MaxGRANT_D_APPROVEDFilter)
                        .WhereIf(input.MinGRANT_D_DECLINEDFilter != null, e => e.GRANT_D_DECLINED >= input.MinGRANT_D_DECLINEDFilter)
                        .WhereIf(input.MaxGRANT_D_DECLINEDFilter != null, e => e.GRANT_D_DECLINED <= input.MaxGRANT_D_DECLINEDFilter)
                        .WhereIf(input.MinGRANT_D_SWEPTFilter != null, e => e.GRANT_D_SWEPT >= input.MinGRANT_D_SWEPTFilter)
                        .WhereIf(input.MaxGRANT_D_SWEPTFilter != null, e => e.GRANT_D_SWEPT <= input.MaxGRANT_D_SWEPTFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_E_STATUSFilter), e => e.GRANT_E_STATUS == input.GRANT_E_STATUSFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_E_STATUS_COMMENTFilter), e => e.GRANT_E_STATUS_COMMENT == input.GRANT_E_STATUS_COMMENTFilter)
                        .WhereIf(input.MinGRANT_E_DISBURSED_DATEFilter != null, e => e.GRANT_E_DISBURSED_DATE >= input.MinGRANT_E_DISBURSED_DATEFilter)
                        .WhereIf(input.MaxGRANT_E_DISBURSED_DATEFilter != null, e => e.GRANT_E_DISBURSED_DATE <= input.MaxGRANT_E_DISBURSED_DATEFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_E_DISBURSED_BYFilter), e => e.GRANT_E_DISBURSED_BY == input.GRANT_E_DISBURSED_BYFilter)
                        .WhereIf(input.MinGRANT_E_APPROVEDFilter != null, e => e.GRANT_E_APPROVED >= input.MinGRANT_E_APPROVEDFilter)
                        .WhereIf(input.MaxGRANT_E_APPROVEDFilter != null, e => e.GRANT_E_APPROVED <= input.MaxGRANT_E_APPROVEDFilter)
                        .WhereIf(input.MinGRANT_E_DECLINEDFilter != null, e => e.GRANT_E_DECLINED >= input.MinGRANT_E_DECLINEDFilter)
                        .WhereIf(input.MaxGRANT_E_DECLINEDFilter != null, e => e.GRANT_E_DECLINED <= input.MaxGRANT_E_DECLINEDFilter)
                        .WhereIf(input.MinGRANT_E_SWEPTFilter != null, e => e.GRANT_E_SWEPT >= input.MinGRANT_E_SWEPTFilter)
                        .WhereIf(input.MaxGRANT_E_SWEPTFilter != null, e => e.GRANT_E_SWEPT <= input.MaxGRANT_E_SWEPTFilter)
                        .WhereIf(input.MinGRANT_A_OVERRIDDEN_AMOUNT_DIFFFilter != null, e => e.GRANT_A_OVERRIDDEN_AMOUNT_DIFF >= input.MinGRANT_A_OVERRIDDEN_AMOUNT_DIFFFilter)
                        .WhereIf(input.MaxGRANT_A_OVERRIDDEN_AMOUNT_DIFFFilter != null, e => e.GRANT_A_OVERRIDDEN_AMOUNT_DIFF <= input.MaxGRANT_A_OVERRIDDEN_AMOUNT_DIFFFilter)
                        .WhereIf(input.MinGRANT_B_OVERRIDDEN_AMOUNT_DIFFFilter != null, e => e.GRANT_B_OVERRIDDEN_AMOUNT_DIFF >= input.MinGRANT_B_OVERRIDDEN_AMOUNT_DIFFFilter)
                        .WhereIf(input.MaxGRANT_B_OVERRIDDEN_AMOUNT_DIFFFilter != null, e => e.GRANT_B_OVERRIDDEN_AMOUNT_DIFF <= input.MaxGRANT_B_OVERRIDDEN_AMOUNT_DIFFFilter)
                        .WhereIf(input.MinGRANT_C_OVERRIDDEN_AMOUNT_DIFFFilter != null, e => e.GRANT_C_OVERRIDDEN_AMOUNT_DIFF >= input.MinGRANT_C_OVERRIDDEN_AMOUNT_DIFFFilter)
                        .WhereIf(input.MaxGRANT_C_OVERRIDDEN_AMOUNT_DIFFFilter != null, e => e.GRANT_C_OVERRIDDEN_AMOUNT_DIFF <= input.MaxGRANT_C_OVERRIDDEN_AMOUNT_DIFFFilter)
                        .WhereIf(input.MinGRANT_D_OVERRIDDEN_AMOUNT_DIFFFilter != null, e => e.GRANT_D_OVERRIDDEN_AMOUNT_DIFF >= input.MinGRANT_D_OVERRIDDEN_AMOUNT_DIFFFilter)
                        .WhereIf(input.MaxGRANT_D_OVERRIDDEN_AMOUNT_DIFFFilter != null, e => e.GRANT_D_OVERRIDDEN_AMOUNT_DIFF <= input.MaxGRANT_D_OVERRIDDEN_AMOUNT_DIFFFilter)
                        .WhereIf(input.MinGRANT_E_OVERRIDDEN_AMOUNT_DIFFFilter != null, e => e.GRANT_E_OVERRIDDEN_AMOUNT_DIFF >= input.MinGRANT_E_OVERRIDDEN_AMOUNT_DIFFFilter)
                        .WhereIf(input.MaxGRANT_E_OVERRIDDEN_AMOUNT_DIFFFilter != null, e => e.GRANT_E_OVERRIDDEN_AMOUNT_DIFF <= input.MaxGRANT_E_OVERRIDDEN_AMOUNT_DIFFFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PROOF_OF_PAYMENT_RECEIVEDFilter), e => e.PROOF_OF_PAYMENT_RECEIVED == input.PROOF_OF_PAYMENT_RECEIVEDFilter)
                        .WhereIf(input.MinTOTAL_GRANT_APPROVEDFilter != null, e => e.TOTAL_GRANT_APPROVED >= input.MinTOTAL_GRANT_APPROVEDFilter)
                        .WhereIf(input.MaxTOTAL_GRANT_APPROVEDFilter != null, e => e.TOTAL_GRANT_APPROVED <= input.MaxTOTAL_GRANT_APPROVEDFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_A_PROCESSEDFilter), e => e.GRANT_A_PROCESSED == input.GRANT_A_PROCESSEDFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_B_PROCESSEDFilter), e => e.GRANT_B_PROCESSED == input.GRANT_B_PROCESSEDFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_C_PROCESSEDFilter), e => e.GRANT_C_PROCESSED == input.GRANT_C_PROCESSEDFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_D_PROCESSEDFilter), e => e.GRANT_D_PROCESSED == input.GRANT_D_PROCESSEDFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_E_PROCESSEDFilter), e => e.GRANT_E_PROCESSED == input.GRANT_E_PROCESSEDFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_A_PAYMENT_STATUSFilter), e => e.GRANT_A_PAYMENT_STATUS == input.GRANT_A_PAYMENT_STATUSFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_B_PAYMENT_STATUSFilter), e => e.GRANT_B_PAYMENT_STATUS == input.GRANT_B_PAYMENT_STATUSFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_C_PAYMENT_STATUSFilter), e => e.GRANT_C_PAYMENT_STATUS == input.GRANT_C_PAYMENT_STATUSFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_D_PAYMENT_STATUSFilter), e => e.GRANT_D_PAYMENT_STATUS == input.GRANT_D_PAYMENT_STATUSFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_E_PAYMENT_STATUSFilter), e => e.GRANT_E_PAYMENT_STATUS == input.GRANT_E_PAYMENT_STATUSFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_A_CHEQUE_EFT_NOFilter), e => e.GRANT_A_CHEQUE_EFT_NO == input.GRANT_A_CHEQUE_EFT_NOFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_B_CHEQUE_EFT_NOFilter), e => e.GRANT_B_CHEQUE_EFT_NO == input.GRANT_B_CHEQUE_EFT_NOFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_C_CHEQUE_EFT_NOFilter), e => e.GRANT_C_CHEQUE_EFT_NO == input.GRANT_C_CHEQUE_EFT_NOFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_D_CHEQUE_EFT_NOFilter), e => e.GRANT_D_CHEQUE_EFT_NO == input.GRANT_D_CHEQUE_EFT_NOFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_E_CHEQUE_EFT_NOFilter), e => e.GRANT_E_CHEQUE_EFT_NO == input.GRANT_E_CHEQUE_EFT_NOFilter)
                        .WhereIf(input.MinGRANT_EFilter != null, e => e.GRANT_E >= input.MinGRANT_EFilter)
                        .WhereIf(input.MaxGRANT_EFilter != null, e => e.GRANT_E <= input.MaxGRANT_EFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_A_BATCH_NOFilter), e => e.GRANT_A_BATCH_NO == input.GRANT_A_BATCH_NOFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_B_BATCH_NOFilter), e => e.GRANT_B_BATCH_NO == input.GRANT_B_BATCH_NOFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_C_BATCH_NOFilter), e => e.GRANT_C_BATCH_NO == input.GRANT_C_BATCH_NOFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_D_BATCH_NOFilter), e => e.GRANT_D_BATCH_NO == input.GRANT_D_BATCH_NOFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_E_BATCH_NOFilter), e => e.GRANT_E_BATCH_NO == input.GRANT_E_BATCH_NOFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_B_USER_COMMENTFilter), e => e.GRANT_B_USER_COMMENT == input.GRANT_B_USER_COMMENTFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_C_USER_COMMENTFilter), e => e.GRANT_C_USER_COMMENT == input.GRANT_C_USER_COMMENTFilter)
                        .WhereIf(input.MinLEVY_AMOUNT_RECEIVEDFilter != null, e => e.LEVY_AMOUNT_RECEIVED >= input.MinLEVY_AMOUNT_RECEIVEDFilter)
                        .WhereIf(input.MaxLEVY_AMOUNT_RECEIVEDFilter != null, e => e.LEVY_AMOUNT_RECEIVED <= input.MaxLEVY_AMOUNT_RECEIVEDFilter)
                        .WhereIf(input.MinINTEREST_AMOUNT_RECEIVEDFilter != null, e => e.INTEREST_AMOUNT_RECEIVED >= input.MinINTEREST_AMOUNT_RECEIVEDFilter)
                        .WhereIf(input.MaxINTEREST_AMOUNT_RECEIVEDFilter != null, e => e.INTEREST_AMOUNT_RECEIVED <= input.MaxINTEREST_AMOUNT_RECEIVEDFilter)
                        .WhereIf(input.MinPENALTY_AMOUNT_RECEIVEDFilter != null, e => e.PENALTY_AMOUNT_RECEIVED >= input.MinPENALTY_AMOUNT_RECEIVEDFilter)
                        .WhereIf(input.MaxPENALTY_AMOUNT_RECEIVEDFilter != null, e => e.PENALTY_AMOUNT_RECEIVED <= input.MaxPENALTY_AMOUNT_RECEIVEDFilter)
                        .WhereIf(input.MinTOTAL_AMOUNT_RECEIVEDFilter != null, e => e.TOTAL_AMOUNT_RECEIVED >= input.MinTOTAL_AMOUNT_RECEIVEDFilter)
                        .WhereIf(input.MaxTOTAL_AMOUNT_RECEIVEDFilter != null, e => e.TOTAL_AMOUNT_RECEIVED <= input.MaxTOTAL_AMOUNT_RECEIVEDFilter)
                        .WhereIf(input.MinSETA_COMPLETE_ADMIN_LEVYFilter != null, e => e.SETA_COMPLETE_ADMIN_LEVY >= input.MinSETA_COMPLETE_ADMIN_LEVYFilter)
                        .WhereIf(input.MaxSETA_COMPLETE_ADMIN_LEVYFilter != null, e => e.SETA_COMPLETE_ADMIN_LEVY <= input.MaxSETA_COMPLETE_ADMIN_LEVYFilter)
                        .WhereIf(input.MinSETA_COMPLETE_ADMIN_INTERESTFilter != null, e => e.SETA_COMPLETE_ADMIN_INTEREST >= input.MinSETA_COMPLETE_ADMIN_INTERESTFilter)
                        .WhereIf(input.MaxSETA_COMPLETE_ADMIN_INTERESTFilter != null, e => e.SETA_COMPLETE_ADMIN_INTEREST <= input.MaxSETA_COMPLETE_ADMIN_INTERESTFilter)
                        .WhereIf(input.MinSETA_COMPLETE_ADMIN_PENALTYFilter != null, e => e.SETA_COMPLETE_ADMIN_PENALTY >= input.MinSETA_COMPLETE_ADMIN_PENALTYFilter)
                        .WhereIf(input.MaxSETA_COMPLETE_ADMIN_PENALTYFilter != null, e => e.SETA_COMPLETE_ADMIN_PENALTY <= input.MaxSETA_COMPLETE_ADMIN_PENALTYFilter)
                        .WhereIf(input.MinSETA_COMPLETE_ADMIN_TOTALFilter != null, e => e.SETA_COMPLETE_ADMIN_TOTAL >= input.MinSETA_COMPLETE_ADMIN_TOTALFilter)
                        .WhereIf(input.MaxSETA_COMPLETE_ADMIN_TOTALFilter != null, e => e.SETA_COMPLETE_ADMIN_TOTAL <= input.MaxSETA_COMPLETE_ADMIN_TOTALFilter)
                        .WhereIf(input.MinGRANT_MGFilter != null, e => e.GRANT_MG >= input.MinGRANT_MGFilter)
                        .WhereIf(input.MaxGRANT_MGFilter != null, e => e.GRANT_MG <= input.MaxGRANT_MGFilter)
                        .WhereIf(input.MinGRANT_DGFilter != null, e => e.GRANT_DG >= input.MinGRANT_DGFilter)
                        .WhereIf(input.MaxGRANT_DGFilter != null, e => e.GRANT_DG <= input.MaxGRANT_DGFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_MG_STATUSFilter), e => e.GRANT_MG_STATUS == input.GRANT_MG_STATUSFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_MG_STATUS_COMMENTFilter), e => e.GRANT_MG_STATUS_COMMENT == input.GRANT_MG_STATUS_COMMENTFilter)
                        .WhereIf(input.MinGRANT_MG_DISBURSED_DATEFilter != null, e => e.GRANT_MG_DISBURSED_DATE >= input.MinGRANT_MG_DISBURSED_DATEFilter)
                        .WhereIf(input.MaxGRANT_MG_DISBURSED_DATEFilter != null, e => e.GRANT_MG_DISBURSED_DATE <= input.MaxGRANT_MG_DISBURSED_DATEFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_MG_DISBURSED_BYFilter), e => e.GRANT_MG_DISBURSED_BY == input.GRANT_MG_DISBURSED_BYFilter)
                        .WhereIf(input.MinGRANT_MG_APPROVEDFilter != null, e => e.GRANT_MG_APPROVED >= input.MinGRANT_MG_APPROVEDFilter)
                        .WhereIf(input.MaxGRANT_MG_APPROVEDFilter != null, e => e.GRANT_MG_APPROVED <= input.MaxGRANT_MG_APPROVEDFilter)
                        .WhereIf(input.MinGRANT_MG_DECLINEDFilter != null, e => e.GRANT_MG_DECLINED >= input.MinGRANT_MG_DECLINEDFilter)
                        .WhereIf(input.MaxGRANT_MG_DECLINEDFilter != null, e => e.GRANT_MG_DECLINED <= input.MaxGRANT_MG_DECLINEDFilter)
                        .WhereIf(input.MinGRANT_MG_SWEPTFilter != null, e => e.GRANT_MG_SWEPT >= input.MinGRANT_MG_SWEPTFilter)
                        .WhereIf(input.MaxGRANT_MG_SWEPTFilter != null, e => e.GRANT_MG_SWEPT <= input.MaxGRANT_MG_SWEPTFilter)
                        .WhereIf(input.MinGRANT_MG_OVERRIDDEN_AMT_DIFFFilter != null, e => e.GRANT_MG_OVERRIDDEN_AMT_DIFF >= input.MinGRANT_MG_OVERRIDDEN_AMT_DIFFFilter)
                        .WhereIf(input.MaxGRANT_MG_OVERRIDDEN_AMT_DIFFFilter != null, e => e.GRANT_MG_OVERRIDDEN_AMT_DIFF <= input.MaxGRANT_MG_OVERRIDDEN_AMT_DIFFFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_MG_PROCESSEDFilter), e => e.GRANT_MG_PROCESSED == input.GRANT_MG_PROCESSEDFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_MG_PAYMENT_STATUSFilter), e => e.GRANT_MG_PAYMENT_STATUS == input.GRANT_MG_PAYMENT_STATUSFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_MG_CHEQUE_EFT_NOFilter), e => e.GRANT_MG_CHEQUE_EFT_NO == input.GRANT_MG_CHEQUE_EFT_NOFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_MG_BATCH_NOFilter), e => e.GRANT_MG_BATCH_NO == input.GRANT_MG_BATCH_NOFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_MG_USER_COMMENTFilter), e => e.GRANT_MG_USER_COMMENT == input.GRANT_MG_USER_COMMENTFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_DG_STATUSFilter), e => e.GRANT_DG_STATUS == input.GRANT_DG_STATUSFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_DG_STATUS_COMMENTFilter), e => e.GRANT_DG_STATUS_COMMENT == input.GRANT_DG_STATUS_COMMENTFilter)
                        .WhereIf(input.MinGRANT_DG_DISBURSED_DATEFilter != null, e => e.GRANT_DG_DISBURSED_DATE >= input.MinGRANT_DG_DISBURSED_DATEFilter)
                        .WhereIf(input.MaxGRANT_DG_DISBURSED_DATEFilter != null, e => e.GRANT_DG_DISBURSED_DATE <= input.MaxGRANT_DG_DISBURSED_DATEFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_DG_DISBURSED_BYFilter), e => e.GRANT_DG_DISBURSED_BY == input.GRANT_DG_DISBURSED_BYFilter)
                        .WhereIf(input.MinGRANT_DG_APPROVEDFilter != null, e => e.GRANT_DG_APPROVED >= input.MinGRANT_DG_APPROVEDFilter)
                        .WhereIf(input.MaxGRANT_DG_APPROVEDFilter != null, e => e.GRANT_DG_APPROVED <= input.MaxGRANT_DG_APPROVEDFilter)
                        .WhereIf(input.MinGRANT_DG_DECLINEDFilter != null, e => e.GRANT_DG_DECLINED >= input.MinGRANT_DG_DECLINEDFilter)
                        .WhereIf(input.MaxGRANT_DG_DECLINEDFilter != null, e => e.GRANT_DG_DECLINED <= input.MaxGRANT_DG_DECLINEDFilter)
                        .WhereIf(input.MinGRANT_DG_SWEPTFilter != null, e => e.GRANT_DG_SWEPT >= input.MinGRANT_DG_SWEPTFilter)
                        .WhereIf(input.MaxGRANT_DG_SWEPTFilter != null, e => e.GRANT_DG_SWEPT <= input.MaxGRANT_DG_SWEPTFilter)
                        .WhereIf(input.MinGRANT_DG_OVERRIDDEN_AMT_DIFFFilter != null, e => e.GRANT_DG_OVERRIDDEN_AMT_DIFF >= input.MinGRANT_DG_OVERRIDDEN_AMT_DIFFFilter)
                        .WhereIf(input.MaxGRANT_DG_OVERRIDDEN_AMT_DIFFFilter != null, e => e.GRANT_DG_OVERRIDDEN_AMT_DIFF <= input.MaxGRANT_DG_OVERRIDDEN_AMT_DIFFFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_DG_PROCESSEDFilter), e => e.GRANT_DG_PROCESSED == input.GRANT_DG_PROCESSEDFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_DG_PAYMENT_STATUSFilter), e => e.GRANT_DG_PAYMENT_STATUS == input.GRANT_DG_PAYMENT_STATUSFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_DG_CHEQUE_EFT_NOFilter), e => e.GRANT_DG_CHEQUE_EFT_NO == input.GRANT_DG_CHEQUE_EFT_NOFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_DG_BATCH_NOFilter), e => e.GRANT_DG_BATCH_NO == input.GRANT_DG_BATCH_NOFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_DG_USER_COMMENTFilter), e => e.GRANT_DG_USER_COMMENT == input.GRANT_DG_USER_COMMENTFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.statusOneFilter), e => e.statusOne == input.statusOneFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.statusTwoFilter), e => e.statusTwo == input.statusTwoFilter);

            var pagedAndFilteredLEVY_PAYMENTSs = filteredLEVY_PAYMENTSs
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var levY_PAYMENTSs = from o in pagedAndFilteredLEVY_PAYMENTSs
                                 select new GetLEVY_PAYMENTSForViewDto()
                                 {
                                     LEVY_PAYMENTS = new LEVY_PAYMENTSDto
                                     {
                                         PERIOD = o.PERIOD,
                                         SDL_NO = o.SDL_NO,
                                         RECEIPT_DATE_SARS = o.RECEIPT_DATE_SARS,
                                         LEVY_AMOUNT = o.LEVY_AMOUNT,
                                         PENALTY_AMOUNT = o.PENALTY_AMOUNT,
                                         INTEREST_AMOUNT = o.INTEREST_AMOUNT,
                                         TOTAL_AMOUNT = o.TOTAL_AMOUNT,
                                         NO_SDL201_OUTSTANDING = o.NO_SDL201_OUTSTANDING,
                                         DEBT_OUTSTANDING_AMOUNT = o.DEBT_OUTSTANDING_AMOUNT,
                                         SARS_LEVY = o.SARS_LEVY,
                                         SARS_INTEREST = o.SARS_INTEREST,
                                         SARS_PENALTY = o.SARS_PENALTY,
                                         NSF_LEVY = o.NSF_LEVY,
                                         NSF_INTEREST = o.NSF_INTEREST,
                                         NSF_PENALTY = o.NSF_PENALTY,
                                         SETA_SETUP_LEVY = o.SETA_SETUP_LEVY,
                                         SETA_SETUP_INTEREST = o.SETA_SETUP_INTEREST,
                                         SETA_SETUP_PENALTY = o.SETA_SETUP_PENALTY,
                                         SETA_ADMIN_LEVY = o.SETA_ADMIN_LEVY,
                                         SETA_ADMIN_INTEREST = o.SETA_ADMIN_INTEREST,
                                         SETA_ADMIN_PENALTY = o.SETA_ADMIN_PENALTY,
                                         UNAPPORTIONED_LEVY = o.UNAPPORTIONED_LEVY,
                                         UNAPPORTIONED_INTEREST = o.UNAPPORTIONED_INTEREST,
                                         UNAPPORTIONED_PENALTY = o.UNAPPORTIONED_PENALTY,
                                         GRANT_A = o.GRANT_A,
                                         GRANT_B = o.GRANT_B,
                                         GRANT_C = o.GRANT_C,
                                         GRANT_D = o.GRANT_D,
                                         FINANCIAL_YEAR = o.FINANCIAL_YEAR,
                                         LEVY_TYPE = o.LEVY_TYPE,
                                         SETA_CODE = o.SETA_CODE,
                                         GRANT_A_STATUS = o.GRANT_A_STATUS,
                                         GRANT_A_STATUS_COMMENT = o.GRANT_A_STATUS_COMMENT,
                                         GRANT_A_DISBURSED_DATE = o.GRANT_A_DISBURSED_DATE,
                                         GRANT_A_DISBURSED_BY = o.GRANT_A_DISBURSED_BY,
                                         GRANT_A_APPROVED = o.GRANT_A_APPROVED,
                                         GRANT_A_DECLINED = o.GRANT_A_DECLINED,
                                         GRANT_A_SWEPT = o.GRANT_A_SWEPT,
                                         GRANT_B_STATUS = o.GRANT_B_STATUS,
                                         GRANT_B_STATUS_COMMENT = o.GRANT_B_STATUS_COMMENT,
                                         GRANT_B_DISBURSED_DATE = o.GRANT_B_DISBURSED_DATE,
                                         GRANT_B_DISBURSED_BY = o.GRANT_B_DISBURSED_BY,
                                         GRANT_B_APPROVED = o.GRANT_B_APPROVED,
                                         GRANT_B_DECLINED = o.GRANT_B_DECLINED,
                                         GRANT_B_SWEPT = o.GRANT_B_SWEPT,
                                         GRANT_C_STATUS = o.GRANT_C_STATUS,
                                         GRANT_C_STATUS_COMMENT = o.GRANT_C_STATUS_COMMENT,
                                         GRANT_C_DISBURSED_DATE = o.GRANT_C_DISBURSED_DATE,
                                         GRANT_C_DISBURSED_BY = o.GRANT_C_DISBURSED_BY,
                                         GRANT_C_APPROVED = o.GRANT_C_APPROVED,
                                         GRANT_C_DECLINED = o.GRANT_C_DECLINED,
                                         GRANT_C_SWEPT = o.GRANT_C_SWEPT,
                                         GRANT_D_STATUS = o.GRANT_D_STATUS,
                                         GRANT_D_STATUS_COMMENT = o.GRANT_D_STATUS_COMMENT,
                                         GRANT_D_DISBURSED_DATE = o.GRANT_D_DISBURSED_DATE,
                                         GRANT_D_DISBURSED_BY = o.GRANT_D_DISBURSED_BY,
                                         GRANT_D_APPROVED = o.GRANT_D_APPROVED,
                                         GRANT_D_DECLINED = o.GRANT_D_DECLINED,
                                         GRANT_D_SWEPT = o.GRANT_D_SWEPT,
                                         GRANT_E_STATUS = o.GRANT_E_STATUS,
                                         GRANT_E_STATUS_COMMENT = o.GRANT_E_STATUS_COMMENT,
                                         GRANT_E_DISBURSED_DATE = o.GRANT_E_DISBURSED_DATE,
                                         GRANT_E_DISBURSED_BY = o.GRANT_E_DISBURSED_BY,
                                         GRANT_E_APPROVED = o.GRANT_E_APPROVED,
                                         GRANT_E_DECLINED = o.GRANT_E_DECLINED,
                                         GRANT_E_SWEPT = o.GRANT_E_SWEPT,
                                         GRANT_A_OVERRIDDEN_AMOUNT_DIFF = o.GRANT_A_OVERRIDDEN_AMOUNT_DIFF,
                                         GRANT_B_OVERRIDDEN_AMOUNT_DIFF = o.GRANT_B_OVERRIDDEN_AMOUNT_DIFF,
                                         GRANT_C_OVERRIDDEN_AMOUNT_DIFF = o.GRANT_C_OVERRIDDEN_AMOUNT_DIFF,
                                         GRANT_D_OVERRIDDEN_AMOUNT_DIFF = o.GRANT_D_OVERRIDDEN_AMOUNT_DIFF,
                                         GRANT_E_OVERRIDDEN_AMOUNT_DIFF = o.GRANT_E_OVERRIDDEN_AMOUNT_DIFF,
                                         PROOF_OF_PAYMENT_RECEIVED = o.PROOF_OF_PAYMENT_RECEIVED,
                                         TOTAL_GRANT_APPROVED = o.TOTAL_GRANT_APPROVED,
                                         GRANT_A_PROCESSED = o.GRANT_A_PROCESSED,
                                         GRANT_B_PROCESSED = o.GRANT_B_PROCESSED,
                                         GRANT_C_PROCESSED = o.GRANT_C_PROCESSED,
                                         GRANT_D_PROCESSED = o.GRANT_D_PROCESSED,
                                         GRANT_E_PROCESSED = o.GRANT_E_PROCESSED,
                                         GRANT_A_PAYMENT_STATUS = o.GRANT_A_PAYMENT_STATUS,
                                         GRANT_B_PAYMENT_STATUS = o.GRANT_B_PAYMENT_STATUS,
                                         GRANT_C_PAYMENT_STATUS = o.GRANT_C_PAYMENT_STATUS,
                                         GRANT_D_PAYMENT_STATUS = o.GRANT_D_PAYMENT_STATUS,
                                         GRANT_E_PAYMENT_STATUS = o.GRANT_E_PAYMENT_STATUS,
                                         GRANT_A_CHEQUE_EFT_NO = o.GRANT_A_CHEQUE_EFT_NO,
                                         GRANT_B_CHEQUE_EFT_NO = o.GRANT_B_CHEQUE_EFT_NO,
                                         GRANT_C_CHEQUE_EFT_NO = o.GRANT_C_CHEQUE_EFT_NO,
                                         GRANT_D_CHEQUE_EFT_NO = o.GRANT_D_CHEQUE_EFT_NO,
                                         GRANT_E_CHEQUE_EFT_NO = o.GRANT_E_CHEQUE_EFT_NO,
                                         GRANT_E = o.GRANT_E,
                                         GRANT_A_BATCH_NO = o.GRANT_A_BATCH_NO,
                                         GRANT_B_BATCH_NO = o.GRANT_B_BATCH_NO,
                                         GRANT_C_BATCH_NO = o.GRANT_C_BATCH_NO,
                                         GRANT_D_BATCH_NO = o.GRANT_D_BATCH_NO,
                                         GRANT_E_BATCH_NO = o.GRANT_E_BATCH_NO,
                                         GRANT_B_USER_COMMENT = o.GRANT_B_USER_COMMENT,
                                         GRANT_C_USER_COMMENT = o.GRANT_C_USER_COMMENT,
                                         LEVY_AMOUNT_RECEIVED = o.LEVY_AMOUNT_RECEIVED,
                                         INTEREST_AMOUNT_RECEIVED = o.INTEREST_AMOUNT_RECEIVED,
                                         PENALTY_AMOUNT_RECEIVED = o.PENALTY_AMOUNT_RECEIVED,
                                         TOTAL_AMOUNT_RECEIVED = o.TOTAL_AMOUNT_RECEIVED,
                                         SETA_COMPLETE_ADMIN_LEVY = o.SETA_COMPLETE_ADMIN_LEVY,
                                         SETA_COMPLETE_ADMIN_INTEREST = o.SETA_COMPLETE_ADMIN_INTEREST,
                                         SETA_COMPLETE_ADMIN_PENALTY = o.SETA_COMPLETE_ADMIN_PENALTY,
                                         SETA_COMPLETE_ADMIN_TOTAL = o.SETA_COMPLETE_ADMIN_TOTAL,
                                         GRANT_MG = o.GRANT_MG,
                                         GRANT_DG = o.GRANT_DG,
                                         GRANT_MG_STATUS = o.GRANT_MG_STATUS,
                                         GRANT_MG_STATUS_COMMENT = o.GRANT_MG_STATUS_COMMENT,
                                         GRANT_MG_DISBURSED_DATE = o.GRANT_MG_DISBURSED_DATE,
                                         GRANT_MG_DISBURSED_BY = o.GRANT_MG_DISBURSED_BY,
                                         GRANT_MG_APPROVED = o.GRANT_MG_APPROVED,
                                         GRANT_MG_DECLINED = o.GRANT_MG_DECLINED,
                                         GRANT_MG_SWEPT = o.GRANT_MG_SWEPT,
                                         GRANT_MG_OVERRIDDEN_AMT_DIFF = o.GRANT_MG_OVERRIDDEN_AMT_DIFF,
                                         GRANT_MG_PROCESSED = o.GRANT_MG_PROCESSED,
                                         GRANT_MG_PAYMENT_STATUS = o.GRANT_MG_PAYMENT_STATUS,
                                         GRANT_MG_CHEQUE_EFT_NO = o.GRANT_MG_CHEQUE_EFT_NO,
                                         GRANT_MG_BATCH_NO = o.GRANT_MG_BATCH_NO,
                                         GRANT_MG_USER_COMMENT = o.GRANT_MG_USER_COMMENT,
                                         GRANT_DG_STATUS = o.GRANT_DG_STATUS,
                                         GRANT_DG_STATUS_COMMENT = o.GRANT_DG_STATUS_COMMENT,
                                         GRANT_DG_DISBURSED_DATE = o.GRANT_DG_DISBURSED_DATE,
                                         GRANT_DG_DISBURSED_BY = o.GRANT_DG_DISBURSED_BY,
                                         GRANT_DG_APPROVED = o.GRANT_DG_APPROVED,
                                         GRANT_DG_DECLINED = o.GRANT_DG_DECLINED,
                                         GRANT_DG_SWEPT = o.GRANT_DG_SWEPT,
                                         GRANT_DG_OVERRIDDEN_AMT_DIFF = o.GRANT_DG_OVERRIDDEN_AMT_DIFF,
                                         GRANT_DG_PROCESSED = o.GRANT_DG_PROCESSED,
                                         GRANT_DG_PAYMENT_STATUS = o.GRANT_DG_PAYMENT_STATUS,
                                         GRANT_DG_CHEQUE_EFT_NO = o.GRANT_DG_CHEQUE_EFT_NO,
                                         GRANT_DG_BATCH_NO = o.GRANT_DG_BATCH_NO,
                                         GRANT_DG_USER_COMMENT = o.GRANT_DG_USER_COMMENT,
                                         statusOne = o.statusOne,
                                         statusTwo = o.statusTwo,
                                         Id = o.Id
                                     }
                                 };

            var totalCount = await filteredLEVY_PAYMENTSs.CountAsync();

            return new PagedResultDto<GetLEVY_PAYMENTSForViewDto>(
                totalCount,
                await levY_PAYMENTSs.ToListAsync()
            );
        }

        public async Task<GetLEVY_PAYMENTSForViewDto> GetLEVY_PAYMENTSForView(int id)
        {
            var levY_PAYMENTS = await _levY_PAYMENTSRepository.GetAsync(id);

            var output = new GetLEVY_PAYMENTSForViewDto { LEVY_PAYMENTS = ObjectMapper.Map<LEVY_PAYMENTSDto>(levY_PAYMENTS) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_LEVY_PAYMENTSs_Edit)]
        public async Task<GetLEVY_PAYMENTSForEditOutput> GetLEVY_PAYMENTSForEdit(EntityDto input)
        {
            var levY_PAYMENTS = await _levY_PAYMENTSRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetLEVY_PAYMENTSForEditOutput { LEVY_PAYMENTS = ObjectMapper.Map<CreateOrEditLEVY_PAYMENTSDto>(levY_PAYMENTS) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditLEVY_PAYMENTSDto input)
        {
            if (input.Id == null)
            {
                await Create(input);
            }
            else
            {
                await Update(input);
            }
        }

        protected virtual async Task Create(CreateOrEditLEVY_PAYMENTSDto input)
        {
            var levY_PAYMENTS = ObjectMapper.Map<LEVY_PAYMENTS>(input);


            if (AbpSession.TenantId != null)
            {
                levY_PAYMENTS.TenantId = (int?)AbpSession.TenantId;
            }


            await _levY_PAYMENTSRepository.InsertAsync(levY_PAYMENTS);
        }

        [AbpAuthorize(AppPermissions.Pages_LEVY_PAYMENTSs_Edit)]
        protected virtual async Task Update(CreateOrEditLEVY_PAYMENTSDto input)
        {
            var levY_PAYMENTS = await _levY_PAYMENTSRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, levY_PAYMENTS);
        }

        [AbpAuthorize(AppPermissions.Pages_LEVY_PAYMENTSs_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _levY_PAYMENTSRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetLEVY_PAYMENTSsToExcel(GetAllLEVY_PAYMENTSsForExcelInput input)
        {

            var filteredLEVY_PAYMENTSs = _levY_PAYMENTSRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.SDL_NO.Contains(input.Filter) || e.LEVY_TYPE.Contains(input.Filter) || e.GRANT_A_STATUS.Contains(input.Filter) || e.GRANT_A_STATUS_COMMENT.Contains(input.Filter) || e.GRANT_A_DISBURSED_BY.Contains(input.Filter) || e.GRANT_B_STATUS.Contains(input.Filter) || e.GRANT_B_STATUS_COMMENT.Contains(input.Filter) || e.GRANT_B_DISBURSED_BY.Contains(input.Filter) || e.GRANT_C_STATUS.Contains(input.Filter) || e.GRANT_C_STATUS_COMMENT.Contains(input.Filter) || e.GRANT_C_DISBURSED_BY.Contains(input.Filter) || e.GRANT_D_STATUS.Contains(input.Filter) || e.GRANT_D_STATUS_COMMENT.Contains(input.Filter) || e.GRANT_D_DISBURSED_BY.Contains(input.Filter) || e.GRANT_E_STATUS.Contains(input.Filter) || e.GRANT_E_STATUS_COMMENT.Contains(input.Filter) || e.GRANT_E_DISBURSED_BY.Contains(input.Filter) || e.PROOF_OF_PAYMENT_RECEIVED.Contains(input.Filter) || e.GRANT_A_PROCESSED.Contains(input.Filter) || e.GRANT_B_PROCESSED.Contains(input.Filter) || e.GRANT_C_PROCESSED.Contains(input.Filter) || e.GRANT_D_PROCESSED.Contains(input.Filter) || e.GRANT_E_PROCESSED.Contains(input.Filter) || e.GRANT_A_PAYMENT_STATUS.Contains(input.Filter) || e.GRANT_B_PAYMENT_STATUS.Contains(input.Filter) || e.GRANT_C_PAYMENT_STATUS.Contains(input.Filter) || e.GRANT_D_PAYMENT_STATUS.Contains(input.Filter) || e.GRANT_E_PAYMENT_STATUS.Contains(input.Filter) || e.GRANT_A_CHEQUE_EFT_NO.Contains(input.Filter) || e.GRANT_B_CHEQUE_EFT_NO.Contains(input.Filter) || e.GRANT_C_CHEQUE_EFT_NO.Contains(input.Filter) || e.GRANT_D_CHEQUE_EFT_NO.Contains(input.Filter) || e.GRANT_E_CHEQUE_EFT_NO.Contains(input.Filter) || e.GRANT_A_BATCH_NO.Contains(input.Filter) || e.GRANT_B_BATCH_NO.Contains(input.Filter) || e.GRANT_C_BATCH_NO.Contains(input.Filter) || e.GRANT_D_BATCH_NO.Contains(input.Filter) || e.GRANT_E_BATCH_NO.Contains(input.Filter) || e.GRANT_B_USER_COMMENT.Contains(input.Filter) || e.GRANT_C_USER_COMMENT.Contains(input.Filter) || e.GRANT_MG_STATUS.Contains(input.Filter) || e.GRANT_MG_STATUS_COMMENT.Contains(input.Filter) || e.GRANT_MG_DISBURSED_BY.Contains(input.Filter) || e.GRANT_MG_PROCESSED.Contains(input.Filter) || e.GRANT_MG_PAYMENT_STATUS.Contains(input.Filter) || e.GRANT_MG_CHEQUE_EFT_NO.Contains(input.Filter) || e.GRANT_MG_BATCH_NO.Contains(input.Filter) || e.GRANT_MG_USER_COMMENT.Contains(input.Filter) || e.GRANT_DG_STATUS.Contains(input.Filter) || e.GRANT_DG_STATUS_COMMENT.Contains(input.Filter) || e.GRANT_DG_DISBURSED_BY.Contains(input.Filter) || e.GRANT_DG_PROCESSED.Contains(input.Filter) || e.GRANT_DG_PAYMENT_STATUS.Contains(input.Filter) || e.GRANT_DG_CHEQUE_EFT_NO.Contains(input.Filter) || e.GRANT_DG_BATCH_NO.Contains(input.Filter) || e.GRANT_DG_USER_COMMENT.Contains(input.Filter) || e.statusOne.Contains(input.Filter) || e.statusTwo.Contains(input.Filter))
                        .WhereIf(input.MinPERIODFilter != null, e => e.PERIOD >= input.MinPERIODFilter)
                        .WhereIf(input.MaxPERIODFilter != null, e => e.PERIOD <= input.MaxPERIODFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.SDL_NOFilter), e => e.SDL_NO == input.SDL_NOFilter)
                        .WhereIf(input.MinRECEIPT_DATE_SARSFilter != null, e => e.RECEIPT_DATE_SARS >= input.MinRECEIPT_DATE_SARSFilter)
                        .WhereIf(input.MaxRECEIPT_DATE_SARSFilter != null, e => e.RECEIPT_DATE_SARS <= input.MaxRECEIPT_DATE_SARSFilter)
                        .WhereIf(input.MinLEVY_AMOUNTFilter != null, e => e.LEVY_AMOUNT >= input.MinLEVY_AMOUNTFilter)
                        .WhereIf(input.MaxLEVY_AMOUNTFilter != null, e => e.LEVY_AMOUNT <= input.MaxLEVY_AMOUNTFilter)
                        .WhereIf(input.MinPENALTY_AMOUNTFilter != null, e => e.PENALTY_AMOUNT >= input.MinPENALTY_AMOUNTFilter)
                        .WhereIf(input.MaxPENALTY_AMOUNTFilter != null, e => e.PENALTY_AMOUNT <= input.MaxPENALTY_AMOUNTFilter)
                        .WhereIf(input.MinINTEREST_AMOUNTFilter != null, e => e.INTEREST_AMOUNT >= input.MinINTEREST_AMOUNTFilter)
                        .WhereIf(input.MaxINTEREST_AMOUNTFilter != null, e => e.INTEREST_AMOUNT <= input.MaxINTEREST_AMOUNTFilter)
                        .WhereIf(input.MinTOTAL_AMOUNTFilter != null, e => e.TOTAL_AMOUNT >= input.MinTOTAL_AMOUNTFilter)
                        .WhereIf(input.MaxTOTAL_AMOUNTFilter != null, e => e.TOTAL_AMOUNT <= input.MaxTOTAL_AMOUNTFilter)
                        .WhereIf(input.MinNO_SDL201_OUTSTANDINGFilter != null, e => e.NO_SDL201_OUTSTANDING >= input.MinNO_SDL201_OUTSTANDINGFilter)
                        .WhereIf(input.MaxNO_SDL201_OUTSTANDINGFilter != null, e => e.NO_SDL201_OUTSTANDING <= input.MaxNO_SDL201_OUTSTANDINGFilter)
                        .WhereIf(input.MinDEBT_OUTSTANDING_AMOUNTFilter != null, e => e.DEBT_OUTSTANDING_AMOUNT >= input.MinDEBT_OUTSTANDING_AMOUNTFilter)
                        .WhereIf(input.MaxDEBT_OUTSTANDING_AMOUNTFilter != null, e => e.DEBT_OUTSTANDING_AMOUNT <= input.MaxDEBT_OUTSTANDING_AMOUNTFilter)
                        .WhereIf(input.MinSARS_LEVYFilter != null, e => e.SARS_LEVY >= input.MinSARS_LEVYFilter)
                        .WhereIf(input.MaxSARS_LEVYFilter != null, e => e.SARS_LEVY <= input.MaxSARS_LEVYFilter)
                        .WhereIf(input.MinSARS_INTERESTFilter != null, e => e.SARS_INTEREST >= input.MinSARS_INTERESTFilter)
                        .WhereIf(input.MaxSARS_INTERESTFilter != null, e => e.SARS_INTEREST <= input.MaxSARS_INTERESTFilter)
                        .WhereIf(input.MinSARS_PENALTYFilter != null, e => e.SARS_PENALTY >= input.MinSARS_PENALTYFilter)
                        .WhereIf(input.MaxSARS_PENALTYFilter != null, e => e.SARS_PENALTY <= input.MaxSARS_PENALTYFilter)
                        .WhereIf(input.MinNSF_LEVYFilter != null, e => e.NSF_LEVY >= input.MinNSF_LEVYFilter)
                        .WhereIf(input.MaxNSF_LEVYFilter != null, e => e.NSF_LEVY <= input.MaxNSF_LEVYFilter)
                        .WhereIf(input.MinNSF_INTERESTFilter != null, e => e.NSF_INTEREST >= input.MinNSF_INTERESTFilter)
                        .WhereIf(input.MaxNSF_INTERESTFilter != null, e => e.NSF_INTEREST <= input.MaxNSF_INTERESTFilter)
                        .WhereIf(input.MinNSF_PENALTYFilter != null, e => e.NSF_PENALTY >= input.MinNSF_PENALTYFilter)
                        .WhereIf(input.MaxNSF_PENALTYFilter != null, e => e.NSF_PENALTY <= input.MaxNSF_PENALTYFilter)
                        .WhereIf(input.MinSETA_SETUP_LEVYFilter != null, e => e.SETA_SETUP_LEVY >= input.MinSETA_SETUP_LEVYFilter)
                        .WhereIf(input.MaxSETA_SETUP_LEVYFilter != null, e => e.SETA_SETUP_LEVY <= input.MaxSETA_SETUP_LEVYFilter)
                        .WhereIf(input.MinSETA_SETUP_INTERESTFilter != null, e => e.SETA_SETUP_INTEREST >= input.MinSETA_SETUP_INTERESTFilter)
                        .WhereIf(input.MaxSETA_SETUP_INTERESTFilter != null, e => e.SETA_SETUP_INTEREST <= input.MaxSETA_SETUP_INTERESTFilter)
                        .WhereIf(input.MinSETA_SETUP_PENALTYFilter != null, e => e.SETA_SETUP_PENALTY >= input.MinSETA_SETUP_PENALTYFilter)
                        .WhereIf(input.MaxSETA_SETUP_PENALTYFilter != null, e => e.SETA_SETUP_PENALTY <= input.MaxSETA_SETUP_PENALTYFilter)
                        .WhereIf(input.MinSETA_ADMIN_LEVYFilter != null, e => e.SETA_ADMIN_LEVY >= input.MinSETA_ADMIN_LEVYFilter)
                        .WhereIf(input.MaxSETA_ADMIN_LEVYFilter != null, e => e.SETA_ADMIN_LEVY <= input.MaxSETA_ADMIN_LEVYFilter)
                        .WhereIf(input.MinSETA_ADMIN_INTERESTFilter != null, e => e.SETA_ADMIN_INTEREST >= input.MinSETA_ADMIN_INTERESTFilter)
                        .WhereIf(input.MaxSETA_ADMIN_INTERESTFilter != null, e => e.SETA_ADMIN_INTEREST <= input.MaxSETA_ADMIN_INTERESTFilter)
                        .WhereIf(input.MinSETA_ADMIN_PENALTYFilter != null, e => e.SETA_ADMIN_PENALTY >= input.MinSETA_ADMIN_PENALTYFilter)
                        .WhereIf(input.MaxSETA_ADMIN_PENALTYFilter != null, e => e.SETA_ADMIN_PENALTY <= input.MaxSETA_ADMIN_PENALTYFilter)
                        .WhereIf(input.MinUNAPPORTIONED_LEVYFilter != null, e => e.UNAPPORTIONED_LEVY >= input.MinUNAPPORTIONED_LEVYFilter)
                        .WhereIf(input.MaxUNAPPORTIONED_LEVYFilter != null, e => e.UNAPPORTIONED_LEVY <= input.MaxUNAPPORTIONED_LEVYFilter)
                        .WhereIf(input.MinUNAPPORTIONED_INTERESTFilter != null, e => e.UNAPPORTIONED_INTEREST >= input.MinUNAPPORTIONED_INTERESTFilter)
                        .WhereIf(input.MaxUNAPPORTIONED_INTERESTFilter != null, e => e.UNAPPORTIONED_INTEREST <= input.MaxUNAPPORTIONED_INTERESTFilter)
                        .WhereIf(input.MinUNAPPORTIONED_PENALTYFilter != null, e => e.UNAPPORTIONED_PENALTY >= input.MinUNAPPORTIONED_PENALTYFilter)
                        .WhereIf(input.MaxUNAPPORTIONED_PENALTYFilter != null, e => e.UNAPPORTIONED_PENALTY <= input.MaxUNAPPORTIONED_PENALTYFilter)
                        .WhereIf(input.MinGRANT_AFilter != null, e => e.GRANT_A >= input.MinGRANT_AFilter)
                        .WhereIf(input.MaxGRANT_AFilter != null, e => e.GRANT_A <= input.MaxGRANT_AFilter)
                        .WhereIf(input.MinGRANT_BFilter != null, e => e.GRANT_B >= input.MinGRANT_BFilter)
                        .WhereIf(input.MaxGRANT_BFilter != null, e => e.GRANT_B <= input.MaxGRANT_BFilter)
                        .WhereIf(input.MinGRANT_CFilter != null, e => e.GRANT_C >= input.MinGRANT_CFilter)
                        .WhereIf(input.MaxGRANT_CFilter != null, e => e.GRANT_C <= input.MaxGRANT_CFilter)
                        .WhereIf(input.MinGRANT_DFilter != null, e => e.GRANT_D >= input.MinGRANT_DFilter)
                        .WhereIf(input.MaxGRANT_DFilter != null, e => e.GRANT_D <= input.MaxGRANT_DFilter)
                        .WhereIf(input.MinFINANCIAL_YEARFilter != null, e => e.FINANCIAL_YEAR >= input.MinFINANCIAL_YEARFilter)
                        .WhereIf(input.MaxFINANCIAL_YEARFilter != null, e => e.FINANCIAL_YEAR <= input.MaxFINANCIAL_YEARFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.LEVY_TYPEFilter), e => e.LEVY_TYPE == input.LEVY_TYPEFilter)
                        .WhereIf(input.MinSETA_CODEFilter != null, e => e.SETA_CODE >= input.MinSETA_CODEFilter)
                        .WhereIf(input.MaxSETA_CODEFilter != null, e => e.SETA_CODE <= input.MaxSETA_CODEFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_A_STATUSFilter), e => e.GRANT_A_STATUS == input.GRANT_A_STATUSFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_A_STATUS_COMMENTFilter), e => e.GRANT_A_STATUS_COMMENT == input.GRANT_A_STATUS_COMMENTFilter)
                        .WhereIf(input.MinGRANT_A_DISBURSED_DATEFilter != null, e => e.GRANT_A_DISBURSED_DATE >= input.MinGRANT_A_DISBURSED_DATEFilter)
                        .WhereIf(input.MaxGRANT_A_DISBURSED_DATEFilter != null, e => e.GRANT_A_DISBURSED_DATE <= input.MaxGRANT_A_DISBURSED_DATEFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_A_DISBURSED_BYFilter), e => e.GRANT_A_DISBURSED_BY == input.GRANT_A_DISBURSED_BYFilter)
                        .WhereIf(input.MinGRANT_A_APPROVEDFilter != null, e => e.GRANT_A_APPROVED >= input.MinGRANT_A_APPROVEDFilter)
                        .WhereIf(input.MaxGRANT_A_APPROVEDFilter != null, e => e.GRANT_A_APPROVED <= input.MaxGRANT_A_APPROVEDFilter)
                        .WhereIf(input.MinGRANT_A_DECLINEDFilter != null, e => e.GRANT_A_DECLINED >= input.MinGRANT_A_DECLINEDFilter)
                        .WhereIf(input.MaxGRANT_A_DECLINEDFilter != null, e => e.GRANT_A_DECLINED <= input.MaxGRANT_A_DECLINEDFilter)
                        .WhereIf(input.MinGRANT_A_SWEPTFilter != null, e => e.GRANT_A_SWEPT >= input.MinGRANT_A_SWEPTFilter)
                        .WhereIf(input.MaxGRANT_A_SWEPTFilter != null, e => e.GRANT_A_SWEPT <= input.MaxGRANT_A_SWEPTFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_B_STATUSFilter), e => e.GRANT_B_STATUS == input.GRANT_B_STATUSFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_B_STATUS_COMMENTFilter), e => e.GRANT_B_STATUS_COMMENT == input.GRANT_B_STATUS_COMMENTFilter)
                        .WhereIf(input.MinGRANT_B_DISBURSED_DATEFilter != null, e => e.GRANT_B_DISBURSED_DATE >= input.MinGRANT_B_DISBURSED_DATEFilter)
                        .WhereIf(input.MaxGRANT_B_DISBURSED_DATEFilter != null, e => e.GRANT_B_DISBURSED_DATE <= input.MaxGRANT_B_DISBURSED_DATEFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_B_DISBURSED_BYFilter), e => e.GRANT_B_DISBURSED_BY == input.GRANT_B_DISBURSED_BYFilter)
                        .WhereIf(input.MinGRANT_B_APPROVEDFilter != null, e => e.GRANT_B_APPROVED >= input.MinGRANT_B_APPROVEDFilter)
                        .WhereIf(input.MaxGRANT_B_APPROVEDFilter != null, e => e.GRANT_B_APPROVED <= input.MaxGRANT_B_APPROVEDFilter)
                        .WhereIf(input.MinGRANT_B_DECLINEDFilter != null, e => e.GRANT_B_DECLINED >= input.MinGRANT_B_DECLINEDFilter)
                        .WhereIf(input.MaxGRANT_B_DECLINEDFilter != null, e => e.GRANT_B_DECLINED <= input.MaxGRANT_B_DECLINEDFilter)
                        .WhereIf(input.MinGRANT_B_SWEPTFilter != null, e => e.GRANT_B_SWEPT >= input.MinGRANT_B_SWEPTFilter)
                        .WhereIf(input.MaxGRANT_B_SWEPTFilter != null, e => e.GRANT_B_SWEPT <= input.MaxGRANT_B_SWEPTFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_C_STATUSFilter), e => e.GRANT_C_STATUS == input.GRANT_C_STATUSFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_C_STATUS_COMMENTFilter), e => e.GRANT_C_STATUS_COMMENT == input.GRANT_C_STATUS_COMMENTFilter)
                        .WhereIf(input.MinGRANT_C_DISBURSED_DATEFilter != null, e => e.GRANT_C_DISBURSED_DATE >= input.MinGRANT_C_DISBURSED_DATEFilter)
                        .WhereIf(input.MaxGRANT_C_DISBURSED_DATEFilter != null, e => e.GRANT_C_DISBURSED_DATE <= input.MaxGRANT_C_DISBURSED_DATEFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_C_DISBURSED_BYFilter), e => e.GRANT_C_DISBURSED_BY == input.GRANT_C_DISBURSED_BYFilter)
                        .WhereIf(input.MinGRANT_C_APPROVEDFilter != null, e => e.GRANT_C_APPROVED >= input.MinGRANT_C_APPROVEDFilter)
                        .WhereIf(input.MaxGRANT_C_APPROVEDFilter != null, e => e.GRANT_C_APPROVED <= input.MaxGRANT_C_APPROVEDFilter)
                        .WhereIf(input.MinGRANT_C_DECLINEDFilter != null, e => e.GRANT_C_DECLINED >= input.MinGRANT_C_DECLINEDFilter)
                        .WhereIf(input.MaxGRANT_C_DECLINEDFilter != null, e => e.GRANT_C_DECLINED <= input.MaxGRANT_C_DECLINEDFilter)
                        .WhereIf(input.MinGRANT_C_SWEPTFilter != null, e => e.GRANT_C_SWEPT >= input.MinGRANT_C_SWEPTFilter)
                        .WhereIf(input.MaxGRANT_C_SWEPTFilter != null, e => e.GRANT_C_SWEPT <= input.MaxGRANT_C_SWEPTFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_D_STATUSFilter), e => e.GRANT_D_STATUS == input.GRANT_D_STATUSFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_D_STATUS_COMMENTFilter), e => e.GRANT_D_STATUS_COMMENT == input.GRANT_D_STATUS_COMMENTFilter)
                        .WhereIf(input.MinGRANT_D_DISBURSED_DATEFilter != null, e => e.GRANT_D_DISBURSED_DATE >= input.MinGRANT_D_DISBURSED_DATEFilter)
                        .WhereIf(input.MaxGRANT_D_DISBURSED_DATEFilter != null, e => e.GRANT_D_DISBURSED_DATE <= input.MaxGRANT_D_DISBURSED_DATEFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_D_DISBURSED_BYFilter), e => e.GRANT_D_DISBURSED_BY == input.GRANT_D_DISBURSED_BYFilter)
                        .WhereIf(input.MinGRANT_D_APPROVEDFilter != null, e => e.GRANT_D_APPROVED >= input.MinGRANT_D_APPROVEDFilter)
                        .WhereIf(input.MaxGRANT_D_APPROVEDFilter != null, e => e.GRANT_D_APPROVED <= input.MaxGRANT_D_APPROVEDFilter)
                        .WhereIf(input.MinGRANT_D_DECLINEDFilter != null, e => e.GRANT_D_DECLINED >= input.MinGRANT_D_DECLINEDFilter)
                        .WhereIf(input.MaxGRANT_D_DECLINEDFilter != null, e => e.GRANT_D_DECLINED <= input.MaxGRANT_D_DECLINEDFilter)
                        .WhereIf(input.MinGRANT_D_SWEPTFilter != null, e => e.GRANT_D_SWEPT >= input.MinGRANT_D_SWEPTFilter)
                        .WhereIf(input.MaxGRANT_D_SWEPTFilter != null, e => e.GRANT_D_SWEPT <= input.MaxGRANT_D_SWEPTFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_E_STATUSFilter), e => e.GRANT_E_STATUS == input.GRANT_E_STATUSFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_E_STATUS_COMMENTFilter), e => e.GRANT_E_STATUS_COMMENT == input.GRANT_E_STATUS_COMMENTFilter)
                        .WhereIf(input.MinGRANT_E_DISBURSED_DATEFilter != null, e => e.GRANT_E_DISBURSED_DATE >= input.MinGRANT_E_DISBURSED_DATEFilter)
                        .WhereIf(input.MaxGRANT_E_DISBURSED_DATEFilter != null, e => e.GRANT_E_DISBURSED_DATE <= input.MaxGRANT_E_DISBURSED_DATEFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_E_DISBURSED_BYFilter), e => e.GRANT_E_DISBURSED_BY == input.GRANT_E_DISBURSED_BYFilter)
                        .WhereIf(input.MinGRANT_E_APPROVEDFilter != null, e => e.GRANT_E_APPROVED >= input.MinGRANT_E_APPROVEDFilter)
                        .WhereIf(input.MaxGRANT_E_APPROVEDFilter != null, e => e.GRANT_E_APPROVED <= input.MaxGRANT_E_APPROVEDFilter)
                        .WhereIf(input.MinGRANT_E_DECLINEDFilter != null, e => e.GRANT_E_DECLINED >= input.MinGRANT_E_DECLINEDFilter)
                        .WhereIf(input.MaxGRANT_E_DECLINEDFilter != null, e => e.GRANT_E_DECLINED <= input.MaxGRANT_E_DECLINEDFilter)
                        .WhereIf(input.MinGRANT_E_SWEPTFilter != null, e => e.GRANT_E_SWEPT >= input.MinGRANT_E_SWEPTFilter)
                        .WhereIf(input.MaxGRANT_E_SWEPTFilter != null, e => e.GRANT_E_SWEPT <= input.MaxGRANT_E_SWEPTFilter)
                        .WhereIf(input.MinGRANT_A_OVERRIDDEN_AMOUNT_DIFFFilter != null, e => e.GRANT_A_OVERRIDDEN_AMOUNT_DIFF >= input.MinGRANT_A_OVERRIDDEN_AMOUNT_DIFFFilter)
                        .WhereIf(input.MaxGRANT_A_OVERRIDDEN_AMOUNT_DIFFFilter != null, e => e.GRANT_A_OVERRIDDEN_AMOUNT_DIFF <= input.MaxGRANT_A_OVERRIDDEN_AMOUNT_DIFFFilter)
                        .WhereIf(input.MinGRANT_B_OVERRIDDEN_AMOUNT_DIFFFilter != null, e => e.GRANT_B_OVERRIDDEN_AMOUNT_DIFF >= input.MinGRANT_B_OVERRIDDEN_AMOUNT_DIFFFilter)
                        .WhereIf(input.MaxGRANT_B_OVERRIDDEN_AMOUNT_DIFFFilter != null, e => e.GRANT_B_OVERRIDDEN_AMOUNT_DIFF <= input.MaxGRANT_B_OVERRIDDEN_AMOUNT_DIFFFilter)
                        .WhereIf(input.MinGRANT_C_OVERRIDDEN_AMOUNT_DIFFFilter != null, e => e.GRANT_C_OVERRIDDEN_AMOUNT_DIFF >= input.MinGRANT_C_OVERRIDDEN_AMOUNT_DIFFFilter)
                        .WhereIf(input.MaxGRANT_C_OVERRIDDEN_AMOUNT_DIFFFilter != null, e => e.GRANT_C_OVERRIDDEN_AMOUNT_DIFF <= input.MaxGRANT_C_OVERRIDDEN_AMOUNT_DIFFFilter)
                        .WhereIf(input.MinGRANT_D_OVERRIDDEN_AMOUNT_DIFFFilter != null, e => e.GRANT_D_OVERRIDDEN_AMOUNT_DIFF >= input.MinGRANT_D_OVERRIDDEN_AMOUNT_DIFFFilter)
                        .WhereIf(input.MaxGRANT_D_OVERRIDDEN_AMOUNT_DIFFFilter != null, e => e.GRANT_D_OVERRIDDEN_AMOUNT_DIFF <= input.MaxGRANT_D_OVERRIDDEN_AMOUNT_DIFFFilter)
                        .WhereIf(input.MinGRANT_E_OVERRIDDEN_AMOUNT_DIFFFilter != null, e => e.GRANT_E_OVERRIDDEN_AMOUNT_DIFF >= input.MinGRANT_E_OVERRIDDEN_AMOUNT_DIFFFilter)
                        .WhereIf(input.MaxGRANT_E_OVERRIDDEN_AMOUNT_DIFFFilter != null, e => e.GRANT_E_OVERRIDDEN_AMOUNT_DIFF <= input.MaxGRANT_E_OVERRIDDEN_AMOUNT_DIFFFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PROOF_OF_PAYMENT_RECEIVEDFilter), e => e.PROOF_OF_PAYMENT_RECEIVED == input.PROOF_OF_PAYMENT_RECEIVEDFilter)
                        .WhereIf(input.MinTOTAL_GRANT_APPROVEDFilter != null, e => e.TOTAL_GRANT_APPROVED >= input.MinTOTAL_GRANT_APPROVEDFilter)
                        .WhereIf(input.MaxTOTAL_GRANT_APPROVEDFilter != null, e => e.TOTAL_GRANT_APPROVED <= input.MaxTOTAL_GRANT_APPROVEDFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_A_PROCESSEDFilter), e => e.GRANT_A_PROCESSED == input.GRANT_A_PROCESSEDFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_B_PROCESSEDFilter), e => e.GRANT_B_PROCESSED == input.GRANT_B_PROCESSEDFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_C_PROCESSEDFilter), e => e.GRANT_C_PROCESSED == input.GRANT_C_PROCESSEDFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_D_PROCESSEDFilter), e => e.GRANT_D_PROCESSED == input.GRANT_D_PROCESSEDFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_E_PROCESSEDFilter), e => e.GRANT_E_PROCESSED == input.GRANT_E_PROCESSEDFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_A_PAYMENT_STATUSFilter), e => e.GRANT_A_PAYMENT_STATUS == input.GRANT_A_PAYMENT_STATUSFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_B_PAYMENT_STATUSFilter), e => e.GRANT_B_PAYMENT_STATUS == input.GRANT_B_PAYMENT_STATUSFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_C_PAYMENT_STATUSFilter), e => e.GRANT_C_PAYMENT_STATUS == input.GRANT_C_PAYMENT_STATUSFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_D_PAYMENT_STATUSFilter), e => e.GRANT_D_PAYMENT_STATUS == input.GRANT_D_PAYMENT_STATUSFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_E_PAYMENT_STATUSFilter), e => e.GRANT_E_PAYMENT_STATUS == input.GRANT_E_PAYMENT_STATUSFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_A_CHEQUE_EFT_NOFilter), e => e.GRANT_A_CHEQUE_EFT_NO == input.GRANT_A_CHEQUE_EFT_NOFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_B_CHEQUE_EFT_NOFilter), e => e.GRANT_B_CHEQUE_EFT_NO == input.GRANT_B_CHEQUE_EFT_NOFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_C_CHEQUE_EFT_NOFilter), e => e.GRANT_C_CHEQUE_EFT_NO == input.GRANT_C_CHEQUE_EFT_NOFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_D_CHEQUE_EFT_NOFilter), e => e.GRANT_D_CHEQUE_EFT_NO == input.GRANT_D_CHEQUE_EFT_NOFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_E_CHEQUE_EFT_NOFilter), e => e.GRANT_E_CHEQUE_EFT_NO == input.GRANT_E_CHEQUE_EFT_NOFilter)
                        .WhereIf(input.MinGRANT_EFilter != null, e => e.GRANT_E >= input.MinGRANT_EFilter)
                        .WhereIf(input.MaxGRANT_EFilter != null, e => e.GRANT_E <= input.MaxGRANT_EFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_A_BATCH_NOFilter), e => e.GRANT_A_BATCH_NO == input.GRANT_A_BATCH_NOFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_B_BATCH_NOFilter), e => e.GRANT_B_BATCH_NO == input.GRANT_B_BATCH_NOFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_C_BATCH_NOFilter), e => e.GRANT_C_BATCH_NO == input.GRANT_C_BATCH_NOFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_D_BATCH_NOFilter), e => e.GRANT_D_BATCH_NO == input.GRANT_D_BATCH_NOFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_E_BATCH_NOFilter), e => e.GRANT_E_BATCH_NO == input.GRANT_E_BATCH_NOFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_B_USER_COMMENTFilter), e => e.GRANT_B_USER_COMMENT == input.GRANT_B_USER_COMMENTFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_C_USER_COMMENTFilter), e => e.GRANT_C_USER_COMMENT == input.GRANT_C_USER_COMMENTFilter)
                        .WhereIf(input.MinLEVY_AMOUNT_RECEIVEDFilter != null, e => e.LEVY_AMOUNT_RECEIVED >= input.MinLEVY_AMOUNT_RECEIVEDFilter)
                        .WhereIf(input.MaxLEVY_AMOUNT_RECEIVEDFilter != null, e => e.LEVY_AMOUNT_RECEIVED <= input.MaxLEVY_AMOUNT_RECEIVEDFilter)
                        .WhereIf(input.MinINTEREST_AMOUNT_RECEIVEDFilter != null, e => e.INTEREST_AMOUNT_RECEIVED >= input.MinINTEREST_AMOUNT_RECEIVEDFilter)
                        .WhereIf(input.MaxINTEREST_AMOUNT_RECEIVEDFilter != null, e => e.INTEREST_AMOUNT_RECEIVED <= input.MaxINTEREST_AMOUNT_RECEIVEDFilter)
                        .WhereIf(input.MinPENALTY_AMOUNT_RECEIVEDFilter != null, e => e.PENALTY_AMOUNT_RECEIVED >= input.MinPENALTY_AMOUNT_RECEIVEDFilter)
                        .WhereIf(input.MaxPENALTY_AMOUNT_RECEIVEDFilter != null, e => e.PENALTY_AMOUNT_RECEIVED <= input.MaxPENALTY_AMOUNT_RECEIVEDFilter)
                        .WhereIf(input.MinTOTAL_AMOUNT_RECEIVEDFilter != null, e => e.TOTAL_AMOUNT_RECEIVED >= input.MinTOTAL_AMOUNT_RECEIVEDFilter)
                        .WhereIf(input.MaxTOTAL_AMOUNT_RECEIVEDFilter != null, e => e.TOTAL_AMOUNT_RECEIVED <= input.MaxTOTAL_AMOUNT_RECEIVEDFilter)
                        .WhereIf(input.MinSETA_COMPLETE_ADMIN_LEVYFilter != null, e => e.SETA_COMPLETE_ADMIN_LEVY >= input.MinSETA_COMPLETE_ADMIN_LEVYFilter)
                        .WhereIf(input.MaxSETA_COMPLETE_ADMIN_LEVYFilter != null, e => e.SETA_COMPLETE_ADMIN_LEVY <= input.MaxSETA_COMPLETE_ADMIN_LEVYFilter)
                        .WhereIf(input.MinSETA_COMPLETE_ADMIN_INTERESTFilter != null, e => e.SETA_COMPLETE_ADMIN_INTEREST >= input.MinSETA_COMPLETE_ADMIN_INTERESTFilter)
                        .WhereIf(input.MaxSETA_COMPLETE_ADMIN_INTERESTFilter != null, e => e.SETA_COMPLETE_ADMIN_INTEREST <= input.MaxSETA_COMPLETE_ADMIN_INTERESTFilter)
                        .WhereIf(input.MinSETA_COMPLETE_ADMIN_PENALTYFilter != null, e => e.SETA_COMPLETE_ADMIN_PENALTY >= input.MinSETA_COMPLETE_ADMIN_PENALTYFilter)
                        .WhereIf(input.MaxSETA_COMPLETE_ADMIN_PENALTYFilter != null, e => e.SETA_COMPLETE_ADMIN_PENALTY <= input.MaxSETA_COMPLETE_ADMIN_PENALTYFilter)
                        .WhereIf(input.MinSETA_COMPLETE_ADMIN_TOTALFilter != null, e => e.SETA_COMPLETE_ADMIN_TOTAL >= input.MinSETA_COMPLETE_ADMIN_TOTALFilter)
                        .WhereIf(input.MaxSETA_COMPLETE_ADMIN_TOTALFilter != null, e => e.SETA_COMPLETE_ADMIN_TOTAL <= input.MaxSETA_COMPLETE_ADMIN_TOTALFilter)
                        .WhereIf(input.MinGRANT_MGFilter != null, e => e.GRANT_MG >= input.MinGRANT_MGFilter)
                        .WhereIf(input.MaxGRANT_MGFilter != null, e => e.GRANT_MG <= input.MaxGRANT_MGFilter)
                        .WhereIf(input.MinGRANT_DGFilter != null, e => e.GRANT_DG >= input.MinGRANT_DGFilter)
                        .WhereIf(input.MaxGRANT_DGFilter != null, e => e.GRANT_DG <= input.MaxGRANT_DGFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_MG_STATUSFilter), e => e.GRANT_MG_STATUS == input.GRANT_MG_STATUSFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_MG_STATUS_COMMENTFilter), e => e.GRANT_MG_STATUS_COMMENT == input.GRANT_MG_STATUS_COMMENTFilter)
                        .WhereIf(input.MinGRANT_MG_DISBURSED_DATEFilter != null, e => e.GRANT_MG_DISBURSED_DATE >= input.MinGRANT_MG_DISBURSED_DATEFilter)
                        .WhereIf(input.MaxGRANT_MG_DISBURSED_DATEFilter != null, e => e.GRANT_MG_DISBURSED_DATE <= input.MaxGRANT_MG_DISBURSED_DATEFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_MG_DISBURSED_BYFilter), e => e.GRANT_MG_DISBURSED_BY == input.GRANT_MG_DISBURSED_BYFilter)
                        .WhereIf(input.MinGRANT_MG_APPROVEDFilter != null, e => e.GRANT_MG_APPROVED >= input.MinGRANT_MG_APPROVEDFilter)
                        .WhereIf(input.MaxGRANT_MG_APPROVEDFilter != null, e => e.GRANT_MG_APPROVED <= input.MaxGRANT_MG_APPROVEDFilter)
                        .WhereIf(input.MinGRANT_MG_DECLINEDFilter != null, e => e.GRANT_MG_DECLINED >= input.MinGRANT_MG_DECLINEDFilter)
                        .WhereIf(input.MaxGRANT_MG_DECLINEDFilter != null, e => e.GRANT_MG_DECLINED <= input.MaxGRANT_MG_DECLINEDFilter)
                        .WhereIf(input.MinGRANT_MG_SWEPTFilter != null, e => e.GRANT_MG_SWEPT >= input.MinGRANT_MG_SWEPTFilter)
                        .WhereIf(input.MaxGRANT_MG_SWEPTFilter != null, e => e.GRANT_MG_SWEPT <= input.MaxGRANT_MG_SWEPTFilter)
                        .WhereIf(input.MinGRANT_MG_OVERRIDDEN_AMT_DIFFFilter != null, e => e.GRANT_MG_OVERRIDDEN_AMT_DIFF >= input.MinGRANT_MG_OVERRIDDEN_AMT_DIFFFilter)
                        .WhereIf(input.MaxGRANT_MG_OVERRIDDEN_AMT_DIFFFilter != null, e => e.GRANT_MG_OVERRIDDEN_AMT_DIFF <= input.MaxGRANT_MG_OVERRIDDEN_AMT_DIFFFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_MG_PROCESSEDFilter), e => e.GRANT_MG_PROCESSED == input.GRANT_MG_PROCESSEDFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_MG_PAYMENT_STATUSFilter), e => e.GRANT_MG_PAYMENT_STATUS == input.GRANT_MG_PAYMENT_STATUSFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_MG_CHEQUE_EFT_NOFilter), e => e.GRANT_MG_CHEQUE_EFT_NO == input.GRANT_MG_CHEQUE_EFT_NOFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_MG_BATCH_NOFilter), e => e.GRANT_MG_BATCH_NO == input.GRANT_MG_BATCH_NOFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_MG_USER_COMMENTFilter), e => e.GRANT_MG_USER_COMMENT == input.GRANT_MG_USER_COMMENTFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_DG_STATUSFilter), e => e.GRANT_DG_STATUS == input.GRANT_DG_STATUSFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_DG_STATUS_COMMENTFilter), e => e.GRANT_DG_STATUS_COMMENT == input.GRANT_DG_STATUS_COMMENTFilter)
                        .WhereIf(input.MinGRANT_DG_DISBURSED_DATEFilter != null, e => e.GRANT_DG_DISBURSED_DATE >= input.MinGRANT_DG_DISBURSED_DATEFilter)
                        .WhereIf(input.MaxGRANT_DG_DISBURSED_DATEFilter != null, e => e.GRANT_DG_DISBURSED_DATE <= input.MaxGRANT_DG_DISBURSED_DATEFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_DG_DISBURSED_BYFilter), e => e.GRANT_DG_DISBURSED_BY == input.GRANT_DG_DISBURSED_BYFilter)
                        .WhereIf(input.MinGRANT_DG_APPROVEDFilter != null, e => e.GRANT_DG_APPROVED >= input.MinGRANT_DG_APPROVEDFilter)
                        .WhereIf(input.MaxGRANT_DG_APPROVEDFilter != null, e => e.GRANT_DG_APPROVED <= input.MaxGRANT_DG_APPROVEDFilter)
                        .WhereIf(input.MinGRANT_DG_DECLINEDFilter != null, e => e.GRANT_DG_DECLINED >= input.MinGRANT_DG_DECLINEDFilter)
                        .WhereIf(input.MaxGRANT_DG_DECLINEDFilter != null, e => e.GRANT_DG_DECLINED <= input.MaxGRANT_DG_DECLINEDFilter)
                        .WhereIf(input.MinGRANT_DG_SWEPTFilter != null, e => e.GRANT_DG_SWEPT >= input.MinGRANT_DG_SWEPTFilter)
                        .WhereIf(input.MaxGRANT_DG_SWEPTFilter != null, e => e.GRANT_DG_SWEPT <= input.MaxGRANT_DG_SWEPTFilter)
                        .WhereIf(input.MinGRANT_DG_OVERRIDDEN_AMT_DIFFFilter != null, e => e.GRANT_DG_OVERRIDDEN_AMT_DIFF >= input.MinGRANT_DG_OVERRIDDEN_AMT_DIFFFilter)
                        .WhereIf(input.MaxGRANT_DG_OVERRIDDEN_AMT_DIFFFilter != null, e => e.GRANT_DG_OVERRIDDEN_AMT_DIFF <= input.MaxGRANT_DG_OVERRIDDEN_AMT_DIFFFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_DG_PROCESSEDFilter), e => e.GRANT_DG_PROCESSED == input.GRANT_DG_PROCESSEDFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_DG_PAYMENT_STATUSFilter), e => e.GRANT_DG_PAYMENT_STATUS == input.GRANT_DG_PAYMENT_STATUSFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_DG_CHEQUE_EFT_NOFilter), e => e.GRANT_DG_CHEQUE_EFT_NO == input.GRANT_DG_CHEQUE_EFT_NOFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_DG_BATCH_NOFilter), e => e.GRANT_DG_BATCH_NO == input.GRANT_DG_BATCH_NOFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GRANT_DG_USER_COMMENTFilter), e => e.GRANT_DG_USER_COMMENT == input.GRANT_DG_USER_COMMENTFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.statusOneFilter), e => e.statusOne == input.statusOneFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.statusTwoFilter), e => e.statusTwo == input.statusTwoFilter);

            var query = (from o in filteredLEVY_PAYMENTSs
                         select new GetLEVY_PAYMENTSForViewDto()
                         {
                             LEVY_PAYMENTS = new LEVY_PAYMENTSDto
                             {
                                 PERIOD = o.PERIOD,
                                 SDL_NO = o.SDL_NO,
                                 RECEIPT_DATE_SARS = o.RECEIPT_DATE_SARS,
                                 LEVY_AMOUNT = o.LEVY_AMOUNT,
                                 PENALTY_AMOUNT = o.PENALTY_AMOUNT,
                                 INTEREST_AMOUNT = o.INTEREST_AMOUNT,
                                 TOTAL_AMOUNT = o.TOTAL_AMOUNT,
                                 NO_SDL201_OUTSTANDING = o.NO_SDL201_OUTSTANDING,
                                 DEBT_OUTSTANDING_AMOUNT = o.DEBT_OUTSTANDING_AMOUNT,
                                 SARS_LEVY = o.SARS_LEVY,
                                 SARS_INTEREST = o.SARS_INTEREST,
                                 SARS_PENALTY = o.SARS_PENALTY,
                                 NSF_LEVY = o.NSF_LEVY,
                                 NSF_INTEREST = o.NSF_INTEREST,
                                 NSF_PENALTY = o.NSF_PENALTY,
                                 SETA_SETUP_LEVY = o.SETA_SETUP_LEVY,
                                 SETA_SETUP_INTEREST = o.SETA_SETUP_INTEREST,
                                 SETA_SETUP_PENALTY = o.SETA_SETUP_PENALTY,
                                 SETA_ADMIN_LEVY = o.SETA_ADMIN_LEVY,
                                 SETA_ADMIN_INTEREST = o.SETA_ADMIN_INTEREST,
                                 SETA_ADMIN_PENALTY = o.SETA_ADMIN_PENALTY,
                                 UNAPPORTIONED_LEVY = o.UNAPPORTIONED_LEVY,
                                 UNAPPORTIONED_INTEREST = o.UNAPPORTIONED_INTEREST,
                                 UNAPPORTIONED_PENALTY = o.UNAPPORTIONED_PENALTY,
                                 GRANT_A = o.GRANT_A,
                                 GRANT_B = o.GRANT_B,
                                 GRANT_C = o.GRANT_C,
                                 GRANT_D = o.GRANT_D,
                                 FINANCIAL_YEAR = o.FINANCIAL_YEAR,
                                 LEVY_TYPE = o.LEVY_TYPE,
                                 SETA_CODE = o.SETA_CODE,
                                 GRANT_A_STATUS = o.GRANT_A_STATUS,
                                 GRANT_A_STATUS_COMMENT = o.GRANT_A_STATUS_COMMENT,
                                 GRANT_A_DISBURSED_DATE = o.GRANT_A_DISBURSED_DATE,
                                 GRANT_A_DISBURSED_BY = o.GRANT_A_DISBURSED_BY,
                                 GRANT_A_APPROVED = o.GRANT_A_APPROVED,
                                 GRANT_A_DECLINED = o.GRANT_A_DECLINED,
                                 GRANT_A_SWEPT = o.GRANT_A_SWEPT,
                                 GRANT_B_STATUS = o.GRANT_B_STATUS,
                                 GRANT_B_STATUS_COMMENT = o.GRANT_B_STATUS_COMMENT,
                                 GRANT_B_DISBURSED_DATE = o.GRANT_B_DISBURSED_DATE,
                                 GRANT_B_DISBURSED_BY = o.GRANT_B_DISBURSED_BY,
                                 GRANT_B_APPROVED = o.GRANT_B_APPROVED,
                                 GRANT_B_DECLINED = o.GRANT_B_DECLINED,
                                 GRANT_B_SWEPT = o.GRANT_B_SWEPT,
                                 GRANT_C_STATUS = o.GRANT_C_STATUS,
                                 GRANT_C_STATUS_COMMENT = o.GRANT_C_STATUS_COMMENT,
                                 GRANT_C_DISBURSED_DATE = o.GRANT_C_DISBURSED_DATE,
                                 GRANT_C_DISBURSED_BY = o.GRANT_C_DISBURSED_BY,
                                 GRANT_C_APPROVED = o.GRANT_C_APPROVED,
                                 GRANT_C_DECLINED = o.GRANT_C_DECLINED,
                                 GRANT_C_SWEPT = o.GRANT_C_SWEPT,
                                 GRANT_D_STATUS = o.GRANT_D_STATUS,
                                 GRANT_D_STATUS_COMMENT = o.GRANT_D_STATUS_COMMENT,
                                 GRANT_D_DISBURSED_DATE = o.GRANT_D_DISBURSED_DATE,
                                 GRANT_D_DISBURSED_BY = o.GRANT_D_DISBURSED_BY,
                                 GRANT_D_APPROVED = o.GRANT_D_APPROVED,
                                 GRANT_D_DECLINED = o.GRANT_D_DECLINED,
                                 GRANT_D_SWEPT = o.GRANT_D_SWEPT,
                                 GRANT_E_STATUS = o.GRANT_E_STATUS,
                                 GRANT_E_STATUS_COMMENT = o.GRANT_E_STATUS_COMMENT,
                                 GRANT_E_DISBURSED_DATE = o.GRANT_E_DISBURSED_DATE,
                                 GRANT_E_DISBURSED_BY = o.GRANT_E_DISBURSED_BY,
                                 GRANT_E_APPROVED = o.GRANT_E_APPROVED,
                                 GRANT_E_DECLINED = o.GRANT_E_DECLINED,
                                 GRANT_E_SWEPT = o.GRANT_E_SWEPT,
                                 GRANT_A_OVERRIDDEN_AMOUNT_DIFF = o.GRANT_A_OVERRIDDEN_AMOUNT_DIFF,
                                 GRANT_B_OVERRIDDEN_AMOUNT_DIFF = o.GRANT_B_OVERRIDDEN_AMOUNT_DIFF,
                                 GRANT_C_OVERRIDDEN_AMOUNT_DIFF = o.GRANT_C_OVERRIDDEN_AMOUNT_DIFF,
                                 GRANT_D_OVERRIDDEN_AMOUNT_DIFF = o.GRANT_D_OVERRIDDEN_AMOUNT_DIFF,
                                 GRANT_E_OVERRIDDEN_AMOUNT_DIFF = o.GRANT_E_OVERRIDDEN_AMOUNT_DIFF,
                                 PROOF_OF_PAYMENT_RECEIVED = o.PROOF_OF_PAYMENT_RECEIVED,
                                 TOTAL_GRANT_APPROVED = o.TOTAL_GRANT_APPROVED,
                                 GRANT_A_PROCESSED = o.GRANT_A_PROCESSED,
                                 GRANT_B_PROCESSED = o.GRANT_B_PROCESSED,
                                 GRANT_C_PROCESSED = o.GRANT_C_PROCESSED,
                                 GRANT_D_PROCESSED = o.GRANT_D_PROCESSED,
                                 GRANT_E_PROCESSED = o.GRANT_E_PROCESSED,
                                 GRANT_A_PAYMENT_STATUS = o.GRANT_A_PAYMENT_STATUS,
                                 GRANT_B_PAYMENT_STATUS = o.GRANT_B_PAYMENT_STATUS,
                                 GRANT_C_PAYMENT_STATUS = o.GRANT_C_PAYMENT_STATUS,
                                 GRANT_D_PAYMENT_STATUS = o.GRANT_D_PAYMENT_STATUS,
                                 GRANT_E_PAYMENT_STATUS = o.GRANT_E_PAYMENT_STATUS,
                                 GRANT_A_CHEQUE_EFT_NO = o.GRANT_A_CHEQUE_EFT_NO,
                                 GRANT_B_CHEQUE_EFT_NO = o.GRANT_B_CHEQUE_EFT_NO,
                                 GRANT_C_CHEQUE_EFT_NO = o.GRANT_C_CHEQUE_EFT_NO,
                                 GRANT_D_CHEQUE_EFT_NO = o.GRANT_D_CHEQUE_EFT_NO,
                                 GRANT_E_CHEQUE_EFT_NO = o.GRANT_E_CHEQUE_EFT_NO,
                                 GRANT_E = o.GRANT_E,
                                 GRANT_A_BATCH_NO = o.GRANT_A_BATCH_NO,
                                 GRANT_B_BATCH_NO = o.GRANT_B_BATCH_NO,
                                 GRANT_C_BATCH_NO = o.GRANT_C_BATCH_NO,
                                 GRANT_D_BATCH_NO = o.GRANT_D_BATCH_NO,
                                 GRANT_E_BATCH_NO = o.GRANT_E_BATCH_NO,
                                 GRANT_B_USER_COMMENT = o.GRANT_B_USER_COMMENT,
                                 GRANT_C_USER_COMMENT = o.GRANT_C_USER_COMMENT,
                                 LEVY_AMOUNT_RECEIVED = o.LEVY_AMOUNT_RECEIVED,
                                 INTEREST_AMOUNT_RECEIVED = o.INTEREST_AMOUNT_RECEIVED,
                                 PENALTY_AMOUNT_RECEIVED = o.PENALTY_AMOUNT_RECEIVED,
                                 TOTAL_AMOUNT_RECEIVED = o.TOTAL_AMOUNT_RECEIVED,
                                 SETA_COMPLETE_ADMIN_LEVY = o.SETA_COMPLETE_ADMIN_LEVY,
                                 SETA_COMPLETE_ADMIN_INTEREST = o.SETA_COMPLETE_ADMIN_INTEREST,
                                 SETA_COMPLETE_ADMIN_PENALTY = o.SETA_COMPLETE_ADMIN_PENALTY,
                                 SETA_COMPLETE_ADMIN_TOTAL = o.SETA_COMPLETE_ADMIN_TOTAL,
                                 GRANT_MG = o.GRANT_MG,
                                 GRANT_DG = o.GRANT_DG,
                                 GRANT_MG_STATUS = o.GRANT_MG_STATUS,
                                 GRANT_MG_STATUS_COMMENT = o.GRANT_MG_STATUS_COMMENT,
                                 GRANT_MG_DISBURSED_DATE = o.GRANT_MG_DISBURSED_DATE,
                                 GRANT_MG_DISBURSED_BY = o.GRANT_MG_DISBURSED_BY,
                                 GRANT_MG_APPROVED = o.GRANT_MG_APPROVED,
                                 GRANT_MG_DECLINED = o.GRANT_MG_DECLINED,
                                 GRANT_MG_SWEPT = o.GRANT_MG_SWEPT,
                                 GRANT_MG_OVERRIDDEN_AMT_DIFF = o.GRANT_MG_OVERRIDDEN_AMT_DIFF,
                                 GRANT_MG_PROCESSED = o.GRANT_MG_PROCESSED,
                                 GRANT_MG_PAYMENT_STATUS = o.GRANT_MG_PAYMENT_STATUS,
                                 GRANT_MG_CHEQUE_EFT_NO = o.GRANT_MG_CHEQUE_EFT_NO,
                                 GRANT_MG_BATCH_NO = o.GRANT_MG_BATCH_NO,
                                 GRANT_MG_USER_COMMENT = o.GRANT_MG_USER_COMMENT,
                                 GRANT_DG_STATUS = o.GRANT_DG_STATUS,
                                 GRANT_DG_STATUS_COMMENT = o.GRANT_DG_STATUS_COMMENT,
                                 GRANT_DG_DISBURSED_DATE = o.GRANT_DG_DISBURSED_DATE,
                                 GRANT_DG_DISBURSED_BY = o.GRANT_DG_DISBURSED_BY,
                                 GRANT_DG_APPROVED = o.GRANT_DG_APPROVED,
                                 GRANT_DG_DECLINED = o.GRANT_DG_DECLINED,
                                 GRANT_DG_SWEPT = o.GRANT_DG_SWEPT,
                                 GRANT_DG_OVERRIDDEN_AMT_DIFF = o.GRANT_DG_OVERRIDDEN_AMT_DIFF,
                                 GRANT_DG_PROCESSED = o.GRANT_DG_PROCESSED,
                                 GRANT_DG_PAYMENT_STATUS = o.GRANT_DG_PAYMENT_STATUS,
                                 GRANT_DG_CHEQUE_EFT_NO = o.GRANT_DG_CHEQUE_EFT_NO,
                                 GRANT_DG_BATCH_NO = o.GRANT_DG_BATCH_NO,
                                 GRANT_DG_USER_COMMENT = o.GRANT_DG_USER_COMMENT,
                                 statusOne = o.statusOne,
                                 statusTwo = o.statusTwo,
                                 Id = o.Id
                             }
                         });


            var levY_PAYMENTSListDtos = await query.ToListAsync();

            return _levY_PAYMENTSsExcelExporter.ExportToFile(levY_PAYMENTSListDtos);
        }


    }
}