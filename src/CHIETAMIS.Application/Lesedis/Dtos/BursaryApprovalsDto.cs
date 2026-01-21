using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Lesedis.Dtos
{
    public class BursaryApprovalsDto: EntityDto
    {
        public int ApplicationId { get; set; }
        public int ApprovalTypeId { get; set; }
        public int ApprovalStatusId { get; set; }
        public bool IdCopy { get; set; }
        public bool Statement { get; set; }
        public bool Results { get; set; }
        public bool Registration { get; set; }
        public string? Comments { get; set; }
        public string? Outcome { get; set; }
        public DateTime? MeetingDate { get; set; }
        public DateTime? OutcomeDate { get; set; }
        public DateTime DateCreated { get; set; }
        public int UserId { get; set; }
        public DateTime? DteUpd { get; set; }
        public int? UsrUpd { get; set; }
    }
}
