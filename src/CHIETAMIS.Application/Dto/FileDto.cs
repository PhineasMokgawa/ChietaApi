using System;
using System.ComponentModel.DataAnnotations;

namespace CHEITAMIS.Dto
{
    public class FileDto
    {
        [Required]
        public string FileName { get; set; }

        public string FileType { get; set; }

        [Required]
        public string FileToken { get; set; }
        public byte[] FileBytes { get; internal set; }

        public FileDto(string fileName, string fileType, byte[] fileBytes)
        {
            FileName = fileName;
            FileType = fileType;
            FileToken = Guid.NewGuid().ToString("N");
            FileBytes = fileBytes;
        }

        public FileDto(string fileName, string applicationVndOpenxmlformatsOfficedocumentSpreadsheetmlSheet)
        {
            FileName = fileName;
        }
    }
}