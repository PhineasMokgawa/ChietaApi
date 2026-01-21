using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Workflows.Dto
{
    public class wfStateActivityDto: EntityDto
    {
        public int StateId { get; set; }
        public int ActivityId { get; set; }
        public DateTime DateCreated { get; set; }
        public int? Userid { get; set; }
    }
}
