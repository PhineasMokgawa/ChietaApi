using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;

namespace CHIETAMIS.Documents.Dtos
{
    /// <summary>
    /// Request DTO for downloading documents with multiple filter options
    /// </summary>
    public class DownloadDocumentRequestDto
    {
        /// <summary>
        /// Entity ID associated with the document
        /// </summary>
        public int? EntityId { get; set; }

        /// <summary>
        /// Document Type (e.g., "ID", "Certificate", "Invoice")
        /// </summary>
        public string DocumentType { get; set; }

        /// <summary>
        /// Module name (e.g., "Learner", "Application", "Grant")
        /// </summary>
        public string Module { get; set; }

        /// <summary>
        /// User ID who owns/uploaded the document
        /// </summary>
        public int? UserId { get; set; }

        /// <summary>
        /// Document ID for direct download
        /// </summary>
        public int? DocumentId { get; set; }
    }

    /// <summary>
    /// Response DTO containing document metadata and download information
    /// </summary>
    public class DocumentDownloadInfoDto : EntityDto
    {
        public int EntityId { get; set; }
        public string OriginalFileName { get; set; }
        public string StoredFileName { get; set; }
        public string FileType { get; set; }
        public string DocumentType { get; set; }
        public string Module { get; set; }
        public string FileSize { get; set; }
        public DateTime DateCreated { get; set; }
        public int UserId { get; set; }
        public string LastModifiedDate { get; set; }
        public string DownloadUrl { get; set; }
    }

    /// <summary>
    /// Request to list user documents with pagination
    /// </summary>
    public class GetUserDocumentsRequestDto : PagedResultRequestDto
    {
        public int UserId { get; set; }
        public string DocumentType { get; set; }
        public string Module { get; set; }
        public int? EntityId { get; set; }
    }
}
