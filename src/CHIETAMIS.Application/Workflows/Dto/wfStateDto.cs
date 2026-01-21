using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Workflows.Dto
{
    public class wfStateDto: EntityDto
    {
        public int StateTypeId { get; set; }
        public int ProcessId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public int? UserId { get; set; }
    }
}
