using Abp.Application.Services.Dto;
using Abp.Events.Bus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Dashboards.Dtos
{
    public class WindowTitlesDto: EntityDto 
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
