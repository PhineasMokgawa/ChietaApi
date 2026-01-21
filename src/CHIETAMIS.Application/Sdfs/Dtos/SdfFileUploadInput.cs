using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Sdfs.Dtos
{
    public class SdfFileUploadInput
    {
        public int SdfId { get; set; }
        public string DocumentType { get; set; }
        public string FileName { get; set; }
        public string SavedFileName { get; set; }
        public int FileSize { get; set; }
        public string FileType { get; set; }
        public DateTime LastModifiedTime { get; set; }
    }
}
