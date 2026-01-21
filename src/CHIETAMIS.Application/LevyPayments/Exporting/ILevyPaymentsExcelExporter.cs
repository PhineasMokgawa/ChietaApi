using System.Collections.Generic;
using CHIETAMIS.LEVYPAYMENTS.Dtos;
using CHIETAMIS.Dto;
using CHEITAMIS.Dto;
using CHIETAMIS.LEVYPAYMENTS.Dtos;

namespace CHIETAMIS.LEVYPAYMENTS.Exporting
{
    public interface ILEVY_PAYMENTSsExcelExporter
    {
        FileDto ExportToFile(List<GetLEVY_PAYMENTSForViewDto> levY_PAYMENTSs);
    }
}