using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Finance.Dto
{
    public class LevyFileListDto: EntityDto
    {
        public int DHETZipFileID { get; set; }

        public string FileName { get; set; }

        public bool ImportInProgress { get; set; }

        public bool CommitInProgress { get; set; }

        public DateTime DateCreated { get; set; }

        public string Status { get; set; }
    }
}
