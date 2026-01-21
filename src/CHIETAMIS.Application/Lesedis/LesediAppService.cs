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
using CHIETAMIS.Lesedis;
using CHIETAMIS.Lesedis.Dtos;
using CHIETAMIS.DiscretionaryWindows;
using CHIETAMIS.DiscretionaryWindows.Dtos;
using Castle.DynamicProxy.Internal;
using CHIETAMIS.Organisations.Dtos;
using CHIETAMIS.DiscretionaryTranches;
using MimeKit.Cryptography;
using CHIETAMIS.Qualifications.Dtos;
using CHIETAMIS.GrantApprovals.Dtos;
using CHIETAMIS.GrantApprovals;

namespace CHIETAMIS.MandatoryGrants
{
    public class LesediAppService : CHIETAMISAppServiceBase
    {
        private readonly IUserEmailer _userEmailer;

        private readonly IRepository<Lesedi> _lesedi;
        private readonly IRepository<DiscretionaryWindow> _win;
        private readonly IRepository<LesediDetails> _lesdet;
        private readonly IRepository<LesediStatus> _lesdstat;
        private readonly IRepository<LesediAddress> _lesaddr;
        private readonly IRepository<BursaryApplications> _bapp;
        private readonly IRepository<Lesedi_Qualification> _qual;
        private readonly IRepository<Document> _doc;
        private readonly IRepository<BursaryApprovals> _appr;
        private readonly IRepository<GrantApprovalType> _apprType;
        private readonly IRepository<BursaryDocumentApprovals> _docApproval;
        private readonly IRepository<GrantApprovalStatus> _grantApprStat;
        private readonly IRepository<GrantApprovalType> _grantApprType;

        public LesediAppService(IRepository<Lesedi> lesedi,
                                IRepository<DiscretionaryWindow> win,
                                IRepository<LesediDetails> lesdet,
                                IRepository<LesediStatus> lesdstat,
                                IRepository<LesediAddress> lesaddr,
                                IRepository<BursaryApplications> bapp,
                                IRepository<Lesedi_Qualification> qual,
                                IRepository<Document> doc,
                                IRepository<BursaryApprovals> appr,
                                IRepository<GrantApprovalType> apprType,
                                IRepository<BursaryDocumentApprovals> docApproval,
                                IRepository<GrantApprovalStatus> grantApprStat,
                                IRepository<GrantApprovalType> grantApprType)
        {
            _lesedi = lesedi;
            _win = win;
            _lesdet = lesdet;
            _lesdstat = lesdstat;
            _lesaddr = lesaddr;
            _bapp = bapp;
            _qual = qual;
            _doc = doc;
            _appr = appr;
            _apprType = apprType;
            _docApproval = docApproval;
            _grantApprStat = grantApprStat;
            _grantApprType = grantApprType;
        }

        public async Task<string> CreateEditBursaryApplication(BursaryApplicationsDto input)
        {
            var output = "";

            var app = _bapp.GetAll().Where(a => a.GrantWindowId == input.GrantWindowId && a.UserId == input.UserId);

            if (app.Count() == 0)
            {
                input.DateCreated = DateTime.Now;
                var bap = ObjectMapper.Map<BursaryApplications>(input);

                await _bapp.InsertAsync(bap);
            }
            else
            {
                var bapp = await _bapp.GetAsync(app.FirstOrDefault().Id);
                bapp.DteUpd = DateTime.Now;
                bapp.UsrUpd = input.UsrUpd;

                await _bapp.UpdateAsync(bapp);
            }

            return output;
        }

        public async Task<string> CreateEditLesediDetails(LesediDetailsDto input, int ApplicationId, int GrantWindowId)
        {
            var output = "";

            var exst = (from ld in _lesdet.GetAll()
                        join ap in _bapp.GetAll() on ld.Id equals ap.StudentId
                        select new
                        {
                            Id = ap.Id,
                            WinId = ap.GrantWindowId,
                            ApplicationId = ap.Id,
                            SAIdNumber = ld.SAIdNumber
                        })
                        .Where(a => a.WinId == GrantWindowId && a.SAIdNumber == input.SAIdNumber && a.ApplicationId != ApplicationId);
            if (exst.Count() == 0)
            {
                var app = (from ld in _lesdet.GetAll()
                           join ap in _bapp.GetAll() on ld.Id equals ap.StudentId
                           select new
                           {
                               Details = ld,
                               ApplicationId = ap.Id
                           })
                    .Where(a => a.ApplicationId == ApplicationId && a.Details.Id == input.Id);

                if (app.Count() == 0)
                {
                    input.DateCreated = DateTime.Now;
                    var bap = ObjectMapper.Map<LesediDetails>(input);

                    var lsdins = await _lesdet.InsertAsync(bap);
                    await CurrentUnitOfWork.SaveChangesAsync();
                    var lsdId = lsdins.Id;

                    var ap = _bapp.Get(ApplicationId);
                    ap.StudentId = lsdId;
                    await _bapp.UpdateAsync(ap);
                }
                else
                {

                    var det = await _lesdet.GetAsync(app.FirstOrDefault().Details.Id);
                    det.DteUpd = DateTime.Now;
                    det.UpdUsr = input.UpdUsr;

                    det.Firstname = input.Firstname;
                    det.Lastname = input.Lastname;
                    det.Race = input.Race;
                    det.Gender = input.Gender;
                    det.Contactnumber = input.Contactnumber;
                    det.Cellphone = input.Cellphone;
                    det.Contactnumber = input.Contactnumber;
                    det.District = input.District;
                    det.Province = input.Province;
                    det.Municipality = input.Municipality;
                    det.Email = input.Email;
                    det.SAIdNumber = input.SAIdNumber;
                    det.Middlename = input.Middlename;

                    await _lesdet.UpdateAsync(det);
                }
            } else
            {
                output = "Duplicate ID found in another Application.";
            }

            return output;
        }

        public async Task<string> CreateEditLesedi(LesediDto input, int ApplicationId)
        {
            var output = "";

            var app = (from ld in _lesedi.GetAll()
                       join ap in _bapp.GetAll() on ld.Id equals ap.LesediId
                       select new
                       {
                           Details = ld,
                           ApplicationId = ap.Id
                       })
                .Where(a => a.ApplicationId == ApplicationId && a.Details.Id == input.Id);

            if (app.Count() == 0)
            {
                input.DateCreated = DateTime.Now;
                if (input.ConsentYN == "Yes")
                {
                    input.ConsentDate = DateTime.Now;
                }

                var bap = ObjectMapper.Map<Lesedi>(input);

                var lsdins = await _lesedi.InsertAsync(bap);
                await CurrentUnitOfWork.SaveChangesAsync();
                var lsdId = lsdins.Id;

                var ap = _bapp.Get(ApplicationId);
                ap.LesediId = lsdId;
                await _bapp.UpdateAsync(ap);
            }
            else
            {
                var det = await _lesedi.GetAsync(app.FirstOrDefault().Details.Id);
                det.DteUpd = DateTime.Now;
                det.UsrUpd = input.UsrUpd;

                if (input.ConsentYN == "Yes" && det.ConsentDate == null)
                {
                    det.ConsentDate = DateTime.Now;
                }
                det.UnderPostGraduate = input.UnderPostGraduate;
                det.Balance = input.Balance;
                det.ConsentYN = input.ConsentYN;
                //det.ConsentDate = input.ConsentDate;
                det.CurrentHist = input.CurrentHist;
                det.CurrentlyStudying = input.CurrentlyStudying;
                det.CurrentHist = input.CurrentHist;
                det.NSFASBeneficiary = input.NSFASBeneficiary;
                det.OtherQualification = input.OtherQualification;
                det.PassRate = input.PassRate;
                det.Qualification = input.Qualification;
                det.StudyYear = input.StudyYear;
                det.UniversityCollege = input.UniversityCollege;

                await _lesedi.UpdateAsync(det);
            }

            return output;
        }

        public async Task<BursaryApplicationsDto> GetApplicationId(int ApplicationId)
        {

            var app = _bapp.GetAll()
                .Where(x => x.Id == ApplicationId).SingleOrDefault();

            var output = ObjectMapper.Map<BursaryApplicationsDto>(app);
            
            return output;
        }

        public async Task<LesediDetailsDto> GetLesediDetailsId(int Id)
        {

            var app = _lesdet.GetAll()
                .Where(x => x.Id == Id).SingleOrDefault();

            var output = ObjectMapper.Map<LesediDetailsDto>(app);

            return output;
        }

        public async Task<LesediDto> GetLesediId(int Id)
        {

            var app = _lesedi.GetAll()
                .Where(x => x.Id == Id).SingleOrDefault();

            var output = ObjectMapper.Map<LesediDto>(app);

            return output;
        }

        public async Task<string> CreateEditLesediAddress(LesediAddressDto input, int ApplicationId)
        {
            var output = "";

            var adr = (from ad in _lesaddr.GetAll()
                       join ap in _bapp.GetAll() on ad.Id equals ap.AddressId
                       select new
                       {
                           Details = ad,
                           ApplicationId = ap.Id
                       })
                .Where(a => a.ApplicationId == ApplicationId && a.Details.Id == input.Id);

            if (adr.Count() == 0)
            {
                input.datecreated = DateTime.Now;
                var bap = ObjectMapper.Map<LesediAddress>(input);

                var lsdins = await _lesaddr.InsertAsync(bap);
                await CurrentUnitOfWork.SaveChangesAsync();
                var lsdId = lsdins.Id;

                var ap = _bapp.Get(ApplicationId);
                ap.AddressId = lsdId;
                await _bapp.UpdateAsync(ap);
            }
            else
            {
                input.DteUpd = DateTime.Now;
                var cAddress = await _lesaddr.FirstOrDefaultAsync(adr.First().Details.Id);
                cAddress.addressline1 = input.addressline1;
                cAddress.addressline2 = input.addressline2;
                cAddress.area = input.area;
                cAddress.district = input.district;
                cAddress.municipality = input.municipality;
                cAddress.postcode = input.postcode;
                cAddress.province = input.province;
                cAddress.suburb = input.suburb;
                await _lesaddr.UpdateAsync(cAddress);
            }

            return output;
        }

        public async Task<LesediAddressDto> GetAddressId(int AddressId)
        {
            var address = _lesaddr.GetAll().Where(a => a.Id == AddressId).FirstOrDefault();

            var output =  ObjectMapper.Map<LesediAddressDto>(address);

            return output;
        }
        public async Task<PagedResultDto<DiscretionaryWindowForViewDto>> GetActiveWindows()
        {
            var window = _win.GetAll().Where(a => a.LaunchDte <= DateTime.Now && a.DeadlineTime >= DateTime.Now && a.Description.Contains("Lesedi"));

            var wind = (from o in window
                        select new DiscretionaryWindowForViewDto
                        {
                            DiscretionaryWindow = new DiscretionaryWindowDto
                            {
                                Reference = o.Reference,
                                Description = o.Description,
                                LaunchDte = o.LaunchDte,
                                DeadlineTime = o.DeadlineTime,
                                ActiveYN = o.ActiveYN,
                                DteCreated = o.DteCreated,
                                Id = o.Id
                            }
                        }).ToList();

            var totalCount = wind.Count();

            return new PagedResultDto<DiscretionaryWindowForViewDto>(
                totalCount,
                wind.ToList()
            );
        }
        public async Task<DiscretionaryWindowDto> GetWindowId(int WindowId)
        {
            var app = _win.GetAll().Where(a => a.Id == WindowId).FirstOrDefault();

            var output = ObjectMapper.Map<DiscretionaryWindowDto>(app);

            return output;
        }

        public async Task<DiscretionaryWindowDto> GetApplicationWindowId(int ApplicationId)
        {
            var app = (from w in _win.GetAll()
                       join a in _bapp.GetAll() on w.Id equals a.GrantWindowId
                       select new
                       {
                           Wind = w,
                           ApplicationId = a.Id
                       })
                       .Where(a => a.ApplicationId == ApplicationId).FirstOrDefault();

            var output = new DiscretionaryWindowDto
            {
                Id = app.Wind.Id,
                Reference = app.Wind.Reference,
                Description = app.Wind.Description,
                LaunchDte = app.Wind.LaunchDte,
                DeadlineTime = app.Wind.DeadlineTime,
                ActiveYN = app.Wind.ActiveYN,
            };

            return output;
        }
        public async Task<PagedResultDto<LesediDetailsViewList>> GetLearnerApplications(int StudentId)
        {
            var app = (from dg in _lesedi.GetAll()
                       join dapp in _bapp.GetAll() on dg.Id equals dapp.LesediId
                       join wnd in _win.GetAll() on dapp.GrantWindowId equals wnd.Id
                       join lrn in _lesdet.GetAll() on dapp.StudentId equals lrn.Id
                       join stat in _lesdstat.GetAll() on dapp.ApplicationStatusId equals stat.Id
                       select new
                       {
                           StudentId = lrn.Id,
                           SAIdNumber = lrn.SAIdNumber,
                           Firstname = lrn.Firstname,
                           Middlename = lrn.Middlename,
                           Grants = dg,
                           Dapp = dapp,
                           GrantStatus = stat.StatusDesc,
                           wind = wnd,
                           GrantWindowId = wnd.Id,
                           Id = dg.Id

                       })
                .Where(a => a.StudentId == StudentId);

            var wind = (from o in app
                        select new LesediDetailsViewList
                        {
                            LesediDetails = new LesediDetailsView                            {
                                GrantWindowId = o.GrantWindowId,
                                SAIdNumber = o.SAIdNumber,
                                Firstname = o.Firstname,
                                Middlename = o.Middlename,
                                GrantStatus = o.GrantStatus,
                                Reference = o.wind.Reference,
                                Description = o.wind.Description,
                                SubmissionDte = o.Dapp.SubmissionDte,
                                DeadlineDate = o.wind.DeadlineTime,
                                Id = o.Grants.Id
                            }
                        }).ToList();

            var totalCount = wind.Count();

            return new PagedResultDto<LesediDetailsViewList>(
                totalCount,
                wind.ToList()
            );
        }
        public async Task<PagedResultDto<BursaryApplicationsViewList>> GetAllApplications(int UserId)
        {
            var app = (from bapp in _bapp.GetAll()
                       join wnd in _win.GetAll() on bapp.GrantWindowId equals wnd.Id
                       join stat in _lesdstat.GetAll() on bapp.ApplicationStatusId equals stat.Id
                       select new
                       {
                           Bapp = bapp,
                           Window = wnd,
                           Description = wnd.Description,
                           ApplicationStatus = stat.StatusDesc,
                           UserId = bapp.UserId,
                       })
                       .Where (a=> a.UserId == UserId);

            var totalCount = app.Count();

            var wind = (from o in app
                        select new BursaryApplicationsViewList
                        {
                            ApplicationsView = new BursaryApplicationssView
                            {
                                ApplicationStatus = o.ApplicationStatus,
                                Title = o.Window.Title,
                                Description = o.Window.Description,
                                SubmissionDte = o.Bapp.SubmissionDte,
                                DeadlineTime = o.Window.DeadlineTime,
                                LauncheDte = o.Window.LaunchDte,
                                DateCreated = o.Bapp.DateCreated,
                                Id = o.Bapp.Id
                            }
                        })
                        .OrderByDescending(a => a.ApplicationsView.Id)
                        .ToList();

            return new PagedResultDto<BursaryApplicationsViewList>(
                totalCount,
                wind.ToList()
            );
        }
        
        public async Task<PagedResultDto<BursaryApplicationsViewList>> GetBursaryApplications(int UserId)
        {
            
           var app = (from p in _bapp.GetAll()
           join ld in _lesdet.GetAll() on p.StudentId equals ld.Id
           join l in _lesedi.GetAll() on p.LesediId equals l.Id
           join w in _win.GetAll() on p.GrantWindowId equals w.Id
           join st in _lesdstat.GetAll() on p.ApplicationStatusId equals st.Id
           select new
                       {
                           Bapp = p,
                           Window = w,
                           Description = w.Description,
                           ApplicationStatus = st.StatusDesc,
                           StudentSAId = ld.SAIdNumber,
                           Firstname = ld.Firstname,
                           Lastname = ld.Lastname,
                           UserId = p.UserId,
                       }).Distinct();

            var totalCount = app.Count();

            var wind = (from o in app
                        select new BursaryApplicationsViewList
                        {
                            ApplicationsView = new BursaryApplicationssView
                            {
                                ApplicationStatus = o.ApplicationStatus,
                                Title = o.Window.Title,
                                Description = o.Window.Description,
                                SubmissionDte = o.Bapp.SubmissionDte,
                                DeadlineTime = o.Window.DeadlineTime,
                                LauncheDte = o.Window.LaunchDte,
                                StudentSAId = o.StudentSAId,
                                Firstname = o.Firstname,
                                Lastname = o.Lastname,
                                DateCreated = o.Bapp.DateCreated,
                                Id = o.Bapp.Id
                            }
                        })
                        .OrderByDescending(a => a.ApplicationsView.Id)
                        .ToList();

            return new PagedResultDto<BursaryApplicationsViewList>(
                totalCount,
                wind.ToList()
            );
        }

        public async Task<PagedResultDto<BursaryApplicationsViewList>> GetAllBursaryApplications()
        {
            var app = (from p in _bapp.GetAll()
                       join ld in _lesdet.GetAll() on p.StudentId equals ld.Id
                       join l in _lesedi.GetAll() on p.LesediId equals l.Id
                       join w in _win.GetAll() on p.GrantWindowId equals w.Id
                       join st in _lesdstat.GetAll() on p.ApplicationStatusId equals st.Id
                       select new
                       {
                           Bapp = p,
                           Window = w,
                           Description = w.Description,
                           ApplicationStatus = st.StatusDesc,
                           StudentSAId = ld.SAIdNumber,
                           Firstname = ld.Firstname,
                           Lastname = ld.Lastname,
                           UserId = p.UserId,
                       }).Distinct();

            var totalCount = app.Count();

            var wind = (from o in app
                        select new BursaryApplicationsViewList
                        {
                            ApplicationsView = new BursaryApplicationssView
                            {
                                ApplicationStatus = o.ApplicationStatus,
                                Title = o.Window.Title,
                                Description = o.Window.Description,
                                SubmissionDte = o.Bapp.SubmissionDte,
                                DeadlineTime = o.Window.DeadlineTime,
                                LauncheDte = o.Window.LaunchDte,
                                StudentSAId = o.StudentSAId,
                                Firstname = o.Firstname,
                                Lastname = o.Lastname,
                                DateCreated = o.Bapp.DateCreated,
                                Id = o.Bapp.Id
                            }
                        })
                        .OrderByDescending(a => a.ApplicationsView.Id)
                        .ToList();

            return new PagedResultDto<BursaryApplicationsViewList>(
                totalCount,
                wind.ToList()
            );
        }
        public async Task<PagedResultDto<Lesedi_QualificationList>> GetQualifications()
        {

            var cquals = (from q in _qual.GetAll()
                          select new
                          {
                              QualificationName = q.QualificationName,
                              Id = q.Id
                          }).ToList()
                          .OrderBy(o=>o.QualificationName);

            var quals = from o in cquals
                        select new Lesedi_QualificationList()
                        {
                            Qualification = new Lesedi_QualificationDto
                            {
                                QualificationName = o.QualificationName,
                                Id = o.Id
                            }
                        };

            var totalCount = quals.Distinct().Count();

            return new PagedResultDto<Lesedi_QualificationList>(
                totalCount,
                quals.Distinct().ToList()
            );
        }

        public async Task<string> validateLesediSubmission(int ApplicationId)
        {
            string output = "";
            var app = _bapp.FirstOrDefault(ApplicationId);
            var lwin = _win.FirstOrDefault(app.GrantWindowId);
            if (lwin.DeadlineTime <= DateTime.Now) { output = "Window is now Closed"; };
                
            if (app.StudentId > 0)
            {
                var std = await _lesdet.FirstOrDefaultAsync((int)app.StudentId);
                if (std.Firstname == "") { output = output + ", Firstname"; }
                if (std.Lastname == "") { output = output + ", Lastname"; }
                if (std.SAIdNumber.Length == 0) { output = output + ", SA Id Number"; }
                if (std.Cellphone == "") { output = output + ", Cellphone"; }
                if (std.Contactnumber.Length == 0) { output = output + ", Alternative No"; }
                if (std.Email.Length == 0) { output = output + ", Email"; }
                if (std.Race == null) { output = output + "Equity"; }
                if (std.Gender == null) { output = output + ", Gender"; }
                if (std.Province == null) { output = output + ", Province"; }
                if (std.District == null) { output = output + ", District"; }
                if (std.Municipality == null) { output = output + ", Municipality"; }
            } else
            {
                output = output + ", Student Details";
            }

            if (app.AddressId > 0)
            {
                var adr = await _lesaddr.FirstOrDefaultAsync((int)app.AddressId);
                if (adr.addressline1 == "") { output = output + ", Street"; }
                if (adr.postcode == "") { output = output + ", Post Code"; }
                if (adr.suburb.Length == 0) { output = output + ", Surbub"; }
                if (adr.area == "") { output = output + ", Area"; }
                if (adr.district.Length == 0) { output = output + ", District"; }
                if (adr.municipality.Length == 0) { output = output + ", Municipality"; }
                if (adr.province.Length == 0) { output = output + ", Province"; }
            } else
            {
                output = output + ", Address";
            }

            if (app.LesediId > 0)
            {
                var educ = await _lesedi.FirstOrDefaultAsync((int)app.LesediId);
                if (educ.UniversityCollege == 0) { output = output + ", University/College"; }
                if (educ.Qualification == 0) { output = output + ", Qualification"; }
                if (educ.CurrentlyStudying.Length == 0) { output = output + ", Currently Studying?"; }
                if (educ.StudyYear == "") { output = output + ", Study Year"; }
                if (educ.UnderPostGraduate.Length == 0) { output = output + ", NSFAS Beneficiary"; }
                if (educ.NSFASBeneficiary.Length == 0) { output = output + ", Municipality"; }
                if (educ.CurrentHist.Length == 0) { output = output + ", Current/Historical?"; }
                if (educ.Balance == 0) { output = output + ", Fees Balance"; }
                if (educ.PassRate == 0) { output = output + ", Pass Rate"; }
                if (educ.ConsentYN.Length == 0) { output = output + ", Consent"; }
                if (educ.NSFASBeneficiary.Length > 0 && educ.NSFASBeneficiary == "Yes") { output = output + ", Invalid application, already NSFAS Beneficiary"; }
            } else
            {
                output = output + ", Application Details";
            }

            var docs = _doc.GetAll().Where(a => a.entityid == app.Id && a.module == "Lesedi").ToList();
            if (docs.Count > 0)
            {
                var id = docs.Where(a => a.documenttype == "ID Copy").Count();
                if (id == 0) { output = output + ", ID Document"; }
                var fees = docs.Where(a => a.documenttype == "Statement").Count();
                if (fees == 0) { output = output + ", Fees Statement"; }
                var res = docs.Where(a => a.documenttype == "Results").Count();
                if (res == 0) { output = output + ", Statement of results"; }
                var reg = docs.Where(a => a.documenttype == "Registration").Count();
                if (reg == 0) { output = output + ", Registration Proof"; }
            } else
            {
                output = output + ", No documents loaded";
            }

            if (output.StartsWith(",")) 
            { 
                output = output.Substring(2, output.Length - 2); 
            }

            return output;
        }

        public async Task SubmitApplication(int ApplicationId, int userid)
        {
            var app = _bapp.GetAll().Where(a => a.Id == ApplicationId);

            if (app.Count() != 0)
            {
                var capp = await _bapp.FirstOrDefaultAsync(app.First().Id);
                capp.DteUpd = DateTime.Now;
                capp.UsrUpd = userid;
                capp.SubmittedBy = userid;
                capp.SubmissionDte = DateTime.Now;
                capp.ApplicationStatusId = 2;

                await _bapp.UpdateAsync(capp);

            }
        }

        public async Task<PagedResultDto<BursaryApprovalsList>> GetBursaryApprovalsForView(int ApplicationId)
        {
            var det = (from ga in _appr.GetAll()
                       join a in _bapp.GetAll() on ga.ApplicationId equals a.Id
                       join st in _lesdstat.GetAll() on a.ApplicationStatusId equals st.Id
                       join typ in _apprType.GetAll() on ga.ApprovalTypeId equals typ.Id
                       select new
                       {
                           GrantApprovals = ga,
                           ApprovalDescription = typ.ApprovalDescription,
                           StatusDescription = st.StatusDesc
                       })
                .Where(a => a.GrantApprovals.ApplicationId == ApplicationId);


            var projdet = await (from o in det
                                 select new BursaryApprovalsList
                                 {
                                     Bursaries = new BursaryApprovalsView
                                     {
                                         ApplicationId = o.GrantApprovals.ApplicationId,
                                         StatusDescription = o.StatusDescription,
                                         ApprovalDescription = o.ApprovalDescription,
                                         IdCopy = o.GrantApprovals.IdCopy,
                                         Statement = o.GrantApprovals.Statement,
                                         Registration = o.GrantApprovals.Registration,
                                         Results = o.GrantApprovals.Results,
                                         Comments = o.GrantApprovals.Comments,
                                         Outcome = o.GrantApprovals.Outcome,
                                         MeetingDate = o.GrantApprovals.MeetingDate,
                                         OutcomeDate = o.GrantApprovals.OutcomeDate,
                                         Id = o.GrantApprovals.Id
                                     }
                                 }).ToListAsync();

            var totalCount = det.Count();

            return new PagedResultDto<BursaryApprovalsList>(
                totalCount,
                projdet.ToList()
            );
        }

        public async Task<string> CreateEditBursaryApproval(BursaryApprovalsDto input)
        {
            var output = "";

            var brsrs =  (from apr in _appr.GetAll()
            join a in _bapp.GetAll() on apr.ApplicationId equals a.Id
            select new
            {
                Approval = apr,
                Id = a.Id
            }).Where(a => a.Id == input.ApplicationId);

            if (brsrs.Count() == 0)
            {
                input.DateCreated = DateTime.Now;
                input.OutcomeDate = DateTime.Now;
                var app = ObjectMapper.Map<BursaryApprovals>(input);
                await _appr.InsertAsync(app);

                var cappl = _bapp.FirstOrDefault(input.ApplicationId);
                cappl.DteUpd = DateTime.Now;
                cappl.UsrUpd = input.UserId;
                if (input.ApprovalStatusId == 1)
                {
                    cappl.ApplicationStatusId = 4;
                }
                if (input.ApprovalStatusId == 2)
                {

                    cappl.ApplicationStatusId = 8;
                }
                await _bapp.UpdateAsync(cappl);
            }
            else
            {
                var dgappr = await _appr.FirstOrDefaultAsync(brsrs.FirstOrDefault().Id);
                dgappr.DteUpd = DateTime.Now;
                dgappr.UsrUpd = input.UsrUpd;

                await _appr.UpdateAsync(dgappr);

                var cappl = await _bapp.FirstOrDefaultAsync(input.ApplicationId);
                cappl.DteUpd = DateTime.Now;
                cappl.UsrUpd = input.UserId;
                if (input.ApprovalStatusId == 1)
                {
                    cappl.ApplicationStatusId = 4;
                }
                if (input.ApprovalStatusId == 2)
                {

                    cappl.ApplicationStatusId = 8;
                }
                await _bapp.UpdateAsync(cappl);
            }

            return output;
        }

        public async Task BursaryApproveDocument(int doctype, int stat, string comment, int ApplicationId, int userid)
        {
            var appr = _docApproval.GetAll().Where(a => a.ApplicationId == ApplicationId && a.DocumentTypeId == doctype && a.ApprovalTypeId == 1).FirstOrDefault();
            if (appr == null)
            {
                var inappr = new BursaryDocumentApprovals();
                inappr.ApplicationId = ApplicationId;
                inappr.ApprovalTypeId = 1;
                inappr.ApprovalStatusId = stat;
                inappr.DocumentTypeId = doctype;
                inappr.Comments = comment;
                inappr.UsrUpd = userid;
                inappr.UserId = userid;
                inappr.DateCreated = DateTime.Now;
                _docApproval.Insert(inappr);
            }
            else
            {
                appr.ApprovalStatusId = stat;
                appr.UsrUpd = userid;
                appr.DteUpd = DateTime.Now;
                appr.Comments = comment;
                _docApproval.UpdateAsync(appr);
            }
        }

        public async Task<BursarydocumentApprovalsView> GetDocumentApproval(int ApplicationId, int doctype)
        {
            var approvals = (from appr in _docApproval.GetAll()
                             join stat in _grantApprStat.GetAll() on appr.ApprovalStatusId equals stat.Id
                             join typ in _grantApprType.GetAll() on appr.ApprovalTypeId equals typ.Id
                             select new
                             {
                                 DocumentApproval = appr,
                                 ApprovalStatus = stat.GrantStatusDescription,
                                 ApprovalType = typ.ApprovalDescription
                             })
                    .Where(a => a.DocumentApproval.ApplicationId == ApplicationId && a.DocumentApproval.DocumentTypeId == doctype)
                    .FirstOrDefault();

            if (approvals != null)
            {
                var approvallist = new BursarydocumentApprovalsView()
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
    }
}
