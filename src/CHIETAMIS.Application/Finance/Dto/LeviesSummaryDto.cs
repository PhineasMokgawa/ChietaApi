using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Finance.Dto
{
    public class LeviesSummaryDto
    {
        public Int64 Id { get; set; }
        public string SDLNumber { get; set; }
        public string LevyFile { get; set; }
        public DateTime LevyDate { get; set; }
        public int LevyYear { get; set; }
        public decimal GrantAmount { get; set; }
        public decimal GrantAmount2 { get; set; }
        public decimal AdminAmount { get; set; }
        public decimal InterestAmount { get; set; }
        public decimal PenaltyAmount { get; set; }
        public decimal DhetAmount { get; set; }
    }
}
