using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Documents
{
    [Table("tbl_Documents")]
    public class Document: Entity
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
