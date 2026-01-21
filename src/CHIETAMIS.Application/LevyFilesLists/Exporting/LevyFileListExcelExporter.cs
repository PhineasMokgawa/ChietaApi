using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using CHIETAMIS.DataExporting.Excel.EpPlus;
using CHIETAMIS.LevyFileLists;
using CHIETAMIS.LevyFileLists.Dtos;
using CHIETAMIS.Dto;
using CHIETAMIS.Storage;
using CHEITAMIS.Dto;
using CHIETAMIS.DataExporting.Excel.EpPlus;
using CHIETAMIS.LevyFileLists.Dtos;
using CHIETAMIS.LevyFilesLists;
using CHIETAMIS.Storage;

namespace CHIETAMIS.LevyFileLists.Exporting
{
    public class LevyFileListExcelExporter : EpPlusExcelExporterBase, ILevyFileListExcelExporter
    {
        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public LevyFileListExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) : base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetLevyFileListForViewDto> LevysFile)
        {
            return CreateExcelPackage(
                "LevysFile.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("LevysFile"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("DHETZipFileID"),
                        L("FileName"),
                        L("ImportInProgress"),
                        L("CommitInProgress"),
                        L("DateCreated"),
                        L("Status")
                        );

                    AddObjects(
                        sheet, 2, LevysFile,
                        _ => _.LevyFileList.DHETZipFileID,
                        _ => _.LevyFileList.FileName,
                        _ => _.LevyFileList.ImportInProgress,
                        _ => _.LevyFileList.CommitInProgress,
                        _ => _.LevyFileList.DateCreated,
                        _ => _.LevyFileList.Status
                        );
                });
        }
    }
}
