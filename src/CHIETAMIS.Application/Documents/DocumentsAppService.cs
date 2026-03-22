using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using CHIETAMIS.Documents.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CHEITAMIS.Dto;
using Abp.UI;
using Abp.Extensions;

namespace CHIETAMIS.Documents
{
    public class DocumentsAppService : CHIETAMISAppServiceBase
    {
        private readonly IRepository<Document> _documentsRepository;

        public DocumentsAppService(IRepository<Document> documentsRepository)
        {
            _documentsRepository = documentsRepository;
        }

        public async Task FileUpload(DocumentDto input)
        {
            var docs = _documentsRepository.GetAll()
                .Where(a => a.entityid == input.entityid
                         && a.module == input.module
                         && a.documenttype == input.documenttype);

            if (!await docs.AnyAsync())
            {
                input.DateCreated = DateTime.Now;
                var doc = ObjectMapper.Map<Document>(input);
                await _documentsRepository.InsertAsync(doc);
            }
            else
            {
                var doc = await _documentsRepository.FirstOrDefaultAsync(docs.First().Id);
                doc.entityid        = input.entityid;
                doc.newfilename     = input.newfilename;
                doc.filename        = input.filename;
                doc.lastmodifieddate = input.lastmodifieddate;
                doc.size            = input.size;
                doc.type            = input.type;
                doc.documenttype    = input.documenttype;
                doc.module          = input.module;
                doc.UserId          = input.UserId;
                await _documentsRepository.UpdateAsync(doc);
            }
        }

        public async Task<FileDto> DownloadDocument(DocumentDto input)
        {
            var doc = await _documentsRepository
                .FirstOrDefaultAsync(a =>
                    a.entityid == input.entityid &&
                    a.module == input.module &&
                    a.documenttype == input.documenttype);

            if (doc == null)
                throw new UserFriendlyException("Document not found.");

            if (string.IsNullOrWhiteSpace(doc.newfilename))
                throw new UserFriendlyException("Document record has no stored filename.");

            // Return the stored filename so the caller can download via
            // GET /api/DocumentDownload/DownloadById?documentId={doc.Id}
            return new FileDto(doc.filename, doc.type, null)
            {
                FileToken = doc.newfilename
            };
        }

        public async Task<PagedResultDto<DocumentsForViewDto>> GetDocumentsByEntity(int entityid, string module, string documenttype)
        {
            var docs = _documentsRepository.GetAll().Where(a => a.entityid == entityid && a.module == module && a.documenttype == documenttype);
            var doc = await (from o in docs
                select new DocumentsForViewDto
                {
                    Documents = new DocumentDto
                    {
                    entityid = o.entityid,
                    newfilename = o.newfilename,
                    filename = o.filename,
                    lastmodifieddate = o.lastmodifieddate,
                    size = o.size,
                    type = o.type,
                    documenttype = o.documenttype,
                    module = o.module,
                    UserId = o.UserId,
                    Id = o.Id
                    }
                }).ToListAsync();

            var totalCount = doc.Count();

            return new PagedResultDto<DocumentsForViewDto>(
                totalCount,
                doc.ToList()
            );
        }

        public async Task DeleteFile(int id, int userid)
        {
            var doc = await _documentsRepository.GetAsync(id);
            await _documentsRepository.DeleteAsync(doc);
        }

        /// <summary>
        /// Resolve a document record by ID, for use by the download controller.
        /// </summary>
        public async Task<Document> GetDocumentRecordById(int documentId)
        {
            var doc = await _documentsRepository.FirstOrDefaultAsync(documentId);
            if (doc == null)
                throw new UserFriendlyException($"Document with ID {documentId} not found.");
            return doc;
        }

        /// <summary>
        /// Resolve the most recent document matching entity + documentType (+ optional module/userId).
        /// </summary>
        public async Task<Document> GetDocumentRecord(DownloadDocumentRequestDto input)
        {
            if (!input.EntityId.HasValue)
                throw new UserFriendlyException("EntityId is required.");

            if (input.DocumentType.IsNullOrWhiteSpace())
                throw new UserFriendlyException("DocumentType is required.");

            var query = _documentsRepository.GetAll()
                .Where(a => a.entityid == input.EntityId.Value &&
                            a.documenttype == input.DocumentType);

            if (!input.Module.IsNullOrWhiteSpace())
                query = query.Where(a => a.module == input.Module);

            if (input.UserId.HasValue)
                query = query.Where(a => a.UserId == input.UserId.Value);

            var doc = await query.OrderByDescending(d => d.DateCreated).FirstOrDefaultAsync();

            if (doc == null)
                throw new UserFriendlyException(
                    $"No document found for EntityId={input.EntityId}, DocumentType='{input.DocumentType}'.");

            return doc;
        }

        /// <summary>
        /// Get a paged list of documents for a user with optional filters.
        /// </summary>
        public async Task<PagedResultDto<DocumentDownloadInfoDto>> GetUserDocuments(GetUserDocumentsRequestDto input)
        {
            var query = _documentsRepository.GetAll()
                .Where(d => d.UserId == input.UserId);

            if (!input.DocumentType.IsNullOrWhiteSpace())
                query = query.Where(d => d.documenttype == input.DocumentType);

            if (!input.Module.IsNullOrWhiteSpace())
                query = query.Where(d => d.module == input.Module);

            if (input.EntityId.HasValue)
                query = query.Where(d => d.entityid == input.EntityId.Value);

            var totalCount = await query.CountAsync();

            var documents = await query
                .OrderByDescending(d => d.DateCreated)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount)
                .ToListAsync();

            return new PagedResultDto<DocumentDownloadInfoDto>(
                totalCount,
                documents.Select(MapToDownloadInfo).ToList()
            );
        }

        /// <summary>
        /// Get all documents for a given entity, optionally filtered by userId.
        /// </summary>
        public async Task<List<DocumentDownloadInfoDto>> GetDocumentsByEntityId(int entityId, int? userId = null)
        {
            var query = _documentsRepository.GetAll()
                .Where(d => d.entityid == entityId);

            if (userId.HasValue)
                query = query.Where(d => d.UserId == userId.Value);

            var documents = await query
                .OrderByDescending(d => d.DateCreated)
                .ToListAsync();

            return documents.Select(MapToDownloadInfo).ToList();
        }

        /// <summary>
        /// Get documents by document type with optional userId and module filters.
        /// </summary>
        public async Task<List<DocumentDownloadInfoDto>> GetDocumentsByType(
            string documentType,
            int? userId = null,
            string module = null)
        {
            var query = _documentsRepository.GetAll()
                .Where(d => d.documenttype == documentType);

            if (userId.HasValue)
                query = query.Where(d => d.UserId == userId.Value);

            if (!module.IsNullOrWhiteSpace())
                query = query.Where(d => d.module == module);

            var documents = await query
                .OrderByDescending(d => d.DateCreated)
                .ToListAsync();

            return documents.Select(MapToDownloadInfo).ToList();
        }

        private static DocumentDownloadInfoDto MapToDownloadInfo(Document doc)
        {
            return new DocumentDownloadInfoDto
            {
                Id = doc.Id,
                EntityId = doc.entityid,
                OriginalFileName = doc.filename,
                StoredFileName = doc.newfilename,
                FileType = doc.type,
                DocumentType = doc.documenttype,
                Module = doc.module,
                FileSize = doc.size,
                DateCreated = doc.DateCreated,
                UserId = doc.UserId,
                LastModifiedDate = doc.lastmodifieddate
            };
        }
    }
}
