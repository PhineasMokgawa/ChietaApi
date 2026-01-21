using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using CHIETAMIS.Authorization.Users;
using CHIETAMIS.Dashboards.Dtos;
using CHIETAMIS.Lookups;
using CHIETAMIS.MandatoryGrants;
using CHIETAMIS.MG_Dashboards.Dtos;
using CHIETAMIS.Organisations;
using Org.BouncyCastle.Asn1.X500;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.MG_Dashboards
{
    public class MGDashboardAppService : CHIETAMISAppServiceBase
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
        private readonly IRepository<Regions> _rgnRepository;

        public MGDashboardAppService(IRepository<MandatoryApplication> mandatoryApplication,
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
                                          IRepository<Regions> rgnRepository)
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
            _rgnRepository = rgnRepository;
        }
        public async Task<PagedResultDto<MWindowCodesDto>> MGrantWindows()
        {
            var apps = (from w in _mandatoryWindow.GetAll()
                        select new
                        {
                            ReferenceNo = w.ReferenceNo
                        }).Distinct();

            var appdash = from o in apps
                          select new MWindowCodesDto()
                          {
                              ReferenceNo = o.ReferenceNo,
                          };

            var totalCount = apps.Count();

            return new PagedResultDto<MWindowCodesDto>(
                totalCount,
                appdash.ToList()
            );
        }
        public async Task<PagedResultDto<MWindowTitlesDto>> MGrantWindowTitles(string ReferenceNo)
        {
            var apps = (from w in _mandatoryWindow.GetAll()
                        select new
                        {
                            Id = w.Id,
                            ReferenceNo = w.ReferenceNo,
                            Description = w.Description,

                        })
                        .Where(a => a.ReferenceNo == ReferenceNo)
                        .Distinct();

            var appdash = from o in apps
                          select new MWindowTitlesDto()
                          {
                              Id = o.Id,
                              ReferenceNo = o.ReferenceNo,
                              Description = o.Description,
                          };

            var totalCount = apps.Count();

            return new PagedResultDto<MWindowTitlesDto>(
                totalCount,
                appdash.ToList()
            );
        }


        public async Task<PagedResultDto<MGrantApplicationsSummaries>> MGrantStatusDashboard(int WindowId)
        {
            var apps = (from mg in _mandatoryApplication.GetAll()
                            //join bd in _bioRepository.GetAll() on mg.Id equals bd.ApplicationId
                        join wnd in _mandatoryWindow.GetAll() on mg.GrantWindowId equals wnd.Id
                        join org in _orgRepository.GetAll() on mg.OrganisationId equals org.Id
                        join stat in _mandStatusRepository.GetAll() on mg.GrantStatusID equals stat.Id


                        select new
                        {
                            Id = mg.Id,
                            StatusDescription = stat.StatusDesc,
                            //Learners = bd.ApplicationId,
                            WindowId = wnd.Id
                        })
             .Where(x => x.WindowId == WindowId)
             .GroupBy(x => x.StatusDescription)
             .Select(x =>
               new
               {
                   Apps = x.Count(),
                   //Learners = x.Count(),
                   StatusDescription = x.Key
               });

            var appdash = from o in apps
                          select new MGrantApplicationsSummaries()
                          {
                              StatusDescription = o.StatusDescription,
                              Apps = o.Apps,
                              //Learners = o.Learners,

                          };

            var totalCount = apps.Count();

            return new PagedResultDto<MGrantApplicationsSummaries>(
                totalCount,
                appdash.ToList()
            );
        }
        public async Task<PagedResultDto<MGrantApplicationsSummaries>> MGrantEmployeeStatusDashboard(int WindowId)
        {
            var apps = (from mg in _mandatoryApplication.GetAll()
                        join bd in _bioRepository.GetAll() on mg.Id equals bd.ApplicationId
                        join wnd in _mandatoryWindow.GetAll() on mg.GrantWindowId equals wnd.Id
                        join org in _orgRepository.GetAll() on mg.OrganisationId equals org.Id
                        join stat in _mandStatusRepository.GetAll() on mg.GrantStatusID equals stat.Id


                        select new
                        {
                            Id = mg.Id,
                            StatusDescription = stat.StatusDesc,
                            Learners = bd.Id,
                            WindowId = wnd.Id
                        })
             .Where(x => x.WindowId == WindowId)
             .GroupBy(x => x.StatusDescription)
             .Select(x =>
               new
               {
                   Apps = x.Count(),
                   Learners = x.Count(),
                   StatusDescription = x.Key
               });

            var appdash = from o in apps
                          select new MGrantApplicationsSummaries()
                          {
                              StatusDescription = o.StatusDescription,
                              Apps = o.Apps,
                              Learners = o.Learners,

                          };

            var totalCount = apps.Count();

            return new PagedResultDto<MGrantApplicationsSummaries>(
                totalCount,
                appdash.ToList()
            );
        }


        public async Task<PagedResultDto<MGrantRegionsSummaries>> MGrantRegionDashboard(int WindowId)
        {
            var apps = (from mg in _mandatoryApplication.GetAll()
                            // join bd in _bioRepository.GetAll() on mg.Id equals bd.ApplicationId
                        join wnd in _mandatoryWindow.GetAll() on mg.GrantWindowId equals wnd.Id
                        join org in _orgRepository.GetAll() on mg.OrganisationId equals org.Id
                        join stat in _mandStatusRepository.GetAll() on mg.GrantStatusID equals stat.Id
                        join rsa in _rsaRepository.GetAll() on mg.RSAId equals rsa.UserID
                        join rgn in _rgnRepository.GetAll() on rsa.RegionID equals rgn.Id


                        select new
                        {
                            Id = mg.Id,
                            Region = rgn.RegionName,
                            StatusDescription = stat.StatusDesc,
                            // Learners = bd.ApplicationId,
                            WindowId = wnd.Id
                        })
              .Where(x => x.WindowId == WindowId)
              .GroupBy(x => new { x.Region }) //, x.StatusDescription
              .Select(x =>
                new
                {
                    Apps = x.Count(),
                    // Learners = x.Count(),
                    Region = x.Key.Region,

                });

            var appdash = from o in apps
                          select new MGrantRegionsSummaries()
                          {
                              Apps = o.Apps,
                              Region = o.Region,
                              //  Learners = o.Learners,


                          };

            var totalCount = apps.Count();

            return new PagedResultDto<MGrantRegionsSummaries>(
                totalCount,
                appdash.ToList()
            );
        }
        public async Task<PagedResultDto<MGrantRegionsSummaries>> MGrantEmployeeRegionDashboard(int WindowId)
        {
            var apps = (from mg in _mandatoryApplication.GetAll()
                        join bd in _bioRepository.GetAll() on mg.Id equals bd.ApplicationId
                        join wnd in _mandatoryWindow.GetAll() on mg.GrantWindowId equals wnd.Id
                        join org in _orgRepository.GetAll() on mg.OrganisationId equals org.Id
                        join stat in _mandStatusRepository.GetAll() on mg.GrantStatusID equals stat.Id
                        join rsa in _rsaRepository.GetAll() on mg.RSAId equals rsa.UserID
                        join rgn in _rgnRepository.GetAll() on rsa.RegionID equals rgn.Id


                        select new
                        {
                            Id = mg.Id,
                            Region = rgn.RegionName,
                            StatusDescription = stat.StatusDesc,
                            Learners = bd.Id,
                            WindowId = wnd.Id
                        })
              .Where(x => x.WindowId == WindowId)
              .GroupBy(x => new { x.Region })
              .Select(x =>
                new
                {
                    Apps = x.Count(),
                    Learners = x.Count(),
                    Region = x.Key.Region,

                });

            var appdash = from o in apps
                          select new MGrantRegionsSummaries()
                          {
                              Apps = o.Apps,
                              Region = o.Region,
                              Learners = o.Learners,


                          };

            var totalCount = apps.Count();

            return new PagedResultDto<MGrantRegionsSummaries>(
                totalCount,
                appdash.ToList()
            );
        }



        public async Task<PagedResultDto<MGrantRSASummaries>> MGrantRegionalRSADashboard(int WindowId)
        {
            var apps = (from mg in _mandatoryApplication.GetAll()
                            //    join bd in _bioRepository.GetAll() on mg.Id equals bd.ApplicationId
                        join wnd in _mandatoryWindow.GetAll() on mg.GrantWindowId equals wnd.Id
                        join org in _orgRepository.GetAll() on mg.OrganisationId equals org.Id
                        join stat in _mandStatusRepository.GetAll() on mg.GrantStatusID equals stat.Id
                        join rsa in _rsaRepository.GetAll() on mg.RSAId equals rsa.UserID
                        join rgn in _rgnRepository.GetAll() on rsa.RegionID equals rgn.Id


                        select new
                        {
                            Id = mg.Id,
                            RSA = rsa.RSA_Name,
                            StatusDescription = stat.StatusDesc,
                            // Learners = bd.ApplicationId,
                            WindowId = wnd.Id
                        })
               .Where(x => x.WindowId == WindowId)
               .GroupBy(x => new { x.RSA }) //, x.StatusDescription
               .Select(x =>
                 new
                 {
                     Apps = x.Count(),
                     //  Learners = x.Count(),
                     RSA = x.Key.RSA,

                 });

            var appdash = from o in apps
                          select new MGrantRSASummaries()
                          {
                              Apps = o.Apps,
                              RSA = o.RSA,
                              // Learners = o.Learners,

                          };

            var totalCount = apps.Count();

            return new PagedResultDto<MGrantRSASummaries>(
                totalCount,
                appdash.ToList()
            );
        }
        public async Task<PagedResultDto<MGrantRSASummaries>> MGrantEmployeeRegionalRSADashboard(int WindowId)
        {
            var apps = (from mg in _mandatoryApplication.GetAll()
                        join bd in _bioRepository.GetAll() on mg.Id equals bd.ApplicationId
                        join wnd in _mandatoryWindow.GetAll() on mg.GrantWindowId equals wnd.Id
                        join org in _orgRepository.GetAll() on mg.OrganisationId equals org.Id
                        join stat in _mandStatusRepository.GetAll() on mg.GrantStatusID equals stat.Id
                        join rsa in _rsaRepository.GetAll() on mg.RSAId equals rsa.UserID
                        join rgn in _rgnRepository.GetAll() on rsa.RegionID equals rgn.Id


                        select new
                        {
                            Id = mg.Id,
                            RSA = rsa.RSA_Name,
                            StatusDescription = stat.StatusDesc,
                            Learners = bd.Id,
                            WindowId = wnd.Id
                        })
               .Where(x => x.WindowId == WindowId)
               .GroupBy(x => new { x.RSA }) //, x.StatusDescription
               .Select(x =>
                 new
                 {
                     Apps = x.Count(),
                     Learners = x.Count(),
                     RSA = x.Key.RSA,

                 });

            var appdash = from o in apps
                          select new MGrantRSASummaries()
                          {
                              Apps = o.Apps,
                              RSA = o.RSA,
                              Learners = o.Learners,

                          };

            var totalCount = apps.Count();

            return new PagedResultDto<MGrantRSASummaries>(
                totalCount,
                appdash.ToList()
            );
        }


        public async Task<PagedResultDto<MGrantProvinceSummaries>> MGrantProvinceDashboard(int WindowId)
        {

            var apps = (from mg in _mandatoryApplication.GetAll()
                        join wnd in _mandatoryWindow.GetAll() on mg.GrantWindowId equals wnd.Id
                        join org in _orgRepository.GetAll() on mg.OrganisationId equals org.Id
                        join a in _addressRepository.GetAll() on org.Id equals a.organisationId
                        join stat in _mandStatusRepository.GetAll() on mg.GrantStatusID equals stat.Id


                        select new
                        {
                            Id = mg.Id,
                            Province = a.province,
                            StatusDescription = stat.StatusDesc,
                            WindowId = wnd.Id
                        })
      .Where(x => x.WindowId == WindowId)
      .GroupBy(x => new { x.Province }) //, x.StatusDescription
      .Select(x =>
        new
        {
            Apps = x.Count(),
            Learners = x.Count(),
            Province = x.Key.Province,
            //StatusDescription = x.Key.StatusDescription
        });

            var appdash = from o in apps
                          select new MGrantProvinceSummaries()
                          {
                              Apps = o.Apps,
                              Province = o.Province,
                              Learners = o.Learners,
                          };

            var totalCount = apps.Count();

            return new PagedResultDto<MGrantProvinceSummaries>(
                totalCount,
                appdash.ToList()
            );
        }
        public async Task<PagedResultDto<MGrantProvinceSummaries>> MGrantEmployeeProvinceDashboard(int WindowId)
        {

            var apps = (from mg in _mandatoryApplication.GetAll()
                        join bd in _bioRepository.GetAll() on mg.Id equals bd.ApplicationId
                        join wnd in _mandatoryWindow.GetAll() on mg.GrantWindowId equals wnd.Id
                        join org in _orgRepository.GetAll() on mg.OrganisationId equals org.Id
                        join stat in _mandStatusRepository.GetAll() on mg.GrantStatusID equals stat.Id


                        select new
                        {
                            Id = mg.Id,
                            Province = bd.Province,
                            StatusDescription = stat.StatusDesc,
                            Learners = bd.Id,
                            WindowId = wnd.Id
                        })
      .Where(x => x.WindowId == WindowId)
      .GroupBy(x => new { x.Province }) //, x.StatusDescription
      .Select(x =>
        new
        {
            Apps = x.Count(),
            Learners = x.Count(),
            Province = x.Key.Province,
            //StatusDescription = x.Key.StatusDescription
        });

            var appdash = from o in apps
                          select new MGrantProvinceSummaries()
                          {
                              Apps = o.Apps,
                              Province = o.Province,
                              Learners = o.Learners,
                          };

            var totalCount = apps.Count();

            return new PagedResultDto<MGrantProvinceSummaries>(
                totalCount,
                appdash.ToList()
        );
        }


        public async Task<PagedResultDto<MGrantChamberSummaries>> MGrantChamberDashboard(int WindowId)
        {
            var apps = (from mg in _mandatoryApplication.GetAll()
                            // join bd in _bioRepository.GetAll() on mg.Id equals bd.ApplicationId
                        join wnd in _mandatoryWindow.GetAll() on mg.GrantWindowId equals wnd.Id
                        join org in _orgRepository.GetAll() on mg.OrganisationId equals org.Id
                        join stat in _mandStatusRepository.GetAll() on mg.GrantStatusID equals stat.Id

                        select new
                        {
                            Id = mg.Id,
                            Chamber = org.CHAMBER,
                            StatusDescription = stat.StatusDesc,
                            // Learners = bd.ApplicationId,
                            WindowId = wnd.Id
                        })
             .Where(x => x.WindowId == WindowId && x.Chamber != null)
             .GroupBy(x => new { x.Chamber }) //, x.StatusDescription 
             .Select(x =>
               new
               {
                   Apps = x.Count(),
                   // Learners = x.Count(),
                   Chamber = x.Key.Chamber,
                   //StatusDescription = x.Key.StatusDescription
               });

            var appdash = from o in apps
                          select new MGrantChamberSummaries()
                          {
                              Apps = o.Apps,
                              Chamber = o.Chamber,
                              //StatusDescription = o.StatusDescription,
                              // Learners = o.Learners,

                          };

            var totalCount = apps.Count();

            return new PagedResultDto<MGrantChamberSummaries>(
                totalCount,
                appdash.ToList()
            );
        }
        public async Task<PagedResultDto<MGrantChamberSummaries>> MGrantEmployeeChamberDashboard(int WindowId)
        {
            var apps = (from mg in _mandatoryApplication.GetAll()
                        join bd in _bioRepository.GetAll() on mg.Id equals bd.ApplicationId
                        join wnd in _mandatoryWindow.GetAll() on mg.GrantWindowId equals wnd.Id
                        join org in _orgRepository.GetAll() on mg.OrganisationId equals org.Id
                        join stat in _mandStatusRepository.GetAll() on mg.GrantStatusID equals stat.Id

                        select new
                        {
                            Id = mg.Id,
                            Chamber = org.CHAMBER,
                            StatusDescription = stat.StatusDesc,
                            Learners = bd.Id,
                            WindowId = wnd.Id
                        })
             .Where(x => x.WindowId == WindowId && x.Chamber != null)
             .GroupBy(x => new { x.Chamber }) //, x.StatusDescription 
             .Select(x =>
               new
               {
                   Apps = x.Count(),
                   Learners = x.Count(),
                   Chamber = x.Key.Chamber,
                   //StatusDescription = x.Key.StatusDescription
               });

            var appdash = from o in apps
                          select new MGrantChamberSummaries()
                          {
                              Apps = o.Apps,
                              Chamber = o.Chamber,
                              //StatusDescription = o.StatusDescription,
                              Learners = o.Learners,

                          };

            var totalCount = apps.Count();

            return new PagedResultDto<MGrantChamberSummaries>(
                totalCount,
                appdash.ToList()
            );
        }






    }

}
