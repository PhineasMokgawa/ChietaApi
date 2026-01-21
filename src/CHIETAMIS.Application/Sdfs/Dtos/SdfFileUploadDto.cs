using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Sdfs.Dtos
{
    public class SdfFileUploadDto: EntityDto
    {
        public int PersonId { get; set; }
        public string DocumentType { get; set; }
        public string fileName { get; set; }
        public string savedFileName { get; set; }
        public int fileSize { get; set; }
        public string fileType { get; set; }
        public DateTime lastModifiedTime { get; set; }
    }
}
