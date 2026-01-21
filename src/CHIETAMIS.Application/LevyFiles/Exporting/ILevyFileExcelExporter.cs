using System.Collections.Generic;
using CHIETAMIS.LevyFiles.Dtos;
using CHIETAMIS.Dto;
using CHEITAMIS.Dto;
using CHIETAMIS.LevyFiles.Dtos;

namespace MIS.LevyFiles.Exporting
{
    public interface ILevyFileExcelExporter
    {
        FileDto ExportToFile(List<GetLevyFileForViewDto> orgDetailses);
    }
}
