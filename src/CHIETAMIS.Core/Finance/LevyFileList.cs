using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace CHIETAMIS.Finance
{
    [Table("tbl_LevyFileList")]
    public class LevyFileList : Entity
    {
        public int DHETZipFileID { get; set; }

        public string FileName { get; set; }

        public bool ImportInProgress { get; set; }

        public bool CommitInProgress { get; set; }

        public DateTime DateCreated { get; set; }

        public string Status { get; set; }
    }
}
