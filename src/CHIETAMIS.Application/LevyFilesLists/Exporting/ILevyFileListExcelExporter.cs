using System.Collections.Generic;
using CHIETAMIS.LevyFileLists.Dtos;
using CHIETAMIS.Dto;
using CHEITAMIS.Dto;
using CHIETAMIS.LevyFileLists.Dtos;

namespace CHIETAMIS.LevyFileLists.Exporting
{
    public interface ILevyFileListExcelExporter
    {
        FileDto ExportToFile(List<GetLevyFileListForViewDto> LevyFileList);
    }
}
