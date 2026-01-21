using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using CHIETAMIS.Sdfs.Dtos;
using CHIETAMIS.People;
using CHIETAMIS.People.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Sdfs
{
    public class SdfAppService : CHIETAMISAppServiceBase
    {
        private readonly IRepository<SdfDetails> _sdfDetailsRepository;
        private readonly IRepository<SdfFileUpload> _fileUploadRepository;
        private readonly IRepository<Organisation_Sdf> _orgSdfRepository;
        private readonly IRepository<Person> _persRepository;

        public SdfAppService(IRepository<SdfDetails> sdfDetailsRepository,
                             IRepository<SdfFileUpload> fileUploadRepository,
                             IRepository<Organisation_Sdf> orgSdfRepository,
                             IRepository<Person> persRepository)
        {
            _fileUploadRepository = fileUploadRepository;
            _sdfDetailsRepository = sdfDetailsRepository;
            _orgSdfRepository = orgSdfRepository;
            _persRepository = persRepository;
        }

        public async Task<SdfFileUploadOutput> SaveSdfFile(SdfFileUploadInput input)
        {
            var existingCount = _fileUploadRepository.GetAll().Count(a => a.sdfId == input.SdfId && a.documentType == input.DocumentType);
            if (existingCount == 0)
            {
                var cfileupload = ObjectMapper.Map<SdfFileUpload>(input);
                await _fileUploadRepository.InsertAsync(cfileupload);
            }

            var fileNewId = _fileUploadRepository.GetAll().Where(a => a.sdfId == input.SdfId && a.documentType == input.DocumentType).FirstOrDefault();

            return new SdfFileUploadOutput
            {
                addressId = fileNewId.Id
            };
        }

        //public async Task<string> UploadFile(SdfFileUploadInput model)
        //{
        //    var image = Request.Form.Files.First();
        //    var uniqueFileName = GetUniqueFileName(image.FileName);
        //    var dir = Path.Combine(_env.ContentRootPath, "Images");
        //    if (!Directory.Exists(dir))
        //    {
        //        Directory.CreateDirectory(dir);
        //    }
        //    var filePath = Path.Combine(dir, uniqueFileName);
        //    await image.CopyToAsync(new FileStream(filePath, FileMode.Create));
        //    SaveImagePathToDb(input.Description, filePath);
        //    return uniqueFileName;
        //}

        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                   + "_"
                   + Guid.NewGuid().ToString().Substring(0, 4)
                   + Path.GetExtension(fileName);
        }

        private void SaveImagePathToDb(string description, string filepath)
        {
            //todo: description and file path to db
        }


        //public async Task<SdfFileUploadOutput> OnPostUploadAsync(SdfFileUploadInput file)
        //{
        //    long size = file.FileSize;

        //    if (size > 0)
        //    {
        //        var filePath = Path.GetTempFileName();

        //        using (var stream = System.IO.File.Create(filePath))
        //        {
        //            await file;
        //        }
        //    }

        //    // Process uploaded files
        //    // Don't rely on or trust the FileName property without validation.

        //    return (new { count = files.Count, size });
        //}

        public async Task Register(SdfDetailsDto input)
        {
            var cSdf = _sdfDetailsRepository.GetAll().Where(a => a.personId == input.personId);
            if (cSdf.Count() == 0)
            {
                var cSdfDetail = ObjectMapper.Map<SdfDetails>(input);
                await _sdfDetailsRepository.InsertAsync(cSdfDetail);
            } else {
                var cSd = await _sdfDetailsRepository.FirstOrDefaultAsync(cSdf.First().Id);
                if (cSd.designation != cSdf.FirstOrDefault().designation)
                {
                    cSd.designation = input.designation;
                    await _sdfDetailsRepository.UpdateAsync(cSd);
                }
                if (cSd.status != cSdf.FirstOrDefault().status)
                {
                    cSd.status = input.status;
                    cSd.statusDate = input.statusDate;
                    await _sdfDetailsRepository.UpdateAsync(cSd);
                }
            }
        }

        public async Task LinkOrganisation(Organisation_SdfDto input)
        {

            var persId = _persRepository.GetAll().Where(a => a.Userid == input.UserId).First().Id;

            var sdfs = _sdfDetailsRepository.GetAll().Where(a => a.userId == input.UserId);
            if (sdfs.Count() > 0)
            {
                input.SdfId = sdfs.First().Id;
            } else
            {
                //throw new InvalidOperationException("The user is not registered as an SDF.");
                input.SdfId = 0;
            }
            input.PersonId = persId;
            input.DateCreated = DateTime.Now;
            input.StatusDate = DateTime.Now;
            var orgsdf = ObjectMapper.Map<Organisation_Sdf>(input);

            await _orgSdfRepository.InsertAsync(orgsdf);
        }

        public async Task DeleteOrgSdf(EntityDto<int> input)
        {
            await _orgSdfRepository.DeleteAsync(input.Id);
        }


        public async Task CompleteApplication(int personId, int statusId)
        {
            var cSdf = _sdfDetailsRepository.GetAll().Where(a => a.personId == personId);
            if (cSdf.Count() != 0)
            {
                var cSd = await _sdfDetailsRepository.FirstOrDefaultAsync(cSdf.First().Id);
                if (statusId != cSdf.FirstOrDefault().status)
                {
                    cSd.status = statusId;
                    await _sdfDetailsRepository.UpdateAsync(cSd);
                }


            }
        }

        public async Task<SdfDetailForViewDto> GetSDFByUser(int userId)
        {
            var orgsdf = _sdfDetailsRepository.GetAll().Where(a=>a.userId == userId).FirstOrDefault();

            var output = new SdfDetailForViewDto { SdfDetails = ObjectMapper.Map<SdfDetailsDto>(orgsdf) };

            return output;
        }

        public async Task<SdfDetailForViewDto> Get(int id)
        {
            var orgsdf = _sdfDetailsRepository.GetAll().Where(a => a.Id == id).FirstOrDefault();

            var output = new SdfDetailForViewDto { SdfDetails = ObjectMapper.Map<SdfDetailsDto>(orgsdf) };

            return output;
        }

        public async Task<OrgSdfForViewDto> GetOrgSdf(int Id)
        {
            var orgsdf = _orgSdfRepository.GetAll().Where(a => a.Id == Id && a.StatusId != 5).FirstOrDefault();

            var output = new OrgSdfForViewDto { organisation_Sdf = ObjectMapper.Map<Organisation_SdfDto>(orgsdf) };

            return output;
        }

        public async Task<OrgSdfForViewDto> GetOrgSdfByOrg(int Id, int userid)
        {
            var orgsdf = _orgSdfRepository.GetAll().Where(a => a.OrganisationId == Id && a.StatusId != 5).FirstOrDefault();

            var output = new OrgSdfForViewDto { organisation_Sdf = ObjectMapper.Map<Organisation_SdfDto>(orgsdf) };

            return output;
        }

        public async Task<SDFUserDetailDto> GetSdfByOrg(int Id)
        {
            var sdfuser = (from org in _orgSdfRepository.GetAll()
                          join osdf in _orgSdfRepository.GetAll() on org.Id equals osdf.OrganisationId
                          join sdf in _sdfDetailsRepository.GetAll() on osdf.SdfId equals sdf.Id
                          join pers in _persRepository.GetAll() on sdf.personId equals pers.Id
                          select new
                          {
                              OrganisationId = org.Id,
                              FirstName = pers.Firstname,
                              Middlename = pers.Middlenames,
                              Lastname = pers.Lastname,
                              EmailAddress = pers.Email
                          })
                .Where(a => a.OrganisationId == Id).FirstOrDefault();

            var output = ObjectMapper.Map<SDFUserDetailDto>(sdfuser);

            return output;
        }
    }
}
