using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CHIETAMIS.DiscretionaryWindows.Dtos
{
    public class ProjectTypeDto : EntityDto<int>
    {
        public string ProjTypCD { get; set; }
        public string ProjTypDesc { get; set; }
        public bool ActiveYN { get; set; }
    }
}
