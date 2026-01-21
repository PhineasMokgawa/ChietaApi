using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Workflows.Dto
{
    public class wfTransitionActionDto: EntityDto
    {
        public int TransitionId { get; set; }
        public int ActionId { get; set; }
        public DateTime DateCreated { get; set; }
        public int? UserId { get; set; }
    }
}
