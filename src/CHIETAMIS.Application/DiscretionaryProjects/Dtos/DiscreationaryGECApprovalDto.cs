using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.DiscretionaryProjects.Dtos
{
    public class DiscreationaryGECApprovalDto: EntityDto
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
