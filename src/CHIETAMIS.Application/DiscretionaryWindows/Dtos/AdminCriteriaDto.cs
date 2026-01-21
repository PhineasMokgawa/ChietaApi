using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CHIETAMIS.DiscretionaryWindows.Dtos
{
    public class AdminCriteriaDto : EntityDto<int>
    {
        public string AdminDesc { get; set; }
        public Boolean UsBased { get; set; }
        public bool ActiveYN { get; set; }  
    }
}
