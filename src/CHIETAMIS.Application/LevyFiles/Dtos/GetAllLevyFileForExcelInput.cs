using Abp.Application.Services.Dto;
using System;

namespace CHIETAMIS.LevyFiles.Dtos
{
    public class GetAllLevyFileForExcelInput
    {
        public string Filter { get; set; }

        public string ZipFileNameFilter { get; set; }

        public DateTime DateExtractedFilter { get; set; }

        public string StatusFilter { get; set; }

        public bool ImportInProgressFilter { get; set; }

        public bool CommitInProgressFilter { get; set; }

        public bool TransferInYNFilter { get; set; }

        public bool TransferOutYNFilter { get; set; }

    }
}
