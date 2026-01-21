using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Finance.Dto
{
    public class LevyFileDto: EntityDto
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
