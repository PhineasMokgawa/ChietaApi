using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CHIETAMIS.DiscretionaryWindows.Dtos
{
    public class EvaluationMethodDto: EntityDto<int>
    {
        public string EvalMthdCd { get; set; }
        public string EvalMthdDesc { get; set; }
        public Boolean UsBased { get; set; }
        public bool ActiveYN { get; set; }
        public bool AllowContinuing { get; set; }
        public bool AllowNew { get; set; }
    }
}
