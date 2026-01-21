using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Workflows.Dto
{
    public class wfRequestDto: EntityDto
    {
        public int ProcessId { get; set; }
        public string Title { get; set; }
        public string RequestPath { get; set; }
        public DateTime DateRequested { get; set; }
        public int UserId { get; set; }
        public string? Username { get; set; }
        public int CurrentStateId { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
