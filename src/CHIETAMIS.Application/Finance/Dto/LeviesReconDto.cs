using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Finance.Dto
{
    public class LeviesReconDto: EntityDto
    {
        public int ZipFileId { get; set; }
        public string ZipFileName { get; set; }
        public DateTime TransactionDate { get; set; }
        public int LevyYear { get; set; }
        public decimal GrantAmount { get; set; }
        public decimal GrantAmount2 { get; set; }
        public decimal AdminAmount { get; set; }
        public decimal InterestAmount { get; set; }
        public decimal InterestAmount2 { get; set; }
        public decimal PenaltyAmount { get; set; }
    }
}
