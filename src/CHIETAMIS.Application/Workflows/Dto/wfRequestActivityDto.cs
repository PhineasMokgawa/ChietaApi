using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Workflows.Dto
{
    public class wfRequestActivityDto
    {
        public int UserId { get; }
        public int GroupId { get; }
        public string Name { get; }
        public string Title { get; }
        public DateTime DateRequested { get; }

    }
}
