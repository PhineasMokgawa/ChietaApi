using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Workflows.Dto
{
    public class CreateRequestDto: EntityDto
    {
        public int ProcessId { get; set; }
        public string Title { get; set; }
        public string RequestPath { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public int CurrentStateId { get; set; }
        public DateTime DateCreated { get; set; }
        public int OrganisationId { get; set; }
        public int ProjectId { get; set; }
        public List<wfRequestData> RequestData { get; set; }
        public DateTime DateRequested { get; set; }

    }
}
