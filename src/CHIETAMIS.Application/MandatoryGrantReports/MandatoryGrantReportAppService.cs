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
using CHIETAMIS.MandatoryGrants;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;

namespace CHIETAMIS.MandatoryGrantReports
{
    public class MandatoryGrantReportAppService : CHIETAMISAppServiceBase
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

        public MandatoryGrantReportAppService (IRepository<MandatoryApplication> mandatoryApplication,
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
                                          IRepository<Regions> regRepository)
        {
            _mandatoryApplication = mandatoryApplication;
            _mandatoryWindow = mandatoryWindow;
            _orgRepository = orgRepository;
            _mandStatusRepository = mandStatusRepository;
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
        public async Task<PagedResultDto<MandatoryApplicationsView>> getMandatoryGrantApplications(string SDLNoFilter, string SDLNoFilterMode, string OrganisationNameFilter, string OrganisationNameFilterMode, string ReferenceFilter, string ReferenceFilterMode, string GrantStatusFilter, string GrantStatusFilterMode, string ProvinceFilter, string ProvinceFilterMode, string RSAFilter, string RSAFilterMode)
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
                .OrderByDescending(a => a.Id);


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

        public async Task<PagedResultDto<MandatoryGrantExtensionsView>> getMandatoryGrantExtensions(string SDLNoFilter, string SDLNoFilterMode, string OrganisationNameFilter, string OrganisationNameFilterMode, string ReferenceFilter, string ReferenceFilterMode, string GrantStatusFilter, string GrantStatusFilterMode, string ProvinceFilter, string ProvinceFilterMode, string RSAFilter, string RSAFilterMode)
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
                                    SubmissionDte = mg.SubmissionDte,
                                    Reason = ext.ReasonForRequest,
                                    ClosingDate = wnd.ExtensionDate,
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
                .OrderByDescending(a => a.Id);


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
    }

}
