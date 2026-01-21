using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Abp;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using AutoMapper;
using Castle.Core.Internal;
using CHIETAMIS.Authorization.Users;
using CHIETAMIS.DiscretionaryProjects.Dtos;
using CHIETAMIS.Documents.Dtos;
using CHIETAMIS.Documents;
using CHIETAMIS.EntityFrameworkCore;
using CHIETAMIS.Lookups;
using CHIETAMIS.Lookups.DTOs;
using CHIETAMIS.MandatoryGrants.Dtos;
using CHIETAMIS.Organisations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto.Signers;
using static CHIETAMIS.Configuration.AppSettings;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Abp.Extensions;
using Abp.Collections.Extensions;
using Org.BouncyCastle.Crypto;
using static System.Net.Mime.MediaTypeNames;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using CHIETAMIS.Workflows.Dto;
using Org.BouncyCastle.Utilities.Zlib;
using CHIETAMIS.People;
using CHIETAMIS.Sdfs;

namespace CHIETAMIS.MandatoryGrants
{
    public class MandatoryGrantsAppService : CHIETAMISAppServiceBase
    {
        private readonly IUserEmailer _userEmailer;

        private readonly IRepository<MandatoryApplication> _mandatoryApplication;
        private readonly IRepository<MandatoryWindow> _mandatoryWindow;
        private readonly IRepository<Organisation> _orgRepository;
        private readonly IRepository<MandatoryStatus> _mandStatusRepository;
        private readonly IRepository<Biodata> _bioRepository;
        private readonly IRepository<Training> _trainRepository;
        private readonly IRepository<HTVF> _htvfRepository;
        private readonly IRepository<SkillGab> _skillGabRepository;
        private readonly IRepository<FINANCE_AND_TRAINING_COMPARISON> _financE_AND_TRAINING_COMPARISONRepository;
        private readonly IRepository<OFO> _occ;
        private readonly IRepository<OFO_Specialization> _spec;
        private readonly IRepository<EmploymentStatus> _empStat;
        private readonly IRepository<Equity> _equity;
        private readonly IRepository<Occupation_Level> _occLevel;
        private readonly IRepository<Learning_Programme> _progType;
        private readonly IRepository<Mandatory_Grant_Achievement_Status> _achStat;
        private readonly IRepository<Mandatory_Grant_Qualification_Type> _qualtype;
        private readonly IRepository<Mandatory_Pivotal_Programmes> _piv;
        private readonly IRepository<BankDetails> _bankRepository;
        private readonly IRepository<OrganisationPhysicalAddress> _addressRepository;
        private readonly IRepository<Extensions> _extRepository;
        private readonly IRepository<Mandatory_Extension_Status> _mandStatRepository;
        private readonly IRepository<MandatoryApproval> _approvalRepository;
        private readonly IRepository<MandatoryApprovalStatus> _approvalStatusRepository;
        private readonly IRepository<MandatoryDocumentApproval> _docApprovalRepository;
        private readonly IRepository<GrantApprovalStatus> _grantApprovalStatusRepository;
        private readonly IRepository<GrantApprovalType> _grantApprovalTypeRepository;
        private readonly IRepository<RegionRM> _rmRepository;
        private readonly IRepository<RegionRSA> _rsaRepository;
        private readonly IRepository<Regions> _regRepository;
        private readonly IRepository<Organisation_Sdf> _orgSdfRepository;
        private readonly IRepository<SdfDetails> _sdfRepository;
        private readonly IRepository<Person> _personRepository;

        public MandatoryGrantsAppService(IRepository<MandatoryApplication> mandatoryApplication,
                                          IRepository<MandatoryWindow> mandatoryWindow,
                                          IRepository<Organisation> orgRepository,
                                          IRepository<MandatoryStatus> mandStatusRepository,
                                          IRepository<Biodata> bioRepository,
                                          IRepository<Training> trainRepository,
                                          IRepository<HTVF> htvfRepository,
                                          IRepository<SkillGab> skillGabRepository,
                                          IRepository<FINANCE_AND_TRAINING_COMPARISON> financE_AND_TRAINING_COMPARISONRepository,
                                          IRepository<OFO> occ,
                                          IRepository<OFO_Specialization> spec,
                                          IRepository<EmploymentStatus> empStat,
                                          IRepository<Equity> equity,
                                          IRepository<Occupation_Level> occLevel,
                                          IRepository<Learning_Programme> progType,
                                          IRepository<Mandatory_Grant_Achievement_Status> achStat,
                                          IRepository<Mandatory_Grant_Qualification_Type> qualtype,
                                          IRepository<Mandatory_Pivotal_Programmes> piv,
                                          IRepository<BankDetails> bankRepository,
                                          IRepository<OrganisationPhysicalAddress> addressRepository,
                                          IUserEmailer userEmailer,
                                          IRepository<Extensions> extRepository,
                                          IRepository<Mandatory_Extension_Status> mandStatRepository,
                                          IRepository<MandatoryApproval> approvalRepository,
                                          IRepository<MandatoryApprovalStatus> approvalStatusRepository,
                                          IRepository<MandatoryDocumentApproval> docApprovalRepository,
                                          IRepository<GrantApprovalStatus> grantApprovalStatusRepository,
                                          IRepository<GrantApprovalType> grantApprovalTypeRepository,
                                          IRepository<RegionRSA> rsaRepository,
                                          IRepository<RegionRM> rmRepository,
                                          IRepository<Regions> regRepository,
                                          IRepository<Organisation_Sdf> orgSdfRepository,
                                          IRepository<SdfDetails> sdfRepository,
                                          IRepository<Person> personRepository)
        {
            _mandatoryApplication = mandatoryApplication;
            _mandatoryWindow = mandatoryWindow;
            _orgRepository = orgRepository;
            _mandStatusRepository = mandStatusRepository;
            _orgSdfRepository = orgSdfRepository;
            _sdfRepository = sdfRepository;
            _personRepository = personRepository;
            _bioRepository = bioRepository;
            _trainRepository = trainRepository;
            _htvfRepository = htvfRepository;
            _skillGabRepository = skillGabRepository;
            _financE_AND_TRAINING_COMPARISONRepository = financE_AND_TRAINING_COMPARISONRepository;
            _occ = occ;
            _spec = spec;
            _empStat = empStat;
            _equity = equity;
            _occLevel = occLevel;
            _progType = progType;
            _achStat = achStat;
            _qualtype = qualtype;
            _piv = piv;
            _addressRepository = addressRepository;
            _bankRepository = bankRepository;
            _userEmailer = userEmailer;
            _extRepository = extRepository;
            _mandStatRepository = mandStatRepository;
            _approvalRepository = approvalRepository;
            _approvalStatusRepository = approvalStatusRepository;
            _docApprovalRepository = docApprovalRepository;
            _grantApprovalStatusRepository = grantApprovalStatusRepository;
            _grantApprovalTypeRepository = grantApprovalTypeRepository;
            _rmRepository = rmRepository;
            _rsaRepository = rsaRepository;
            _regRepository = regRepository;
        }

        public async Task<string> CreateEditApplication(MandatoryApplicationDto input)
        {
            var output = "";

            var mgapp = _mandatoryApplication.GetAll().Where(a => a.GrantWindowId == input.GrantWindowId && a.OrganisationId == input.OrganisationId);

            if (mgapp.Count() == 0)
            {
                input.DteCreated = DateTime.Now;
                input.CaptureDte = DateTime.Now;

                var app = ObjectMapper.Map<MandatoryApplication>(input);

                await _mandatoryApplication.InsertAsync(app);
            }
            else
            {
                var mg = await _mandatoryApplication.GetAsync(mgapp.FirstOrDefault().Id);
                mg.DteUpd = DateTime.Now;
                mg.UsrUpd = input.UsrUpd;

                await _mandatoryApplication.UpdateAsync(mg);
            }

            return output;
        }

        public async Task CreateEditWindow(MandatoryWindowDto input)
        {
            var window = _mandatoryWindow.GetAll().Where(a => a.ReferenceNo == input.ReferenceNo);

            if (window.Count() == 0)
            {
                input.DateCreated = DateTime.Now;

                var wind = ObjectMapper.Map<MandatoryWindow>(input);

                await _mandatoryWindow.InsertAsync(wind);
            }
            else
            {
                input.DteUpd = DateTime.Now;

                var cWind = await _mandatoryWindow.GetAsync(window.FirstOrDefault().Id);
                cWind.Description = input.Description;
                cWind.StartDate = input.StartDate;
                cWind.EndDate = input.EndDate;
                cWind.ExtensionDate = input.ExtensionDate;
                cWind.ActiveYN = input.ActiveYN;
                cWind.ExtenstionActive = input.ExtenstionActive;

                await _mandatoryWindow.UpdateAsync(cWind);
            }
        }

        public async Task<MandatoryApplicationDto> GetApplicationId(int ApplicationId)
        {

            var app = _mandatoryApplication.GetAll().Where(x => x.Id == ApplicationId).SingleOrDefault();

            var papp = (from a in _mandatoryApplication.GetAll().Where(x => x.Id == ApplicationId)
                        join win in _mandatoryWindow.GetAll() on a.GrantWindowId equals win.Id
                        join pa in _mandatoryApplication.GetAll()
                            .Where(b => b.Id != ApplicationId && b.GrantStatusID > 1) 
                             on a.OrganisationId equals pa.OrganisationId
                        select new { 
                            Id = pa.Id,
                            GrantWindowId = a.GrantWindowId, 
                            pWindId = pa.GrantWindowId,
                            OrganisationId = a.OrganisationId,
                            GrantStatusID = a.GrantStatusID,
                            ReferenceNo = win.ReferenceNo,
                        }
                      )
                      .Where(a => a.pWindId == a.GrantWindowId - 1);

            var output = ObjectMapper.Map<MandatoryApplicationDto>(app);
            if (papp.Count() == 0)
            {
                output.PreviousSubmissionId = 0;
                output.SubmittedPrevious = false;
            } else
            if (papp.Count() > 0)
            {
                output.PreviousSubmissionId = papp.FirstOrDefault().Id;
                output.SubmittedPrevious = true;
            }

            return output;
        }
        public async Task<PagedResultDto<MandatoryWindowsView>> GetActiveWindows()
        {
            var window = _mandatoryWindow.GetAll().Where(a => a.StartDate <= DateTime.Now && a.EndDate >= DateTime.Now);

            var wind = (from o in window
                        select new MandatoryWindowsView
                        {
                            MandatoryWindow = new MandatoryWindowForView
                            {
                                ReferenceNo = o.ReferenceNo,
                                Description = o.Description,
                                StartDate = o.StartDate,
                                EndDate = o.EndDate,
                                ExtensionDate = o.ExtensionDate,
                                ActiveYN = o.ActiveYN,
                                ExtenstionActive = o.ExtenstionActive,
                                DateCreated = o.DateCreated,
                                UserId = o.UserId,
                                DteUpd = o.DteUpd,
                                UsrUpd = o.UsrUpd,
                                Id = o.Id

                            }
                        }).ToList();

            var totalCount = wind.Count();

            return new PagedResultDto<MandatoryWindowsView>(
                totalCount,
                wind.ToList()
            );
        }
        public async Task<MandatoryWindowDto> GetWindowId(int WindowId)
        {
            var app = _mandatoryWindow.GetAll().Where(a => a.Id == WindowId).FirstOrDefault();

            var output = ObjectMapper.Map<MandatoryWindowDto>(app);

            return output;
        }

        public async Task<MandatoryWindowForView> GetApplicationWindowId(int ApplicationId)
        {
            var app = (from w in _mandatoryWindow.GetAll()
                       join a in _mandatoryApplication.GetAll() on w.Id equals a.GrantWindowId
                       select new { Wind = w,
                                    ApplicationId = a.Id})
                       .Where(a => a.ApplicationId == ApplicationId).FirstOrDefault();

            var output = new MandatoryWindowForView
            {
                Id = app.Wind.Id,
                ReferenceNo = app.Wind.ReferenceNo,
                Description = app.Wind.Description,
                StartDate = app.Wind.StartDate,
                EndDate = app.Wind.EndDate,
                ExtensionDate = app.Wind.ExtensionDate,
                ActiveYN = app.Wind.ActiveYN,
                ExtenstionActive = app.Wind.ExtenstionActive
            };

            return output;
        }
        public async Task<PagedResultDto<MandatoryApplicationsView>> GetOrgApplications(int Organisationid)
        {
            var app = (from mg in _mandatoryApplication.GetAll()
                       join wnd in _mandatoryWindow.GetAll() on mg.GrantWindowId equals wnd.Id
                       join org in _orgRepository.GetAll() on mg.OrganisationId equals org.Id
                       join stat in _mandStatusRepository.GetAll() on mg.GrantStatusID equals stat.Id
                       select new
                       {
                           OrganisationId = org.Id,
                           OrganisationSDL = org.SDL_No,
                           Organisation_Name = org.Organisation_Name,
                           Organisation_Trading_Name = org.Organisation_Trading_Name,
                           Grants = mg,
                           GrantStatus = stat.StatusDesc,
                           Window = wnd,
                           Description = wnd.Description
                       })
                .Where(a => a.Grants.OrganisationId == Organisationid);

            var wind = (from o in app
                        select new MandatoryApplicationsView
                        {
                            MandatoryApplication = new MandatoryApplicationViewDto
                            {
                                GrantWindowId = o.Grants.GrantWindowId,
                                OrganisationId = o.OrganisationId,
                                OrganisationSDL = o.OrganisationSDL,
                                Organisation_Name = o.Organisation_Name,
                                Organisation_Trading_Name = o.Organisation_Trading_Name,
                                GrantStatus = o.GrantStatus,
                                ReferenceNo = o.Window.ReferenceNo,
                                Description = o.Window.Description,
                                UserId = o.Grants.UserId,
                                CaptureDte = o.Grants.CaptureDte,
                                SubmissionDte = o.Grants.SubmissionDte,
                                ClosingDate = o.Window.EndDate,
                                Id = o.Grants.Id
                            }
                        }).ToList();

            var totalCount = wind.Count();

            return new PagedResultDto<MandatoryApplicationsView>(
                totalCount,
                wind.ToList()
            );
        }
        public async Task<PagedResultDto<MandatoryApplicationsView>> GetAllMandatory(int first,int rows, int userId, string group)
        {
            var app = (from mg in _mandatoryApplication.GetAll()
                       join wnd in _mandatoryWindow.GetAll() on mg.GrantWindowId equals wnd.Id
                       join org in _orgRepository.GetAll() on mg.OrganisationId equals org.Id
                       join stat in _mandStatusRepository.GetAll() on mg.GrantStatusID equals stat.Id
                       join pha in _addressRepository.GetAll() on org.Id equals pha.organisationId into pa
                       from pas in pa.DefaultIfEmpty()
                       join rsa in _rsaRepository.GetAll() on mg.RSAId equals rsa.UserID into r
                       from rsas in r.DefaultIfEmpty()
                       //join rm in _rmRepository.GetAll() on rsas.RegionID equals rm.RegionId into rmn
                       //from rms in rmn.DefaultIfEmpty()
                       join rg in _regRepository.GetAll() on rsas.RegionID equals rg.Id into rgn
                       from rgs in rgn.DefaultIfEmpty()
                       select new
                       {
                           OrganisationId = org.Id,
                           OrganisationSDL = org.SDL_No,
                           Organisation_Name = org.Organisation_Name,
                           Organisation_Trading_Name = org.Organisation_Trading_Name,
                           Grants = mg,
                           GrantStatus = stat.StatusDesc,
                           ReferenceNo = wnd.ReferenceNo,
                           Window = wnd,
                           Description = wnd.Description,
                           Province = pas.province,
                           Region = rgs.RegionName,
                           RSA = rsas.RSA_Name,
                           //RSAId = rsas.UserID,
                           //RMId = rms.Id
                       });

            //if (group == "RSA")
            //{
            //    app = app.Where(a => (a.GrantStatus == "Submitted by Online User" || a.GrantStatus == "Rejected after Full Assessment") && a.RSAId == userId);
            //}

            //if (group == "RM")
            //{
            //    app = app.Where(a => a.GrantStatus == "RSA Review Completed" && a.RMId == userId);
            //}

            var totalCount = app.Count();

            var wind = (from o in app
                        select new MandatoryApplicationsView
                        {
                            MandatoryApplication = new MandatoryApplicationViewDto
                            {
                                GrantWindowId = o.Grants.GrantWindowId,
                                OrganisationId = o.OrganisationId,
                                OrganisationSDL = o.OrganisationSDL,
                                Organisation_Name = o.Organisation_Name,
                                Organisation_Trading_Name = o.Organisation_Trading_Name,
                                Province = o.Province,
                                Region = o.Region,
                                RSA = o.RSA,
                                GrantStatus = o.GrantStatus,
                                ReferenceNo = o.ReferenceNo,
                                Description = o.Window.Description,
                                UserId = o.Grants.UserId,
                                CaptureDte = o.Grants.CaptureDte,
                                SubmissionDte = o.Grants.SubmissionDte,
                                ClosingDate = o.Window.EndDate,
                                Id = o.Grants.Id
                            }
                        })
                        .OrderByDescending(a=>a.MandatoryApplication.Id)
                        .Skip(first)
                        .Take(rows)
                        .ToList();

            return new PagedResultDto<MandatoryApplicationsView>(
                totalCount,
                wind.ToList()
            );
        }
        protected virtual async Task InsertOrUpdateCBioData_(CreateOrEditCBiodataDto data)
        {
            var checkId = await _bioRepository.FirstOrDefaultAsync(e => e.Id == data.Id);
            if (checkId == null)
            {
                // if the id 
                var input = new Biodata();
                input.Race = data.Race;
                input.ApplicationId = data.ApplicationId;
                input.SA_Id_Number = data.SA_Id_Number;
                input.Passport_Number = data.Passport_Number;
                input.Firstname = data.Firstname;
                input.Middlename = data.Middlename;
                input.Surname = data.Surname;
                input.Birth_Year = data.Birth_Year;
                input.Gender = data.Gender;
                input.Race = data.Race;
                input.Disability = data.Disability;
                input.Nationality = data.Nationality;
                input.Province = data.Province;
                input.Municipality = data.Municipality;
                input.Highest_Qualification_Type = data.Highest_Qualification_Type;
                input.Employment_Status = data.Employment_Status;
                input.Occupation_Level_For_Equity_Reporting = data.Occupation_Level_For_Equity_Reporting;
                input.Organisational_Structure_Filter = data.Organisational_Structure_Filter;
                input.Post_Reference = data.Post_Reference;
                input.Job_Title = data.Job_Title;
                input.OFO_Occupation_Code = data.OFO_Occupation_Code;
                input.OFO_Specialisation = data.OFO_Specialisation;
                input.OFO_Occupation = data.OFO_Occupation;
                input.UserId = data.UserId;
                input.DateCreated = DateTime.Now;
                input.Status = data.Status;
                var item = ObjectMapper.Map<CBiodataDto>(data);
                await _bioRepository.InsertAsync(input);
            }
            else
            {
                var item = data;
                item.Id = checkId.Id;
                item.DteUpd = DateTime.Now;
                item.UsrUpd = data.UserId;
                ObjectMapper.Map(item, checkId);
            }
        }
        public async Task<int> GetApplicationRecordCount(int ApplicationId)
        {
            var cnt = _financE_AND_TRAINING_COMPARISONRepository.GetAll().Where(e => e.ApplicationId == ApplicationId).Count();
            cnt = cnt + _bioRepository.GetAll().Where(e => e.ApplicationId == ApplicationId).Count();
            cnt = cnt + _trainRepository.GetAll().Where(e => e.ApplicationId == ApplicationId).Count();
            cnt = cnt + _htvfRepository.GetAll().Where(e => e.ApplicationId == ApplicationId).Count();
            cnt = cnt + _skillGabRepository.GetAll().Where(e => e.ApplicationId == ApplicationId).Count();
            cnt = cnt + _financE_AND_TRAINING_COMPARISONRepository.GetAll().Where(e => e.ApplicationId == ApplicationId).Count() + 1;

            return cnt;
        }

        public async Task<int> InsertOrUpdateCBioData(CBiodataDto data)
        {

            data.DateCreated = DateTime.Now;
            var checkId = await _bioRepository.FirstOrDefaultAsync(e => e.Id == data.Id);
            if (checkId is null)
            {
                // if the id 
                var item = ObjectMapper.Map<Biodata>(data);
                item.DateCreated = DateTime.Now;
                await _bioRepository.InsertAsync(item);
            }
            else
            {
                var item = data;
                item.Id = checkId.Id;
                ObjectMapper.Map(item, checkId);
            }

            var cnt = _financE_AND_TRAINING_COMPARISONRepository.GetAll().Where(e => e.ApplicationId == data.ApplicationId).Count();
            cnt = cnt + _bioRepository.GetAll().Where(e => e.ApplicationId == data.ApplicationId).Count();
            cnt = cnt + _trainRepository.GetAll().Where(e => e.ApplicationId == data.ApplicationId).Count();
            cnt = cnt + _htvfRepository.GetAll().Where(e => e.ApplicationId == data.ApplicationId).Count();
            cnt = cnt + _skillGabRepository.GetAll().Where(e => e.ApplicationId == data.ApplicationId).Count();
            cnt = cnt + _financE_AND_TRAINING_COMPARISONRepository.GetAll().Where(e => e.ApplicationId == data.ApplicationId).Count() + 1;

            return cnt;

        }

        public async Task<int> InsertOrUpdateTraining(TrainingDto data)
        {
            data.DateCreated = DateTime.Now;
            var check = await _trainRepository.FirstOrDefaultAsync(e => e.Id == data.Id);
            if (check == null)
            {
                var item = ObjectMapper.Map<Training>(data);
                item.DateCreated = DateTime.Now;
                await _trainRepository.InsertAsync(item);
            }
            else
            {
                var item = data;
                item.Id = check.Id;
                ObjectMapper.Map(item, check);
            }

            var cnt = _financE_AND_TRAINING_COMPARISONRepository.GetAll().Where(e => e.ApplicationId == data.ApplicationId).Count();
            cnt = cnt + _bioRepository.GetAll().Where(e => e.ApplicationId == data.ApplicationId).Count();
            cnt = cnt + _trainRepository.GetAll().Where(e => e.ApplicationId == data.ApplicationId).Count();
            cnt = cnt + _htvfRepository.GetAll().Where(e => e.ApplicationId == data.ApplicationId).Count();
            cnt = cnt + _skillGabRepository.GetAll().Where(e => e.ApplicationId == data.ApplicationId).Count();
            cnt = cnt + _financE_AND_TRAINING_COMPARISONRepository.GetAll().Where(e => e.ApplicationId == data.ApplicationId).Count() + 1;

            return cnt;
        }

        public async Task DeleteBioId(int id)
        {
            _bioRepository.DeleteAsync(id);
        }

        public async Task DeleteTrainingId(int id)
        {
            _trainRepository.DeleteAsync(id);
        }

        public async Task DeleteAllBios(int applicationId)
        {
            _bioRepository.Delete(e => e.ApplicationId == applicationId);
        }

        public async Task DeleteAllTrainings(int applicationId)
        {
            _trainRepository.Delete(e => e.ApplicationId == applicationId);
        }

        //public async Task DeleteAllTrainings(int applicationId)
        //{
        //    var cnt = _trainRepository.GetAll().Where(e => e.ApplicationId == applicationId).Count()-1;
        //    var bCnt = true;
        //    var skp = 100;
        //    if (cnt > 0)
        //    {
        //        while (bCnt)
        //        {
        //            if (cnt < skp) {
        //                skp = cnt - skp-1; 
        //            }
        //            var lstId = _trainRepository.GetAll().Where(e => e.ApplicationId == applicationId).ToList().OrderBy(e => e.Id).Skip(skp).Take(1).FirstOrDefault().Id;
        //            _trainRepository.Delete(e => e.ApplicationId == applicationId && e.Id <= lstId);
        //            if (skp >= cnt) { bCnt = false; }
        //            skp += 100;
        //        }
        //    }
        //}

        public async Task<PagedResultDto<GetCBiodataForViewDto>> GetApplicationBios_(int applicationId)
        {

            var filteredCBiodatas = _bioRepository.GetAll()
                        .Where(a => a.ApplicationId == applicationId);

            var pagedAndFilteredCBiodatas = filteredCBiodatas;

            var cBiodatas = from o in pagedAndFilteredCBiodatas
                            select new GetCBiodataForViewDto()
                            {
                                Biodata = new CBioDataForViewDto()
                                {
                                    ApplicationId = applicationId,
                                    SA_Id_Number = o.SA_Id_Number,
                                    Passport_Number = o.Passport_Number,
                                    Firstname = o.Firstname,
                                    Middlename = o.Middlename,
                                    Surname = o.Surname,
                                    Birth_Year = o.Birth_Year,
                                    Gender = o.Gender,
                                    Race = o.Race,
                                    Disability = o.Disability,
                                    Nationality = o.Nationality,
                                    Province = o.Province,
                                    Municipality = o.Municipality,
                                    Highest_Qualification_Type = o.Highest_Qualification_Type,
                                    Employment_Status = o.Employment_Status,
                                    Occupation_Level_For_Equity_Reporting = o.Occupation_Level_For_Equity_Reporting,
                                    Organisational_Structure_Filter = o.Organisational_Structure_Filter,
                                    Post_Reference = o.Post_Reference,
                                    Job_Title = o.Job_Title,
                                    OFO_Occupation_Code = o.OFO_Occupation_Code,
                                    OFO_Specialisation = o.OFO_Specialisation,
                                    OFO_Occupation = o.OFO_Occupation,
                                    UserId = o.UserId,
                                    DateCreated = DateTime.Now,
                                    Status = o.Status,
                                    Id = o.Id
                                }
                            };

            var totalCount = filteredCBiodatas.Count();

            return new PagedResultDto<GetCBiodataForViewDto>(
                totalCount,
                cBiodatas.ToList()
            );
        }

        public async Task<PagedResultDto<GetCBiodataForViewDto>> GetAllBio(int first, int rows, int ApplicationId)
        {

            var filteredCBiodatas = _bioRepository.GetAll()
                .Where(a => a.ApplicationId == ApplicationId)
                .OrderByDescending(a => a.Id)
                .Skip(first)
                .Take(rows)
                .ToList();

            var cBiodatas = from o in filteredCBiodatas
                            select new GetCBiodataForViewDto()
                            {
                                Biodata = new CBioDataForViewDto
                                {
                                    ApplicationId = ApplicationId,
                                    SA_Id_Number = o.SA_Id_Number,
                                    Passport_Number = o.Passport_Number,
                                    Firstname = o.Firstname,
                                    Middlename = o.Middlename,
                                    Surname = o.Surname,
                                    Birth_Year = o.Birth_Year,
                                    Gender = o.Gender,
                                    Race = o.Race,
                                    Disability = o.Disability,
                                    Nationality = o.Nationality,
                                    Province = o.Province,
                                    Municipality = o.Municipality,
                                    Highest_Qualification_Type = o.Highest_Qualification_Type,
                                    Employment_Status = o.Employment_Status,
                                    Occupation_Level_For_Equity_Reporting = o.Occupation_Level_For_Equity_Reporting,
                                    Organisational_Structure_Filter = o.Organisational_Structure_Filter,
                                    Post_Reference = o.Post_Reference,
                                    Job_Title = o.Job_Title,
                                    OFO_Occupation_Code = o.OFO_Occupation_Code,
                                    OFO_Specialisation = o.OFO_Specialisation,
                                    OFO_Occupation = o.OFO_Occupation,
                                    UserId = o.UserId,
                                    DateCreated = o.DateCreated,
                                    Status = o.Status,
                                    Id = o.Id
                                }
                            };

            var totalCount = (_bioRepository.GetAll()
                        .Where(a => a.ApplicationId == ApplicationId)).Count();

            return new PagedResultDto<GetCBiodataForViewDto>(
                totalCount,
                cBiodatas.ToList()
            );
        }


        public async Task<PagedResultDto<GetCBiodataForViewDto>> GetBioErrors(int ApplicationId)
        {
            var filteredCBiodatas = _bioRepository.GetAll()
                        .Where(a => a.ApplicationId == ApplicationId && a.Status == "Fatal");

            var pagedAndFilteredCBiodatas = filteredCBiodatas;

            var cBiodatas = from o in pagedAndFilteredCBiodatas
                            select new GetCBiodataForViewDto()
                            {
                                Biodata = new CBioDataForViewDto
                                {
                                    ApplicationId = ApplicationId,
                                    SA_Id_Number = o.SA_Id_Number,
                                    Passport_Number = o.Passport_Number,
                                    Firstname = o.Firstname,
                                    Middlename = o.Middlename,
                                    Surname = o.Surname,
                                    Birth_Year = o.Birth_Year,
                                    Gender = o.Gender,
                                    Race = o.Race,
                                    Disability = o.Disability,
                                    Nationality = o.Nationality,
                                    Province = o.Province,
                                    Municipality = o.Municipality,
                                    Highest_Qualification_Type = o.Highest_Qualification_Type,
                                    Employment_Status = o.Employment_Status,
                                    Occupation_Level_For_Equity_Reporting = o.Occupation_Level_For_Equity_Reporting,
                                    Organisational_Structure_Filter = o.Organisational_Structure_Filter,
                                    Post_Reference = o.Post_Reference,
                                    Job_Title = o.Job_Title,
                                    OFO_Occupation_Code = o.OFO_Occupation_Code,
                                    OFO_Specialisation = o.OFO_Specialisation,
                                    OFO_Occupation = o.OFO_Occupation,
                                    UserId = o.UserId,
                                    DateCreated = o.DateCreated,
                                    Status = o.Status,
                                    Comment = o.Comment,
                                    Id = o.Id
                                }
                            };

            var totalCount = filteredCBiodatas.Count();

            return new PagedResultDto<GetCBiodataForViewDto>(
                totalCount,
                cBiodatas.ToList()
            );
        }

        public async Task<string> CheckBioErrors(int ApplicationId)
        {
            var nerrors = _bioRepository.GetAll()
                        .Where(a => a.ApplicationId == ApplicationId && a.Status == "Fatal").Count();



            return nerrors.ToString(); 
        }

        public async Task CreateOrEditTraining(CreateOrEditTrainingDto input)
        {
            if (input.Id == 0)
            {
                var training = ObjectMapper.Map<Training>(input);

                await _trainRepository.InsertAsync(training);
            }
            else
            {
                var training = await _trainRepository.FirstOrDefaultAsync(input.Id);
                ObjectMapper.Map(input, training);
            }
        }

        public async Task<PagedResultDto<GetTrainingForViewDto>> GetApplicationTrainings(int first, int rows, int applicationId)
        {

            var filteredTrainings = _trainRepository.GetAll()
                        .Where(a => a.ApplicationId == applicationId)
                        .OrderByDescending(a => a.Id)
                        .Skip(first)
                        .Take(rows)
                        .ToList();
            ;

            var trainings = from o in filteredTrainings
                            select new GetTrainingForViewDto()
                            {
                                Training = new TrainingForViewDto()
                                {
                                    ApplicationId = o.ApplicationId,
                                    SA_Id_Number = o.SA_Id_Number,
                                    Passport_Number = o.Passport_Number,
                                    Qualification_Learning_Program_Type = o.Qualification_Learning_Program_Type.Replace('_',' '),
                                    Details_Of_Learning_Program = o.Details_Of_Learning_Program,
                                    Study_Field_Or_Specialisation_Specification = o.Study_Field_Or_Specialisation_Specification,
                                    Total_Training_Cost = o.Total_Training_Cost,
                                    Achievement_status = o.Achievement_status,
                                    Year_enrolled_or_completed = o.Year_enrolled_or_completed,
                                    UserId = o.UserId,
                                    BiodataId = o.BiodataId,
                                    Id = o.Id
                                }
                            };

            var totalCount = (_trainRepository.GetAll()
                        .Where(a => a.ApplicationId == applicationId)).ToList().Count();

            return new PagedResultDto<GetTrainingForViewDto>(
                totalCount,
                 trainings.ToList()
            );
        }

        public async Task<PagedResultDto<GetTrainingForViewDto>> GetTrainingErrors(int ApplicationId)
        {

            var filteredTrainings = _trainRepository.GetAll()
                        .Where(a => a.ApplicationId == ApplicationId && a.Status == "Fatal");

            var pagedAndFilteredTrainings = filteredTrainings;

            var trainings = from o in pagedAndFilteredTrainings
                            select new GetTrainingForViewDto()
                            {
                                Training = new TrainingForViewDto()
                                {
                                    ApplicationId = o.ApplicationId,
                                    SA_Id_Number = o.SA_Id_Number,
                                    Passport_Number = o.Passport_Number,
                                    Qualification_Learning_Program_Type = o.Qualification_Learning_Program_Type,
                                    Details_Of_Learning_Program = o.Details_Of_Learning_Program,
                                    Study_Field_Or_Specialisation_Specification = o.Study_Field_Or_Specialisation_Specification,
                                    Total_Training_Cost = o.Total_Training_Cost,
                                    Achievement_status = o.Achievement_status,
                                    Year_enrolled_or_completed = o.Year_enrolled_or_completed,
                                    UserId = o.UserId,
                                    BiodataId = o.BiodataId,
                                    Status = o.Status,
                                    Comment = o.Comment,
                                    Id = o.Id
                                }
                            };

            var totalCount = filteredTrainings.Count();

            return new PagedResultDto<GetTrainingForViewDto>(
                totalCount,
                 trainings.ToList()
            );
        }

        public async Task<string> CheckTrainingErrors(int ApplicationId)
        {

            var nerrors = _trainRepository.GetAll()
                        .Where(a => a.ApplicationId == ApplicationId && a.Status == "Fatal").Count();

            return nerrors.ToString();
        }
        public async Task<PagedResultDto<GetHTVFForViewDto>> GetHTFVErrors(int ApplicationId)
        {
            var filteredHTFV = _htvfRepository.GetAll()
                        .Where(a => a.ApplicationId == ApplicationId && a.Status == "Fatal");

            var pagedAndFilteredTrainings = filteredHTFV;

            var htfv = from o in pagedAndFilteredTrainings
                       select new GetHTVFForViewDto()
                       {
                           HTVF = new HTVFDto()
                           {
                               ApplicationId = o.ApplicationId,
                               OCCUPATION_OR_SPECIALISATION_TITLE = o.OCCUPATION_OR_SPECIALISATION_TITLE,
                               OCCUPATION_CODE = o.OCCUPATION_CODE,
                               PRIMARY_REASON = o.PRIMARY_REASON,
                               FURTHER_REASON = o.FURTHER_REASON,
                               FURTHER_REASON_1 = o.FURTHER_REASON_1,
                               COMMENTS = o.COMMENTS,
                               PROVINCE = o.PROVINCE,
                               NUMBER_OF_VACANCIES = o.NUMBER_OF_VACANCIES,
                               Comment = o.Comment,
                               Status = o.Status,
                               UserId = o.UserId,
                               DateCreated = o.DateCreated,
                               UsrUpd = o.UsrUpd,
                               DteUpd = o.DteUpd,
                               Id = o.Id
                           }
                       };


            var totalCount = filteredHTFV.Count();

            return new PagedResultDto<GetHTVFForViewDto>(
                totalCount,
                 htfv.ToList()
            );
        }

        public async Task<PagedResultDto<GetSkillGabForViewDto>> GetSkillsGapsErrors(int ApplicationId)
        {

            var filteredSkillsGaps = _skillGabRepository.GetAll()
                        .Where(a => a.ApplicationId == ApplicationId && a.Status == "Fatal");

            var pagedAndFilteredGaps = filteredSkillsGaps;

            var skillsgaps = from o in pagedAndFilteredGaps
                             select new GetSkillGabForViewDto()
                             {
                                 SkillGab = new SkillGabDto()
                                 {
                                     ApplicationId = o.ApplicationId,
                                     OCCUPATION_OR_SPECIALISATION_TITLE = o.OCCUPATION_OR_SPECIALISATION_TITLE,
                                     Code = o.Code,
                                     SKILL_GAB = o.SKILL_GAB,
                                     REASON_FOR_THE_SKILLS_GAP = o.REASON_FOR_THE_SKILLS_GAP,
                                     ADDITIONAL_COMMENTS = o.ADDITIONAL_COMMENTS,
                                     Comment = o.Comment,
                                     Status = o.Status,
                                     UserID = o.UserID,
                                     DateCreated = o.DateCreated,
                                     UsrUpd = o.UsrUpd,
                                     DteUpd = o.DteUpd,
                                     Id = o.Id
                                 }
                             };


            var totalCount = filteredSkillsGaps.Count();

            return new PagedResultDto<GetSkillGabForViewDto>(
                totalCount,
                 skillsgaps.ToList()
            );
        }

        public async Task<PagedResultDto<GetHTVFForViewDto>> GetHTVFsForView(int ApplicationId)
        {
            var filteredHTVFs = _htvfRepository.GetAll()
                        .Where(a => a.ApplicationId == ApplicationId);

            var pagedAndFilteredHTVFs = filteredHTVFs;

            var htvFs = from o in pagedAndFilteredHTVFs
                        select new GetHTVFForViewDto()
                        {
                            HTVF = new HTVFDto
                            {
                                ApplicationId = o.ApplicationId,
                                OCCUPATION_OR_SPECIALISATION_TITLE = o.OCCUPATION_OR_SPECIALISATION_TITLE,
                                OCCUPATION_CODE = o.OCCUPATION_CODE,
                                PRIMARY_REASON = o.PRIMARY_REASON,
                                FURTHER_REASON = o.FURTHER_REASON,
                                FURTHER_REASON_1 = o.FURTHER_REASON_1,
                                COMMENTS = o.COMMENTS,
                                PROVINCE = o.PROVINCE,
                                NUMBER_OF_VACANCIES = o.NUMBER_OF_VACANCIES,
                                UserId = o.UserId,
                                Id = o.Id
                            }
                        };

            var totalCount = filteredHTVFs.Count();

            return new PagedResultDto<GetHTVFForViewDto>(
                totalCount,
                htvFs.ToList()
            );
        }

        public async Task<GetHTVFForViewDto> GetHTVFForView(int id)
        {
            var htvf = await _htvfRepository.GetAsync(id);

            var output = new GetHTVFForViewDto { HTVF = ObjectMapper.Map<HTVFDto>(htvf) };

            return output;
        }

        public async Task<CBiodataDto> GetBioDataForEdit(int Id)
        {
            var b = await _bioRepository.FirstOrDefaultAsync(Id);

            var output = ObjectMapper.Map<CBiodataDto>(b);

            return output;
        }

        public async Task<TrainingDto> GetTrainingForEdit(int Id)
        {
            var t = await _trainRepository.FirstOrDefaultAsync(Id);

            var output = ObjectMapper.Map<TrainingDto>(t);

            return output;
        }

        public async Task<HTVFDto> GetHTVFForEdit(int Id)
        {
            var htvf = await _htvfRepository.FirstOrDefaultAsync(Id);

            var output = ObjectMapper.Map<HTVFDto>(htvf);

            return output;
        }

        public async Task<int> CreateOrEditHTFV(HTVFDto data)
        {

            if (data.Id == 0)
            {
                // if the id 
                var item = ObjectMapper.Map<HTVF>(data);
                item.DateCreated = DateTime.Now;
                await _htvfRepository.InsertAsync(item);
            }
            else
            {
                var htvf = await _htvfRepository.FirstOrDefaultAsync(data.Id);
                htvf.OCCUPATION_CODE = data.OCCUPATION_CODE;
                htvf.OCCUPATION_OR_SPECIALISATION_TITLE = data.OCCUPATION_OR_SPECIALISATION_TITLE;
                htvf.PRIMARY_REASON = data.PRIMARY_REASON;
                htvf.FURTHER_REASON = data.FURTHER_REASON;
                htvf.FURTHER_REASON_1 = data.FURTHER_REASON_1;
                htvf.PROVINCE = data.PROVINCE;
                await _htvfRepository.UpdateAsync(htvf);
            }

            var cnt = _financE_AND_TRAINING_COMPARISONRepository.GetAll().Where(e => e.ApplicationId == data.ApplicationId).Count();
            cnt = cnt + _bioRepository.GetAll().Where(e => e.ApplicationId == data.ApplicationId).Count();
            cnt = cnt + _trainRepository.GetAll().Where(e => e.ApplicationId == data.ApplicationId).Count();
            cnt = cnt + _htvfRepository.GetAll().Where(e => e.ApplicationId == data.ApplicationId).Count();
            cnt = cnt + _skillGabRepository.GetAll().Where(e => e.ApplicationId == data.ApplicationId).Count();
            cnt = cnt + _financE_AND_TRAINING_COMPARISONRepository.GetAll().Where(e => e.ApplicationId == data.ApplicationId).Count() + 1;

            return cnt;

        }

        public async Task CreateOrEditHTFV_(HTVFDto input)
        {
            if (input.Id == null)
            {
                var htvf = ObjectMapper.Map<HTVF>(input);

                await _htvfRepository.InsertAsync(htvf);
            }
            else
            {
                var htvf = await _htvfRepository.FirstOrDefaultAsync(input.Id);
                htvf.OCCUPATION_CODE = input.OCCUPATION_CODE;
                htvf.OCCUPATION_OR_SPECIALISATION_TITLE = input.OCCUPATION_OR_SPECIALISATION_TITLE;
                htvf.PRIMARY_REASON = input.PRIMARY_REASON;
                htvf.FURTHER_REASON = input.FURTHER_REASON;
                htvf.FURTHER_REASON_1 = input.FURTHER_REASON_1;
                htvf.PROVINCE = input.PROVINCE;
                await _htvfRepository.UpdateAsync(htvf);
            }
        }
        public async Task DeleteHTFVId(int id)
        {
            _htvfRepository.DeleteAsync(id);
        }

        public async Task DeleteAllHTFVs(int applicationId)
        {
            await _htvfRepository.DeleteAsync(e => e.ApplicationId == applicationId);
        }

        public async Task<PagedResultDto<GetSkillGabForViewDto>> GetAllSkillsGabs(int ApplicationId)
        {

            var filteredSkillGabs = _skillGabRepository.GetAll()
                        .Where(a => a.ApplicationId == ApplicationId);

            var pagedAndFilteredSkillGabs = filteredSkillGabs;

            var skillGabs = from o in pagedAndFilteredSkillGabs
                            select new GetSkillGabForViewDto()
                            {
                                SkillGab = new SkillGabDto
                                {
                                    ApplicationId = o.ApplicationId,
                                    OCCUPATION_OR_SPECIALISATION_TITLE = o.OCCUPATION_OR_SPECIALISATION_TITLE,
                                    Code = o.Code,
                                    SKILL_GAB = o.SKILL_GAB,
                                    REASON_FOR_THE_SKILLS_GAP = o.REASON_FOR_THE_SKILLS_GAP,
                                    ADDITIONAL_COMMENTS = o.ADDITIONAL_COMMENTS,
                                    UserID = o.UserID,
                                    Id = o.Id
                                }
                            };

            var totalCount = filteredSkillGabs.Count();

            return new PagedResultDto<GetSkillGabForViewDto>(
                totalCount,
                skillGabs.ToList()
            );
        }

        public async Task<GetSkillGabForViewDto> GetSkillGabForView(int id)
        {
            var skillGab = await _skillGabRepository.GetAsync(id);

            var output = new GetSkillGabForViewDto { SkillGab = ObjectMapper.Map<SkillGabDto>(skillGab) };

            return output;
        }

        public async Task<SkillGabDto> GetSkillGabForEdit(int Id)
        {
            var skillGab = await _skillGabRepository.FirstOrDefaultAsync(Id);

            var output = ObjectMapper.Map<SkillGabDto>(skillGab);

            return output;
        }

        public async Task<int> CreateOrEditSkillsGap(SkillGabDto data)
        {

            data.DateCreated = DateTime.Now;
            if (data.Id == 0)
            {
                // if the id 
                var item = ObjectMapper.Map<SkillGab>(data);
                item.DateCreated = DateTime.Now;
                await _skillGabRepository.InsertAsync(item);
            }
            else
            {
                var sg = await _skillGabRepository.FirstOrDefaultAsync(data.Id);
                sg.Code = data.Code;
                sg.OCCUPATION_OR_SPECIALISATION_TITLE = data.OCCUPATION_OR_SPECIALISATION_TITLE;
                sg.SKILL_GAB = data.SKILL_GAB;
                sg.REASON_FOR_THE_SKILLS_GAP = data.REASON_FOR_THE_SKILLS_GAP;
                sg.Comment = data.Comment;
                await _skillGabRepository.UpdateAsync(sg);
            }

            var cnt = _financE_AND_TRAINING_COMPARISONRepository.GetAll().Where(e => e.ApplicationId == data.ApplicationId).Count();
            cnt = cnt + _bioRepository.GetAll().Where(e => e.ApplicationId == data.ApplicationId).Count();
            cnt = cnt + _trainRepository.GetAll().Where(e => e.ApplicationId == data.ApplicationId).Count();
            cnt = cnt + _htvfRepository.GetAll().Where(e => e.ApplicationId == data.ApplicationId).Count();
            cnt = cnt + _skillGabRepository.GetAll().Where(e => e.ApplicationId == data.ApplicationId).Count();
            cnt = cnt + _financE_AND_TRAINING_COMPARISONRepository.GetAll().Where(e => e.ApplicationId == data.ApplicationId).Count() + 1;

            return cnt;

        }

        public async Task CreateOrEditSkillsGap__(SkillGabDto input)
        {
            if (input.Id == 0)
            {
                var skillGab = ObjectMapper.Map<SkillGab>(input);

                await _skillGabRepository.InsertAsync(skillGab);
            }
            else
            {
                var skillGab = await _skillGabRepository.FirstOrDefaultAsync((int)input.Id);
                ObjectMapper.Map(input, skillGab);
            }
        }

        public async Task deleteSkillsGapId(int id)
        {
            await _skillGabRepository.DeleteAsync(id);
        }

        public async Task deleteAllSkillsGaps(int applicationId)
        {
            await _skillGabRepository.DeleteAsync(e => e.ApplicationId == applicationId);
        }

        public async Task DeleteFinanceId(int id)
        {
            await _financE_AND_TRAINING_COMPARISONRepository.DeleteAsync(id);
        }

        public async Task DeleteAlFinancess(int applicationId)
        {
            await _financE_AND_TRAINING_COMPARISONRepository.DeleteAsync(e => e.ApplicationId == applicationId);
        }

        public async Task<PagedResultDto<GetFINANCE_AND_TRAINING_COMPARISONForViewDto>> GetAllFinances(int ApplicationId)
        {

            var filteredFINANCE_AND_TRAINING_COMPARISONs = _financE_AND_TRAINING_COMPARISONRepository.GetAll()
                        .Where(a => a.ApplicationId == ApplicationId);

            var pagedAndFilteredFINANCE_AND_TRAINING_COMPARISONs = filteredFINANCE_AND_TRAINING_COMPARISONs;

            var financE_AND_TRAINING_COMPARISONs = from o in pagedAndFilteredFINANCE_AND_TRAINING_COMPARISONs
                                                   select new GetFINANCE_AND_TRAINING_COMPARISONForViewDto()
                                                   {
                                                       FINANCE_AND_TRAINING_COMPARISON = new FINANCE_AND_TRAINING_COMPARISONDto
                                                       {
                                                           ApplicationId = o.ApplicationId,
                                                           TOTAL_ACTUAL_PAYROLL_FOR_THE_YEAR = o.TOTAL_ACTUAL_PAYROLL_FOR_THE_YEAR,
                                                           TOTAL_ACTUAL_SKILLS_DEVELOPMENT_SPEND_FOR_THE_YEAR = o.TOTAL_ACTUAL_SKILLS_DEVELOPMENT_SPEND_FOR_THE_YEAR,
                                                           OF_PAYROLL_SPENT_ON_SKILLS_DEVELOPMENT = o.OF_PAYROLL_SPENT_ON_SKILLS_DEVELOPMENT,
                                                           TOTAL_PROJECTED_PAYROLL_FOR_THE_YEAR = o.TOTAL_PROJECTED_PAYROLL_FOR_THE_YEAR,
                                                           TOTAL_PROJECTED_SKILLS_DEVELOPMENT_BUDGET = o.TOTAL_PROJECTED_SKILLS_DEVELOPMENT_BUDGET,
                                                           PROJECTED_PAYROLL = o.PROJECTED_PAYROLL,
                                                           BENEFICIARIES_TRAIN = o.BENEFICIARIES_TRAIN,
                                                           TOTAL_BENEFICIARIES_ACTUALLY_TRAINED_IN_THE = o.TOTAL_BENEFICIARIES_ACTUALLY_TRAINED_IN_THE,
                                                           ACTUAL_TRAINING_VS_PLANNED_TRAINING = o.ACTUAL_TRAINING_VS_PLANNED_TRAINING,
                                                           CONFIRMATION_OF_EMPLOYEES_HIGHEST_QUALIFICATIONS = o.CONFIRMATION_OF_EMPLOYEES_HIGHEST_QUALIFICATIONS,
                                                           LEARNING_OPPORTUNITIES_UNEMPLOYED_PEOPLE = o.LEARNING_OPPORTUNITIES_UNEMPLOYED_PEOPLE,
                                                           LEARNING_AREAS_AND_OPPORTUNITIES_FOR_EMPLOYED_STAFF = o.LEARNING_AREAS_AND_OPPORTUNITIES_FOR_EMPLOYED_STAFF,
                                                           ADDRESSING_EQUITY_AND_BBBEE_TARGETS = o.ADDRESSING_EQUITY_AND_BBBEE_TARGETS,
                                                           WORK_PLACEMENT = o.WORK_PLACEMENT,
                                                           AREAS_FOR_RESEARCH_AND_INNOVATION = o.AREAS_FOR_RESEARCH_AND_INNOVATION,
                                                           LEARNERS_RETAINED = o.LEARNERS_RETAINED,
                                                           PEOPLE_FOUND_EMPLOYMENT_DUE_TRAINING = o.PEOPLE_FOUND_EMPLOYMENT_DUE_TRAINING,
                                                           UserId = o.UserId,
                                                           General_Comments = o.General_Comments,
                                                           Id = o.Id
                                                       }
                                                   };

            var totalCount = filteredFINANCE_AND_TRAINING_COMPARISONs.Count();

            return new PagedResultDto<GetFINANCE_AND_TRAINING_COMPARISONForViewDto>(
                totalCount,
                 financE_AND_TRAINING_COMPARISONs.ToList()
            );
        }

        public async Task<GetFINANCE_AND_TRAINING_COMPARISONForViewDto> GetFINANCE_AND_TRAINING_COMPARISONForView(int id)
        {
            var financE_AND_TRAINING_COMPARISON = await _financE_AND_TRAINING_COMPARISONRepository.GetAsync(id);

            var output = new GetFINANCE_AND_TRAINING_COMPARISONForViewDto { FINANCE_AND_TRAINING_COMPARISON = ObjectMapper.Map<FINANCE_AND_TRAINING_COMPARISONDto>(financE_AND_TRAINING_COMPARISON) };

            return output;
        }

        public async Task<FINANCE_AND_TRAINING_COMPARISONDto> GetFINANCEForEdit(int Id)
        {
            var financE_AND_TRAINING_COMPARISON = await _financE_AND_TRAINING_COMPARISONRepository.FirstOrDefaultAsync(Id);

            var output = ObjectMapper.Map<FINANCE_AND_TRAINING_COMPARISONDto>(financE_AND_TRAINING_COMPARISON);

            return output;
        }

        public async Task<int> CreateOrEditFinance(FINANCE_AND_TRAINING_COMPARISONDto data)
        {
            if (data.Id > 0)
            {
                data.DteUpd = DateTime.Now;

                var financE_AND_TRAINING_COMPARISON = await _financE_AND_TRAINING_COMPARISONRepository.FirstOrDefaultAsync((int)data.Id);
                ObjectMapper.Map(data, financE_AND_TRAINING_COMPARISON);
            }
            else
            {
                var fin = _financE_AND_TRAINING_COMPARISONRepository.GetAll().Where(a => a.ApplicationId == data.ApplicationId).Count();
                if (fin == 0)
                {
                    data.DateCreated = DateTime.Now;

                    var item = ObjectMapper.Map<FINANCE_AND_TRAINING_COMPARISON>(data);
                    item.DateCreated = DateTime.Now;
                    await _financE_AND_TRAINING_COMPARISONRepository.InsertAsync(item);
                }
            }

            var cnt = _financE_AND_TRAINING_COMPARISONRepository.GetAll().Where(e => e.ApplicationId == data.ApplicationId).Count();
            cnt = cnt + _bioRepository.GetAll().Where(e => e.ApplicationId == data.ApplicationId).Count();
            cnt = cnt + _trainRepository.GetAll().Where(e => e.ApplicationId == data.ApplicationId).Count();
            cnt = cnt + _htvfRepository.GetAll().Where(e => e.ApplicationId == data.ApplicationId).Count();
            cnt = cnt + _skillGabRepository.GetAll().Where(e => e.ApplicationId == data.ApplicationId).Count();
            cnt = cnt + _financE_AND_TRAINING_COMPARISONRepository.GetAll().Where(e => e.ApplicationId == data.ApplicationId).Count() + 1;

            return cnt;
        }

        public async Task CreateOrEditFinance_(FINANCE_AND_TRAINING_COMPARISONDto input)
        {
            if (input.Id == null)
            {
                var financE_AND_TRAINING_COMPARISON = ObjectMapper.Map<FINANCE_AND_TRAINING_COMPARISON>(input);

                await _financE_AND_TRAINING_COMPARISONRepository.InsertAsync(financE_AND_TRAINING_COMPARISON);
            }
            else
            {
                var financE_AND_TRAINING_COMPARISON = await _financE_AND_TRAINING_COMPARISONRepository.FirstOrDefaultAsync((int)input.Id);
                ObjectMapper.Map(input, financE_AND_TRAINING_COMPARISON);
            }
        }

        public async Task<bool> ValidateOFOCode(string code)
        {
            var validcode = false;
            var ofocode = _occ.GetAll().Where(e => e.OFO_Code == code).FirstOrDefault().OFO_Code;

            if (ofocode.Length == 0)
            {
                validcode = false;
            }
            else
            {
                validcode = true;
            }

            return validcode;
        }

        public async Task<string> GetOFOOccupation(string code)
        {
            var occupation = "";
            var occup = _occ.GetAll().Where(e => e.OFO_Code == code).FirstOrDefault();

            if (occup.Id == null)
            {
                occupation = "";
            }
            else
            {
                occupation = occup.Description;
            }

            return occupation;
        }

        public Boolean validsaid(string id)
        {

            return true;
        }

        public async Task<string> ValidateAllData(int applicationId)
        {
            var output = "";

            var bio = _bioRepository.GetAll()
                        .Where(a => a.ApplicationId == applicationId);

            foreach (var b in bio)
            {
                if (!validsaid(b.SA_Id_Number))
                {
                    output = "Invlid ID Number";
                }
            }

            return output;
        }

        public async Task ValidateBioGrantSubmission(int applicationId)
        {
            var mand = _mandatoryApplication.GetAll().Where(a => a.Id == applicationId).FirstOrDefault();

            if (mand.Id > 0)
            {
                mand.UsrUpd =  1;
                await _mandatoryApplication.UpdateAsync(mand);
            }
            //var msg = "";
            //var err = "";
            //var stat = "";
            //var outcome = "";

            //var bio = _bioRepository.GetAll().Where(e => e.ApplicationId == applicationId).ToList();
            //foreach (var b in bio)
            //{
            //    msg = "";
            //    err = "";
            //    stat = "";
            //    if ((b.SA_Id_Number == "" || b.SA_Id_Number == null) && (b.Passport_Number == "" || b.Passport_Number == null))
            //    {
            //        err = err + ",Missing ID/Passport,";
            //        stat = "Fatal";
            //        msg = "Error";
            //    }

            //    //if (b.SA_Id_Number == null && checkIDNumber(b.SA_Id_Number) == false)
            //    //{
            //    //    err = err + ",Invalid ID Number,";
            //    //    stat = "Fatal";
            //    //    msg = "Error";
            //    //}

            //    if (b.Firstname == "" || b.Firstname == null || b.Surname == "" || b.Surname == null)
            //    {
            //        err = err + ",Missing Name/Surname";
            //        stat = "Fatal";
            //        msg = "Error";
            //    }

            //    if (b.Gender == "" || b.Gender == null)
            //    {
            //        err = err + ",Missing Gender";
            //        stat = "Fatal";
            //        msg = "Error";
            //    }
            //    else
            //    {
            //        if (b.Gender != "Male" && b.Gender != "Female")
            //        {
            //            err = err + ",Invalid Gender";
            //            stat = "Fatal";
            //            msg = "Error";
            //        }
            //    }

            //    if (b.Race == "" || b.Race == null)
            //    {
            //        err = err + ",Missing Race";
            //        stat = "Fatal";
            //        msg = "Error";
            //    }
            //    else
            //    {
            //        var r = _equity.GetAll().Where(e => e.Description == b.Race).Count();
            //        if (r == 0)
            //        {
            //            err = err + ",Invalid Equity";
            //            stat = "Fatal";
            //            msg = "Error";
            //        }
            //    }

            //    if (b.Nationality == "" || b.Nationality == null)
            //    {
            //        err = err + ",Missing Nationality";
            //        b.Status = "Fatal";
            //        _bioRepository.Update(b);
            //        msg = "Error";
            //    }

            //    if (b.Province == "" || b.Province == null)
            //    {
            //        err = err + ",Missing Province";
            //        stat = "Fatal";
            //        msg = "Error";
            //    }

            //    if (b.Municipality == "" || b.Municipality == null)
            //    {
            //        err = err + ",Missing Municipality";
            //        b.Status = "Fatal";
            //        _bioRepository.Update(b);
            //        msg = "Error";
            //    }

            //    if (b.Highest_Qualification_Type == "" || b.Highest_Qualification_Type == null)
            //    {
            //        err = err + ",Missing Highest Qualification";
            //        stat = "Fatal";
            //        msg = "Error";
            //    }

            //    if (b.Employment_Status == "" || b.Employment_Status == null)
            //    {
            //        err = err + ",Missing Employment Status";
            //        stat = "Fatal";
            //        msg = "Error";
            //    }
            //    else
            //    {
            //        var es = _empStat.GetAll().Where(e => e.Employment_Status == b.Employment_Status).Count();
            //        if (es == 0)
            //        {
            //            err = err + ",Invalid Employment Status";
            //            stat = "Fatal";
            //            msg = "Error";
            //        }
            //    }

            //    if (b.Occupation_Level_For_Equity_Reporting == "" || b.Occupation_Level_For_Equity_Reporting == null)
            //    {
            //        err = err + ",Missing Occupational Level";
            //        stat = "Fatal";
            //        msg = "Error";
            //    }
            //    else
            //    {
            //        var ol = _occLevel.GetAll().Where(e => e.Occupational_Levels == b.Occupation_Level_For_Equity_Reporting).Count();
            //        if (ol == 0)
            //        {
            //            err = err + ",Invalid Occupational Level";
            //            stat = "Fatal";
            //            msg = "Error";
            //        }
            //    }

            //    if (b.OFO_Occupation_Code == "" || b.OFO_Occupation_Code == null || b.OFO_Occupation == "" || b.OFO_Occupation == null)
            //    {
            //        err = err + ",Missing OFO";
            //        stat = "Fatal";
            //        msg = "Error";
            //    }
            //    else
            //    {
            //        var o = _occ.GetAll().Where(e => e.OFO_Code == b.OFO_Occupation_Code).Count();
            //        if (o == 0)
            //        {
            //            err = err + ",Invalid OFO";
            //            stat = "Fatal";
            //            msg = "Error";
            //        }
            //    }

            //    if (b.OFO_Specialisation == "" || b.OFO_Specialisation == null)
            //    {
            //        err = err + ",Missing Specialization";
            //        stat = "Fatal";
            //        msg = "Error";
            //    }
            //    else
            //    {
            //        var o = _spec.GetAll().Where(e => e.Specilization == b.OFO_Specialisation).Count();
            //        if (o == 0)
            //        {
            //            err = err + ",Invalid Specialization";
            //            stat = "Fatal";
            //            msg = "Error";
            //        }

            //        if (b.OFO_Occupation_Code != null)
            //        {
            //            var os = _spec.GetAll().Where(e => e.Specilization == b.OFO_Specialisation && e.OFO_Code == b.OFO_Occupation_Code).Count();
            //            if (os == 0)
            //            {
            //                err = err + ",Specialization not linked to Occupation";
            //                stat = "Fatal";
            //                msg = "Error";
            //            }
            //        }
            //    }

            //    if (b.Birth_Year == null)
            //    {
            //        err = err + ",Missing Birth Year";
            //        stat = "Fatal";
            //        msg = "Error";
            //    }
            //    else
            //    {
            //        if (Int32.Parse(b.Birth_Year) > DateTime.Now.AddYears(-15).Year || Int32.Parse(b.Birth_Year) < DateTime.Now.AddYears(-90).Year)
            //        {
            //            err = err + ",Invalid Birth Year";
            //            stat = "Fatal";
            //            msg = "Error";
            //        }
            //    }

            //    if (msg == "")
            //    {
            //        b.Comment = null;
            //        b.Status = "Success";
            //        _bioRepository.Update(b);
            //    }
            //    else
            //    {
            //        b.Comment = err; ;
            //        b.Status = stat;
            //        _bioRepository.Update(b);
            //        outcome = msg;
            //    }
            //}

            //return outcome;

        }

        public async Task<string> ValidateTrainingGrantSubmission(int applicationId)
        {
            var output = "";
            var err = "";
            var stat = "";
            var outcome = "";

            var train = _trainRepository.GetAll().Where(e => e.ApplicationId == applicationId).ToList();
            foreach (var b in train)
            {
                output = "";
                err = "";
                stat = "";

                if ((b.SA_Id_Number == "" || b.SA_Id_Number == null) && (b.Passport_Number == "" || b.Passport_Number == null))
                {
                    err = err + ",Missing ID/Passport ";
                    stat = "Fatal";
                    output = "Error";
                }

                //if (b.SA_Id_Number != null && !checkIDNumber(b.SA_Id_Number))
                //{
                //    err = err + ",Invalid ID Number,";
                //    stat = "Fatal";
                //    output = "Error";
                //}

                if (b.SA_Id_Number != null)
                {
                    var id = _bioRepository.GetAll().Where(e => e.SA_Id_Number == b.SA_Id_Number).Count();
                    if (id == 0)
                    {
                        err = err + ",ID Missing from Bio Data";
                        stat = "Fatal";
                        output = "Error";
                    }
                }

                if (b.Passport_Number != null)
                {
                    var id = _bioRepository.GetAll().Where(e => e.Passport_Number == b.Passport_Number).Count();
                    if (id == 0)
                    {
                        err = err + ",Passport Missing from Bio Data";
                        stat = "Fatal";
                        output = "Error";
                    }
                }

                if (b.Qualification_Learning_Program_Type == "" || b.Qualification_Learning_Program_Type == null)
                {
                    err = err + ",Missing Programme Type ";
                    stat = "Fatal";
                    output = "Error";
                }

                if ((b.Details_Of_Learning_Program == "" || b.Details_Of_Learning_Program == null) && (b.Study_Field_Or_Specialisation_Specification == "" || b.Study_Field_Or_Specialisation_Specification == null))
                {
                    err = err + ",Missing Description/Study Field ";
                    stat = "Fatal";
                    output = "Error";
                }

                if (b.Total_Training_Cost == null)
                {
                    err = err + ",Missing Training Cost ";
                    stat = "Fatal";
                    output = "Error";
                }

                if (b.Achievement_status == "" || b.Achievement_status == null)
                {
                    err = err + ",Missing Achievement Status ";
                    stat = "Fatal";
                    output = "Error";
                }

                if (b.Year_enrolled_or_completed == 0 || b.Year_enrolled_or_completed == null)
                {
                    err = err + ", Missing Year Enrolled/Achieved";
                    stat = "Fatal";
                    output = "Error";
                }

                var p = _progType.GetAll().Where(e => e.Learning_Programmes == b.Qualification_Learning_Program_Type).Count();
                if (p == 0)
                {
                    err = err + ",Invalid Programme Type";
                    stat = "Fatal";
                    output = "Error";
                }

                var ast = _achStat.GetAll().Where(e => e.Achievement_Status == b.Achievement_status).Count();
                if (ast == 0)
                {
                    err = err + ",Invalid Achievement Status";
                    stat = "Fatal";
                    output = "Error";
                }

                if (output == "")
                {
                    b.Comment = null;
                    b.Status = "Success";
                    _trainRepository.Update(b);
                }
                else
                {
                    b.Comment = err; ;
                    b.Status = stat;
                    _trainRepository.Update(b);
                    outcome = "Error";
                }
            }

            return outcome;
        }

        public async Task<string> ValidateHTFVSubmission(int applicationId)
        {
            var output = "";
            var err = "";
            var stat = "";
            var outcome = "";

            var htfv = _htvfRepository.GetAll().Where(e => e.ApplicationId == applicationId).ToList();
            foreach (var b in htfv)
            {
                output = "";
                err = "";
                stat = "";

                if (b.OCCUPATION_CODE == "" || b.OCCUPATION_CODE == null || b.OCCUPATION_CODE == "" || b.OCCUPATION_CODE == null)
                {
                    err = err + ",Missing OFO";
                    stat = "Fatal";
                    output = "Error";
                }
                else
                {
                    var o = _occ.GetAll().Where(e => e.OFO_Code == b.OCCUPATION_CODE).Count();
                    if (o == 0)
                    {
                        err = err + ",Invalid OFO";
                        stat = "Fatal";
                        output = "Error";
                    }
                }

                if (b.OCCUPATION_OR_SPECIALISATION_TITLE == "" || b.OCCUPATION_OR_SPECIALISATION_TITLE == null)
                {
                    b.Comment = b.Comment + ",Missing Specialization";
                    b.Status = "Fatal";
                    _htvfRepository.Update(b);
                    output = "Error";
                }
                //else
                //{
                //    var s = _spec.GetAll().Where(e => e.Specilization == b.OCCUPATION_OR_SPECIALISATION_TITLE).Count();
                //    if (s == 0)
                //    {
                //        var o = _occ.GetAll().Where(e => e.Description == b.OCCUPATION_OR_SPECIALISATION_TITLE).Count();
                //        if (o == 0)
                //        {

                //            err = err + ",Invalid Specialization";
                //            stat = "Fatal";
                //            output = "Error";
                //            b.Comment = b.Comment + ",Invalid Specialization";
                //            b.Status = "Fatal";
                //            _htvfRepository.Update(b);
                //            output = "Error";
                //        }
                //    }
                //}

                if (output == "")
                {
                    b.Comment = null;
                    b.Status = "Success";
                    _htvfRepository.Update(b);
                }
                else
                {
                    b.Comment = err; ;
                    b.Status = stat;
                    _htvfRepository.Update(b);
                    outcome = "Error";
                }
            }

            return outcome;
        }


        public async Task<string> ValidateSkillsGapSubmission(int applicationId)
        {
            var output = "";
            var err = "";
            var stat = "";
            var outcome = "";

            var sg = _skillGabRepository.GetAll().Where(e => e.ApplicationId == applicationId).ToList();
            foreach (var b in sg)
            {
                output = "";
                err = "";
                stat = "";

                if (b.Code == "" || b.Code == null || b.Code == "" || b.Code == null)
                {
                    err = err + ",Missing OFO";
                    stat = "Fatal";
                    output = "Error";
                }
                else
                {
                    var o = _occ.GetAll().Where(e => e.OFO_Code == b.Code).Count();
                    if (o == 0)
                    {
                        err = err + ",Invalid OFO";
                        stat = "Fatal";
                        output = "Error";
                    }
                }

                if (b.OCCUPATION_OR_SPECIALISATION_TITLE == "" || b.OCCUPATION_OR_SPECIALISATION_TITLE == null)
                {
                    err = err + ",Missing Specialization";
                    stat = "Fatal";
                    output = "Error";
                }
                //else
                //{
                //    var o = _spec.GetAll().Where(e => e.Specilization == b.OCCUPATION_OR_SPECIALISATION_TITLE).Count();
                //    if (o == 0)
                //    {
                //        err = err + ",Invalid Specialization";
                //        stat = "Fatal";
                //        output = "Error";
                //    }
                //}

                if (output == "")
                {
                    b.Comment = null;
                    b.Status = "Success";
                    _skillGabRepository.Update(b);
                }
                else
                {
                    b.Comment = err; ;
                    b.Status = stat;
                    _skillGabRepository.Update(b);
                    outcome = "Error";
                }
            }

            return outcome;
        }

        public async Task<string> ValidateFinanceSubmission(int applicationId)
        {
            var output = "";
            var err = "";
            var stat = "";
            var outcome = "";

            var fin = _financE_AND_TRAINING_COMPARISONRepository.GetAll().Where(e => e.ApplicationId == applicationId).ToList();
            foreach (var b in fin)
            {
                output = "";
                err = "";
                stat = "";

                if (b.TOTAL_ACTUAL_PAYROLL_FOR_THE_YEAR == null || b.TOTAL_ACTUAL_PAYROLL_FOR_THE_YEAR == 0 ||
                    b.TOTAL_PROJECTED_PAYROLL_FOR_THE_YEAR == null || b.TOTAL_PROJECTED_PAYROLL_FOR_THE_YEAR == 0 ||
                    b.TOTAL_ACTUAL_SKILLS_DEVELOPMENT_SPEND_FOR_THE_YEAR == null || b.TOTAL_PROJECTED_SKILLS_DEVELOPMENT_BUDGET == null ||
                    b.PROJECTED_PAYROLL == null || b.OF_PAYROLL_SPENT_ON_SKILLS_DEVELOPMENT == null ||
                    b.BENEFICIARIES_TRAIN == null || b.TOTAL_BENEFICIARIES_ACTUALLY_TRAINED_IN_THE == null ||
                    b.ACTUAL_TRAINING_VS_PLANNED_TRAINING == null ||
                    b.CONFIRMATION_OF_EMPLOYEES_HIGHEST_QUALIFICATIONS == null || b.CONFIRMATION_OF_EMPLOYEES_HIGHEST_QUALIFICATIONS == "" ||
                    b.LEARNING_OPPORTUNITIES_UNEMPLOYED_PEOPLE == null || b.LEARNING_OPPORTUNITIES_UNEMPLOYED_PEOPLE == "" ||
                    b.LEARNING_AREAS_AND_OPPORTUNITIES_FOR_EMPLOYED_STAFF == null || b.LEARNING_AREAS_AND_OPPORTUNITIES_FOR_EMPLOYED_STAFF == "" ||
                    b.ADDRESSING_EQUITY_AND_BBBEE_TARGETS == null || b.ADDRESSING_EQUITY_AND_BBBEE_TARGETS == "" ||
                    b.WORK_PLACEMENT == null || b.WORK_PLACEMENT == "" ||
                    b.AREAS_FOR_RESEARCH_AND_INNOVATION == null || b.AREAS_FOR_RESEARCH_AND_INNOVATION == "" ||
                    b.LEARNERS_RETAINED == null || b.PEOPLE_FOUND_EMPLOYMENT_DUE_TRAINING == null)
                {
                    err = err + ",Finance Details are not complete";
                    stat = "Fatal";
                    output = "Error";
                }
            }

            return outcome;
        }

        public async Task<string> isSubmissionValid(int applicationId)
        {
            var output = "";
            var bio = _bioRepository.GetAll().Count();
            if (bio == 0) { output = output + ", No bio data captured. Please capture before you continue"; }
            var tr = _trainRepository.GetAll().Count();
            if (tr == 0) { output = output + ", No training data captured.Please capture before you continue"; }
            var fin = _financE_AND_TRAINING_COMPARISONRepository.GetAll().Count();
            if (fin == 0) { output = output + ", No finance data captured. Please capture before you continue"; }
            bio = _bioRepository.GetAll().Where(e => e.Status == "Fatal").Count();
            if (bio != 0) { output = output + ", There are errors on bio data. Please validate your data."; }
            tr = _trainRepository.GetAll().Where(e => e.Status == "Fatal").Count();
            if (tr != 0) { output = output + ",There are errors on training data."; }
            var htfv = _htvfRepository.GetAll().Where(e => e.Status == "Fatal").Count();
            if (htfv != 0) { output = output + ",There are errors on HTFV data. Please validate your data."; }
            var sg = _skillGabRepository.GetAll().Where(e => e.Status == "Fatal").Count();
            if (sg != 0) { output = output + ",There are errors on Skills Gap data. Please validate your data."; }

            return output;
        }

        int generateLuhnDigit(string inputString)
        {
            var total = 0;
            var count = 0;
            for (var i = 0; i < inputString.Length; i++)
            {
                var multiple = count % 2 + 1;
                count++;
                var temp = multiple * +inputString[i];
                temp = (int)Math.Floor((decimal)temp / 10) + (temp % 10);
                total += temp;
            }

            total = (total * 9) % 10;
            return total;
        }

        bool checkIDNumber(string idNumber)
        {
            var number = idNumber.Remove(idNumber.Length - 1, 1);

            return this.generateLuhnDigit(number) == +idNumber[idNumber.Length - 1];
        }

        public async Task<PagedResultDto<MandWorkforceProfileView>> GetWorkplaceProfile(int applicationId)
        {
            var datagrouped = (from bio in _bioRepository.GetAll().Where(a => a.ApplicationId == applicationId && a.Employment_Status != "Terminated" && a.Employment_Status != "Unemployed (Contracted for period of Learning Programme)")
                               join ofo in _occ.GetAll() on bio.OFO_Occupation_Code equals ofo.OFO_Code
                               select new
                               {
                                   OFODescription = ofo.Description,
                                   OFOCode = bio.OFO_Occupation_Code,
                                   Municipality = bio.Municipality,
                                   ApplicationId = bio.ApplicationId
                               })
                .Where(a => a.ApplicationId == applicationId)
                .Distinct()
                .ToList();

            var lst = (from bio in _bioRepository.GetAll().Where(a => a.ApplicationId == applicationId && a.Employment_Status != "Terminated" && a.Employment_Status != "Unemployed (Contracted for period of Learning Programme)")
                       join ofo in _occ.GetAll() on bio.OFO_Occupation_Code equals ofo.OFO_Code
                       select new { OFOCode = bio.OFO_Occupation_Code, OFODescription = ofo.Description, Municipality = bio.Municipality, Gender = bio.Gender, Race = bio.Race, Disability = bio.Disability, Nationality = bio.Nationality, Birth_Year = bio.Birth_Year });

            var wf = new MandWorkforceProfile();
            List<MandWorkforceProfile> wfv = new List<MandWorkforceProfile>();
            wf.AM = 0; wf.AF = 0; wf.CM = 0; wf.CF = 0; wf.IM = 0; wf.IF = 0; wf.WM = 0; wf.WF = 0; wf.TM = 0; wf.TF = 0; wf.TD = 0; wf.TNSA = 0; wf.A35 = 0; wf.A55 = 0; wf.A55P = 0;

            foreach (var bio in datagrouped)
            {
                wf = new MandWorkforceProfile();
                wf.OFOCode = bio.OFOCode;
                wf.OFODescription = bio.OFODescription;
                wf.Municipality = bio.Municipality;
                wf.AM = (int)lst.Where(a => a.OFOCode == bio.OFOCode && a.OFODescription == bio.OFODescription && a.Municipality == bio.Municipality && a.Race == "African" && a.Gender == "Male").Count();
                wf.AF = (int)lst.Where(a => a.OFOCode == bio.OFOCode && a.OFODescription == bio.OFODescription && a.Municipality == bio.Municipality && a.Race == "African" && a.Gender == "Female").Count();
                wf.CM = (int)lst.Where(a => a.OFOCode == bio.OFOCode && a.OFODescription == bio.OFODescription && a.Municipality == bio.Municipality && a.Race == "Coloured" && a.Gender == "Male").Count();
                wf.CF = (int)lst.Where(a => a.OFOCode == bio.OFOCode && a.OFODescription == bio.OFODescription && a.Municipality == bio.Municipality && a.Race == "Coloured" && a.Gender == "Female").Count();
                wf.IM = (int)lst.Where(a => a.OFOCode == bio.OFOCode && a.OFODescription == bio.OFODescription && a.Municipality == bio.Municipality && a.Race == "Indian" && a.Gender == "Male").Count();
                wf.IF = (int)lst.Where(a => a.OFOCode == bio.OFOCode && a.OFODescription == bio.OFODescription && a.Municipality == bio.Municipality && a.Race == "Indian" && a.Gender == "Female").Count();
                wf.WM = (int)lst.Where(a => a.OFOCode == bio.OFOCode && a.OFODescription == bio.OFODescription && a.Municipality == bio.Municipality && a.Race == "White" && a.Gender == "Male").Count();
                wf.WF = (int)lst.Where(a => a.OFOCode == bio.OFOCode && a.OFODescription == bio.OFODescription && a.Municipality == bio.Municipality && a.Race == "White" && a.Gender == "Female").Count();
                wf.TM = (int)lst.Where(a => a.OFOCode == bio.OFOCode && a.OFODescription == bio.OFODescription && a.Municipality == bio.Municipality && a.Gender == "Male").Count();
                wf.TF = (int)lst.Where(a => a.OFOCode == bio.OFOCode && a.OFODescription == bio.OFODescription && a.Municipality == bio.Municipality && a.Gender == "Female").Count();
                wf.TD = (int)lst.Where(a => a.OFOCode == bio.OFOCode && a.OFODescription == bio.OFODescription && a.Municipality == bio.Municipality && a.Disability == "Yes").Count();
                wf.TNSA = (int)lst.Where(a => a.OFOCode == bio.OFOCode && a.OFODescription == bio.OFODescription && a.Municipality == bio.Municipality && a.Nationality.ToUpper() != "SOUTH AFRICA").Count();
                wf.A35 = (int)lst.Where(a => a.OFOCode == bio.OFOCode && a.OFODescription == bio.OFODescription && a.Municipality == bio.Municipality && (DateTime.Now.Year - System.Convert.ToInt32(a.Birth_Year) < 36)).Count();
                wf.A55 = (int)lst.Where(a => a.OFOCode == bio.OFOCode && a.OFODescription == bio.OFODescription && a.Municipality == bio.Municipality && (DateTime.Now.Year - System.Convert.ToInt32(a.Birth_Year)) > 35 && (DateTime.Now.Year - System.Convert.ToInt32(a.Birth_Year)) < 55).Count();
                wf.A55P = (int)lst.Where(a => a.OFOCode == bio.OFOCode && a.OFODescription == bio.OFODescription && a.Municipality == bio.Municipality && (DateTime.Now.Year - System.Convert.ToInt32(a.Birth_Year)) > 54).Count();

                wfv.Add(wf);
            }
           
            var workprof = from o in wfv
                select new MandWorkforceProfileView()
                {
                    WorkplaceProfile = new MandWorkforceProfile
                    {
                        OFOCode = o.OFOCode,
                        OFODescription = o.OFODescription,
                        Municipality = o.Municipality,
                        AM = o.AM,
                        AF = o.AF,
                        CM = o.CM,
                        CF = o.CF,
                        IM = o.IM,
                        IF = o.IF,
                        WM = o.WM,
                        WF = o.WF,
                        TM = o.TM,
                        TF = o.TF,
                        TD = o.TD,
                        TNSA = o.TNSA,
                        A35 = o.A35,
                        A55 = o.A55,
                        A55P = o.A55P,
                    }
                };

            var totalCount = workprof.Count();

            return new PagedResultDto<MandWorkforceProfileView>(
                totalCount,
                workprof.ToList()
            );
        }

        public async Task<PagedResultDto<MandGeoDistributionView>> GetGeoDistribution(int applicationId)
        {
            var datagrouped = (from bio in _bioRepository.GetAll().Where(a => a.ApplicationId == applicationId && a.Employment_Status != "Terminated" && a.Employment_Status != "Unemployed (Contracted for period of Learning Programme)")
                               select new
                               {
                                   Province = bio.Province,
                                   ApplicationId = bio.ApplicationId
                               })
                .Where(a => a.ApplicationId == applicationId)
                .Distinct()
                .ToList();

            var lst = (from bio in _bioRepository.GetAll().Where(a => a.ApplicationId == applicationId && a.Employment_Status != "Terminated" && a.Employment_Status != "Unemployed (Contracted for period of Learning Programme)")
                       select new { Province = bio.Province, Gender = bio.Gender, Race = bio.Race, Disability = bio.Disability, Nationality = bio.Nationality, Birth_Year = bio.Birth_Year });

            var wf = new MandGeoDistribution();
            List<MandGeoDistribution> wfv = new List<MandGeoDistribution>();
            foreach (var bio in datagrouped)
            {
                wf = new MandGeoDistribution();
                wf.Province = bio.Province;
                wf.AM = (int)lst.Where(a => a.Province == bio.Province && a.Race == "African" && a.Gender == "Male").Count();
                wf.AF = (int)lst.Where(a => a.Province == bio.Province && a.Race == "African" && a.Gender == "Female").Count();
                wf.CM = (int)lst.Where(a => a.Province == bio.Province && a.Race == "Coloured" && a.Gender == "Male").Count();
                wf.CF = (int)lst.Where(a => a.Province == bio.Province && a.Race == "Coloured" && a.Gender == "Female").Count();
                wf.IM = (int)lst.Where(a => a.Province == bio.Province && a.Race == "Indian" && a.Gender == "Male").Count();
                wf.IF = (int)lst.Where(a => a.Province == bio.Province && a.Race == "Indian" && a.Gender == "Female").Count();
                wf.WM = (int)lst.Where(a => a.Province == bio.Province && a.Race == "White" && a.Gender == "Male").Count();
                wf.WF = (int)lst.Where(a => a.Province == bio.Province && a.Race == "White" && a.Gender == "Female").Count();
                wf.TM = (int)lst.Where(a => a.Province == bio.Province && a.Gender == "Male").Count();
                wf.TF = (int)lst.Where(a => a.Province == bio.Province && a.Gender == "Female").Count();
                wf.TD = (int)lst.Where(a => a.Province == bio.Province && a.Disability == "Yes").Count();
                wf.TNSA = (int)lst.Where(a => a.Province == bio.Province && a.Nationality.ToUpper() != "SOUTH AFRICA").Count();
                wf.A35 = (int)lst.Where(a => a.Province == bio.Province && (DateTime.Now.Year - System.Convert.ToInt32(a.Birth_Year) < 36)).Count();
                wf.A55 = (int)lst.Where(a => a.Province == bio.Province && (DateTime.Now.Year - System.Convert.ToInt32(a.Birth_Year)) > 35 && (DateTime.Now.Year - System.Convert.ToInt32(a.Birth_Year)) < 55).Count();
                wf.A55P = (int)lst.Where(a => a.Province == bio.Province && (DateTime.Now.Year - System.Convert.ToInt32(a.Birth_Year)) > 54).Count();

                wfv.Add(wf);
            }

            var geo = from o in wfv
                      select new MandGeoDistributionView()
                      {
                          GeoDistribution = new MandGeoDistribution
                          {
                              Province = o.Province,
                              AM = o.AM,
                              AF = o.AF,
                              CM = o.CM,
                              CF = o.CF,
                              IM = o.IM,
                              IF = o.IF,
                              WM = o.WM,
                              WF = o.WF,
                              TM = o.TM,
                              TF = o.TF,
                              TD = o.TD,
                              TNSA = o.TNSA,
                              A35 = o.A35,
                              A55 = o.A55,
                              A55P = o.A55P,
                          }
                      };

            var totalCount = datagrouped.Count();

            return new PagedResultDto<MandGeoDistributionView>(
                totalCount,
                geo.ToList()
            );
        }

        public async Task<PagedResultDto<MandEducationLevelView>> GetEducationLevel(int applicationId)
        {
            var datagrouped = (from bio in _bioRepository.GetAll().Where(a => a.ApplicationId == applicationId && a.Employment_Status != "Terminated" && a.Employment_Status != "Unemployed (Contracted for period of Learning Programme)")
                               join qt in _qualtype.GetAll() on bio.Highest_Qualification_Type equals qt.Qualification_Type
                               select new
                               {
                                   QualType = bio.Highest_Qualification_Type,
                                   NQF_Level = qt.NQF_Level,
                                   Band = qt.Band,
                               })
                .Distinct()
                .ToList();

            var lst = (from bio in _bioRepository.GetAll().Where(a => a.ApplicationId == applicationId && a.Employment_Status != "Terminated" && a.Employment_Status != "Unemployed (Contracted for period of Learning Programme)")
                       join qt in _qualtype.GetAll() on bio.Highest_Qualification_Type equals qt.Qualification_Type
                       select new { QualType = bio.Highest_Qualification_Type, NQF_Level = qt.NQF_Level, Band = qt.Band, Gender = bio.Gender, Race = bio.Race, Disability = bio.Disability, Nationality = bio.Nationality, Birth_Year = bio.Birth_Year });

            var wf = new MandEducationLevel();
            List<MandEducationLevel> wfv = new List<MandEducationLevel>();

            foreach (var bio in datagrouped)
            {
                wf = new MandEducationLevel();
                wf.Band = bio.Band;
                wf.NQF_Level = bio.NQF_Level;
                wf.Classification = bio.QualType;
                wf.AM = lst.Where(a => a.QualType == bio.QualType && a.NQF_Level == bio.NQF_Level && a.Band == bio.Band && a.Race == "African" && a.Gender == "Male").Count();
                wf.AF = lst.Where(a => a.QualType == bio.QualType && a.NQF_Level == bio.NQF_Level && a.Band == bio.Band && a.Race == "African" && a.Gender == "Female").Count();
                wf.CM = lst.Where(a => a.QualType == bio.QualType && a.NQF_Level == bio.NQF_Level && a.Band == bio.Band && a.Race == "Coloured" && a.Gender == "Male").Count();
                wf.CF = lst.Where(a => a.QualType == bio.QualType && a.NQF_Level == bio.NQF_Level && a.Band == bio.Band && a.Race == "Coloured" && a.Gender == "Female").Count();
                wf.IM = lst.Where(a => a.QualType == bio.QualType && a.NQF_Level == bio.NQF_Level && a.Band == bio.Band && a.Race == "Indian" && a.Gender == "Male").Count();
                wf.IF = lst.Where(a => a.QualType == bio.QualType && a.NQF_Level == bio.NQF_Level && a.Band == bio.Band && a.Race == "Indian" && a.Gender == "Female").Count();
                wf.WM = lst.Where(a => a.QualType == bio.QualType && a.NQF_Level == bio.NQF_Level && a.Band == bio.Band && a.Race == "White" && a.Gender == "Male").Count();
                wf.WF = lst.Where(a => a.QualType == bio.QualType && a.NQF_Level == bio.NQF_Level && a.Band == bio.Band && a.Race == "White" && a.Gender == "Female").Count();
                wf.TM = lst.Where(a => a.QualType == bio.QualType && a.NQF_Level == bio.NQF_Level && a.Band == bio.Band && a.Gender == "Male").Count();
                wf.TF = lst.Where(a => a.QualType == bio.QualType && a.NQF_Level == bio.NQF_Level && a.Band == bio.Band && a.Gender == "Female").Count();
                wf.TD = lst.Where(a => a.QualType == bio.QualType && a.NQF_Level == bio.NQF_Level && a.Band == bio.Band && a.Disability == "Yes").Count();
                wf.TNSA = lst.Where(a => a.QualType == bio.QualType && a.NQF_Level == bio.NQF_Level && a.Band == bio.Band && a.Nationality.ToUpper() != "SOUTH AFRICA").Count();
                wf.A35 = lst.Where(a => a.QualType == bio.QualType && a.NQF_Level == bio.NQF_Level && a.Band == bio.Band && (DateTime.Now.Year - System.Convert.ToInt32(a.Birth_Year) < 36)).Count();
                wf.A55 = lst.Where(a => a.QualType == bio.QualType && a.NQF_Level == bio.NQF_Level && a.Band == bio.Band && (DateTime.Now.Year - System.Convert.ToInt32(a.Birth_Year)) > 35 && (DateTime.Now.Year - System.Convert.ToInt32(a.Birth_Year)) < 55).Count();
                wf.A55P = lst.Where(a => a.QualType == bio.QualType && a.NQF_Level == bio.NQF_Level && a.Band == bio.Band && (DateTime.Now.Year - System.Convert.ToInt32(a.Birth_Year)) > 54).Count();

                wfv.Add(wf);
            }

            var clsf = from o in wfv
                       select new MandEducationLevelView()
                       {
                           EducationLevel = new MandEducationLevel
                           {
                               Band = o.Band,
                               NQF_Level = o.NQF_Level,
                               Classification = o.Classification,
                               AM = o.AM,
                               AF = o.AF,
                               CM = o.CM,
                               CF = o.CF,
                               IM = o.IM,
                               IF = o.IF,
                               WM = o.WM,
                               WF = o.WF,
                               TM = o.TM,
                               TF = o.TF,
                               TD = o.TD,
                               TNSA = o.TNSA,
                               A35 = o.A35,
                               A55 = o.A55,
                               A55P = o.A55P,
                           }
                       };

            var totalCount = datagrouped.Count();

            return new PagedResultDto<MandEducationLevelView>(
                totalCount,
                clsf.ToList()
            );
        }

        public async Task<PagedResultDto<MandEmployeesTrainedView>> GetPlannedTrained(int applicationId)
        {
            var unqtraining = (from t in _trainRepository.GetAll()
                               .Where(a => a.ApplicationId == applicationId && a.SA_Id_Number != null && (a.Achievement_status == "Achieved" || a.Achievement_status == "Busy"))
                               select new { ApplicationId = t.ApplicationId, SA_Id_Number = t.SA_Id_Number })
                               .Distinct();

            var datagrouped1 = (from t in unqtraining
                                join bio in _bioRepository.GetAll().Where(a => a.ApplicationId == applicationId) on t.SA_Id_Number equals bio.SA_Id_Number
                                join ofo in _occ.GetAll() on bio.OFO_Occupation_Code equals ofo.OFO_Code
                                select new
                                {
                                    Occupation = ofo.Description,
                                    tAppId = t.ApplicationId,
                                    bAppId = bio.ApplicationId,
                                })
                .Where(a => a.tAppId == applicationId && a.bAppId == applicationId)
                .Distinct()
                .ToList();

            var lst = (from t in unqtraining
                       join b in _bioRepository.GetAll().Where(a => a.ApplicationId == applicationId) on t.SA_Id_Number equals b.SA_Id_Number
                       join ofo in _occ.GetAll() on b.OFO_Occupation_Code equals ofo.OFO_Code
                       select new
                       {
                           Id = b.Id,
                           tAppId = t.ApplicationId,
                           bAppId = b.ApplicationId,
                           bSAId = b.SA_Id_Number,
                           Occupation = ofo.Description,
                           Race = b.Race,
                           Gender = b.Gender,
                           Disability = b.Disability,
                           Nationality = b.Nationality,
                           Birth_Year = b.Birth_Year
                       })
                        .Where(a => a.tAppId == applicationId && a.bAppId == applicationId)
                        .ToList();

            var unqtraining2 = (from t in _trainRepository.GetAll()
                               .Where(a => a.ApplicationId == applicationId && (a.Achievement_status == "Achieved" || a.Achievement_status == "Busy") && a.SA_Id_Number == null && a.Passport_Number != null)
                                select new { ApplicationId = t.ApplicationId, SA_Id_Number = t.SA_Id_Number, Passport_Number = t.Passport_Number })
                               .Distinct();

            var datagrouped2 = (from t in unqtraining2
                                join bio in _bioRepository.GetAll().Where(a => a.ApplicationId == applicationId) on t.Passport_Number equals bio.Passport_Number
                                join ofo in _occ.GetAll() on bio.OFO_Occupation_Code equals ofo.OFO_Code
                                select new
                                {
                                    Occupation = ofo.Description,
                                    tAppId = t.ApplicationId,
                                    bAppId = bio.ApplicationId,
                                    bSAId = bio.SA_Id_Number,
                                    tSAId = t.SA_Id_Number
                                })
                .Where(a => a.tAppId == applicationId && a.bAppId == applicationId && a.bSAId == null && a.tSAId == null)
                .Distinct()
                .ToList();


            var lst2 = (from t in unqtraining2
                        join b in _bioRepository.GetAll().Where(a => a.ApplicationId == applicationId) on t.Passport_Number equals b.Passport_Number
                        join ofo in _occ.GetAll() on b.OFO_Occupation_Code equals ofo.OFO_Code
                        select new
                        {
                            Id = b.Id,
                            tAppId = t.ApplicationId,
                            bAppId = b.ApplicationId,
                            bSAId = b.SA_Id_Number,
                            tSAId = t.SA_Id_Number,
                            Occupation = ofo.Description,
                            Race = b.Race,
                            Gender = b.Gender,
                            Disability = b.Disability,
                            Nationality = b.Nationality,
                            Birth_Year = b.Birth_Year
                        })
                        .Where(a => a.tAppId == applicationId && a.bAppId == applicationId && a.bSAId == null && a.tSAId == null)
                        .ToList();

            var wf = new MandEmployeesTrained();
            List<MandEmployeesTrained> wfv = new List<MandEmployeesTrained>();

            foreach (var bio in datagrouped1)
            {
                wf = new MandEmployeesTrained();
                wf.Occupation = bio.Occupation;

                wf.AM =
                    (int)(from t in lst.Where(a => a.Occupation == bio.Occupation && a.Race == "African" && a.Gender == "Male")
                          select new { Id = t.Id }).Count();

                wf.AF =
                    (int)(from t in lst.Where(a => a.Occupation == bio.Occupation && a.Race == "African" && a.Gender == "Female")
                          select new { Id = t.Id }).Count();

                wf.CM =
                    (int)(from t in lst.Where(a => a.Occupation == bio.Occupation && a.Race == "Coloured" && a.Gender == "Male")
                          select new { Id = t.Id }).Count();

                wf.CF =
                    (int)(from t in lst.Where(a => a.Occupation == bio.Occupation && a.Race == "Coloured" && a.Gender == "Female")
                          select new { Id = t.Id }).Count();

                wf.IM =
                    (int)(from t in lst.Where(a => a.Occupation == bio.Occupation && a.Race == "Indian" && a.Gender == "Male")
                          select new { Id = t.Id }).Count();

                wf.IF =
                    (int)(from t in lst.Where(a => a.Occupation == bio.Occupation && a.Race == "Indian" && a.Gender == "Female")
                          select new { Id = t.Id }).Count();

                wf.WM =
                    (int)(from t in lst.Where(a => a.Occupation == bio.Occupation && a.Race == "White" && a.Gender == "Male")
                          select new { Id = t.Id }).Count();

                wf.WF =
                    (int)(from t in lst.Where(a => a.Occupation == bio.Occupation && a.Race == "White" && a.Gender == "Female")
                          select new { Id = t.Id }).Count();

                wf.TM =
                    (int)(from t in lst.Where(a => a.Occupation == bio.Occupation && a.Gender == "Male")
                          select new { Id = t.Id }).Count();

                wf.TF =
                    (int)(from t in lst.Where(a => a.Occupation == bio.Occupation && a.Gender == "Female")
                          select new { Id = t.Id }).Count();

                wf.TD =
                    (int)(from t in lst.Where(a => a.Occupation == bio.Occupation && a.Disability == "Yes")
                          select new { Id = t.Id }).Count();

                wf.TNSA =
                    (int)(from t in lst.Where(a => a.Occupation == bio.Occupation && a.Nationality.ToUpper() != "SOUTH AFRICA")
                          select new { Id = t.Id }).Count();

                wf.A35 =
                    (int)(from t in lst.Where(a => a.Occupation == bio.Occupation && (DateTime.Now.Year - System.Convert.ToInt32(a.Birth_Year)) < 36)
                          select new { Id = t.Id }).Count();

                wf.A55 =
                    (int)(from t in lst.Where(a => a.Occupation == bio.Occupation && (DateTime.Now.Year - System.Convert.ToInt32(a.Birth_Year)) > 35 && (DateTime.Now.Year - System.Convert.ToInt32(a.Birth_Year)) < 55)
                          select new { Id = t.Id }).Count();

                wf.A55P =
                    (int)(from t in lst.Where(a => a.Occupation == bio.Occupation && (DateTime.Now.Year - System.Convert.ToInt32(a.Birth_Year)) > 54)
                          select new { Id = t.Id }).Count();

                wfv.Add(wf);
            }

            foreach (var bio in datagrouped2)
            {
                wf = new MandEmployeesTrained();
                wf.Occupation = bio.Occupation;
                wf.AM =
                    (int)(from t in lst2.Where(a => a.Occupation == bio.Occupation && a.Race == "African" && a.Gender == "Male")
                          select new { Id = t.Id }).Count();

                wf.AF =
                    (int)(from t in lst2.Where(a => a.Occupation == bio.Occupation && a.Race == "African" && a.Gender == "Female")
                          select new { Id = t.Id }).Count();

                wf.CM =
                    (int)(from t in lst2.Where(a => a.Occupation == bio.Occupation && a.Race == "Coloured" && a.Gender == "Male")
                          select new { Id = t.Id }).Count();

                wf.CF =
                    (int)(from t in lst2.Where(a => a.Occupation == bio.Occupation && a.Race == "Coloured" && a.Gender == "Female")
                          select new { Id = t.Id }).Count();

                wf.IM =
                    (int)(from t in lst2.Where(a => a.Occupation == bio.Occupation && a.Race == "Indian" && a.Gender == "Male")
                          select new { Id = t.Id }).Count();

                wf.IF =
                    (int)(from t in lst2.Where(a => a.Occupation == bio.Occupation && a.Race == "Indian" && a.Gender == "Female")
                          select new { Id = t.Id }).Count();

                wf.WM =
                    (int)(from t in lst2.Where(a => a.Occupation == bio.Occupation && a.Race == "White" && a.Gender == "Male")
                          select new { Id = t.Id }).Count();

                wf.WF =
                    (int)(from t in lst2.Where(a => a.Occupation == bio.Occupation && a.Race == "White" && a.Gender == "Female")
                          select new { Id = t.Id }).Count();

                wf.TM =
                    (int)(from t in lst2.Where(a => a.Occupation == bio.Occupation && a.Gender == "Male")
                          select new { Id = t.Id }).Count();

                wf.TF =
                    (int)(from t in lst2.Where(a => a.Occupation == bio.Occupation && a.Gender == "Female")
                          select new { Id = t.Id }).Count();

                wf.TD =
                    (int)(from t in lst2.Where(a => a.Occupation == bio.Occupation && a.Disability == "Yes")
                          select new { Id = t.Id }).Count();

                wf.TNSA =
                    (int)(from t in lst2.Where(a => a.Occupation == bio.Occupation && a.Nationality.ToUpper() != "SOUTH AFRICA")
                          select new { Id = t.Id }).Count();

                wf.A35 =
                    (int)(from t in lst2.Where(a => a.Occupation == bio.Occupation && (DateTime.Now.Year - System.Convert.ToInt32(a.Birth_Year)) < 36)
                          select new { Id = t.Id }).Count();

                wf.A55 =
                    (int)(from t in lst2.Where(a => a.Occupation == bio.Occupation && (DateTime.Now.Year - System.Convert.ToInt32(a.Birth_Year)) > 35 && (DateTime.Now.Year - System.Convert.ToInt32(a.Birth_Year)) < 55)
                          select new { Id = t.Id }).Count();

                wf.A55P =
                    (int)(from t in lst2.Where(a => a.Occupation == bio.Occupation && (DateTime.Now.Year - System.Convert.ToInt32(a.Birth_Year)) > 54)
                          select new { Id = t.Id }).Count();

                wfv.Add(wf);
            }

            var geo = from o in wfv
                      select new MandEmployeesTrainedView()
                      {
                          EmployeesTrained = new MandEmployeesTrained
                          {
                              Occupation = o.Occupation,
                              AM = o.AM,
                              AF = o.AF,
                              CM = o.CM,
                              CF = o.CF,
                              IM = o.IM,
                              IF = o.IF,
                              WM = o.WM,
                              WF = o.WF,
                              TM = o.TM,
                              TF = o.TF,
                              TD = o.TD,
                              TNSA = o.TNSA,
                              A35 = o.A35,
                              A55 = o.A55,
                              A55P = o.A55P,
                          }
                      };

            var totalCount = datagrouped1.Count() + datagrouped2.Count();

            return new PagedResultDto<MandEmployeesTrainedView>(
                totalCount,
                geo.ToList()
            );
        }

        public async Task<PagedResultDto<MandTrainingInterventionsView>> GetEmployeeTrainingInter(int applicationId)
        {
            var wf = new MandTrainingInterventions();
            List<MandTrainingInterventions> wfv = new List<MandTrainingInterventions>();

            var cn1 = _bioRepository.GetAll().Where(a => a.ApplicationId == applicationId && a.SA_Id_Number != null).Count();

            if (cn1 > 0)
            {
                var datagrouped1 = (from t in _trainRepository.GetAll().Where(a => a.SA_Id_Number != null && a.ApplicationId == applicationId && a.Achievement_status == "Planned")
                                    select new
                                    {
                                        Classification = t.Study_Field_Or_Specialisation_Specification,
                                        Learning_Program = t.Details_Of_Learning_Program,
                                        QualificationType = t.Qualification_Learning_Program_Type,
                                        ApplicationId = t.ApplicationId
                                    })
                    .Where(a => a.ApplicationId == applicationId)
                    .Distinct()
                    .ToList();

                var lst = (from t in _trainRepository.GetAll().Where(a => a.ApplicationId == applicationId && a.Achievement_status == "Planned")
                           join b in _bioRepository.GetAll().Where(a => a.ApplicationId == applicationId && a.Employment_Status != "Unemployed (Contracted for period of Learning Programme)") on t.SA_Id_Number equals b.SA_Id_Number
                            select new { Id = t.Id, tAppId = t.ApplicationId, bAppId = b.ApplicationId, ProgramType = t.Qualification_Learning_Program_Type,
                                Classification = t.Study_Field_Or_Specialisation_Specification, Learning_Program = t.Details_Of_Learning_Program,
                                Race = b.Race, Gender = b.Gender, Disability = b.Disability, Nationality = b.Nationality, Birth_Year = b.Birth_Year })
                                     .Where(a => a.tAppId == applicationId && a.bAppId == applicationId);

                int i = 0;
                foreach (var bio in datagrouped1)
                {
                    i += 1;
                    wf = new MandTrainingInterventions();
                    wf.ProgramType = bio.QualificationType;
                    wf.Program = bio.Learning_Program;
                    wf.Classification = bio.Classification;
                    wf.AM =
                        (int)(from t in lst.Where(a => (a.Classification == bio.Classification) && a.ProgramType == bio.QualificationType && a.Learning_Program == bio.Learning_Program && a.Race == "African" && a.Gender == "Male")
                                select new { Id = t.Id }).Count();

                    wf.AF =
                        (int)(from t in lst.Where(a => (a.Classification == bio.Classification) && a.ProgramType == bio.QualificationType && a.Learning_Program == bio.Learning_Program && a.Race == "African" && a.Gender == "Female")
                                select new { Id = t.Id }).Count();

                    wf.CM =
                        (int)(from t in lst.Where(a => (a.Classification == bio.Classification) && a.ProgramType == bio.QualificationType && a.Learning_Program == bio.Learning_Program && a.Race == "Coloured" && a.Gender == "Male")
                                select new { Id = t.Id }).Count();

                    wf.CF =
                        (int)(from t in lst.Where(a => (a.Classification == bio.Classification) && a.ProgramType == bio.QualificationType && a.Learning_Program == bio.Learning_Program && a.Race == "Coloured" && a.Gender == "Female")
                                select new { Id = t.Id }).Count();

                    wf.IM =
                        (int)(from t in lst.Where(a => (a.Classification == bio.Classification) && a.ProgramType == bio.QualificationType && a.Learning_Program == bio.Learning_Program && a.Race == "Indian" && a.Gender == "Male")
                                select new { Id = t.Id }).Count();

                    wf.IF =
                        (int)(from t in lst.Where(a => (a.Classification == bio.Classification) && a.ProgramType == bio.QualificationType && a.Learning_Program == bio.Learning_Program && a.Race == "Indian" && a.Gender == "Female")
                                select new { Id = t.Id }).Count();

                    wf.WM =
                        (int)(from t in lst.Where(a => (a.Classification == bio.Classification) && a.ProgramType == bio.QualificationType && a.Learning_Program == bio.Learning_Program && a.Race == "White" && a.Gender == "Male")
                                select new { Id = t.Id }).Count();

                    wf.WF =
                        (int)(from t in lst.Where(a => (a.Classification == bio.Classification) && a.ProgramType == bio.QualificationType && a.Learning_Program == bio.Learning_Program && a.Race == "White" && a.Gender == "Female")
                                select new { Id = t.Id }).Count();

                    wf.TM =
                        (int)(from t in lst.Where(a => (a.Classification == bio.Classification) && a.ProgramType == bio.QualificationType && a.Learning_Program == bio.Learning_Program && a.Gender == "Male")
                                select new { Id = t.Id }).Count();

                    wf.TF =
                        (int)(from t in lst.Where(a => (a.Classification == bio.Classification) && a.ProgramType == bio.QualificationType && a.Learning_Program == bio.Learning_Program && a.Gender == "Female")
                                select new { Id = t.Id }).Count();

                    wf.TD =
                        (int)(from t in lst.Where(a => (a.Classification == bio.Classification) && a.ProgramType == bio.QualificationType && a.Learning_Program == bio.Learning_Program && a.Disability == "Yes")
                                select new { Id = t.Id }).Count();

                    wf.TNSA =
                        (int)(from t in lst.Where(a => (a.Classification == bio.Classification) && a.ProgramType == bio.QualificationType && a.Learning_Program == bio.Learning_Program && a.Nationality.ToUpper() != "SOUTH AFRICA")
                                select new { Id = t.Id }).Count();

                    wf.A35 =
                        (int)(from t in lst.Where(a => (a.Classification == bio.Classification) && a.ProgramType == bio.QualificationType && a.Learning_Program == bio.Learning_Program && (DateTime.Now.Year - System.Convert.ToInt32(a.Birth_Year) < 36))
                                select new { Id = t.Id }).Count();

                    wf.A55 =
                        (int)(from t in lst.Where(a => (a.Classification == bio.Classification) && a.ProgramType == bio.QualificationType && a.Learning_Program == bio.Learning_Program && (DateTime.Now.Year - System.Convert.ToInt32(a.Birth_Year)) > 35 && (DateTime.Now.Year - System.Convert.ToInt32(a.Birth_Year)) < 55)
                                select new { Id = t.Id }).Count();

                    wf.A55P =
                        (int)(from t in lst.Where(a => (a.Classification == bio.Classification) && a.ProgramType == bio.QualificationType && a.Learning_Program == bio.Learning_Program && (DateTime.Now.Year - System.Convert.ToInt32(a.Birth_Year)) > 54)
                                select new { Id = t.Id }).Count();
                    wfv.Add(wf);

                }
            }

            var cn2 = _bioRepository.GetAll().Where(a => a.ApplicationId == applicationId && a.SA_Id_Number == null && a.Passport_Number != null).Count();
            if (cn2 > 0)
            {
                var datagrouped2 = (from t in _trainRepository.GetAll().Where(a => a.ApplicationId == applicationId && a.SA_Id_Number == null && a.Passport_Number != null && a.Achievement_status == "Planned")
                                    select new
                                    {
                                        Classification = t.Study_Field_Or_Specialisation_Specification,
                                        Learning_Program = t.Details_Of_Learning_Program,
                                        QualificationType = t.Qualification_Learning_Program_Type,
                                        ApplicationId = t.ApplicationId
                                    })
                .Where(a => a.ApplicationId == applicationId)
                .Distinct()
                .ToList();

                var lst2 = (from t in _trainRepository.GetAll().Where(a => a.ApplicationId == applicationId && a.Achievement_status == "Planned")
                            join b in _bioRepository.GetAll().Where(a => a.ApplicationId == applicationId && a.Employment_Status != "Unemployed (Contracted for period of Learning Programme)") on t.Passport_Number equals b.Passport_Number
                            select new
                            { Id = t.Id,
                                tAppId = t.ApplicationId,
                                bAppId = b.ApplicationId,
                                tSAId = t.SA_Id_Number,
                                bSAId = b.SA_Id_Number,
                                ProgramType = t.Qualification_Learning_Program_Type,
                                Learning_Program = t.Details_Of_Learning_Program,
                                Classification = t.Study_Field_Or_Specialisation_Specification,
                                Race = b.Race,
                                Gender = b.Gender,
                                Disability = b.Disability,
                                Nationality = b.Nationality,
                                Birth_Year = b.Birth_Year
                            })
                            .Where(a => a.tAppId == applicationId && a.bAppId == applicationId && a.bSAId == null && a.tSAId == null)
                            .ToList();

                int i = 0;
                foreach (var bio in datagrouped2)
                {
                    i += 1;
                    wf = new MandTrainingInterventions();
                    wf.ProgramType = bio.QualificationType;
                    wf.Classification = bio.Classification;
                    wf.Program = bio.Learning_Program;
                    wf.AM =
                    (int)(from t in lst2.Where(a => a.Classification == bio.Classification && a.ProgramType == bio.QualificationType && a.Learning_Program == bio.Learning_Program && a.Race == "African" && a.Gender == "Male")
                            select new { Id = t.Id }).Count();

                    wf.AF =
                        (int)(from t in lst2.Where(a => a.Classification == bio.Classification && a.ProgramType == bio.QualificationType && a.Learning_Program == bio.Learning_Program && a.Race == "African" && a.Gender == "Female")
                                select new { Id = t.Id }).Count();

                    wf.CM =
                        (int)(from t in lst2.Where(a => a.Classification == bio.Classification && a.ProgramType == bio.QualificationType && a.Learning_Program == bio.Learning_Program && a.Race == "Coloured" && a.Gender == "Male")
                                select new { Id = t.Id }).Count();

                    wf.CF =
                        (int)(from t in lst2.Where(a => a.Classification == bio.Classification && a.ProgramType == bio.QualificationType && a.Learning_Program == bio.Learning_Program && a.Race == "Coloured" && a.Gender == "Female")
                                select new { Id = t.Id }).Count();

                    wf.IM =
                        (int)(from t in lst2.Where(a => a.Classification == bio.Classification && a.ProgramType == bio.QualificationType && a.Learning_Program == bio.Learning_Program && a.Race == "Indian" && a.Gender == "Male")
                                select new { Id = t.Id }).Count();

                    wf.IF =
                        (int)(from t in lst2.Where(a => a.Classification == bio.Classification && a.ProgramType == bio.QualificationType && a.Learning_Program == bio.Learning_Program && a.Race == "Indian" && a.Gender == "Female")
                                select new { Id = t.Id }).Count();

                    wf.WM =
                        (int)(from t in lst2.Where(a => a.Classification == bio.Classification && a.ProgramType == bio.QualificationType && a.Learning_Program == bio.Learning_Program && a.Race == "White" && a.Gender == "Male")
                                select new { Id = t.Id }).Count();

                    wf.WF =
                        (int)(from t in lst2.Where(a => a.Classification == bio.Classification && a.ProgramType == bio.QualificationType && a.Learning_Program == bio.Learning_Program && a.Race == "White" && a.Gender == "Female")
                                select new { Id = t.Id }).Count();

                    wf.TM =
                        (int)(from t in lst2.Where(a => a.Classification == bio.Classification && a.ProgramType == bio.QualificationType && a.Learning_Program == bio.Learning_Program && a.Gender == "Male")
                                select new { Id = t.Id }).Count();

                    wf.TF =
                        (int)(from t in lst2.Where(a => a.Classification == bio.Classification && a.ProgramType == bio.QualificationType && a.Learning_Program == bio.Learning_Program && a.Gender == "Female")
                                select new { Id = t.Id }).Count();

                    wf.TD =
                        (int)(from t in lst2.Where(a => a.Classification == bio.Classification && a.ProgramType == bio.QualificationType && a.Learning_Program == bio.Learning_Program && a.Disability == "Yes")
                                select new { Id = t.Id }).Count();

                    wf.TNSA =
                        (int)(from t in lst2.Where(a => a.Classification == bio.Classification && a.ProgramType == bio.QualificationType && a.Learning_Program == bio.Learning_Program && a.Nationality.ToUpper() != "SOUTH AFRICA")
                                select new { Id = t.Id }).Count();

                    wf.A35 =
                        (int)(from t in lst2.Where(a => a.Classification == bio.Classification && a.ProgramType == bio.QualificationType && a.Learning_Program == bio.Learning_Program && (DateTime.Now.Year - System.Convert.ToInt32(a.Birth_Year) < 36))
                                select new { Id = t.Id }).Count();

                    wf.A55 =
                        (int)(from t in lst2.Where(a => a.Classification == bio.Classification && a.ProgramType == bio.QualificationType && a.Learning_Program == bio.Learning_Program && (DateTime.Now.Year - System.Convert.ToInt32(a.Birth_Year)) > 35 && (DateTime.Now.Year - System.Convert.ToInt32(a.Birth_Year)) < 55)
                                select new { Id = t.Id }).Count();

                    wf.A55P =
                        (int)(from t in lst2.Where(a => a.Classification == bio.Classification && a.ProgramType == bio.QualificationType && a.Learning_Program == bio.Learning_Program && (DateTime.Now.Year - System.Convert.ToInt32(a.Birth_Year)) > 54)
                                select new { Id = t.Id }).Count();

                    wfv.Add(wf);
                }
            }

            var intr = from o in wfv
                       select new MandTrainingInterventionsView()
                       {
                           TrainingInterventions = new MandTrainingInterventions
                           {
                               ProgramType = o.ProgramType,
                               Classification = o.Classification,
                               Program = o.Program,
                               AM = o.AM,
                               AF = o.AF,
                               CM = o.CM,
                               CF = o.CF,
                               IM = o.IM,
                               IF = o.IF,
                               WM = o.WM,
                               WF = o.WF,
                               TM = o.TM,
                               TF = o.TF,
                               TD = o.TD,
                               TNSA = o.TNSA,
                               A35 = o.A35,
                               A55 = o.A55,
                               A55P = o.A55P,
                           }
                       };

            var totalCount = wfv.Count();

            return new PagedResultDto<MandTrainingInterventionsView>(
                totalCount,
                intr.ToList()
            );
        }

        public async Task<PagedResultDto<MandPlannedTrainingView>> GetPlannedTraining(int applicationId)
        {
            var datagrouped1 = (from t in _trainRepository.GetAll().Where(a => a.ApplicationId == applicationId && a.Achievement_status == "Planned")
                                join bio in _bioRepository.GetAll().Where(a=> a.ApplicationId == applicationId && a.Employment_Status != "Unemployed (Contracted for period of Learning Programme)") on t.SA_Id_Number equals bio.SA_Id_Number
                                join ofo in _occ.GetAll() on bio.OFO_Occupation_Code equals ofo.OFO_Code
                                select new
                                {
                                    Occupation = ofo.Description,
                                    bAppId = bio.ApplicationId,
                                    tAppId = t.ApplicationId
                                })
                .Where(a => a.tAppId == applicationId && a.bAppId == applicationId)
                .Distinct()
                .ToList();

            var datagrouped2 = (from t in _trainRepository.GetAll().Where(a => a.ApplicationId == applicationId && a.Achievement_status == "Planned")
                                join bio in _bioRepository.GetAll().Where(a=> a.ApplicationId == applicationId && a.Employment_Status != "Unemployed (Contracted for period of Learning Programme)") on t.Passport_Number equals bio.Passport_Number
                                join ofo in _occ.GetAll() on bio.OFO_Occupation_Code equals ofo.OFO_Code
                                select new
                                {
                                    Occupation = ofo.Description,
                                    bAppId = bio.ApplicationId,
                                    tAppId = t.ApplicationId,
                                    bSAId = bio.SA_Id_Number,
                                    tSAID = t.SA_Id_Number
                                })
                .Where(a => a.tAppId == applicationId && a.bAppId == applicationId && a.bSAId == null && a.tSAID == null)
                .Distinct()
                .ToList();

            var lst = (from t in _trainRepository.GetAll().Where(a => a.ApplicationId == applicationId && a.Achievement_status == "Planned")
                       join bio in _bioRepository.GetAll().Where(a => a.ApplicationId == applicationId && a.Employment_Status != "Unemployed (Contracted for period of Learning Programme)") on t.SA_Id_Number equals bio.SA_Id_Number
                       join ofo in _occ.GetAll() on bio.OFO_Occupation_Code equals ofo.OFO_Code
                       select new
                       {
                           Id = t.Id,
                           Occupation = ofo.Description,
                           bAppId = bio.ApplicationId,
                           tAppId = t.ApplicationId,
                           Race = bio.Race,
                           Gender = bio.Gender,
                           Disability = bio.Disability,
                           Nationality = bio.Nationality,
                           Birth_Year = bio.Birth_Year
                       })
                .Where(a => a.tAppId == applicationId && a.bAppId == applicationId)
                .ToList();

            var lst2 = (from t in _trainRepository.GetAll().Where(a => a.ApplicationId == applicationId && a.Achievement_status == "Planned")
                       join bio in _bioRepository.GetAll().Where(a => a.ApplicationId == applicationId && a.Employment_Status != "Unemployed (Contracted for period of Learning Programme)") on t.Passport_Number equals bio.Passport_Number
                       join ofo in _occ.GetAll() on bio.OFO_Occupation_Code equals ofo.OFO_Code
                       select new
                       {
                           Id = t.Id,
                           Occupation = ofo.Description,
                           bAppId = bio.ApplicationId,
                           tAppId = t.ApplicationId,
                           tSAId = t.SA_Id_Number,
                           bSAId = bio.SA_Id_Number,
                           Race = bio.Race,
                           Gender = bio.Gender,
                           Disability = bio.Disability,
                           Nationality = bio.Nationality,
                           Birth_Year = bio.Birth_Year
                       })
                .Where(a => a.tAppId == applicationId && a.bAppId == applicationId && a.tSAId == null && a.bSAId == null)
                .ToList();

            var wf = new MandPlannedTraining();
            List<MandPlannedTraining> wfv = new List<MandPlannedTraining>();

            foreach (var bio in datagrouped1)
            {
                wf = new MandPlannedTraining();
                wf.Occupation = bio.Occupation;
                wf.AM =
                        (int)(from t in lst.Where(a => a.Occupation == bio.Occupation && a.Gender == "Male" && a.Race == "African")
                              select new { Id = t.Id }).Count();


                wf.AF =
                    (int)(from t in lst.Where(a => a.Occupation == bio.Occupation && a.Gender == "Female" && a.Race == "African")
                          select new { Id = t.Id }).Count();

                wf.CM =
                    (int)(from t in lst.Where(a => a.Occupation == bio.Occupation && a.Gender == "Male" && a.Race == "Coloured")
                          select new { Id = t.Id }).Count();

                wf.CF =
                    (int)(from t in lst.Where(a => a.Occupation == bio.Occupation && a.Gender == "Female" && a.Race == "Coloured")
                          select new { Id = t.Id }).Count();

                wf.IM =
                    (int)(from t in lst.Where(a => a.Occupation == bio.Occupation && a.Gender == "Male" && a.Race == "Indian")
                          select new { Id = t.Id }).Count();

                wf.IF =
                    (int)(from t in lst.Where(a => a.Occupation == bio.Occupation && a.Gender == "Female" && a.Race == "Indian")
                          select new { Id = t.Id }).Count();

                wf.WM =
                    (int)(from t in lst.Where(a => a.Occupation == bio.Occupation && a.Gender == "Male" && a.Race == "White")
                          select new { Id = t.Id }).Count();
                
                wf.WF =
                    (int)(from t in lst.Where(a => a.Occupation == bio.Occupation && a.Gender == "Female" && a.Race == "White")
                          select new { Id = t.Id }).Count();

                wf.TM =
                    (int)(from t in lst.Where(a => a.Occupation == bio.Occupation && a.Gender == "Male")
                          select new { Id = t.Id }).Count();

                wf.TF =
                    (int)(from t in lst.Where(a => a.Occupation == bio.Occupation && a.Gender == "Female")
                          select new { Id = t.Id }).Count();

                wf.TD =
                    (int)(from t in lst.Where(a => a.Occupation == bio.Occupation && a.Disability == "Yes")
                          select new { Id = t.Id }).Count();

                wf.TNSA =
                    (int)(from t in lst.Where(a => a.Occupation == bio.Occupation && a.Nationality.ToUpper() != "SOUTH AFRICA")
                          select new { Id = t.Id }).Count();

                wf.A35 =
                    (int)(from t in lst.Where(a => a.Occupation == bio.Occupation && (DateTime.Now.Year - System.Convert.ToInt32(a.Birth_Year)) < 36)
                          select new { Id = t.Id }).Count();

                wf.A55 =
                    (int)(from t in lst.Where(a => a.Occupation == bio.Occupation && (DateTime.Now.Year - System.Convert.ToInt32(a.Birth_Year)) > 35 && (DateTime.Now.Year - System.Convert.ToInt32(a.Birth_Year)) < 56)
                          select new { Id = t.Id }).Count();

                wf.A55P = 
                    (int)(from t in lst.Where(a => a.Occupation == bio.Occupation && (DateTime.Now.Year - System.Convert.ToInt32(a.Birth_Year)) > 55)
                                select new { Id = t.Id }).Count();

                wfv.Add(wf);
            }

            foreach (var bio in datagrouped2)
            {
                wf = new MandPlannedTraining();
                wf.Occupation = bio.Occupation;
                wf.AM =
                        (int)(from t in lst2.Where(a => a.Occupation == bio.Occupation && a.Gender == "Male" && a.Race == "African")
                              select new { Id = t.Id }).Count();


                wf.AF =
                    (int)(from t in lst2.Where(a => a.Occupation == bio.Occupation && a.Gender == "Female" && a.Race == "African")
                          select new { Id = t.Id }).Count();

                wf.CM =
                    (int)(from t in lst2.Where(a => a.Occupation == bio.Occupation && a.Gender == "Male" && a.Race == "Coloured")
                          select new { Id = t.Id }).Count();

                wf.CF =
                    (int)(from t in lst2.Where(a => a.Occupation == bio.Occupation && a.Gender == "Female" && a.Race == "Coloured")
                          select new { Id = t.Id }).Count();

                wf.IM =
                    (int)(from t in lst2.Where(a => a.Occupation == bio.Occupation && a.Gender == "Male" && a.Race == "Indian")
                          select new { Id = t.Id }).Count();

                wf.IF =
                    (int)(from t in lst2.Where(a => a.Occupation == bio.Occupation && a.Gender == "Female" && a.Race == "Indian")
                          select new { Id = t.Id }).Count();

                wf.WM =
                    (int)(from t in lst2.Where(a => a.Occupation == bio.Occupation && a.Gender == "Male" && a.Race == "White")
                          select new { Id = t.Id }).Count();

                wf.WF =
                    (int)(from t in lst2.Where(a => a.Occupation == bio.Occupation && a.Gender == "Female" && a.Race == "White")
                          select new { Id = t.Id }).Count();

                wf.TM =
                    (int)(from t in lst2.Where(a => a.Occupation == bio.Occupation && a.Gender == "Male")
                          select new { Id = t.Id }).Count();

                wf.TF =
                    (int)(from t in lst2.Where(a => a.Occupation == bio.Occupation && a.Gender == "Female")
                          select new { Id = t.Id }).Count();

                wf.TD =
                    (int)(from t in lst2.Where(a => a.Occupation == bio.Occupation && a.Disability == "Yes")
                          select new { Id = t.Id }).Count();

                wf.TNSA =
                    (int)(from t in lst2.Where(a => a.Occupation == bio.Occupation && a.Nationality.ToUpper() != "SOUTH AFRICA")
                          select new { Id = t.Id }).Count();

                wf.A35 =
                    (int)(from t in lst2.Where(a => a.Occupation == bio.Occupation && (DateTime.Now.Year - System.Convert.ToInt32(a.Birth_Year)) < 36)
                          select new { Id = t.Id }).Count();

                wf.A55 =
                    (int)(from t in lst2.Where(a => a.Occupation == bio.Occupation && (DateTime.Now.Year - System.Convert.ToInt32(a.Birth_Year)) > 35 && (DateTime.Now.Year - System.Convert.ToInt32(a.Birth_Year)) < 56)
                          select new { Id = t.Id }).Count();

                wf.A55P =
                    (int)(from t in lst2.Where(a => a.Occupation == bio.Occupation && (DateTime.Now.Year - System.Convert.ToInt32(a.Birth_Year)) > 55)
                          select new { Id = t.Id }).Count();

                wfv.Add(wf);
            }


            var geo = from o in wfv
                      select new MandPlannedTrainingView()
                      {
                          PlannedTraining = new MandPlannedTraining
                          {
                              Occupation = o.Occupation,
                              AM = o.AM,
                              AF = o.AF,
                              CM = o.CM,
                              CF = o.CF,
                              IM = o.IM,
                              IF = o.IF,
                              WM = o.WM,
                              WF = o.WF,
                              TM = o.TM,
                              TF = o.TF,
                              TD = o.TD,
                              TNSA = o.TNSA,
                              A35 = o.A35,
                              A55 = o.A55,
                              A55P = o.A55P,
                          }
                      };

            var totalCount = datagrouped1.Count() + datagrouped2.Count();

            return new PagedResultDto<MandPlannedTrainingView>(
                totalCount,
                geo.ToList()
            );
        }

        public async Task<PagedResultDto<MandPivotalView>> GetPivotalProg(int applicationId)
        {
            var datagrouped1 = (from t in _trainRepository.GetAll().Where(a => a.ApplicationId == applicationId && a.Achievement_status == "Planned")
                                join bio in _bioRepository.GetAll().Where(a => a.ApplicationId == applicationId && a.Employment_Status != "Unemployed (Contracted for period of Learning Programme)") on t.SA_Id_Number equals bio.SA_Id_Number
                                join ofo in _occ.GetAll() on bio.OFO_Occupation_Code equals ofo.OFO_Code
                                join p in _piv.GetAll() on t.Qualification_Learning_Program_Type equals p.Pivotal_Programme
                                select new
                                {
                                    Occupation = ofo.Description,
                                    Learning = t.Study_Field_Or_Specialisation_Specification,
                                    bAppId = bio.ApplicationId,
                                    tAppId = t.ApplicationId,
                                })
                .Where(a => a.bAppId == applicationId && a.tAppId == applicationId)
                .Distinct()
                .ToList();

            var lst = (from t in _trainRepository.GetAll().Where(a => a.ApplicationId == applicationId && a.Achievement_status == "Planned")
                       join bio in _bioRepository.GetAll().Where(a => a.ApplicationId == applicationId && a.Employment_Status != "Unemployed (Contracted for period of Learning Programme)") on t.SA_Id_Number equals bio.SA_Id_Number
                       join ofo in _occ.GetAll() on bio.OFO_Occupation_Code equals ofo.OFO_Code
                       join p in _piv.GetAll() on t.Qualification_Learning_Program_Type equals p.Pivotal_Programme
                       select new
                       {
                           Id = t.Id,
                           Occupation = ofo.Description,
                           Learning = t.Study_Field_Or_Specialisation_Specification,
                           Cost = t.Total_Training_Cost,
                           bAppId = bio.ApplicationId,
                           tAppId = t.ApplicationId,
                       })
                    .Where(a => a.bAppId == applicationId && a.tAppId == applicationId);

            var datagrouped2 = (from t in _trainRepository.GetAll().Where(a => a.ApplicationId == applicationId && a.Achievement_status == "Planned" && (a.SA_Id_Number == null || a.SA_Id_Number == ""))
                                join bio in _bioRepository.GetAll().Where(a => a.ApplicationId == applicationId && a.Employment_Status != "Unemployed (Contracted for period of Learning Programme)" && (a.SA_Id_Number == null || a.SA_Id_Number == "")) on t.Passport_Number equals bio.Passport_Number
                                join ofo in _occ.GetAll() on bio.OFO_Occupation_Code equals ofo.OFO_Code
                                join p in _piv.GetAll() on t.Qualification_Learning_Program_Type equals p.Pivotal_Programme
                                select new
                                {
                                    Occupation = ofo.Description,
                                    Learning = t.Study_Field_Or_Specialisation_Specification,
                                    bAppId = bio.ApplicationId,
                                    tAppId = t.ApplicationId,
                                    tSAId = t.SA_Id_Number,
                                    bSAId = bio.SA_Id_Number
                                })
                .Where(a => a.bAppId == applicationId && a.tAppId == applicationId && (a.bSAId == null || a.bSAId == "") && (a.tSAId == null || a.tSAId == ""))
                .Distinct()
                .ToList();

            var lst2 = (from t in _trainRepository.GetAll().Where(a => a.ApplicationId == applicationId && a.Achievement_status == "Planned" && (a.SA_Id_Number == null || a.SA_Id_Number == ""))
                        join bio in _bioRepository.GetAll().Where(a => a.ApplicationId == applicationId && a.Employment_Status != "Unemployed (Contracted for period of Learning Programme)" && (a.SA_Id_Number == null || a.SA_Id_Number == "")) on t.Passport_Number equals bio.Passport_Number
                        join ofo in _occ.GetAll() on bio.OFO_Occupation_Code equals ofo.OFO_Code
                        join p in _piv.GetAll() on t.Qualification_Learning_Program_Type equals p.Pivotal_Programme
                        select new
                        {
                            Id = t.Id,
                            Occupation = ofo.Description,
                            Learning = t.Study_Field_Or_Specialisation_Specification,
                            Cost = t.Total_Training_Cost,
                            bAppId = bio.ApplicationId,
                            tAppId = t.ApplicationId,
                            tSAId = t.SA_Id_Number,
                            bSAId = bio.SA_Id_Number
                        })
                        .Where(a => a.bAppId == applicationId && a.tAppId == applicationId && (a.bSAId == null || a.bSAId == "") && (a.tSAId == null || a.tSAId == ""));
            var wf = new MandPivotal();
            List<MandPivotal> wfv = new List<MandPivotal>();
            wf.Employed = 0; wf.Cost = 0;

            foreach (var bio in datagrouped1)
            {
                wf = new MandPivotal();
                wf.Occupation = bio.Occupation;
                wf.Learning = bio.Learning;
                wf.Employed = (int)(from t in lst.Where(a=>a.Occupation == bio.Occupation && a.Learning == bio.Learning) select new {Id = t.Id}).Count();

                wf.Cost = (decimal)((from t in lst.Where(a => a.Occupation == bio.Occupation && a.Learning == bio.Learning) select new { Id= t.Id, Cost = t.Cost }).Sum(s=>s.Cost));
                wfv.Add(wf);
            }

            foreach (var bio in datagrouped2)
            {
                wf = new MandPivotal();
                wf.Occupation = bio.Occupation;
                wf.Learning = bio.Learning;
                wf.Employed = (int)(from t in lst2.Where(a => a.Occupation == bio.Occupation && a.Learning == bio.Learning) select new { Id = t.Id }).Count();

                wf.Cost = (decimal)((from t in lst2.Where(a => a.Occupation == bio.Occupation && a.Learning == bio.Learning) select new { Id = t.Id, Cost = t.Cost }).Sum(s => s.Cost));

                wfv.Add(wf);
            }

            var piv = from o in wfv
                      select new MandPivotalView()
                      {
                          Pivotal = new MandPivotal
                          {
                              Occupation = o.Occupation,
                              Learning = o.Learning,
                              Employed = o.Employed,
                              Cost = o.Cost,
                          }
                      };

            var totalCount = datagrouped1.Count() + datagrouped2.Count();

            return new PagedResultDto<MandPivotalView>(
                totalCount,
                piv.ToList()
            );
        }

        public async Task<PagedResultDto<MandPivotalView>> GetPivotalProgUnEmp(int applicationId)
        {
            var datagrouped1 = (from t in _trainRepository.GetAll().Where(a => a.ApplicationId == applicationId && a.Achievement_status == "Planned")
                                join bio in _bioRepository.GetAll().Where(a => a.ApplicationId == applicationId && a.Employment_Status == "Unemployed (Contracted for period of Learning Programme)") on t.SA_Id_Number equals bio.SA_Id_Number
                                join ofo in _occ.GetAll() on bio.OFO_Occupation_Code equals ofo.OFO_Code
                                join p in _piv.GetAll() on t.Qualification_Learning_Program_Type equals p.Pivotal_Programme
                                select new
                                {
                                    Occupation = ofo.Description,
                                    Learning = t.Study_Field_Or_Specialisation_Specification,
                                    bAppId = bio.ApplicationId,
                                    tAppId = t.ApplicationId,
                                })
                .Where(a => a.bAppId == applicationId && a.tAppId == applicationId)
                .Distinct()
                .ToList();

            var lst = (from t in _trainRepository.GetAll().Where(a => a.ApplicationId == applicationId && a.Achievement_status == "Planned")
                       join bio in _bioRepository.GetAll().Where(a => a.ApplicationId == applicationId && a.Employment_Status == "Unemployed (Contracted for period of Learning Programme)") on t.SA_Id_Number equals bio.SA_Id_Number
                       join ofo in _occ.GetAll() on bio.OFO_Occupation_Code equals ofo.OFO_Code
                       join p in _piv.GetAll() on t.Qualification_Learning_Program_Type equals p.Pivotal_Programme
                       select new
                       {
                           Id = t.Id,
                           Occupation = ofo.Description,
                           Learning = t.Study_Field_Or_Specialisation_Specification,
                           Cost = t.Total_Training_Cost,
                           bAppId = bio.ApplicationId,
                           tAppId = t.ApplicationId,
                       })
                    .Where(a => a.bAppId == applicationId && a.tAppId == applicationId);

            var datagrouped2 = (from t in _trainRepository.GetAll().Where(a => a.ApplicationId == applicationId && a.Achievement_status == "Planned" && (a.SA_Id_Number == null || a.SA_Id_Number == ""))
                                join bio in _bioRepository.GetAll().Where(a => a.ApplicationId == applicationId && a.Employment_Status == "Unemployed (Contracted for period of Learning Programme)" && (a.SA_Id_Number == null || a.SA_Id_Number == "")) on t.Passport_Number equals bio.Passport_Number
                                join ofo in _occ.GetAll() on bio.OFO_Occupation_Code equals ofo.OFO_Code
                                join p in _piv.GetAll() on t.Qualification_Learning_Program_Type equals p.Pivotal_Programme
                                select new
                                {
                                    Occupation = ofo.Description,
                                    Learning = t.Study_Field_Or_Specialisation_Specification,
                                    bAppId = bio.ApplicationId,
                                    tAppId = t.ApplicationId,
                                    tSAId = t.SA_Id_Number,
                                    bSAId = bio.SA_Id_Number
                                })
                .Where(a => a.bAppId == applicationId && a.tAppId == applicationId && (a.bSAId == null || a.bSAId == "") && (a.tSAId == null || a.tSAId == ""))
                .Distinct()
                .ToList();

            var lst2 = (from t in _trainRepository.GetAll().Where(a => a.ApplicationId == applicationId && a.Achievement_status == "Planned" && (a.SA_Id_Number == null || a.SA_Id_Number == ""))
                        join bio in _bioRepository.GetAll().Where(a => a.ApplicationId == applicationId && a.Employment_Status == "Unemployed (Contracted for period of Learning Programme)" && (a.SA_Id_Number == null || a.SA_Id_Number == "")) on t.Passport_Number equals bio.Passport_Number
                        join ofo in _occ.GetAll() on bio.OFO_Occupation_Code equals ofo.OFO_Code
                        join p in _piv.GetAll() on t.Qualification_Learning_Program_Type equals p.Pivotal_Programme
                        select new
                        {
                            Id = t.Id,
                            Occupation = ofo.Description,
                            Learning = t.Study_Field_Or_Specialisation_Specification,
                            Cost = t.Total_Training_Cost,
                            bAppId = bio.ApplicationId,
                            tAppId = t.ApplicationId,
                            tSAId = t.SA_Id_Number,
                            bSAId = bio.SA_Id_Number
                        })
                        .Where(a => a.bAppId == applicationId && a.tAppId == applicationId && (a.bSAId == null || a.bSAId == "") && (a.tSAId == null || a.tSAId == ""));
            var wf = new MandPivotal();
            List<MandPivotal> wfv = new List<MandPivotal>();
            wf.Employed = 0; wf.Cost = 0;

            foreach (var bio in datagrouped1)
            {
                wf = new MandPivotal();
                wf.Occupation = bio.Occupation;
                wf.Learning = bio.Learning;
                wf.Employed = (int)(from t in lst.Where(a => a.Occupation == bio.Occupation && a.Learning == bio.Learning) select new { Id = t.Id }).Count();

                wf.Cost = (decimal)((from t in lst.Where(a => a.Occupation == bio.Occupation && a.Learning == bio.Learning) select new { Id = t.Id, Cost = t.Cost }).Sum(s => s.Cost));
                wfv.Add(wf);
            }

            foreach (var bio in datagrouped2)
            {
                wf = new MandPivotal();
                wf.Occupation = bio.Occupation;
                wf.Learning = bio.Learning;
                wf.Employed = (int)(from t in lst2.Where(a => a.Occupation == bio.Occupation && a.Learning == bio.Learning) select new { Id = t.Id }).Count();

                wf.Cost = (decimal)((from t in lst2.Where(a => a.Occupation == bio.Occupation && a.Learning == bio.Learning) select new { Id = t.Id, Cost = t.Cost }).Sum(s => s.Cost));

                wfv.Add(wf);
            }

            var piv = from o in wfv
                      select new MandPivotalView()
                      {
                          Pivotal = new MandPivotal
                          {
                              Occupation = o.Occupation,
                              Learning = o.Learning,
                              Employed = o.Employed,
                              Cost = o.Cost,
                          }
                      };

            var totalCount = datagrouped1.Count() + datagrouped2.Count();

            return new PagedResultDto<MandPivotalView>(
                totalCount,
                piv.ToList()
            );
        }
        public async Task<PagedResultDto<MandContractorsView>> GetContractors(int applicationId)
        {
            var datagrouped1 = (from t in _trainRepository.GetAll().Where(a => a.ApplicationId == applicationId && a.Achievement_status == "Achieved")
                                join bio in _bioRepository.GetAll().Where(a => a.ApplicationId == applicationId && a.Employment_Status == "Contractor") on t.SA_Id_Number equals bio.SA_Id_Number
                                join ofo in _occ.GetAll() on bio.OFO_Occupation_Code equals ofo.OFO_Code
                                select new
                                {
                                    Learning = t.Study_Field_Or_Specialisation_Specification,
                                    bAppId = bio.ApplicationId,
                                    tAppId = t.ApplicationId,
                                    Employment_Status = bio.Employment_Status,
                                    Achievement_Status = t.Achievement_status
                                })
                .Where(a => a.bAppId == applicationId && a.tAppId == applicationId)
                .Distinct()
                .ToList();

            var lst = (from t in _trainRepository.GetAll().Where(a => a.ApplicationId == applicationId && a.Achievement_status == "Achieved")
                       join bio in _bioRepository.GetAll().Where(a => a.ApplicationId == applicationId && a.Employment_Status == "Contractor") on t.SA_Id_Number equals bio.SA_Id_Number
                       join ofo in _occ.GetAll() on bio.OFO_Occupation_Code equals ofo.OFO_Code
                       select new
                       {
                           Learning = t.Study_Field_Or_Specialisation_Specification,
                           bAppId = bio.ApplicationId,
                           tAppId = t.ApplicationId,
                           Employment_Status = bio.Employment_Status,
                           Achievement_Status = t.Achievement_status,
                           OFO_Code = bio.OFO_Occupation_Code
                       })
                .Where(a => a.bAppId == applicationId && a.tAppId == applicationId)
                .Distinct()
                .ToList();

            var datagrouped2 = (from t in _trainRepository.GetAll().Where(a => a.ApplicationId == applicationId && a.Achievement_status == "Achieved" && (a.SA_Id_Number == null || a.SA_Id_Number == ""))
                                join bio in _bioRepository.GetAll().Where(a => a.ApplicationId == applicationId && a.Employment_Status == "Contractor" && (a.SA_Id_Number == null || a.SA_Id_Number == "")) on t.Passport_Number equals bio.Passport_Number
                                join ofo in _occ.GetAll() on bio.OFO_Occupation_Code equals ofo.OFO_Code
                                select new
                                {
                                    Learning = t.Study_Field_Or_Specialisation_Specification,
                                    Employment_Status = bio.Employment_Status,
                                    Achievement_Status = t.Achievement_status,
                                    bAppId = bio.ApplicationId,
                                    tAppId = t.ApplicationId,
                                    bSAId = bio.SA_Id_Number,
                                    tSAId = t.SA_Id_Number
                                })
                .Where(a => a.bAppId == applicationId && a.tAppId == applicationId && (a.tSAId == null || a.tSAId == "") && (a.bSAId == null || a.bSAId == ""))
                .Distinct()
                .ToList();

            var lst2 = (from t in _trainRepository.GetAll().Where(a => a.ApplicationId == applicationId && a.Achievement_status == "Achieved" && (a.SA_Id_Number == null || a.SA_Id_Number == ""))
                        join bio in _bioRepository.GetAll().Where(a => a.ApplicationId == applicationId && a.Employment_Status == "Contractor" && (a.SA_Id_Number == null || a.SA_Id_Number == "")) on t.Passport_Number equals bio.Passport_Number
                        join ofo in _occ.GetAll() on bio.OFO_Occupation_Code equals ofo.OFO_Code
                        select new
                        {
                            Learning = t.Study_Field_Or_Specialisation_Specification,
                            Employment_Status = bio.Employment_Status,
                            Achievement_Status = t.Achievement_status,
                            bAppId = bio.ApplicationId,
                            tAppId = t.ApplicationId,
                            bSAId = bio.SA_Id_Number,
                            tSAId = t.SA_Id_Number,
                            OFO_Code = bio.OFO_Occupation_Code
                        })
                .Where(a => a.bAppId == applicationId && a.tAppId == applicationId && (a.tSAId == null || a.tSAId == "") && (a.bSAId == null || a.bSAId == ""));

            var wf = new MandContractors();
            List<MandContractors> wfv = new List<MandContractors>();

            foreach (var bio in datagrouped1)
            {
                wf = new MandContractors();
                wf.Programme = bio.Learning;
                wf.Man = (from t in lst.Where(a => a.Learning == bio.Learning && a.OFO_Code.StartsWith("1")) select new { OFO_Code = t.OFO_Code}).Count();
                wf.Pro = (from t in lst.Where(a => a.Learning == bio.Learning && a.OFO_Code.StartsWith("2")) select new { OFO_Code = t.OFO_Code }).Count(); 
                wf.Tech = (from t in lst.Where(a => a.Learning == bio.Learning && a.OFO_Code.StartsWith("3")) select new { OFO_Code = t.OFO_Code }).Count();
                wf.Crit = (from t in lst.Where(a => a.Learning == bio.Learning && a.OFO_Code.StartsWith("4")) select new { OFO_Code = t.OFO_Code }).Count(); 
                wf.Serv = (from t in lst.Where(a => a.Learning == bio.Learning && a.OFO_Code.StartsWith("5")) select new { OFO_Code = t.OFO_Code }).Count();
                wf.Trad = (from t in lst.Where(a => a.Learning == bio.Learning && a.OFO_Code.StartsWith("6")) select new { OFO_Code = t.OFO_Code }).Count(); 
                wf.Op = (from t in lst.Where(a => a.Learning == bio.Learning && a.OFO_Code.StartsWith("7")) select new { OFO_Code = t.OFO_Code }).Count(); 
                wf.Ele = (from t in lst.Where(a => a.Learning == bio.Learning && a.OFO_Code.StartsWith("8")) select new { OFO_Code = t.OFO_Code }).Count();

                wf.Man2 = (from t in lst.Where(a => a.Learning == bio.Learning && a.OFO_Code.StartsWith("1")) select new { OFO_Code = t.OFO_Code }).Count();
                wf.Pro2 = (from t in lst.Where(a => a.Learning == bio.Learning && a.OFO_Code.StartsWith("2")) select new { OFO_Code = t.OFO_Code }).Count();
                wf.Tech2 = (from t in lst.Where(a => a.Learning == bio.Learning && a.OFO_Code.StartsWith("3")) select new { OFO_Code = t.OFO_Code }).Count();
                wf.Crit2 = (from t in lst.Where(a => a.Learning == bio.Learning && a.OFO_Code.StartsWith("4")) select new { OFO_Code = t.OFO_Code }).Count();
                wf.Serv2 = (from t in lst.Where(a => a.Learning == bio.Learning && a.OFO_Code.StartsWith("5")) select new { OFO_Code = t.OFO_Code }).Count();
                wf.Trad2 = (from t in lst.Where(a => a.Learning == bio.Learning && a.OFO_Code.StartsWith("6")) select new { OFO_Code = t.OFO_Code }).Count();
                wf.Op2 = (from t in lst.Where(a => a.Learning == bio.Learning && a.OFO_Code.StartsWith("7")) select new { OFO_Code = t.OFO_Code }).Count();
                wf.Ele2 = (from t in lst.Where(a => a.Learning == bio.Learning && a.OFO_Code.StartsWith("8")) select new { OFO_Code = t.OFO_Code }).Count();

                wfv.Add(wf);
            }

            foreach (var bio in datagrouped2)
            {
                wf = new MandContractors();
                wf.Programme = bio.Learning;
                wf.Man = (from t in lst2.Where(a => a.Learning == bio.Learning && a.OFO_Code.StartsWith("1")) select new { OFO_Code = t.OFO_Code }).Count();
                wf.Pro = (from t in lst2.Where(a => a.Learning == bio.Learning && a.OFO_Code.StartsWith("2")) select new { OFO_Code = t.OFO_Code }).Count();
                wf.Tech = (from t in lst2.Where(a => a.Learning == bio.Learning && a.OFO_Code.StartsWith("3")) select new { OFO_Code = t.OFO_Code }).Count();
                wf.Crit = (from t in lst2.Where(a => a.Learning == bio.Learning && a.OFO_Code.StartsWith("4")) select new { OFO_Code = t.OFO_Code }).Count();
                wf.Serv = (from t in lst2.Where(a => a.Learning == bio.Learning && a.OFO_Code.StartsWith("5")) select new { OFO_Code = t.OFO_Code }).Count();
                wf.Trad = (from t in lst2.Where(a => a.Learning == bio.Learning && a.OFO_Code.StartsWith("6")) select new { OFO_Code = t.OFO_Code }).Count();
                wf.Op = (from t in lst2.Where(a => a.Learning == bio.Learning && a.OFO_Code.StartsWith("7")) select new { OFO_Code = t.OFO_Code }).Count();
                wf.Ele = (from t in lst2.Where(a => a.Learning == bio.Learning && a.OFO_Code.StartsWith("8")) select new { OFO_Code = t.OFO_Code }).Count();
                wf.Man2 = (from t in lst2.Where(a => a.Learning == bio.Learning && a.OFO_Code.StartsWith("1")) select new { OFO_Code = t.OFO_Code }).Count();
                wf.Pro2 = (from t in lst2.Where(a => a.Learning == bio.Learning && a.OFO_Code.StartsWith("2")) select new { OFO_Code = t.OFO_Code }).Count();
                wf.Tech2 = (from t in lst2.Where(a => a.Learning == bio.Learning && a.OFO_Code.StartsWith("3")) select new { OFO_Code = t.OFO_Code }).Count();
                wf.Crit2 = (from t in lst2.Where(a => a.Learning == bio.Learning && a.OFO_Code.StartsWith("4")) select new { OFO_Code = t.OFO_Code }).Count();
                wf.Serv2 = (from t in lst2.Where(a => a.Learning == bio.Learning && a.OFO_Code.StartsWith("5")) select new { OFO_Code = t.OFO_Code }).Count();
                wf.Trad2 = (from t in lst2.Where(a => a.Learning == bio.Learning && a.OFO_Code.StartsWith("6")) select new { OFO_Code = t.OFO_Code }).Count();
                wf.Op2 = (from t in lst.Where(a => a.Learning == bio.Learning && a.OFO_Code.StartsWith("7")) select new { OFO_Code = t.OFO_Code }).Count();
                wf.Ele2 = (from t in lst2.Where(a => a.Learning == bio.Learning && a.OFO_Code.StartsWith("8")) select new { OFO_Code = t.OFO_Code }).Count();
                wfv.Add(wf);
            }

            var con = from o in wfv
                      select new MandContractorsView()
                      {
                          Contractors = new MandContractors
                          {
                              Programme = o.Programme,
                              Man = o.Man,
                              Pro = o.Pro,
                              Tech = o.Tech,
                              Crit = o.Crit,
                              Serv = o.Serv,
                              Trad = o.Trad,
                              Op = o.Op,
                              Ele = o.Ele,
                              Man2 = o.Man2,
                              Pro2 = o.Pro2,
                              Tech2 = o.Tech2,
                              Crit2 = o.Crit2,
                              Serv2 = o.Serv2,
                              Trad2 = o.Trad2,
                              Op2 = o.Op2,
                              Ele2 = o.Ele2
                          }
                      };

            var totalCount = datagrouped1.Count() + datagrouped2.Count();

            return new PagedResultDto<MandContractorsView>(
                totalCount,
                con.ToList()
            );
        }

        public async Task<PagedResultDto<MandHardToFillSummaryView>> GetHTFVacancies(int applicationId)
        {
            var datagrouped = (from h in _htvfRepository.GetAll().Where(a=>a.ApplicationId == applicationId)
                select new
                {
                    Occupation_Code = h.OCCUPATION_CODE,
                    Occupation = h.OCCUPATION_OR_SPECIALISATION_TITLE,
                    Primary_Reason = h.PRIMARY_REASON,
                    Further_Reason = h.FURTHER_REASON,
                    Further_Reason1 = h.FURTHER_REASON_1,
                    Comment = h.Comment,
                    ApplicationId = applicationId
                })
                .Distinct()
                .ToList();

            var wf = new MandHardToFillSummary();
            List<MandHardToFillSummary> wfv = new List<MandHardToFillSummary>();

            foreach (var bio in datagrouped)
            {
                wf = new MandHardToFillSummary();
                wf.Occupation = bio.Occupation;
                wf.OFO = bio.Occupation_Code;
                wf.PrimaryReason = bio.Primary_Reason;
                wf.Further1 = bio.Further_Reason;
                wf.Further2 = bio.Further_Reason1;
                wf.Comment = bio.Comment;
                wf.EC = _htvfRepository.GetAll().Where(a => a.ApplicationId == applicationId && a.OCCUPATION_OR_SPECIALISATION_TITLE == bio.Occupation && a.OCCUPATION_CODE == bio.Occupation_Code && (a.PROVINCE == "Eastern Cape" || a.PROVINCE == "Eastern_Cape" || a.PROVINCE == "Eastern-Cape")).Sum(a=>a.NUMBER_OF_VACANCIES);
                wf.FS = _htvfRepository.GetAll().Where(a => a.ApplicationId == applicationId && a.OCCUPATION_OR_SPECIALISATION_TITLE == bio.Occupation && a.OCCUPATION_CODE == bio.Occupation_Code && (a.PROVINCE == "Free State" || a.PROVINCE == "Free_State" || a.PROVINCE == "Free-State")).Sum(a => a.NUMBER_OF_VACANCIES);
                wf.GP = _htvfRepository.GetAll().Where(a => a.ApplicationId == applicationId && a.OCCUPATION_OR_SPECIALISATION_TITLE == bio.Occupation && a.OCCUPATION_CODE == bio.Occupation_Code && a.PROVINCE == "Gauteng").Sum(a => a.NUMBER_OF_VACANCIES);
                wf.KZN = _htvfRepository.GetAll().Where(a => a.ApplicationId == applicationId && a.OCCUPATION_OR_SPECIALISATION_TITLE == bio.Occupation && a.OCCUPATION_CODE == bio.Occupation_Code && (a.PROVINCE == "Kwazulu Natal" || a.PROVINCE == "Kwazulu_Natal" || a.PROVINCE == "Kwazulu-Natal")).Sum(a => a.NUMBER_OF_VACANCIES);
                wf.LP = _htvfRepository.GetAll().Where(a => a.ApplicationId == applicationId && a.OCCUPATION_OR_SPECIALISATION_TITLE == bio.Occupation && a.OCCUPATION_CODE == bio.Occupation_Code && a.PROVINCE == "Limpopo").Sum(a => a.NUMBER_OF_VACANCIES);
                wf.MP = _htvfRepository.GetAll().Where(a => a.ApplicationId == applicationId && a.OCCUPATION_OR_SPECIALISATION_TITLE == bio.Occupation && a.OCCUPATION_CODE == bio.Occupation_Code && a.PROVINCE == "Mpumalanga").Sum(a => a.NUMBER_OF_VACANCIES);
                wf.NP = _htvfRepository.GetAll().Where(a => a.ApplicationId == applicationId && a.OCCUPATION_OR_SPECIALISATION_TITLE == bio.Occupation && a.OCCUPATION_CODE == bio.Occupation_Code && (a.PROVINCE == "Northern Province" || a.PROVINCE == "Northern_Province" || a.PROVINCE == "Northern-Province")).Sum(a => a.NUMBER_OF_VACANCIES);
                wf.NW = _htvfRepository.GetAll().Where(a => a.ApplicationId == applicationId && a.OCCUPATION_OR_SPECIALISATION_TITLE == bio.Occupation && a.OCCUPATION_CODE == bio.Occupation_Code && (a.PROVINCE == "North West" || a.PROVINCE == "North_West" || a.PROVINCE == "North-West")).Sum(a => a.NUMBER_OF_VACANCIES);                
                wf.WC = _htvfRepository.GetAll().Where(a => a.ApplicationId == applicationId && a.OCCUPATION_OR_SPECIALISATION_TITLE == bio.Occupation && a.OCCUPATION_CODE == bio.Occupation_Code && (a.PROVINCE == "Western Cape" || a.PROVINCE == "Western_Cape" || a.PROVINCE == "Western-Cape")).Sum(a => a.NUMBER_OF_VACANCIES);
                wfv.Add(wf);
            }

            var htf = from o in wfv
                      select new MandHardToFillSummaryView()
                      {
                          HTFV = new MandHardToFillSummary
                          {
                              Occupation = o.Occupation,
                              OFO = o.OFO,
                              PrimaryReason = o.PrimaryReason,
                              Further1 = o.Further1,
                              Further2 = o.Further2,
                              Comment = o.Comment,
                              EC = o.EC,
                              FS = o.FS,
                              GP = o.GP,
                              KZN = o.KZN,
                              LP = o.LP,
                              MP = o.MP,
                              NP = o.NP,
                              NW = o.NW,
                              WC = o.WC,
                          }
                      };

            var totalCount = datagrouped.Count();

            return new PagedResultDto<MandHardToFillSummaryView>(
                totalCount,
                htf.ToList()
            );
        }

        public async Task<PagedResultDto<MandSkillsGapSummaryView>> GetScarceCritical(int applicationId)
        {
            var datagrouped = (from h in _skillGabRepository.GetAll().Where(a => a.ApplicationId == applicationId)
                               select new
                               {
                                   Occupation_Code = h.Code,
                                   Occupation = h.OCCUPATION_OR_SPECIALISATION_TITLE,
                                   SkillSet = h.SKILL_GAB,
                                   Comment = h.REASON_FOR_THE_SKILLS_GAP,
                                   ApplicationId = applicationId
                               })
                .Where(a => a.ApplicationId == applicationId)
                .Distinct()
                .ToList();

            var wf = new MandSkillsGapSummary();
            List<MandSkillsGapSummary> wfv = new List<MandSkillsGapSummary>();

            foreach (var bio in datagrouped)
            {
                wf = new MandSkillsGapSummary();
                wf.OFO = bio.Occupation_Code;
                wf.Occupation = bio.Occupation;
                wf.SkillSet = bio.SkillSet;
                wf.Comment = bio.Comment;
                wfv.Add(wf);
            }

            var htf = from o in wfv
                      select new MandSkillsGapSummaryView()
                      {
                          Scarce = new MandSkillsGapSummary
                          {
                              Occupation = o.Occupation,
                              OFO = o.OFO,
                              SkillSet = o.SkillSet,
                              Comment = o.Comment,
                          }
                      };

            var totalCount = datagrouped.Count();

            return new PagedResultDto<MandSkillsGapSummaryView>(
                totalCount,
                htf.ToList()
            );
        }

        public async Task SubmitApplication(int id, int userid)
        {
            var mand = _mandatoryApplication.GetAll().Where(a => a.Id == id);

            if (mand.Count() == 1)
            {
                var app = mand.FirstOrDefault();
                app.UsrUpd = userid;
                app.DteUpd = DateTime.Now;
                app.UserSubmitted = userid;
                app.SubmissionDte = DateTime.Now;
                app.GrantStatusID = 2;

                await _mandatoryApplication.UpdateAsync(app);
            }
        }

        public async Task MGSendSubmissionEmail(ConfirmMGEmailInput input)
        {
            await _userEmailer.MGSendSubmissionEmailAsync(input.EmailAddress, input.sdlNumber, input.organisationName, input.projectCode, input.projectName);
        }

        public async Task SendGenericEmail(ConfirmMGEmailInput input)
        {
            await _userEmailer.SendGenericEmailAsync("mlotshwas@gmail.com", "Testing Generic IMS Email", "Dear User", "It is with pleasure <br>|that I welcome you to the IMS system. <br>| This is a generic messages so check the line breaks <br>| and the formatting of the message.","Yours faithfully, <br>| <b>Sipho Mlotshwa</b> <br> | ICT Manager");
        }

        public async Task<string> validateMGSubmission(int appId)
        {
            string output = "";
            var appwin = (from a in _mandatoryApplication.GetAll()
                       join w in _mandatoryWindow.GetAll() on a.GrantWindowId equals w.Id
                       join ext in _extRepository.GetAll() on a.Id equals ext.ApplicationId into e
                          from exts in e.DefaultIfEmpty()
                          select new
                       {
                           EndDate = w.EndDate,
                           ExtensionDate = w.ExtensionDate,
                           GrantStatusID = a.GrantStatusID,
                           OrganisationId = a.OrganisationId,
                           ExtenstionActive = w.ExtenstionActive,
                           Id = a.Id
                       }).Where(a=>a.Id == appId).FirstOrDefault();

            if (appwin.EndDate <= DateTime.Now) 
            {
                if (appwin.ExtensionDate <= DateTime.Now)
                {
                    output = "The Window is now Closed";
                } else
                {
                    if (appwin.GrantStatusID != 3)
                    {
                        output = "This application is not approved for extension.";
                    }
                }
            };

            var org = await _orgRepository.FirstOrDefaultAsync(appwin.OrganisationId);
            if (org.SIC_Code == null || org.SIC_Code == "") { output = output + ", Sic Code"; }
            if (org.CORE_BUSINESS == null || org.CORE_BUSINESS == "") { output = output + ", Core Business"; }
            if (org.NUMBER_OF_EMPLOYEES == 0) { output = output + ", Number of Employees"; }
            if (org.BBBEE_Status == null || org.BBBEE_Status == "") { output = output + ", BBBEE_Status"; }
            if (org.BBBEE_LEVEL == 0) { output = output + ", BBBEE Level"; }
            if (org.CHAMBER == null || org.CHAMBER == "") { output = output + ", Sub Sector"; }
            if (org.CEO_Email == null || org.CEO_Email == "") { output = output + "CEO Email"; }
            if (org.CEO_Name == null || org.CEO_Name == "") { output = output + ", CEO Name"; }
            if (org.CEO_RaceId == null || org.CEO_RaceId == "") { output = output + ", CEO Race"; }
            if (org.CEO_GenderId == null || org.CEO_GenderId == "") { output = output + ", CEO Gender"; }
            if (org.CEO_Surname == null || org.CEO_Surname == "") { output = output + ", CEO Surname"; }
            if (org.Organisation_Contact_Cell_Number == null || org.Organisation_Contact_Cell_Number == "") { output = output + ", Contact Cellphone"; }
            if (org.Organisation_Contact_Email_Address == null || org.Organisation_Contact_Email_Address == "") { output = output + ", Contact Email"; }
            if (org.Organisation_Contact_Phone_Number == null || org.Organisation_Contact_Phone_Number == "") { output = output + ", Contact Phone"; }
            if (org.Organisation_Registration_Number == null || org.Organisation_Registration_Number == "") { output = output + ", Registration Number"; }
            if (org.Senior_Rep_Email == null || org.Senior_Rep_Email == "") { output = output + ", Rep Email"; }
            if (org.Senior_Rep_GenderId == null || org.Senior_Rep_GenderId == "") { output = output + ", Rep Gender"; }
            if (org.Senior_Rep_Name == null || org.Senior_Rep_Name == "") { output = output + ", Rep Name"; }
            if (org.Senior_Rep_RaceId == null || org.Senior_Rep_RaceId == "") { output = output + ", Rep Race"; }
            if (org.Senior_Rep_Surname == null || org.Senior_Rep_Surname == "") { output = output + ", Rep Surname"; }
            if (org.SIC_Code == null || org.SIC_Code == "") { output = output + ", SIC Code"; }

            var paddress = _addressRepository.GetAll().Where(a => a.organisationId == org.Id).FirstOrDefault();
            if (paddress == null)
            {
                output = output + ", Physical Address";
            }

            var bank = _bankRepository.GetAll().Where(a => a.OrganisationId == org.Id).FirstOrDefault();
            if (bank == null)
            {
                output = output + ", Bank Details";
            }
            else
            {
                if (bank.AccountType == 0) { output = output + ", Bank Account Type"; }
                if (bank.Branch_Code.Length == 0) { output = output + ", Bank Branch Code"; }
                if (bank.Branch_Name.Length == 0) { output = output + ", Bank Branch Name"; }
                if (bank.Account_Holder.Length == 0) { output = output + ", Bank Account Holder"; }
                if (bank.Account_Number.Length == 0) { output = output + ", Bank Account Number"; }
                if (bank.Bank_Name.Length == 0) { output = output + ", Bank Name"; }
            }

            var b = _bioRepository.GetAll().Where(a=>a.ApplicationId == appId).Count();
            if (b == 0) { 
                output = output + ", Please complete Biographical Details before Submitting."; 
            }

            var t = _trainRepository.GetAll().Where(a => a.ApplicationId == appId).Count();
            if (t == 0) { 
                output = output + ", Please complete Training Details before Submitting."; 
            }

            var f = _financE_AND_TRAINING_COMPARISONRepository.GetAll().Where(a => a.ApplicationId == appId).Count();
            if (f == 0) { 
                output = output + ", Please complete Finance and Training before Submitting."; 
            } else {
                if (f > 1)
                {
                    output = output + ", More than one(1) entry was detected. Please make sure there is only one entry.";
                } else {
                    var fin = _financE_AND_TRAINING_COMPARISONRepository.GetAll().Where(a => a.ApplicationId == appId).FirstOrDefault();
                    if ((fin.TOTAL_ACTUAL_PAYROLL_FOR_THE_YEAR == null || fin.TOTAL_ACTUAL_PAYROLL_FOR_THE_YEAR == 0 ||
                    fin.TOTAL_PROJECTED_PAYROLL_FOR_THE_YEAR == null || fin.TOTAL_PROJECTED_PAYROLL_FOR_THE_YEAR == 0 ||
                    fin.TOTAL_ACTUAL_SKILLS_DEVELOPMENT_SPEND_FOR_THE_YEAR == null || fin.TOTAL_PROJECTED_SKILLS_DEVELOPMENT_BUDGET == null ||
                    fin.PROJECTED_PAYROLL == null || fin.OF_PAYROLL_SPENT_ON_SKILLS_DEVELOPMENT == null ||
                    fin.BENEFICIARIES_TRAIN == null || fin.TOTAL_BENEFICIARIES_ACTUALLY_TRAINED_IN_THE == null ||
                    fin.ACTUAL_TRAINING_VS_PLANNED_TRAINING == null ||
                    fin.CONFIRMATION_OF_EMPLOYEES_HIGHEST_QUALIFICATIONS == null || fin.CONFIRMATION_OF_EMPLOYEES_HIGHEST_QUALIFICATIONS == "" ||
                    fin.LEARNING_OPPORTUNITIES_UNEMPLOYED_PEOPLE == null || fin.LEARNING_OPPORTUNITIES_UNEMPLOYED_PEOPLE == "" ||
                    fin.LEARNING_AREAS_AND_OPPORTUNITIES_FOR_EMPLOYED_STAFF == null || fin.LEARNING_AREAS_AND_OPPORTUNITIES_FOR_EMPLOYED_STAFF == "" ||
                    fin.ADDRESSING_EQUITY_AND_BBBEE_TARGETS == null || fin.ADDRESSING_EQUITY_AND_BBBEE_TARGETS == "" ||
                    fin.WORK_PLACEMENT == null || fin.WORK_PLACEMENT == "" ||
                    fin.AREAS_FOR_RESEARCH_AND_INNOVATION == null || fin.AREAS_FOR_RESEARCH_AND_INNOVATION == "" ||
                    fin.LEARNERS_RETAINED == null || fin.PEOPLE_FOUND_EMPLOYMENT_DUE_TRAINING == null))
                    {
                        output = output + ", Finance details are incomplete.";
                    }
                }
            }

            b = _bioRepository.GetAll().Where(a => a.ApplicationId == appId && a.Status == null).Count();
            if (b > 0) { 
                output = output + ", Biographical data has not been validated. Please click Validate on BioData to check."; 
            }

            t = _trainRepository.GetAll().Where(a => a.ApplicationId == appId && a.Status == null).Count();
            if (t > 0) { 
                output = output + ", Training data has not been validated. Please click Validate on Training to check."; 
            }

            var htf = _htvfRepository.GetAll().Where(a => a.ApplicationId == appId && a.Status == null).Count();
            if (htf > 0) { 
                output = output + ", HTFV data has not been validated. Please click Validate on HTFV to check."; 
            }

            var ss = _skillGabRepository.GetAll().Where(a => a.ApplicationId == appId && a.Status == null).Count();
            if (ss > 0) { 
                output = output + ", Skiills Gap data has not been validated. Please click Validate on Skills Gap to check."; 
            }

            b = _bioRepository.GetAll().Where(a => a.ApplicationId == appId && a.Status == "Fatal").Count();
            if (b > 0) { 
                output = output + ", Biographical data has errors. Please click Validate on BioData to check."; 
            }

            t = _trainRepository.GetAll().Where(a => a.ApplicationId == appId && a.Status == "Fatal").Count();
            if (t > 0) { output = output + ", Training data has errors. Please click Validate on Training to check."; }

            htf = _htvfRepository.GetAll().Where(a => a.ApplicationId == appId && a.Status == "Fatal").Count();
            if (htf > 0) { output = output + ", HTFV data has errors. Please click Validate on HTFV to check."; }

            ss = _skillGabRepository.GetAll().Where(a => a.ApplicationId == appId && a.Status == "Fatal").Count();
            if (ss > 0) { output = output + ", Skiills Gap data has errors. Please click Validate on Skills Gap to check."; }

            if (output.StartsWith(",")) { output = output.Substring(2, output.Length - 2); }

            return output;
        }

        public async Task<string> CreateEditExtension(ExtensionsDto input, int UserId)
        {
            var output = "";

            var ext = _extRepository.GetAll().Where(a => a.ApplicationId == input.ApplicationId);

            if (ext.Count() == 0)
            {
                input.DateCreated = DateTime.Now;
                input.DateRequested = DateTime.Now;
                input.UserId = UserId;

                var app = ObjectMapper.Map<Extensions>(input);

                await _extRepository.InsertAsync(app);
            }
            else
            {
                var extn = await _extRepository.FirstOrDefaultAsync(ext.FirstOrDefault().Id);
                extn.DteUpd = DateTime.Now;
                extn.UsrUpd = UserId;

                await _extRepository.UpdateAsync(extn);
            }

            return output;
        }

        public async Task<ExtensionsDto> GetExtensionId(int ExtensionId)
        {
            var app = _extRepository.GetAll().Where(a => a.Id == ExtensionId).FirstOrDefault();

            var output = ObjectMapper.Map<ExtensionsDto>(app);

            return output;
        }

        public async Task<ExtensionsDto> GetExtensionByApplicationId(int ApplicationId)
        {
            var app = _extRepository.GetAll().Where(a => a.ApplicationId == ApplicationId).FirstOrDefault();

            var output = ObjectMapper.Map<ExtensionsDto>(app);

            return output;
        }

        public async Task<PagedResultDto<ExtensionsDtoView>> GetExtensionsByWindow(int WindowId)
        {
            var ext = (from exn in _extRepository.GetAll()
                       join stat in _mandStatRepository.GetAll() on exn.RequestStatus equals stat.Id
                       join app in _mandatoryApplication.GetAll() on exn.ApplicationId equals app.Id
                       join win in _mandatoryWindow.GetAll() on app.GrantWindowId equals win.Id
                       select new
                       {
                           ExensionDate = win.ExtensionDate,
                           ExtStatus = stat.StatusDescription,
                           ApplicationId = app.Id,
                           WindowId = win.Id,
                           ext = exn
                       })
                .Where(a => a.WindowId == WindowId);

            var extn = (from o in ext
                        select new ExtensionsDtoView
                        {
                            Extensions = new MandatoryExtensionsView
                            {
                                Id = o.ext.Id,
                                ApplicationId = o.ext.ApplicationId,
                                RequestStatus = o.ExtStatus,
                                ExtensionDate = o.ExensionDate,
                                DateRequested = o.ext.DateRequested,
                                ReasonForRequest = o.ext.ReasonForRequest,
                                DateCreated = o.ext.DateCreated,
                                UserId = o.ext.UserId,
                            }
                        }).ToList();

            var totalCount = extn.Count();

            return new PagedResultDto<ExtensionsDtoView>(
                totalCount,
                extn.ToList()
            );
        }

        public async Task CreateOrEditMandatoryApproval(MandatoryApprovalDto input)
        {
            if (input.Id == 0)
            {
                var approval = ObjectMapper.Map<MandatoryApproval>(input);
                approval.DateCreated = DateTime.Now;
                approval.DateReviewed = DateTime.Now;
                approval.UserReviewed = input.UserId;
                await _approvalRepository.InsertAsync(approval);

                var app = _mandatoryApplication.GetAll().Where(a=>a.Id == input.ApplicationId).FirstOrDefault();
                if (input.StatusId == 1)
                {
                    app.GrantStatusID = 4;
                    app.DteUpd = DateTime.Now;
                    app.UsrUpd = input.UserId;
                }

                if (input.StatusId == 2)
                {
                    app.GrantStatusID = 8;
                    app.DteUpd = DateTime.Now;
                    app.UsrUpd = input.UserId;
                }
                _mandatoryApplication.UpdateAsync(app);
            }
            else
            {
                var approval = await _approvalRepository.FirstOrDefaultAsync(input.Id);
                input.DteUpd= DateTime.Now;
                input.UsrUpd = input.UserId;
                ObjectMapper.Map(input, approval);

                var app = _mandatoryApplication.GetAll().Where(a=>a.Id == input.ApplicationId).FirstOrDefault();
                if (input.StatusId == 1)
                {
                    app.GrantStatusID = 4;
                    app.DteUpd = DateTime.Now;
                    app.UsrUpd = input.UserId;
                }

                if (input.StatusId == 2)
                {
                    app.GrantStatusID = 8;
                    app.DteUpd = DateTime.Now;
                    app.UsrUpd = input.UserId;
                }
                _mandatoryApplication.UpdateAsync(app);
            }
        }

        public async Task<string> ValidateApproval(int applicationId)
        {
            var output = "";
            var approval = _approvalRepository.GetAll().Where(a=>a.ApplicationId == applicationId).FirstOrDefault();

            if (approval.StatusId == 1)
            {
                if (approval.ParentChild == true && approval.Sublevies == false) { output = "Sublevies not provided"; }
                if (approval.Bankdetails == false) { output = output + ", Bank details not matched"; }
                if (approval.Employees == false) { output = output + ", Employees not captured"; }
                if (approval.TrainingPlanned == false) { output = output + ", Planned training not captured"; }
                if (approval.TrainingReceived == false && approval.Firstsubmission == false) { output = output + ", Received training not captured"; }
                if (approval.Finance == false) { output = output + ", Finance details not captured"; }
                if (approval.EmployerRep == false) { output = output + ", Employer Rep Signatures missing"; }
                
                var emps = _bioRepository.GetAll().Where(a => a.ApplicationId == applicationId).Count();
                if (emps > 50 && approval.UnionSignatory ==  false) { output = output + ", Employee Reps Signatures missing"; }

                var docs = (from d in _docApprovalRepository.GetAll().Where(a => a.ApplicationId == applicationId && a.ApprovalStatusId == 1)
                    select new {doctype = d.DocumentTypeId})
                        .Distinct()
                        .Count();
                if (docs != 3) { output = output + ", Validate all documents"; }
            }
            return output;
        }

        public async Task FinaliseApproval(int applicationId, int userId)
        {
            var approval = _approvalRepository.GetAll().Where(a => a.ApplicationId == applicationId).FirstOrDefault();

            var app = _mandatoryApplication.GetAll().Where(a => a.Id == applicationId).FirstOrDefault();
            if (approval.StatusId == 1)
            {
                app.GrantStatusID = 4;
                app.DteUpd= DateTime.Now;
                app.UsrUpd = userId;
                _mandatoryApplication.UpdateAsync(app);
            }
            if (approval.StatusId == 2)
            {
                app.GrantStatusID = 8;
                app.DteUpd = DateTime.Now;
                app.UsrUpd = userId;
                _mandatoryApplication.UpdateAsync(app);
            }
        }

        public async Task<PagedResultDto<MandatoryApprovalViewPaged>> GetMandatoryApprovals()
        {

            var filteredApprovals = _approvalRepository.GetAll();

            var pagedAndFilteredApprovals = filteredApprovals;

            var approvals = from o in pagedAndFilteredApprovals
            select new MandatoryApprovalViewPaged()
            {
                MandatoryApproval = new MandatoryApprovalDto()
                {
                    ApplicationId = o.ApplicationId,
                    StatusId = o.StatusId,
                    UserReviewed = o.UserReviewed,
                    DateReviewed = o.DateReviewed,
                    Firstsubmission = o.Firstsubmission,
                    ParentChild = o.ParentChild,
                    Sublevies = o.Sublevies,
                    Bankdetails = o.Bankdetails,
                    Employees = o.Employees,
                    TrainingReceived = o.TrainingReceived,
                    TrainingPlanned = o.TrainingPlanned,
                    Finance = o.Finance,
                    EmployerRep = o.EmployerRep,
                    Comment = o.Comment,
                    UnionSignatory = o.UnionSignatory,
                    DateCreated = o.DateCreated,
                    UserId = o.UserId,
                    Id = o.Id
                }
            };

            var totalCount = filteredApprovals.Count();

            return new PagedResultDto<MandatoryApprovalViewPaged>(
                totalCount,
                 approvals.ToList()
            );
        }

        public async Task<PagedResultDto<MandatoryApprovalView>> GetMandatoryApprovalView(int applicationId)
        {
            var apro = (from apr in _approvalRepository.GetAll()
                       join stat in _approvalStatusRepository.GetAll() on apr.StatusId equals stat.Id
                       join app in _mandatoryApplication.GetAll() on apr.ApplicationId equals app.Id
                       select new
                       {
                           ApprovalStatus = stat.StatusDescription,
                           ApplicationId = app.Id,
                           apr = apr
                       })
                .Where(a => a.ApplicationId == applicationId);

            var apprv = (from o in apro
            select new MandatoryApprovalView
            {
                Id = o.apr.Id,
                ApplicationId = o.apr.ApplicationId,
                ApprovalStatus = o.ApprovalStatus,
                UserReviewed = o.apr.UserReviewed,
                DateReviewed = o.apr.DateReviewed,
                ParentChild = o.apr.ParentChild,
                Sublevies = o.apr.Sublevies,
                Bankdetails = o.apr.Bankdetails,
                Employees = o.apr.Employees,
                TrainingReceived = o.apr.TrainingReceived,
                TrainingPlanned = o.apr.TrainingPlanned,
                Finance = o.apr.Finance,
                EmployerRep = o.apr.EmployerRep,
                UnionSignatory = o.apr.UnionSignatory,
                Comment = o.apr.Comment,
                DateCreated = o.apr.DateCreated,
                UserId = o.apr.UserId
            }).ToList();

            var totalCount = apprv.Count();

            return new PagedResultDto<MandatoryApprovalView>(
                totalCount,
                apprv.ToList()
            );
        }

        public async Task<MandatoryApprovalDto> GetMandatoryApprovalId(int applicationId)
        {
            var appr = _approvalRepository.GetAll().Where(a => a.ApplicationId == applicationId).FirstOrDefault();

            var output = ObjectMapper.Map<MandatoryApprovalDto>(appr);

            return output;
        }

        public async Task ApproveDocument(int doctype, int stat, string comment, int applicationId, int userid)
        {
            var appr = _docApprovalRepository.GetAll().Where(a => a.ApplicationId == applicationId && a.DocumentTypeId == doctype && a.ApprovalTypeId == 1).FirstOrDefault();
            if (appr == null)
            {
                var inappr = new MandatoryDocumentApproval();
                inappr.ApplicationId = applicationId;
                inappr.ApprovalTypeId = 1;
                inappr.ApprovalStatusId = stat;
                inappr.DocumentTypeId = doctype;
                inappr.Comments = comment;
                inappr.UsrUpd = userid;
                inappr.UserId = userid;
                inappr.DateCreated = DateTime.Now;
                _docApprovalRepository.Insert(inappr);
            }
            else
            {
                appr.ApprovalStatusId = stat;
                appr.UsrUpd = userid;
                appr.DteUpd = DateTime.Now;
                appr.Comments = comment;
                _docApprovalRepository.UpdateAsync(appr);
            }
        }

        public async Task<MandatoryDocumentApprovalForView> GetDocumentApproval(int applicationId, int doctype)
        {
            var approvals = (from appr in _docApprovalRepository.GetAll()
                             join stat in _grantApprovalStatusRepository.GetAll() on appr.ApprovalStatusId equals stat.Id
                             join typ in _grantApprovalTypeRepository.GetAll() on appr.ApprovalTypeId equals typ.Id
                             select new
                             {
                                 DocumentApproval = appr,
                                 ApprovalStatus = stat.GrantStatusDescription,
                                 ApprovalType = typ.ApprovalDescription
                             })
                    .Where(a => a.DocumentApproval.ApplicationId == applicationId && a.DocumentApproval.DocumentTypeId == doctype)
                    .FirstOrDefault();

            if (approvals != null)
            {
                var approvallist = new MandatoryDocumentApprovalForView()
                {
                    ApplicationId = approvals.DocumentApproval.ApplicationId,
                    ApprovalType = approvals.ApprovalType,
                    ApprovalStatus = approvals.ApprovalStatus,
                    Comments = approvals.DocumentApproval.Comments,
                    Id = approvals.DocumentApproval.Id
                };

                return approvallist;
            }

            return null;
        }

        public async Task ReOpenDocument(int doctype, int applicationId, int userid)
        {
            var doc = _docApprovalRepository.GetAll().Where(a => a.ApplicationId == applicationId && a.DocumentTypeId == doctype).FirstOrDefault();
            var proj = await _docApprovalRepository.GetAsync(doc.Id);
            await _docApprovalRepository.DeleteAsync(proj);
        }

        public async Task SendRejectionEmail(int ApplicationId, string Email, string Reason)
        {
            await _userEmailer.SendMandatoryRejectionEmailsAsync("smlotshwa@chieta.org.za", Reason);
        }

        public async Task SendExtensionEmail(int WindowId)
        {
            var extlist = (from exn in _extRepository.GetAll()
            join stat in _mandStatRepository.GetAll() on exn.RequestStatus equals stat.Id
            join app in _mandatoryApplication.GetAll() on exn.ApplicationId equals app.Id
            join org in _orgRepository.GetAll() on app.OrganisationId equals org.Id
            join sdf in _personRepository.GetAll() on app.UserId equals sdf.Userid
            join win in _mandatoryWindow.GetAll() on app.GrantWindowId equals win.Id
            select new
            {
                ExensionDate = win.ExtensionDate,
                ExtStatus = stat.StatusDescription,
                ApplicationId = app.Id,
                AppStatus = app.GrantStatusID,
                WindowId = win.Id,
                EmailAddress = sdf.Email,
                SDL_Number = org.SDL_No,
                Organisation_Name = org.Organisation_Name,
                ext = exn
            })
            //.Where(a => a.WindowId == WindowId && a.ExtStatus == "Approved" && a.ExensionDate > DateTime.Now && a.AppStatus == 3)
            .Where(a => a.WindowId == WindowId && a.ExtStatus == "Approved" && a.ExensionDate > DateTime.Now && a.AppStatus == 3)
            .ToList();

            foreach (var ext in extlist)
            {
                await _userEmailer.SendMandatoryExtensionEmailsAsync(ext.EmailAddress, ext.SDL_Number, ext.Organisation_Name);
            }

            await _userEmailer.SendMandatoryExtensionEmailsAsync("smlotshwa@chieta.org.za","L000000000", "Anonymous");
        }

        public async Task<PagedResultDto<GetCBiodataForViewDto>> GetBiodataLazy(int first, int rows, int ApplicationId, string SAIdNoFilter, string SAIdNoFilterMode,
        string PPNoFilter, string PPNoFilterMode, string NameFilter, string NameFilterMode, string SurnameFilter, string SurnameFilterMode)
        {
            var filteredCBiodatas = _bioRepository.GetAll()
            .Where(a => a.ApplicationId == ApplicationId);

            if (SAIdNoFilter != null)
            {
                if (SAIdNoFilterMode == "startsWith")
                {
                    filteredCBiodatas = filteredCBiodatas.Where(a => a.SA_Id_Number.StartsWith(SAIdNoFilter));
                }
                if (SAIdNoFilterMode == "endsWith")
                {
                    filteredCBiodatas = filteredCBiodatas.Where(a => a.SA_Id_Number.EndsWith(SAIdNoFilter));
                }
                if (SAIdNoFilterMode == "contains")
                {
                    filteredCBiodatas = filteredCBiodatas.Where(a => a.SA_Id_Number.Contains(SAIdNoFilter));
                }
                if (SAIdNoFilterMode == "notContains")
                {
                    filteredCBiodatas = filteredCBiodatas.Where(a => !(a.SA_Id_Number.Contains(SAIdNoFilter)));
                }
                if (SAIdNoFilterMode == "equals")
                {
                    filteredCBiodatas = filteredCBiodatas.Where(a => a.SA_Id_Number ==SAIdNoFilter);
                }
            }

            if (PPNoFilter != null)
            {
                if (PPNoFilterMode == "startsWith")
                {
                    filteredCBiodatas = filteredCBiodatas.Where(a => a.Passport_Number.StartsWith(PPNoFilter));
                }
                if (PPNoFilterMode == "endsWith")
                {
                    filteredCBiodatas = filteredCBiodatas.Where(a => a.Passport_Number.EndsWith(PPNoFilter));
                }
                if (PPNoFilterMode == "contains")
                {
                    filteredCBiodatas = filteredCBiodatas.Where(a => a.Passport_Number.Contains(PPNoFilter));
                }
                if (PPNoFilterMode == "notContains")
                {
                    filteredCBiodatas = filteredCBiodatas.Where(a => !(a.Passport_Number.Contains(PPNoFilter)));
                }
                if (PPNoFilterMode == "equals")
                {
                    filteredCBiodatas = filteredCBiodatas.Where(a => a.Passport_Number == PPNoFilter);
                }
            }

            if (NameFilter != null)
            {
                if (NameFilterMode == "startsWith")
                {
                    filteredCBiodatas = filteredCBiodatas.Where(a => a.Firstname.StartsWith(NameFilter));
                }
                if (NameFilterMode == "endsWith")
                {
                    filteredCBiodatas = filteredCBiodatas.Where(a => a.Firstname.EndsWith(NameFilter));
                }
                if (NameFilterMode == "contains")
                {
                    filteredCBiodatas = filteredCBiodatas.Where(a => a.Firstname.Contains(NameFilter));
                }
                if (NameFilterMode == "notContains")
                {
                    filteredCBiodatas = filteredCBiodatas.Where(a => !(a.Firstname.Contains(NameFilter)));
                }
                if (NameFilterMode == "equals")
                {
                    filteredCBiodatas = filteredCBiodatas.Where(a => a.Firstname == NameFilter);
                }
            }

            if (SurnameFilter != null)
            {
                if (SurnameFilterMode == "startsWith")
                {
                    filteredCBiodatas = filteredCBiodatas.Where(a => a.Surname.StartsWith(SurnameFilter));
                }
                if (SurnameFilterMode == "endsWith")
                {
                    filteredCBiodatas = filteredCBiodatas.Where(a => a.Surname.EndsWith(SurnameFilter));
                }
                if (SurnameFilterMode == "contains")
                {
                    filteredCBiodatas = filteredCBiodatas.Where(a => a.Surname.Contains(SurnameFilter));
                }
                if (SurnameFilterMode == "notContains")
                {
                    filteredCBiodatas = filteredCBiodatas.Where(a => !(a.Surname.Contains(SurnameFilter)));
                }
                if (SurnameFilterMode == "equals")
                {
                    filteredCBiodatas = filteredCBiodatas.Where(a => a.Surname == SurnameFilter);
                }
            }

            var totalCount = filteredCBiodatas.Count();
            var filtCBiodatas = filteredCBiodatas
                .OrderByDescending(a => a.Id)
                .Skip(first)
                .Take(rows)
                .ToList();

            var cBiodatas = from o in filtCBiodatas
                            select new GetCBiodataForViewDto()
                            {
                                Biodata = new CBioDataForViewDto
                                {
                                    ApplicationId = ApplicationId,
                                    SA_Id_Number = o.SA_Id_Number,
                                    Passport_Number = o.Passport_Number,
                                    Firstname = o.Firstname,
                                    Middlename = o.Middlename,
                                    Surname = o.Surname,
                                    Birth_Year = o.Birth_Year,
                                    Gender = o.Gender,
                                    Race = o.Race,
                                    Disability = o.Disability,
                                    Nationality = o.Nationality,
                                    Province = o.Province,
                                    Municipality = o.Municipality,
                                    Highest_Qualification_Type = o.Highest_Qualification_Type,
                                    Employment_Status = o.Employment_Status,
                                    Occupation_Level_For_Equity_Reporting = o.Occupation_Level_For_Equity_Reporting,
                                    Organisational_Structure_Filter = o.Organisational_Structure_Filter,
                                    Post_Reference = o.Post_Reference,
                                    Job_Title = o.Job_Title,
                                    OFO_Occupation_Code = o.OFO_Occupation_Code,
                                    OFO_Specialisation = o.OFO_Specialisation,
                                    OFO_Occupation = o.OFO_Occupation,
                                    UserId = o.UserId,
                                    DateCreated = o.DateCreated,
                                    Status = o.Status,
                                    UsrUpd = o.UsrUpd,
                                    DteUpd = o.DteUpd,
                                    Id = o.Id
                                }
                            };

            return new PagedResultDto<GetCBiodataForViewDto>(
                totalCount,
                cBiodatas.ToList()
            );
        }

        public async Task<PagedResultDto<GetTrainingForViewDto>> getTraindataLazy(int first, int rows, int ApplicationId, string SAIdNoFilter, string SAIdNoFilterMode,
        string PPNoFilter, string PPNoFilterMode, string QualFilter, string QualFilterMode, string DetFilter, string DetFilterMode, string StatFilter, string StatFilterMode, string YearFilter, string YearFilterMode)
        {

            var filteredCTrainings = _trainRepository.GetAll()
            .Where(a => a.ApplicationId == ApplicationId);

            if (SAIdNoFilter != null)
            {
                if (SAIdNoFilterMode == "startsWith")
                {
                    filteredCTrainings = filteredCTrainings.Where(a => a.SA_Id_Number.StartsWith(SAIdNoFilter));
                }
                if (SAIdNoFilterMode == "endsWith")
                {
                    filteredCTrainings = filteredCTrainings.Where(a => a.SA_Id_Number.EndsWith(SAIdNoFilter));
                }
                if (SAIdNoFilterMode == "contains")
                {
                    filteredCTrainings = filteredCTrainings.Where(a => a.SA_Id_Number.Contains(SAIdNoFilter));
                }
                if (SAIdNoFilterMode == "notContains")
                {
                    filteredCTrainings = filteredCTrainings.Where(a => !(a.SA_Id_Number.Contains(SAIdNoFilter)));
                }
                if (SAIdNoFilterMode == "equals")
                {
                    filteredCTrainings = filteredCTrainings.Where(a => a.SA_Id_Number == SAIdNoFilter);
                }
            }

            if (PPNoFilter != null)
            {
                if (PPNoFilterMode == "startsWith")
                {
                    filteredCTrainings = filteredCTrainings.Where(a => a.Passport_Number.StartsWith(PPNoFilter));
                }
                if (PPNoFilterMode == "endsWith")
                {
                    filteredCTrainings = filteredCTrainings.Where(a => a.Passport_Number.EndsWith(PPNoFilter));
                }
                if (PPNoFilterMode == "contains")
                {
                    filteredCTrainings = filteredCTrainings.Where(a => a.Passport_Number.Contains(PPNoFilter));
                }
                if (PPNoFilterMode == "notContains")
                {
                    filteredCTrainings = filteredCTrainings.Where(a => !(a.Passport_Number.Contains(PPNoFilter)));
                }
                if (PPNoFilterMode == "equals")
                {
                    filteredCTrainings = filteredCTrainings.Where(a => a.Passport_Number == PPNoFilter);
                }
            }

            if (QualFilter != null)
            {
                if (QualFilterMode == "startsWith")
                {
                    filteredCTrainings = filteredCTrainings.Where(a => a.Qualification_Learning_Program_Type.StartsWith(QualFilter));
                }
                if (QualFilterMode == "endsWith")
                {
                    filteredCTrainings = filteredCTrainings.Where(a => a.Qualification_Learning_Program_Type.EndsWith(QualFilter));
                }
                if (QualFilterMode == "contains")
                {
                    filteredCTrainings = filteredCTrainings.Where(a => a.Qualification_Learning_Program_Type.Contains(QualFilter));
                }
                if (QualFilterMode == "notContains")
                {
                    filteredCTrainings = filteredCTrainings.Where(a => !(a.Qualification_Learning_Program_Type.Contains(QualFilter)));
                }
                if (QualFilterMode == "equals")
                {
                    filteredCTrainings = filteredCTrainings.Where(a => a.Qualification_Learning_Program_Type == QualFilter);
                }
            }

            if (DetFilter != null)
            {
                if (DetFilterMode == "startsWith")
                {
                    filteredCTrainings = filteredCTrainings.Where(a => a.Details_Of_Learning_Program.StartsWith(DetFilter));
                }
                if (DetFilterMode == "endsWith")
                {
                    filteredCTrainings = filteredCTrainings.Where(a => a.Details_Of_Learning_Program.EndsWith(DetFilter));
                }
                if (DetFilterMode == "contains")
                {
                    filteredCTrainings = filteredCTrainings.Where(a => a.Details_Of_Learning_Program.Contains(DetFilter));
                }
                if (DetFilterMode == "notContains")
                {
                    filteredCTrainings = filteredCTrainings.Where(a => !(a.Details_Of_Learning_Program.Contains(DetFilter)));
                }
                if (DetFilterMode == "equals")
                {
                    filteredCTrainings = filteredCTrainings.Where(a => a.Details_Of_Learning_Program == DetFilter);
                }
            }

            if (StatFilter != null)
            {
                if (StatFilterMode == "startsWith")
                {
                    filteredCTrainings = filteredCTrainings.Where(a => a.Achievement_status.StartsWith(StatFilter));
                }
                if (StatFilterMode == "endsWith")
                {
                    filteredCTrainings = filteredCTrainings.Where(a => a.Achievement_status.EndsWith(StatFilter));
                }
                if (StatFilterMode == "contains")
                {
                    filteredCTrainings = filteredCTrainings.Where(a => a.Achievement_status.Contains(StatFilter));
                }
                if (StatFilterMode == "notContains")
                {
                    filteredCTrainings = filteredCTrainings.Where(a => !(a.Achievement_status.Contains(StatFilter)));
                }
                if (StatFilterMode == "equals")
                {
                    filteredCTrainings = filteredCTrainings.Where(a => a.Achievement_status == StatFilter);
                }
            }

            if (YearFilter != null)
            {
                if (YearFilterMode == "startsWith")
                {
                    filteredCTrainings = filteredCTrainings.Where(a => a.Year_enrolled_or_completed.ToString().StartsWith(YearFilter.ToString()));
                }
                if (YearFilterMode == "endsWith")
                {
                    filteredCTrainings = filteredCTrainings.Where(a => a.Year_enrolled_or_completed.ToString().EndsWith(YearFilter.ToString()));
                }
                if (YearFilterMode == "contains")
                {
                    filteredCTrainings = filteredCTrainings.Where(a => a.Year_enrolled_or_completed.ToString().Contains(YearFilter.ToString()));
                }
                if (YearFilterMode == "notContains")
                {
                    filteredCTrainings = filteredCTrainings.Where(a => !(a.Year_enrolled_or_completed.ToString().Contains(YearFilter)));
                }
                if (YearFilterMode == "equals")
                {
                    filteredCTrainings = filteredCTrainings.Where(a => a.Year_enrolled_or_completed.ToString() == YearFilter);
                }
            }

            var totalCount = filteredCTrainings.Count();
            var filtCTrainings = filteredCTrainings
                .OrderByDescending(a => a.Id)
                .Skip(first)
                .Take(rows)
                .ToList();

            var cTrainings = from o in filtCTrainings
                             select new GetTrainingForViewDto()
                            {
                                Training = new TrainingForViewDto()
                                {
                                    ApplicationId = o.ApplicationId,
                                    SA_Id_Number = o.SA_Id_Number,
                                    Passport_Number = o.Passport_Number,
                                    Qualification_Learning_Program_Type = o.Qualification_Learning_Program_Type.Replace('_', ' '),
                                    Details_Of_Learning_Program = o.Details_Of_Learning_Program,
                                    Study_Field_Or_Specialisation_Specification = o.Study_Field_Or_Specialisation_Specification,
                                    Total_Training_Cost = o.Total_Training_Cost,
                                    Achievement_status = o.Achievement_status,
                                    Year_enrolled_or_completed = o.Year_enrolled_or_completed,
                                    UserId = o.UserId,
                                    BiodataId = o.BiodataId,
                                    Id = o.Id
                                }
                            };

            return new PagedResultDto<GetTrainingForViewDto>(
                totalCount,
                cTrainings.ToList()
            );
        }

        public async Task<PagedResultDto<MandatoryApplicationsView>> getMandatoryGrantApplicationsLazy(int first, int rows, string SDLNoFilter, string SDLNoFilterMode, string OrganisationNameFilter, string OrganisationNameFilterMode, string ReferenceFilter, string ReferenceFilterMode, string GrantStatusFilter, string GrantStatusFilterMode, string ProvinceFilter, string ProvinceFilterMode, string RSAFilter, string RSAFilterMode)
        {
            var filteredApps = (from mg in _mandatoryApplication.GetAll()
                                join wnd in _mandatoryWindow.GetAll() on mg.GrantWindowId equals wnd.Id
                                join org in _orgRepository.GetAll() on mg.OrganisationId equals org.Id
                                join stat in _mandStatusRepository.GetAll() on mg.GrantStatusID equals stat.Id
                                join pha in _addressRepository.GetAll() on org.Id equals pha.organisationId into pa
                                from pas in pa.DefaultIfEmpty()
                                join rsa in _rsaRepository.GetAll() on mg.RSAId equals rsa.UserID into r
                                from rsas in r.DefaultIfEmpty()
                                join rg in _regRepository.GetAll() on rsas.RegionID equals rg.Id into rgn
                                from rgs in rgn.DefaultIfEmpty()
                                select new
                                {
                                    OrganisationId = org.Id,
                                    OrganisationSDL = org.SDL_No,
                                    Organisation_Name = org.Organisation_Name,
                                    Organisation_Trading_Name = org.Organisation_Trading_Name,
                                    ReferenceNo = wnd.ReferenceNo,
                                    Grants = mg,
                                    GrantStatus = stat.StatusDesc,
                                    Window = wnd,
                                    Description = wnd.Description,
                                    Province = pas.province,
                                    RSA = rsas.RSA_Name,
                                    Id = mg.Id
                                }).Distinct();

            if (SDLNoFilter != null)
            {
                if (SDLNoFilterMode == "startsWith")
                {
                    filteredApps = filteredApps.Where(a => a.OrganisationSDL.StartsWith(SDLNoFilter));
                }
                if (SDLNoFilterMode == "endsWith")
                {
                    filteredApps = filteredApps.Where(a => a.OrganisationSDL.EndsWith(SDLNoFilter));
                }
                if (SDLNoFilterMode == "contains")
                {
                    filteredApps = filteredApps.Where(a => a.OrganisationSDL.Contains(SDLNoFilter));
                }
                if (SDLNoFilterMode == "notContains")
                {
                    filteredApps = filteredApps.Where(a => !(a.OrganisationSDL.Contains(SDLNoFilter)));
                }
                if (SDLNoFilterMode == "equals")
                {
                    filteredApps = filteredApps.Where(a => a.OrganisationSDL == SDLNoFilter);
                }
            }

            if (OrganisationNameFilter != null)
            {
                if (OrganisationNameFilterMode == "startsWith")
                {
                    filteredApps = filteredApps.Where(a => a.Organisation_Name.StartsWith(OrganisationNameFilter));
                }
                if (OrganisationNameFilterMode == "endsWith")
                {
                    filteredApps = filteredApps.Where(a => a.Organisation_Name.EndsWith(OrganisationNameFilter));
                }
                if (OrganisationNameFilterMode == "contains")
                {
                    filteredApps = filteredApps.Where(a => a.Organisation_Name.Contains(OrganisationNameFilter));
                }
                if (OrganisationNameFilterMode == "notContains")
                {
                    filteredApps = filteredApps.Where(a => !(a.Organisation_Name.Contains(OrganisationNameFilter)));
                }
                if (OrganisationNameFilterMode == "equals")
                {
                    filteredApps = filteredApps.Where(a => a.Organisation_Name == OrganisationNameFilter);
                }
            }

            if (ReferenceFilter != null)
            {
                if (ReferenceFilterMode == "startsWith")
                {
                    filteredApps = filteredApps.Where(a => a.ReferenceNo.StartsWith(ReferenceFilter));
                }
                if (ReferenceFilterMode == "endsWith")
                {
                    filteredApps = filteredApps.Where(a => a.ReferenceNo.EndsWith(ReferenceFilter));
                }
                if (ReferenceFilterMode == "contains")
                {
                    filteredApps = filteredApps.Where(a => a.ReferenceNo.Contains(ReferenceFilter));
                }
                if (ReferenceFilterMode == "notContains")
                {
                    filteredApps = filteredApps.Where(a => !(a.ReferenceNo.Contains(ReferenceFilter)));
                }
                if (ReferenceFilterMode == "equals")
                {
                    filteredApps = filteredApps.Where(a => a.ReferenceNo == ReferenceFilter);
                }
            }

            if (GrantStatusFilter != null)
            {
                if (GrantStatusFilterMode == "startsWith")
                {
                    filteredApps = filteredApps.Where(a => a.GrantStatus.StartsWith(GrantStatusFilter));
                }
                if (GrantStatusFilterMode == "endsWith")
                {
                    filteredApps = filteredApps.Where(a => a.GrantStatus.EndsWith(GrantStatusFilter));
                }
                if (GrantStatusFilterMode == "contains")
                {
                    filteredApps = filteredApps.Where(a => a.GrantStatus.Contains(GrantStatusFilter));
                }
                if (GrantStatusFilterMode == "notContains")
                {
                    filteredApps = filteredApps.Where(a => !(a.GrantStatus.Contains(GrantStatusFilter)));
                }
                if (GrantStatusFilterMode == "equals")
                {
                    filteredApps = filteredApps.Where(a => a.GrantStatus == GrantStatusFilter);
                }
            }

            if (ProvinceFilter != null)
            {
                if (ProvinceFilterMode == "startsWith")
                {
                    filteredApps = filteredApps.Where(a => a.Province.StartsWith(ProvinceFilter));
                }
                if (ProvinceFilterMode == "endsWith")
                {
                    filteredApps = filteredApps.Where(a => a.Province.EndsWith(ProvinceFilter));
                }
                if (ProvinceFilterMode == "contains")
                {
                    filteredApps = filteredApps.Where(a => a.Province.Contains(ProvinceFilter));
                }
                if (ProvinceFilterMode == "notContains")
                {
                    filteredApps = filteredApps.Where(a => !(a.Province.Contains(ProvinceFilter)));
                }
                if (ProvinceFilterMode == "equals")
                {
                    filteredApps = filteredApps.Where(a => a.Province == ProvinceFilter);
                }
            }

            if (RSAFilter != null)
            {
                if (RSAFilterMode == "startsWith")
                {
                    filteredApps = filteredApps.Where(a => a.RSA.StartsWith(RSAFilter));
                }
                if (RSAFilterMode == "endsWith")
                {
                    filteredApps = filteredApps.Where(a => a.RSA.EndsWith(RSAFilter));
                }
                if (RSAFilterMode == "contains")
                {
                    filteredApps = filteredApps.Where(a => a.RSA.Contains(RSAFilter));
                }
                if (RSAFilterMode == "notContains")
                {
                    filteredApps = filteredApps.Where(a => !(a.RSA.Contains(RSAFilter)));
                }
                if (RSAFilterMode == "equals")
                {
                    filteredApps = filteredApps.Where(a => a.RSA == RSAFilter);
                }
            }

            var filteredCApps = filteredApps.ToList();

            var totalCount = filteredCApps.Count();
            var filtCApps = filteredCApps
                .OrderByDescending(a => a.Id)
                .Skip(first)
                .Take(rows);


            var cApps = from o in filtCApps
                        select new MandatoryApplicationsView()
                        {
                            MandatoryApplication = new MandatoryApplicationViewDto()
                            {
                                GrantWindowId = o.Grants.GrantWindowId,
                                OrganisationId = o.OrganisationId,
                                OrganisationSDL = o.OrganisationSDL,
                                Organisation_Name = o.Organisation_Name,
                                Organisation_Trading_Name = o.Organisation_Trading_Name,
                                GrantStatus = o.GrantStatus,
                                ReferenceNo = o.Window.ReferenceNo,
                                Description = o.Window.Description,
                                Province = o.Province,
                                RSA = o.RSA,
                                UserId = o.Grants.UserId,
                                CaptureDte = o.Grants.CaptureDte,
                                SubmissionDte = o.Grants.SubmissionDte,
                                ClosingDate = o.Window.EndDate,
                                Id = o.Grants.Id
                            }
                        };

            return new PagedResultDto<MandatoryApplicationsView>(
                totalCount,
                cApps.ToList()
            );
        }

        public async Task<PagedResultDto<MandatoryGrantExtensionsView>> GetAllExtensions(int first, int rows, int userId, string group)
        {
            var app = (from mg in _mandatoryApplication.GetAll()
                       join wnd in _mandatoryWindow.GetAll() on mg.GrantWindowId equals wnd.Id
                       join org in _orgRepository.GetAll() on mg.OrganisationId equals org.Id
                       join stat in _mandStatusRepository.GetAll() on mg.GrantStatusID equals stat.Id
                       join ext in _extRepository.GetAll() on mg.Id equals ext.ApplicationId
                       join pha in _addressRepository.GetAll() on org.Id equals pha.organisationId into pa
                       from pas in pa.DefaultIfEmpty()
                       join rsa in _rsaRepository.GetAll() on mg.RSAId equals rsa.UserID into r
                       from rsas in r.DefaultIfEmpty()
                       join rg in _regRepository.GetAll() on rsas.RegionID equals rg.Id into rgn
                       from rgs in rgn.DefaultIfEmpty()
                       select new
                       {
                           OrganisationId = org.Id,
                           OrganisationSDL = org.SDL_No,
                           Organisation_Name = org.Organisation_Name,
                           Organisation_Trading_Name = org.Organisation_Trading_Name,
                           GrantStatus = stat.StatusDesc,
                           DateRequested = ext.DateRequested,
                           Reason = ext.ReasonForRequest,
                           ClosingDate = wnd.ExtensionDate,
                           SubmissionDte = mg.SubmissionDte,
                           ReferenceNo = wnd.ReferenceNo,
                           Description = wnd.Description,
                           Province = pas.province,
                           Region = rgs.RegionName,
                           RSA = rsas.RSA_Name,
                           Id = ext.Id
                       }).Distinct();

            var totalCount = app.Count();

            var wind = (from o in app
                        select new MandatoryGrantExtensionsView
                        {
                            Extensions = new MandatoryExtensionsViewDto
                            {
                                OrganisationSDL = o.OrganisationSDL,
                                Organisation_Name = o.Organisation_Name,
                                Organisation_Trading_Name = o.Organisation_Trading_Name,
                                Province = o.Province,
                                Region = o.Region,
                                RSA = o.RSA,
                                GrantStatus = o.GrantStatus,
                                DateRequested = o.DateRequested,
                                SubmissionDte = o.SubmissionDte,
                                Reason = o.Reason,
                                ClosingDate = o.ClosingDate,
                                ReferenceNo = o.ReferenceNo,
                                Description = o.Description,
                                Id = o.Id
                            }
                        })
                        .OrderByDescending(a => a.Extensions.Id)
                        .Skip(first)
                        .Take(rows)
                        .ToList();

            return new PagedResultDto<MandatoryGrantExtensionsView>(
                totalCount,
                wind.ToList()
            );
        }

        public async Task<PagedResultDto<MandatoryGrantExtensionsView>> getMandatoryExtensionsLazy(int first, int rows, string SDLNoFilter, string SDLNoFilterMode, string OrganisationNameFilter, string OrganisationNameFilterMode, string ReferenceFilter, string ReferenceFilterMode, string GrantStatusFilter, string GrantStatusFilterMode, string ProvinceFilter, string ProvinceFilterMode, string RSAFilter, string RSAFilterMode)
        {

            var filteredApps = (from mg in _mandatoryApplication.GetAll()
                                join wnd in _mandatoryWindow.GetAll() on mg.GrantWindowId equals wnd.Id
                                join org in _orgRepository.GetAll() on mg.OrganisationId equals org.Id
                                join stat in _mandStatusRepository.GetAll() on mg.GrantStatusID equals stat.Id
                                join ext in _extRepository.GetAll() on mg.Id equals ext.ApplicationId
                                join pha in _addressRepository.GetAll() on org.Id equals pha.organisationId into pa
                                from pas in pa.DefaultIfEmpty()
                                join rsa in _rsaRepository.GetAll() on mg.RSAId equals rsa.UserID into r
                                from rsas in r.DefaultIfEmpty()
                                join rg in _regRepository.GetAll() on rsas.RegionID equals rg.Id into rgn
                                from rgs in rgn.DefaultIfEmpty()
                                select new
                                {
                                    OrganisationId = org.Id,
                                    OrganisationSDL = org.SDL_No,
                                    Organisation_Name = org.Organisation_Name,
                                    Organisation_Trading_Name = org.Organisation_Trading_Name,
                                    GrantStatus = stat.StatusDesc,
                                    DateRequested = ext.DateRequested,
                                    Reason = ext.ReasonForRequest,
                                    ClosingDate = wnd.ExtensionDate,
                                    SubmissionDte = mg.SubmissionDte,
                                    ReferenceNo = wnd.ReferenceNo,
                                    Description = wnd.Description,
                                    Province = pas.province,
                                    Region = rgs.RegionName,
                                    RSA = rsas.RSA_Name,
                                    Id = ext.Id
                                }).Distinct();

            if (SDLNoFilter != null)
            {
                if (SDLNoFilterMode == "startsWith")
                {
                    filteredApps = filteredApps.Where(a => a.OrganisationSDL.StartsWith(SDLNoFilter));
                }
                if (SDLNoFilterMode == "endsWith")
                {
                    filteredApps = filteredApps.Where(a => a.OrganisationSDL.EndsWith(SDLNoFilter));
                }
                if (SDLNoFilterMode == "contains")
                {
                    filteredApps = filteredApps.Where(a => a.OrganisationSDL.Contains(SDLNoFilter));
                }
                if (SDLNoFilterMode == "notContains")
                {
                    filteredApps = filteredApps.Where(a => !(a.OrganisationSDL.Contains(SDLNoFilter)));
                }
                if (SDLNoFilterMode == "equals")
                {
                    filteredApps = filteredApps.Where(a => a.OrganisationSDL == SDLNoFilter);
                }
            }

            if (OrganisationNameFilter != null)
            {
                if (OrganisationNameFilterMode == "startsWith")
                {
                    filteredApps = filteredApps.Where(a => a.Organisation_Name.StartsWith(OrganisationNameFilter));
                }
                if (OrganisationNameFilterMode == "endsWith")
                {
                    filteredApps = filteredApps.Where(a => a.Organisation_Name.EndsWith(OrganisationNameFilter));
                }
                if (OrganisationNameFilterMode == "contains")
                {
                    filteredApps = filteredApps.Where(a => a.Organisation_Name.Contains(OrganisationNameFilter));
                }
                if (OrganisationNameFilterMode == "notContains")
                {
                    filteredApps = filteredApps.Where(a => !(a.Organisation_Name.Contains(OrganisationNameFilter)));
                }
                if (OrganisationNameFilterMode == "equals")
                {
                    filteredApps = filteredApps.Where(a => a.Organisation_Name == OrganisationNameFilter);
                }
            }

            if (ReferenceFilter != null)
            {
                if (ReferenceFilterMode == "startsWith")
                {
                    filteredApps = filteredApps.Where(a => a.ReferenceNo.StartsWith(ReferenceFilter));
                }
                if (ReferenceFilterMode == "endsWith")
                {
                    filteredApps = filteredApps.Where(a => a.ReferenceNo.EndsWith(ReferenceFilter));
                }
                if (ReferenceFilterMode == "contains")
                {
                    filteredApps = filteredApps.Where(a => a.ReferenceNo.Contains(ReferenceFilter));
                }
                if (ReferenceFilterMode == "notContains")
                {
                    filteredApps = filteredApps.Where(a => !(a.ReferenceNo.Contains(ReferenceFilter)));
                }
                if (ReferenceFilterMode == "equals")
                {
                    filteredApps = filteredApps.Where(a => a.ReferenceNo == ReferenceFilter);
                }
            }

            if (GrantStatusFilter != null)
            {
                if (GrantStatusFilterMode == "startsWith")
                {
                    filteredApps = filteredApps.Where(a => a.GrantStatus.StartsWith(GrantStatusFilter));
                }
                if (GrantStatusFilterMode == "endsWith")
                {
                    filteredApps = filteredApps.Where(a => a.GrantStatus.EndsWith(GrantStatusFilter));
                }
                if (GrantStatusFilterMode == "contains")
                {
                    filteredApps = filteredApps.Where(a => a.GrantStatus.Contains(GrantStatusFilter));
                }
                if (GrantStatusFilterMode == "notContains")
                {
                    filteredApps = filteredApps.Where(a => !(a.GrantStatus.Contains(GrantStatusFilter)));
                }
                if (GrantStatusFilterMode == "equals")
                {
                    filteredApps = filteredApps.Where(a => a.GrantStatus == GrantStatusFilter);
                }
            }

            if (ProvinceFilter != null)
            {
                if (ProvinceFilterMode == "startsWith")
                {
                    filteredApps = filteredApps.Where(a => a.Province.StartsWith(ProvinceFilter));
                }
                if (ProvinceFilterMode == "endsWith")
                {
                    filteredApps = filteredApps.Where(a => a.Province.EndsWith(ProvinceFilter));
                }
                if (ProvinceFilterMode == "contains")
                {
                    filteredApps = filteredApps.Where(a => a.Province.Contains(ProvinceFilter));
                }
                if (ProvinceFilterMode == "notContains")
                {
                    filteredApps = filteredApps.Where(a => !(a.Province.Contains(ProvinceFilter)));
                }
                if (ProvinceFilterMode == "equals")
                {
                    filteredApps = filteredApps.Where(a => a.Province == ProvinceFilter);
                }
            }

            if (RSAFilter != null)
            {
                if (RSAFilterMode == "startsWith")
                {
                    filteredApps = filteredApps.Where(a => a.RSA.StartsWith(RSAFilter));
                }
                if (RSAFilterMode == "endsWith")
                {
                    filteredApps = filteredApps.Where(a => a.RSA.EndsWith(RSAFilter));
                }
                if (RSAFilterMode == "contains")
                {
                    filteredApps = filteredApps.Where(a => a.RSA.Contains(RSAFilter));
                }
                if (RSAFilterMode == "notContains")
                {
                    filteredApps = filteredApps.Where(a => !(a.RSA.Contains(RSAFilter)));
                }
                if (RSAFilterMode == "equals")
                {
                    filteredApps = filteredApps.Where(a => a.RSA == RSAFilter);
                }
            }

            var filteredCApps = filteredApps.ToList();

            var totalCount = filteredCApps.Count();
            var filtCApps = filteredCApps
                .OrderByDescending(a => a.Id)
                .Skip(first)
                .Take(rows);


            var cApps = from o in filtCApps
                        select new MandatoryGrantExtensionsView()
                        {
                            Extensions = new MandatoryExtensionsViewDto()
                            {
                                OrganisationSDL = o.OrganisationSDL,
                                Organisation_Name = o.Organisation_Name,
                                Organisation_Trading_Name = o.Organisation_Trading_Name,
                                Province = o.Province,
                                Region = o.Region,
                                RSA = o.RSA,
                                GrantStatus = o.GrantStatus,
                                DateRequested = o.DateRequested,
                                SubmissionDte = o.SubmissionDte,
                                Reason = o.Reason,
                                ClosingDate = o.ClosingDate,
                                ReferenceNo = o.ReferenceNo,
                                Description = o.Description,
                                Id = o.Id
                            }
                        };

            return new PagedResultDto<MandatoryGrantExtensionsView>(
                totalCount,
                cApps.ToList()
            );
        }

        //public async Task<PagedResultDto<MGStatementView>> GetMGStatement(int first, int rows, int userId, string group)
        //{
        //    var app = (from mg in _mandatoryApplication.GetAll()
        //               join wnd in _mandatoryWindow.GetAll() on mg.GrantWindowId equals wnd.Id
        //               join org in _orgRepository.GetAll() on mg.OrganisationId equals org.Id
                       
                       
        //               select new
        //               {
        //                   OrganisationId = org.Id,
        //                   OrganisationSDL = org.SDL_No,
        //                   Organisation_Name = org.Organisation_Name,
        //                   Organisation_Trading_Name = org.Organisation_Trading_Name,
        //                   //GrantStatus = stat.StatusDesc,
        //                   //DateRequested = ext.DateRequested,
        //                   //Reason = ext.ReasonForRequest,
        //                   ClosingDate = wnd.ExtensionDate,
        //                   SubmissionDte = mg.SubmissionDte,
        //                   ReferenceNo = wnd.ReferenceNo,
        //                   Description = wnd.Description,
        //                   //Province = pas.province,
        //                   //Region = rgs.RegionName,
        //                   //RSA = rsas.RSA_Name,
        //                   //Id = ext.Id
        //               }).Distinct();

        //    var totalCount = app.Count();

        //    var wind = (from o in app
        //                select new MGStatementView
        //                {
        //                    MGStatement = new MGStatementDto
        //                    {
        //                        Year = o.Year,
        //                        Month = o.Month,
        //                        Batch_No = o.Batch_No,
        //                        Levies_Paid = o.Levies_Paid,
        //                        Mg_Grant = o.Mg_Grant,
        //                        Mg_Grant_Paid = o.Mg_Grant_Paid,
        //                        Id = o.Id
        //                    }
        //                })
        //                .OrderByDescending(a => a.Extensions.Id)
        //                .Skip(first)
        //                .Take(rows)
        //                .ToList();

        //    return new PagedResultDto<MandatoryGrantExtensionsView>(
        //        totalCount,
        //        wind.ToList()
        //    );
        //}

        protected int FinYrFromDte(DateTime Dte)
        {
            int FirstMonth;
            int FinYrReturn;

            FirstMonth = 4;

            if (Dte.Month < FirstMonth)
            {
                FinYrReturn = Dte.Year - 1;
            } else {
                FinYrReturn = Dte.Year;
            }

            return FinYrReturn;

        }

        public async Task MGNotYetSubmittedEmail(int WindowId)
        {
            var nys = await (from a in _mandatoryApplication.GetAll()
                join st in _mandStatusRepository.GetAll() on a.GrantStatusID equals st.Id
                join o in _orgRepository.GetAll() on a.OrganisationId equals o.Id
                join osdf in _orgSdfRepository.GetAll() on a.OrganisationId equals osdf.OrganisationId
                join sdf in _sdfRepository.GetAll() on osdf.SdfId equals sdf.Id
                join p in _personRepository.GetAll() on sdf.personId equals p.Id
                select new
                {
                    GrantWindowId = a.GrantWindowId,
                    GrantStatus = st.StatusDesc,
                    email = p.Email,
                    SDL_Number = o.SDL_No,
                    Organisation_Name = o.Organisation_Name
                })
            .Where(a => a.GrantWindowId == WindowId && a.GrantStatus == "Application")
            .ToListAsync();

            foreach (var d in nys.Distinct())
            {
                await _userEmailer.MGSendNotYetSubmittedAsync(d.email, d.SDL_Number, d.Organisation_Name);
            }

            await _userEmailer.MGSendNotYetSubmittedAsync("smlotshwa@chieta.org.za", "N/A", "N/A");
        }
    }
}
