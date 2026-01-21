using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CHIETAMIS.DiscretionaryWindows.Dtos
{
    public class FocusCriteriaEvaluationDto: EntityDto
    {
        public Int16 FocusAreaKey { get; set; }
        public Int16 AdminCritKey { get; set; }
        public Int16 EvalMthdCd { get; set; }
        public Int16 ProjTypCD { get; set; }
        public bool ActiveYN { get; set; }
        public bool AllowNew { get; set; }
        public bool AllowContinuing { get; set; }
    }
}
