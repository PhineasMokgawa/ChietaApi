using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.MG_Dashboards.Dtos
{
    public class MWindowTitlesDto : EntityDto
    {
        public string ReferenceNo { get; set; }
        public string Description { get; set; }
    }
}
