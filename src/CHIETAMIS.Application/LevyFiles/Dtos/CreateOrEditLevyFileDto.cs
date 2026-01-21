using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace CHIETAMIS.LevyFiles.Dtos
{
    public class CreateOrEditLevyFileDto : EntityDto<long?>
    {
        public int ID { get; set; }

        public string ZipFileName { get; set; }

        public DateTime DateExtracted { get; set; }

        public string Status { get; set; }

        public bool ImportInProgress { get; set; }

        public bool CommitInProgress { get; set; }

        public bool TransferInYN { get; set; }

        public bool TransferOutYN { get; set; }
    }
}
