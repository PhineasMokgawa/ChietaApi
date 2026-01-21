using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using CHIETAMIS.Documents.Dtos;
using Abp.Application.Services.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Configuration;
using Abp.Zero.Configuration;

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
            var docs = _documentsRepository.GetAll().Where(a => a.entityid == input.entityid && a.module == input.module && a.documenttype == input.documenttype);

            if (docs.Count() == 0)
            {
                input.DateCreated = DateTime.Now;
                var doc = ObjectMapper.Map<Document>(input);

                await _documentsRepository.InsertAsync(doc);
            }
            else
            {
                var doc = await _documentsRepository.FirstOrDefaultAsync(docs.First().Id);
                doc.entityid = input.entityid;
                doc.newfilename = input.newfilename;
                doc.filename = input.filename;
                doc.lastmodifieddate = input.lastmodifieddate;
                doc.size = input.size;
                doc.type = input.type;
                doc.documenttype = input.documenttype;
                doc.module = input.module;

                await _documentsRepository.UpdateAsync(doc);
            }

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
    }
}
