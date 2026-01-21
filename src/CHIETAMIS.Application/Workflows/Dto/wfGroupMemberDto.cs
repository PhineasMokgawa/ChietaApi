using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Workflows.Dto
{
    public class wfGroupMemberDto: EntityDto
    {
        public int GroupId { get; set; }
        public int UserId { get; set; }
        public DateTime DateCreated { get; set; }
        public int? UserCreated { get; set; }
    }
}
