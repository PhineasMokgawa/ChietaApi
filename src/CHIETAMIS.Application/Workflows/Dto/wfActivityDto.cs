using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Workflows.Dto
{
    public class wfActivityDto: EntityDto
    {
        public int ActivityTypeId { get; set; }
        public int ProcessId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public int? UserId { get; set; }
    }
}
