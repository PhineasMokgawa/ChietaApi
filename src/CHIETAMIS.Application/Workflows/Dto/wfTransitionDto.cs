using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Workflows.Dto
{
    public class wfTransitionDto: EntityDto
    {
        public int ProcessId { get; set; }
        public int CurrentStateId { get; set; }
        public int NextStateId { get; set; }
        public DateTime DateCreated { get; set; }
        public int UserId { get; set; }
    }
}
