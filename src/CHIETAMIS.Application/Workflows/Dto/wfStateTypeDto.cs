using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Workflows.Dto
{
    public class wfStateTypeDto: EntityDto
    {
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
