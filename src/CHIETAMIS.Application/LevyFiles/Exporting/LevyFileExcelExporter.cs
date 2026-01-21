using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using CHEITAMIS.Dto;
using CHIETAMIS.DataExporting.Excel.EpPlus;
using CHIETAMIS.LevyFiles.Dtos;
using CHIETAMIS.LevyFiles.Dtos;
using CHIETAMIS.LevyFiles.Exporting;
using CHIETAMIS.Storage;

namespace CHIETAMIS.LevyFiles.Exporting
{
    public class LevyFileExcelExporter : EpPlusExcelExporterBase
    {
        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public LevyFileExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :

         base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetLevyFileForViewDto> LevysFile)
        {
            return CreateExcelPackage(
                "LevysFile.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("LevysFile"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("ZipFileName"),
                        L("DateExtracted"),
                        L("Status"),
                        L("ImportInProgress"),
                        L("CommitInProgress"),
                        L("TransferInYN"),
                        L("TransferOutYN"));

                    AddObjects(
                        sheet, 2, LevysFile,
                        _ => _.LevyFile.ZipFileName,
                        _ => _.LevyFile.DateExtracted,
                        _ => _.LevyFile.Status,
                        _ => _.LevyFile.ImportInProgress,
                        _ => _.LevyFile.CommitInProgress,
                        _ => _.LevyFile.TransferInYN,
                        _ => _.LevyFile.TransferOutYN
                        );
                });
        }
    }
}
