using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace CHIETAMIS.LevyFileLists.Dtos
{
    public class CreateOrEditLevyFileListDto
    {
        public int Id { get; set; }

        public int DHETZipFileID { get; set; }

        public string FileName { get; set; }

        public bool ImportInProgress { get; set; }

        public bool CommitInProgress { get; set; }

        public DateTime DateCreated { get; set; }

        public string Status { get; set; }

    }
}
