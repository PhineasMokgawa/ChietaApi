using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Workflows.Dto
{
    public class wfActivityTargetDto : EntityDto
    {
        public int ActivityId { get; set; }
        public int TargetId { get; set; }
        public int? GroupId { get; set; }
        public DateTime DateCreated { get; set; }
        public int? UserId { get; set; }
    }
}
