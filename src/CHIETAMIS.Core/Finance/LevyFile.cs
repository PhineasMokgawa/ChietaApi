using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace CHIETAMIS.Finance
{
    [Table("tbl_LevyFile")]
    public class LevyFile : Entity
    {
        public virtual string ZipFileName { get; set; }

        public virtual DateTime DateExtracted { get; set; }

        public virtual string Status { get; set; }

        public virtual bool ImportInProgress { get; set; }

        public virtual bool CommitInProgress { get; set; }

        public virtual bool TransferInYN { get; set; }

        public virtual bool TransferOutYN { get; set; }
    }
}
