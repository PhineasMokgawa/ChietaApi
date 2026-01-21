using Abp.Application.Services.Dto;
using System;

namespace CHIETAMIS.DiscretionaryProjects.Dtos
{
    public class DiscreationaryGCApprovalDto : EntityDto
    {
        public int ApplicationId { get; set; }
        public int ApprovalTypeId { get; set; }
        public int ApprovalStatusId { get; set; }
        public string Comments { get; set; }
        public DateTime DateCreated { get; set; }
        public int UserId { get; set; }
        public DateTime DteUpd { get; set; }
        public int UserUpd { get; set; }
    }
}
