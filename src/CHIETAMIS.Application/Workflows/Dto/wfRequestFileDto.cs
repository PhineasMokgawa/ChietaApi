using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Workflows.Dto
{
    public class wfRequestFileDto: EntityDto
    {
        public int RequestId { get; set; }
        public int UserId { get; set; }
        public DateTime DateUploaded { get; set; }
        public string FileName { get; set; }
        public string MIMEType { get; set; }
        public string Filelocation { get; set; }
    }
}
