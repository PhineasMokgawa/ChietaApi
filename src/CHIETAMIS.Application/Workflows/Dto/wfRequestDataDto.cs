using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Workflows.Dto
{
    public class wfRequestDataDto: EntityDto
    {
        public int RequestId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public DateTime DateCreated { get; set; }
        public int UserId { get; set; }
    }
}
