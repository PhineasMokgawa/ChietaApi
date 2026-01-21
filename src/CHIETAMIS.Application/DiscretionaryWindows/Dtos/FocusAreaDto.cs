using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CHIETAMIS.DiscretionaryWindows.Dtos
{
    public class FocusAreaDto: EntityDto<int>
    {
        public string FocusAreaCd { get; set; }
        public string FocusAreaDesc { get; set; }
        public int FundinglImit {get; set;}
        public bool ActiveYN { get; set; }
    }
}
