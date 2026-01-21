using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CHIETAMIS.Sdfs

{
    [Table("tbl_Sdf_File")]
    public class SdfFileUpload : Entity
    {
        public SdfFileUpload() { }
        public int sdfId { get; set; }
        public string documentType { get; set; }
        public string fileName { get; set; }
        public string savedFileName { get; set; }
        public int fileSize { get; set; }
        public string fileType { get; set; }
        public DateTime lastModifiedTime { get; set; }
    }
}
