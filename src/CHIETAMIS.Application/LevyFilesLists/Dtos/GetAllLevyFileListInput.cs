using Abp.Application.Services.Dto;
using System;

namespace CHIETAMIS.LevyFileLists.Dtos
{
    public class GetAllLevyFileListInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public int DHETZipFileIDFilter { get; set; }

        public string FileNameFilter { get; set; }

        public bool ImportInProgressFilter { get; set; }

        public bool CommitInProgressFilter { get; set; }

        public DateTime DateCreatedFilter { get; set; }

        public string StatusFilter { get; set; }
    }
}
