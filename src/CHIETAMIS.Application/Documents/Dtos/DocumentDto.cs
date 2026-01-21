using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace CHIETAMIS.Documents.Dtos
{
    public class DocumentDto: EntityDto
    {
        public int entityid { get; set; }
        public string newfilename { get; set; }
        public string filename { get; set; }
        public string lastmodifieddate { get; set; }
        public string size { get; set; }
        public string type { get; set; }
        public string documenttype { get; set; }
        public string module { get; set; }
        public DateTime DateCreated { get; set; }
        public int UserId { get; set; }
    }
}
