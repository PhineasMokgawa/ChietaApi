using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Workflows.Dto
{
    public class wfRequestNoteDto: EntityDto
    {
        public int RequestId { get; set; }
        public int UserId { get; set; }
        public string Note { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
