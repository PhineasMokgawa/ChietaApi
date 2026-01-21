using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using CHIETAMIS.DataExporting.Excel.EpPlus;
using CHIETAMIS.LEVYPAYMENTS.Dtos;
using CHIETAMIS.Dto;
using CHIETAMIS.Storage;
using CHEITAMIS.Dto;
using CHIETAMIS.DataExporting.Excel.EpPlus;
using CHIETAMIS.LEVYPAYMENTS.Dtos;
using CHIETAMIS.LEVYPAYMENTS.Exporting;
using CHIETAMIS.Storage;

namespace CHIETAMIS.LEVYPAYMENTS.Exporting
{
    public class LEVY_PAYMENTSsExcelExporter : EpPlusExcelExporterBase, ILEVY_PAYMENTSsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public LEVY_PAYMENTSsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetLEVY_PAYMENTSForViewDto> levY_PAYMENTSs)
        {
            return CreateExcelPackage(
                "LEVY_PAYMENTSs.xlsx",
                excelPackage =>
                {

                    var sheet = excelPackage.Workbook.Worksheets.Add(L("LEVY_PAYMENTSs"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("PERIOD"),
                        L("SDL_NO"),
                        L("RECEIPT_DATE_SARS"),
                        L("LEVY_AMOUNT"),
                        L("PENALTY_AMOUNT"),
                        L("INTEREST_AMOUNT"),
                        L("TOTAL_AMOUNT"),
                        L("NO_SDL201_OUTSTANDING"),
                        L("DEBT_OUTSTANDING_AMOUNT"),
                        L("SARS_LEVY"),
                        L("SARS_INTEREST"),
                        L("SARS_PENALTY"),
                        L("NSF_LEVY"),
                        L("NSF_INTEREST"),
                        L("NSF_PENALTY"),
                        L("SETA_SETUP_LEVY"),
                        L("SETA_SETUP_INTEREST"),
                        L("SETA_SETUP_PENALTY"),
                        L("SETA_ADMIN_LEVY"),
                        L("SETA_ADMIN_INTEREST"),
                        L("SETA_ADMIN_PENALTY"),
                        L("UNAPPORTIONED_LEVY"),
                        L("UNAPPORTIONED_INTEREST"),
                        L("UNAPPORTIONED_PENALTY"),
                        L("GRANT_A"),
                        L("GRANT_B"),
                        L("GRANT_C"),
                        L("GRANT_D"),
                        L("FINANCIAL_YEAR"),
                        L("LEVY_TYPE"),
                        L("SETA_CODE"),
                        L("GRANT_A_STATUS"),
                        L("GRANT_A_STATUS_COMMENT"),
                        L("GRANT_A_DISBURSED_DATE"),
                        L("GRANT_A_DISBURSED_BY"),
                        L("GRANT_A_APPROVED"),
                        L("GRANT_A_DECLINED"),
                        L("GRANT_A_SWEPT"),
                        L("GRANT_B_STATUS"),
                        L("GRANT_B_STATUS_COMMENT"),
                        L("GRANT_B_DISBURSED_DATE"),
                        L("GRANT_B_DISBURSED_BY"),
                        L("GRANT_B_APPROVED"),
                        L("GRANT_B_DECLINED"),
                        L("GRANT_B_SWEPT"),
                        L("GRANT_C_STATUS"),
                        L("GRANT_C_STATUS_COMMENT"),
                        L("GRANT_C_DISBURSED_DATE"),
                        L("GRANT_C_DISBURSED_BY"),
                        L("GRANT_C_APPROVED"),
                        L("GRANT_C_DECLINED"),
                        L("GRANT_C_SWEPT"),
                        L("GRANT_D_STATUS"),
                        L("GRANT_D_STATUS_COMMENT"),
                        L("GRANT_D_DISBURSED_DATE"),
                        L("GRANT_D_DISBURSED_BY"),
                        L("GRANT_D_APPROVED"),
                        L("GRANT_D_DECLINED"),
                        L("GRANT_D_SWEPT"),
                        L("GRANT_E_STATUS"),
                        L("GRANT_E_STATUS_COMMENT"),
                        L("GRANT_E_DISBURSED_DATE"),
                        L("GRANT_E_DISBURSED_BY"),
                        L("GRANT_E_APPROVED"),
                        L("GRANT_E_DECLINED"),
                        L("GRANT_E_SWEPT"),
                        L("GRANT_A_OVERRIDDEN_AMOUNT_DIFF"),
                        L("GRANT_B_OVERRIDDEN_AMOUNT_DIFF"),
                        L("GRANT_C_OVERRIDDEN_AMOUNT_DIFF"),
                        L("GRANT_D_OVERRIDDEN_AMOUNT_DIFF"),
                        L("GRANT_E_OVERRIDDEN_AMOUNT_DIFF"),
                        L("PROOF_OF_PAYMENT_RECEIVED"),
                        L("TOTAL_GRANT_APPROVED"),
                        L("GRANT_A_PROCESSED"),
                        L("GRANT_B_PROCESSED"),
                        L("GRANT_C_PROCESSED"),
                        L("GRANT_D_PROCESSED"),
                        L("GRANT_E_PROCESSED"),
                        L("GRANT_A_PAYMENT_STATUS"),
                        L("GRANT_B_PAYMENT_STATUS"),
                        L("GRANT_C_PAYMENT_STATUS"),
                        L("GRANT_D_PAYMENT_STATUS"),
                        L("GRANT_E_PAYMENT_STATUS"),
                        L("GRANT_A_CHEQUE_EFT_NO"),
                        L("GRANT_B_CHEQUE_EFT_NO"),
                        L("GRANT_C_CHEQUE_EFT_NO"),
                        L("GRANT_D_CHEQUE_EFT_NO"),
                        L("GRANT_E_CHEQUE_EFT_NO"),
                        L("GRANT_E"),
                        L("GRANT_A_BATCH_NO"),
                        L("GRANT_B_BATCH_NO"),
                        L("GRANT_C_BATCH_NO"),
                        L("GRANT_D_BATCH_NO"),
                        L("GRANT_E_BATCH_NO"),
                        L("GRANT_B_USER_COMMENT"),
                        L("GRANT_C_USER_COMMENT"),
                        L("LEVY_AMOUNT_RECEIVED"),
                        L("INTEREST_AMOUNT_RECEIVED"),
                        L("PENALTY_AMOUNT_RECEIVED"),
                        L("TOTAL_AMOUNT_RECEIVED"),
                        L("SETA_COMPLETE_ADMIN_LEVY"),
                        L("SETA_COMPLETE_ADMIN_INTEREST"),
                        L("SETA_COMPLETE_ADMIN_PENALTY"),
                        L("SETA_COMPLETE_ADMIN_TOTAL"),
                        L("GRANT_MG"),
                        L("GRANT_DG"),
                        L("GRANT_MG_STATUS"),
                        L("GRANT_MG_STATUS_COMMENT"),
                        L("GRANT_MG_DISBURSED_DATE"),
                        L("GRANT_MG_DISBURSED_BY"),
                        L("GRANT_MG_APPROVED"),
                        L("GRANT_MG_DECLINED"),
                        L("GRANT_MG_SWEPT"),
                        L("GRANT_MG_OVERRIDDEN_AMT_DIFF"),
                        L("GRANT_MG_PROCESSED"),
                        L("GRANT_MG_PAYMENT_STATUS"),
                        L("GRANT_MG_CHEQUE_EFT_NO"),
                        L("GRANT_MG_BATCH_NO"),
                        L("GRANT_MG_USER_COMMENT"),
                        L("GRANT_DG_STATUS"),
                        L("GRANT_DG_STATUS_COMMENT"),
                        L("GRANT_DG_DISBURSED_DATE"),
                        L("GRANT_DG_DISBURSED_BY"),
                        L("GRANT_DG_APPROVED"),
                        L("GRANT_DG_DECLINED"),
                        L("GRANT_DG_SWEPT"),
                        L("GRANT_DG_OVERRIDDEN_AMT_DIFF"),
                        L("GRANT_DG_PROCESSED"),
                        L("GRANT_DG_PAYMENT_STATUS"),
                        L("GRANT_DG_CHEQUE_EFT_NO"),
                        L("GRANT_DG_BATCH_NO"),
                        L("GRANT_DG_USER_COMMENT"),
                        L("statusOne"),
                        L("statusTwo")
                        );

                    AddObjects(
                        sheet, 2, levY_PAYMENTSs,
                        _ => _.LEVY_PAYMENTS.PERIOD,
                        _ => _.LEVY_PAYMENTS.SDL_NO,
                        _ => _timeZoneConverter.Convert(_.LEVY_PAYMENTS.RECEIPT_DATE_SARS, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _.LEVY_PAYMENTS.LEVY_AMOUNT,
                        _ => _.LEVY_PAYMENTS.PENALTY_AMOUNT,
                        _ => _.LEVY_PAYMENTS.INTEREST_AMOUNT,
                        _ => _.LEVY_PAYMENTS.TOTAL_AMOUNT,
                        _ => _.LEVY_PAYMENTS.NO_SDL201_OUTSTANDING,
                        _ => _.LEVY_PAYMENTS.DEBT_OUTSTANDING_AMOUNT,
                        _ => _.LEVY_PAYMENTS.SARS_LEVY,
                        _ => _.LEVY_PAYMENTS.SARS_INTEREST,
                        _ => _.LEVY_PAYMENTS.SARS_PENALTY,
                        _ => _.LEVY_PAYMENTS.NSF_LEVY,
                        _ => _.LEVY_PAYMENTS.NSF_INTEREST,
                        _ => _.LEVY_PAYMENTS.NSF_PENALTY,
                        _ => _.LEVY_PAYMENTS.SETA_SETUP_LEVY,
                        _ => _.LEVY_PAYMENTS.SETA_SETUP_INTEREST,
                        _ => _.LEVY_PAYMENTS.SETA_SETUP_PENALTY,
                        _ => _.LEVY_PAYMENTS.SETA_ADMIN_LEVY,
                        _ => _.LEVY_PAYMENTS.SETA_ADMIN_INTEREST,
                        _ => _.LEVY_PAYMENTS.SETA_ADMIN_PENALTY,
                        _ => _.LEVY_PAYMENTS.UNAPPORTIONED_LEVY,
                        _ => _.LEVY_PAYMENTS.UNAPPORTIONED_INTEREST,
                        _ => _.LEVY_PAYMENTS.UNAPPORTIONED_PENALTY,
                        _ => _.LEVY_PAYMENTS.GRANT_A,
                        _ => _.LEVY_PAYMENTS.GRANT_B,
                        _ => _.LEVY_PAYMENTS.GRANT_C,
                        _ => _.LEVY_PAYMENTS.GRANT_D,
                        _ => _.LEVY_PAYMENTS.FINANCIAL_YEAR,
                        _ => _.LEVY_PAYMENTS.LEVY_TYPE,
                        _ => _.LEVY_PAYMENTS.SETA_CODE,
                        _ => _.LEVY_PAYMENTS.GRANT_A_STATUS,
                        _ => _.LEVY_PAYMENTS.GRANT_A_STATUS_COMMENT,
                        _ => _timeZoneConverter.Convert(_.LEVY_PAYMENTS.GRANT_A_DISBURSED_DATE, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _.LEVY_PAYMENTS.GRANT_A_DISBURSED_BY,
                        _ => _.LEVY_PAYMENTS.GRANT_A_APPROVED,
                        _ => _.LEVY_PAYMENTS.GRANT_A_DECLINED,
                        _ => _.LEVY_PAYMENTS.GRANT_A_SWEPT,
                        _ => _.LEVY_PAYMENTS.GRANT_B_STATUS,
                        _ => _.LEVY_PAYMENTS.GRANT_B_STATUS_COMMENT,
                        _ => _timeZoneConverter.Convert(_.LEVY_PAYMENTS.GRANT_B_DISBURSED_DATE, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _.LEVY_PAYMENTS.GRANT_B_DISBURSED_BY,
                        _ => _.LEVY_PAYMENTS.GRANT_B_APPROVED,
                        _ => _.LEVY_PAYMENTS.GRANT_B_DECLINED,
                        _ => _.LEVY_PAYMENTS.GRANT_B_SWEPT,
                        _ => _.LEVY_PAYMENTS.GRANT_C_STATUS,
                        _ => _.LEVY_PAYMENTS.GRANT_C_STATUS_COMMENT,
                        _ => _timeZoneConverter.Convert(_.LEVY_PAYMENTS.GRANT_C_DISBURSED_DATE, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _.LEVY_PAYMENTS.GRANT_C_DISBURSED_BY,
                        _ => _.LEVY_PAYMENTS.GRANT_C_APPROVED,
                        _ => _.LEVY_PAYMENTS.GRANT_C_DECLINED,
                        _ => _.LEVY_PAYMENTS.GRANT_C_SWEPT,
                        _ => _.LEVY_PAYMENTS.GRANT_D_STATUS,
                        _ => _.LEVY_PAYMENTS.GRANT_D_STATUS_COMMENT,
                        _ => _timeZoneConverter.Convert(_.LEVY_PAYMENTS.GRANT_D_DISBURSED_DATE, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _.LEVY_PAYMENTS.GRANT_D_DISBURSED_BY,
                        _ => _.LEVY_PAYMENTS.GRANT_D_APPROVED,
                        _ => _.LEVY_PAYMENTS.GRANT_D_DECLINED,
                        _ => _.LEVY_PAYMENTS.GRANT_D_SWEPT,
                        _ => _.LEVY_PAYMENTS.GRANT_E_STATUS,
                        _ => _.LEVY_PAYMENTS.GRANT_E_STATUS_COMMENT,
                        _ => _timeZoneConverter.Convert(_.LEVY_PAYMENTS.GRANT_E_DISBURSED_DATE, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _.LEVY_PAYMENTS.GRANT_E_DISBURSED_BY,
                        _ => _.LEVY_PAYMENTS.GRANT_E_APPROVED,
                        _ => _.LEVY_PAYMENTS.GRANT_E_DECLINED,
                        _ => _.LEVY_PAYMENTS.GRANT_E_SWEPT,
                        _ => _.LEVY_PAYMENTS.GRANT_A_OVERRIDDEN_AMOUNT_DIFF,
                        _ => _.LEVY_PAYMENTS.GRANT_B_OVERRIDDEN_AMOUNT_DIFF,
                        _ => _.LEVY_PAYMENTS.GRANT_C_OVERRIDDEN_AMOUNT_DIFF,
                        _ => _.LEVY_PAYMENTS.GRANT_D_OVERRIDDEN_AMOUNT_DIFF,
                        _ => _.LEVY_PAYMENTS.GRANT_E_OVERRIDDEN_AMOUNT_DIFF,
                        _ => _.LEVY_PAYMENTS.PROOF_OF_PAYMENT_RECEIVED,
                        _ => _.LEVY_PAYMENTS.TOTAL_GRANT_APPROVED,
                        _ => _.LEVY_PAYMENTS.GRANT_A_PROCESSED,
                        _ => _.LEVY_PAYMENTS.GRANT_B_PROCESSED,
                        _ => _.LEVY_PAYMENTS.GRANT_C_PROCESSED,
                        _ => _.LEVY_PAYMENTS.GRANT_D_PROCESSED,
                        _ => _.LEVY_PAYMENTS.GRANT_E_PROCESSED,
                        _ => _.LEVY_PAYMENTS.GRANT_A_PAYMENT_STATUS,
                        _ => _.LEVY_PAYMENTS.GRANT_B_PAYMENT_STATUS,
                        _ => _.LEVY_PAYMENTS.GRANT_C_PAYMENT_STATUS,
                        _ => _.LEVY_PAYMENTS.GRANT_D_PAYMENT_STATUS,
                        _ => _.LEVY_PAYMENTS.GRANT_E_PAYMENT_STATUS,
                        _ => _.LEVY_PAYMENTS.GRANT_A_CHEQUE_EFT_NO,
                        _ => _.LEVY_PAYMENTS.GRANT_B_CHEQUE_EFT_NO,
                        _ => _.LEVY_PAYMENTS.GRANT_C_CHEQUE_EFT_NO,
                        _ => _.LEVY_PAYMENTS.GRANT_D_CHEQUE_EFT_NO,
                        _ => _.LEVY_PAYMENTS.GRANT_E_CHEQUE_EFT_NO,
                        _ => _.LEVY_PAYMENTS.GRANT_E,
                        _ => _.LEVY_PAYMENTS.GRANT_A_BATCH_NO,
                        _ => _.LEVY_PAYMENTS.GRANT_B_BATCH_NO,
                        _ => _.LEVY_PAYMENTS.GRANT_C_BATCH_NO,
                        _ => _.LEVY_PAYMENTS.GRANT_D_BATCH_NO,
                        _ => _.LEVY_PAYMENTS.GRANT_E_BATCH_NO,
                        _ => _.LEVY_PAYMENTS.GRANT_B_USER_COMMENT,
                        _ => _.LEVY_PAYMENTS.GRANT_C_USER_COMMENT,
                        _ => _.LEVY_PAYMENTS.LEVY_AMOUNT_RECEIVED,
                        _ => _.LEVY_PAYMENTS.INTEREST_AMOUNT_RECEIVED,
                        _ => _.LEVY_PAYMENTS.PENALTY_AMOUNT_RECEIVED,
                        _ => _.LEVY_PAYMENTS.TOTAL_AMOUNT_RECEIVED,
                        _ => _.LEVY_PAYMENTS.SETA_COMPLETE_ADMIN_LEVY,
                        _ => _.LEVY_PAYMENTS.SETA_COMPLETE_ADMIN_INTEREST,
                        _ => _.LEVY_PAYMENTS.SETA_COMPLETE_ADMIN_PENALTY,
                        _ => _.LEVY_PAYMENTS.SETA_COMPLETE_ADMIN_TOTAL,
                        _ => _.LEVY_PAYMENTS.GRANT_MG,
                        _ => _.LEVY_PAYMENTS.GRANT_DG,
                        _ => _.LEVY_PAYMENTS.GRANT_MG_STATUS,
                        _ => _.LEVY_PAYMENTS.GRANT_MG_STATUS_COMMENT,
                        _ => _timeZoneConverter.Convert(_.LEVY_PAYMENTS.GRANT_MG_DISBURSED_DATE, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _.LEVY_PAYMENTS.GRANT_MG_DISBURSED_BY,
                        _ => _.LEVY_PAYMENTS.GRANT_MG_APPROVED,
                        _ => _.LEVY_PAYMENTS.GRANT_MG_DECLINED,
                        _ => _.LEVY_PAYMENTS.GRANT_MG_SWEPT,
                        _ => _.LEVY_PAYMENTS.GRANT_MG_OVERRIDDEN_AMT_DIFF,
                        _ => _.LEVY_PAYMENTS.GRANT_MG_PROCESSED,
                        _ => _.LEVY_PAYMENTS.GRANT_MG_PAYMENT_STATUS,
                        _ => _.LEVY_PAYMENTS.GRANT_MG_CHEQUE_EFT_NO,
                        _ => _.LEVY_PAYMENTS.GRANT_MG_BATCH_NO,
                        _ => _.LEVY_PAYMENTS.GRANT_MG_USER_COMMENT,
                        _ => _.LEVY_PAYMENTS.GRANT_DG_STATUS,
                        _ => _.LEVY_PAYMENTS.GRANT_DG_STATUS_COMMENT,
                        _ => _timeZoneConverter.Convert(_.LEVY_PAYMENTS.GRANT_DG_DISBURSED_DATE, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _.LEVY_PAYMENTS.GRANT_DG_DISBURSED_BY,
                        _ => _.LEVY_PAYMENTS.GRANT_DG_APPROVED,
                        _ => _.LEVY_PAYMENTS.GRANT_DG_DECLINED,
                        _ => _.LEVY_PAYMENTS.GRANT_DG_SWEPT,
                        _ => _.LEVY_PAYMENTS.GRANT_DG_OVERRIDDEN_AMT_DIFF,
                        _ => _.LEVY_PAYMENTS.GRANT_DG_PROCESSED,
                        _ => _.LEVY_PAYMENTS.GRANT_DG_PAYMENT_STATUS,
                        _ => _.LEVY_PAYMENTS.GRANT_DG_CHEQUE_EFT_NO,
                        _ => _.LEVY_PAYMENTS.GRANT_DG_BATCH_NO,
                        _ => _.LEVY_PAYMENTS.GRANT_DG_USER_COMMENT,
                        _ => _.LEVY_PAYMENTS.statusOne,
                        _ => _.LEVY_PAYMENTS.statusTwo
                        );

                    var receipT_DATE_SARSColumn = sheet.Column(3);
                    receipT_DATE_SARSColumn.Style.Numberformat.Format = "yyyy-mm-dd";
                    receipT_DATE_SARSColumn.AutoFit();
                    var granT_A_DISBURSED_DATEColumn = sheet.Column(34);
                    granT_A_DISBURSED_DATEColumn.Style.Numberformat.Format = "yyyy-mm-dd";
                    granT_A_DISBURSED_DATEColumn.AutoFit();
                    var granT_B_DISBURSED_DATEColumn = sheet.Column(41);
                    granT_B_DISBURSED_DATEColumn.Style.Numberformat.Format = "yyyy-mm-dd";
                    granT_B_DISBURSED_DATEColumn.AutoFit();
                    var granT_C_DISBURSED_DATEColumn = sheet.Column(48);
                    granT_C_DISBURSED_DATEColumn.Style.Numberformat.Format = "yyyy-mm-dd";
                    granT_C_DISBURSED_DATEColumn.AutoFit();
                    var granT_D_DISBURSED_DATEColumn = sheet.Column(55);
                    granT_D_DISBURSED_DATEColumn.Style.Numberformat.Format = "yyyy-mm-dd";
                    granT_D_DISBURSED_DATEColumn.AutoFit();
                    var granT_E_DISBURSED_DATEColumn = sheet.Column(62);
                    granT_E_DISBURSED_DATEColumn.Style.Numberformat.Format = "yyyy-mm-dd";
                    granT_E_DISBURSED_DATEColumn.AutoFit();
                    var granT_MG_DISBURSED_DATEColumn = sheet.Column(109);
                    granT_MG_DISBURSED_DATEColumn.Style.Numberformat.Format = "yyyy-mm-dd";
                    granT_MG_DISBURSED_DATEColumn.AutoFit();
                    var granT_DG_DISBURSED_DATEColumn = sheet.Column(122);
                    granT_DG_DISBURSED_DATEColumn.Style.Numberformat.Format = "yyyy-mm-dd";
                    granT_DG_DISBURSED_DATEColumn.AutoFit();


                });
        }
    }
}
