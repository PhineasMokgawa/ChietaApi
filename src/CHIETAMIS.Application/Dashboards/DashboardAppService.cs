using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using CHIETAMIS.Authorization.Users;
using CHIETAMIS.Dashboards.Dtos;
using CHIETAMIS.DiscretionaryProjects;
using CHIETAMIS.DiscretionaryProjects.Dtos;
using CHIETAMIS.DiscretionaryStratRess;
using CHIETAMIS.DiscretionaryWindows;
using CHIETAMIS.Lesedis;
using CHIETAMIS.Lookups;
using CHIETAMIS.Organisations;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Dashboards
{
    public class DashboardAppService : CHIETAMISAppServiceBase
    {
        private readonly IUserEmailer _userEmailer;

        private readonly IRepository<DiscretionaryProject> _discProjRepository;
        private readonly IRepository<Organisation> _orgRepository;
        private readonly IRepository<DiscretionaryStatus> _discStatusRepository;
        private readonly IRepository<DiscretionaryWindow> _windowRepository;
        private readonly IRepository<DiscretionaryProjectDetails> _discProjDetRepository;
        private readonly IRepository<DiscretionaryStratResDetails> _discStratDetRepository;
        private readonly IRepository<DiscretionaryStratResObjectives> _stratObjRepository;
        private readonly IRepository<DiscretionaryProjectDetailsApproval> _discProjDetApprRepository;
        private readonly IRepository<DiscretionaryDetailApproval> _dgApprRepository;
        private readonly IRepository<DiscretionaryStatus> _dgStatusRepository;
        private readonly IRepository<RegionRSA> _rsaRepository;
        private readonly IRepository<Regions> _rgnRepository;
        private readonly IRepository<DiscretionaryGCApproval> _gcRepository;
        private readonly IRepository<BursaryApplications> _bappRepository;
        private readonly IRepository<Lesedi> _lesediRepository;
        private readonly IRepository<LesediDetails> _ldRepository;
        private readonly IRepository<RegionProvince> _rpRepository;
        private readonly IRepository<ProvinceCode> _prCdRepository;
        
        private readonly IRepository<LesediStatus> _lsdstRepository;

        public DashboardAppService(IRepository<DiscretionaryProject> dicprojRepository,
                                              IRepository<Organisation> orgRepository,
                                              IRepository<DiscretionaryStatus> discStatusRepository,
                                              IRepository<DiscretionaryWindow> windowRepository,
                                              IRepository<DiscretionaryProjectDetailsApproval> discprojDetApprRepository,
                                              IRepository<DiscretionaryProjectDetails> discprojDetRepository,
                                              IRepository<DiscretionaryStratResDetails> discStratDetRepository,
                                              IRepository<DiscretionaryStratResObjectives> stratObjRepository,
                                              IRepository<DiscretionaryDetailApproval> dgApprRepository,
                                              IRepository<DiscretionaryStatus> dgStatusRepository,
                                              IRepository<RegionRSA> rsaRepository,
                                              IRepository<Regions> rgnRepository,
                                              IRepository<DiscretionaryGCApproval> gcRepository,
                                              IRepository<BursaryApplications> bappRepository,
                                              IRepository<Lesedi> lesediRepository,
                                              IRepository<LesediDetails> ldRepository,
                                              IRepository<RegionProvince> rpRepository,
                                              IRepository<ProvinceCode> prCdRepository,
                                              IRepository<LesediStatus> lsdstRepository
                                              )
        {
            _discProjRepository = dicprojRepository;
            _orgRepository = orgRepository;
            _discStatusRepository = discStatusRepository;
            _windowRepository = windowRepository;
            _discProjDetApprRepository = discprojDetApprRepository;
            _discStratDetRepository = discStratDetRepository;
            _stratObjRepository = stratObjRepository;
            _discProjDetRepository = discprojDetRepository;
            _dgApprRepository= dgApprRepository;
            _dgStatusRepository = dgStatusRepository;
            _rsaRepository = rsaRepository;
            _rgnRepository = rgnRepository;
            _gcRepository = gcRepository;
            _rpRepository = rpRepository;
            _lsdstRepository = lsdstRepository;
            _bappRepository = bappRepository;
            _lesediRepository = lesediRepository;
            _ldRepository = ldRepository;
            _prCdRepository = prCdRepository;
        }

        public async Task<PagedResultDto<WindowCodesDto>> GrantWindows()
        {
            var apps = (from w in _windowRepository.GetAll()
                        select new
                        {
                            ProgCd = w.ProgCd
                        }).Distinct();

            var appdash = from o in apps
                          select new WindowCodesDto()
                          {
                              ProgCd = o.ProgCd,
                          };

            var totalCount = apps.Count();

            return new PagedResultDto<WindowCodesDto>(
                totalCount,
                appdash.ToList()
            );
        }

        public async Task<PagedResultDto<WindowTitlesDto>> GrantWindowTitles(string ProgCd)
        {
            var apps = (from w in _windowRepository.GetAll()
                        select new
                        {
                            Id = w.Id,
                            Title = w.Title,
                            Description = w.Description,
                            ProgCd = w.ProgCd
                        })
                        .Where(a=>a.ProgCd == ProgCd)
                        .Distinct();

            var appdash = from o in apps
                          select new WindowTitlesDto()
                          {
                              Id = o.Id,
                              Title = o.Title,
                              Description = o.Description,
                          };

            var totalCount = apps.Count();

            return new PagedResultDto<WindowTitlesDto>(
                totalCount,
                appdash.ToList()
            );
        }
        public async Task<PagedResultDto<GrantApplicationsSummaries>> GrantStatusDashboard(int WindowId)
        {
            var apps = (from p in _discProjRepository.GetAll()
                        join dpd in _discProjDetRepository.GetAll() on p.Id equals dpd.ProjectId
                        join w in _windowRepository.GetAll() on p.GrantWindowId equals w.Id
                        join o in _orgRepository.GetAll() on p.OrganisationId equals o.Id
                        join st in _dgStatusRepository.GetAll() on p.ProjectStatusID equals st.Id
                        select new
                        {
                            Id = dpd.Id,
                            StatusDescription = st.StatusDesc,
                            Learners = dpd.Number_New + dpd.Number_Continuing,
                            Budget = (dpd.Number_New + dpd.Number_Continuing) * dpd.CostPerLearner,
                            HDI = dpd.HDI,
                            Female = dpd.Female,
                            Youth = dpd.Youth,
                            Number_Disabled = dpd.Number_Disabled,
                            Rural = dpd.Rural,
                            WindowId = w.Id
                        })
              .Where(x => x.WindowId == WindowId)
              .GroupBy(x => x.StatusDescription)
              .Select(x =>
                new
                {
                    Apps = x.Count(),
                    Learners = x.Sum(y => y.Learners),
                    TotalBudget = x.Sum(y => y.Budget),
                    HDI = x.Sum(y => y.HDI),
                    Female = x.Sum(y => y.Female),
                    Youth = x.Sum(y => y.Youth),
                    Number_Disabled = x.Sum(y => y.Number_Disabled),
                    Rural = x.Sum(y => y.Rural),
                    StatusDescription = x.Key
                });

            var appdash = from o in apps
                          select new GrantApplicationsSummaries()
                          {
                              StatusDescription = o.StatusDescription,
                              Apps = o.Apps,
                              Learners = o.Learners,
                              TotalBudget = o.TotalBudget,
                              HDI = o.HDI,
                              Female = o.Female,
                              Youth = o.Youth,
                              Number_Disabled = o.Number_Disabled,
                              Rural = o.Rural
                          };

            var totalCount = apps.Count();

            return new PagedResultDto<GrantApplicationsSummaries>(
                totalCount,
                appdash.ToList()
            );
        }

        public async Task<PagedResultDto<GrantRegionsSummaries>> GrantRegionDashboard(int WindowId)
        {
            var apps = (from p in _discProjRepository.GetAll()
                        join dpd in _discProjDetRepository.GetAll() on p.Id equals dpd.ProjectId
                        join w in _windowRepository.GetAll() on p.GrantWindowId equals w.Id
                        join o in _orgRepository.GetAll() on p.OrganisationId equals o.Id
                        join st in _dgStatusRepository.GetAll() on p.ProjectStatusID equals st.Id
                        join pr in _prCdRepository.GetAll() on dpd.Province equals pr.Description
                        join rp in _rpRepository.GetAll() on pr.Province_Code equals rp.ProvinceId
                        join rgn in _rgnRepository.GetAll() on rp.RegionId equals rgn.Id

                        select new
                        {
                            Id = dpd.Id,
                            Region = rgn.RegionName,
                            StatusDescription = st.StatusDesc,
                            Learners = dpd.Number_New + dpd.Number_Continuing,
                            Budget = (dpd.Number_New + dpd.Number_Continuing) * dpd.CostPerLearner,
                            HDI = dpd.HDI,
                            Female = dpd.Female,
                            Youth = dpd.Youth,
                            Number_Disabled = dpd.Number_Disabled,
                            Rural = dpd.Rural,
                            WindowId = w.Id
                        })
              .Where(x => x.WindowId == WindowId)
              .GroupBy(x => new { x.Region }) //, x.StatusDescription
              .Select(x =>
                new
                {
                    Apps = x.Count(),
                    Learners = x.Sum(y => y.Learners),
                    TotalBudget = x.Sum(y => y.Budget),
                    HDI = x.Sum(y => y.HDI),
                    Female = x.Sum(y => y.Female),
                    Youth = x.Sum(y => y.Youth),
                    Number_Disabled = x.Sum(y => y.Number_Disabled),
                    Rural = x.Sum(y => y.Rural),
                    Region = x.Key.Region,
                    //StatusDescription = x.Key.StatusDescription
                });

            var appdash = from o in apps
                          select new GrantRegionsSummaries()
                          {
                              Apps = o.Apps,
                              Region = o.Region,
                              Learners = o.Learners,
                              TotalBudget = o.TotalBudget,
                              HDI = o.HDI,
                              Female = o.Female,
                              Youth = o.Youth,
                              Number_Disabled = o.Number_Disabled,
                              Rural = o.Rural,
                              //StatusDescription = o.StatusDescription
                          };

            var totalCount = apps.Count();

            return new PagedResultDto<GrantRegionsSummaries>(
                totalCount,
                appdash.ToList()
            );
        }

        public async Task<PagedResultDto<GrantRSASummaries>> GrantRegionalRSADashboard(int WindowId)
        {
            var apps = (from p in _discProjRepository.GetAll()
                        join dpd in _discProjDetRepository.GetAll() on p.Id equals dpd.ProjectId
                        join w in _windowRepository.GetAll() on p.GrantWindowId equals w.Id
                        join o in _orgRepository.GetAll() on p.OrganisationId equals o.Id
                        join st in _dgStatusRepository.GetAll() on p.ProjectStatusID equals st.Id
                        join rsa in _rsaRepository.GetAll() on p.RSAId equals rsa.UserID
                        join rgn in _rgnRepository.GetAll() on rsa.RegionID equals rgn.Id

                        select new
                        {
                            Id = dpd.Id,
                            RSA = rsa.RSA_Name,
                            StatusDescription = st.StatusDesc,
                            Learners = dpd.Number_New + dpd.Number_Continuing,
                            Budget = (dpd.Number_New + dpd.Number_Continuing) * dpd.CostPerLearner,
                            HDI = dpd.HDI,
                            Female = dpd.Female,
                            Youth = dpd.Youth,
                            Number_Disabled = dpd.Number_Disabled,
                            Rural = dpd.Rural,
                            WindowId = w.Id
                        })
              .Where(x => x.WindowId == WindowId)
              .GroupBy(x => new { x.RSA}) //, x.StatusDescription
              .Select(x =>
                new
                {
                    Apps = x.Count(),
                    Learners = x.Sum(y => y.Learners),
                    TotalBudget = x.Sum(y => y.Budget),
                    HDI = x.Sum(y => y.HDI),
                    Female = x.Sum(y => y.Female),
                    Youth = x.Sum(y => y.Youth),
                    Number_Disabled = x.Sum(y => y.Number_Disabled),
                    Rural = x.Sum(y => y.Rural),
                    RSA = x.Key.RSA, 
                    //StatusDescription = x.Key.StatusDescription
                });

            var appdash = from o in apps
                          select new GrantRSASummaries()
                          {
                              Apps = o.Apps,
                              RSA = o.RSA,
                              //StatusDescription = o.StatusDescription,
                              Learners = o.Learners,
                              TotalBudget = o.TotalBudget,
                              HDI = o.HDI,
                              Female = o.Female,
                              Youth = o.Youth,
                              Number_Disabled = o.Number_Disabled,
                              Rural = o.Rural
                          };

            var totalCount = apps.Count();

            return new PagedResultDto<GrantRSASummaries>(
                totalCount,
                appdash.ToList()
            );
        }

        public async Task<PagedResultDto<GrantProvinceSummaries>> GrantProvinceDashboard(int WindowId)
        {
            var apps = (from p in _discProjRepository.GetAll()
                        join dpd in _discProjDetRepository.GetAll() on p.Id equals dpd.ProjectId
                        join w in _windowRepository.GetAll() on p.GrantWindowId equals w.Id
                        join o in _orgRepository.GetAll() on p.OrganisationId equals o.Id
                        join st in _dgStatusRepository.GetAll() on p.ProjectStatusID equals st.Id

                        select new
                        {
                            Id = dpd.Id,
                            Province = dpd.Province,
                            StatusDescription = st.StatusDesc,
                            Learners = dpd.Number_New + dpd.Number_Continuing,
                            Budget = (dpd.Number_New + dpd.Number_Continuing) * dpd.CostPerLearner,
                            HDI = dpd.HDI,
                            Female = dpd.Female,
                            Youth = dpd.Youth,
                            Number_Disabled = dpd.Number_Disabled,
                            Rural = dpd.Rural,
                            WindowId = w.Id
                        })
              .Where(x => x.WindowId == WindowId)
              .GroupBy(x => new { x.Province }) //, x.StatusDescription
              .Select(x =>
                new
                {
                    Apps = x.Count(),
                    Learners = x.Sum(y => y.Learners),
                    TotalBudget = x.Sum(y => y.Budget),
                    HDI = x.Sum(y => y.HDI),
                    Female = x.Sum(y => y.Female),
                    Youth = x.Sum(y => y.Youth),
                    Number_Disabled = x.Sum(y => y.Number_Disabled),
                    Rural = x.Sum(y => y.Rural),
                    Province = x.Key.Province,
                    //StatusDescription = x.Key.StatusDescription
                });

            var appdash = from o in apps
                          select new GrantProvinceSummaries()
                          {
                              Apps = o.Apps,
                              Province = o.Province,
                              //StatusDescription = o.StatusDescription,
                              Learners = o.Learners,
                              TotalBudget = o.TotalBudget,
                              HDI = o.HDI,
                              Female = o.Female,
                              Youth = o.Youth,
                              Number_Disabled = o.Number_Disabled,
                              Rural = o.Rural
                          };

            var totalCount = apps.Count();

            return new PagedResultDto<GrantProvinceSummaries>(
                totalCount,
                appdash.ToList()
            );
        }

        public async Task<PagedResultDto<GrantChamberSummaries>> GrantChamberDashboard(int WindowId)
        {
            var apps = (from p in _discProjRepository.GetAll()
                        join dpd in _discProjDetRepository.GetAll() on p.Id equals dpd.ProjectId
                        join w in _windowRepository.GetAll() on p.GrantWindowId equals w.Id
                        join o in _orgRepository.GetAll() on p.OrganisationId equals o.Id
                        join st in _dgStatusRepository.GetAll() on p.ProjectStatusID equals st.Id

                        select new
                        {
                            Id = dpd.Id,
                            Chamber = o.CHAMBER,
                            StatusDescription = st.StatusDesc,
                            Learners = dpd.Number_New + dpd.Number_Continuing,
                            Budget = (dpd.Number_New + dpd.Number_Continuing) * dpd.CostPerLearner,
                            HDI = dpd.HDI,
                            Female = dpd.Female,
                            Youth = dpd.Youth,
                            Number_Disabled = dpd.Number_Disabled,
                            Rural = dpd.Rural,
                            WindowId = w.Id
                        })
              .Where(x => x.WindowId == WindowId && x.Chamber != null)
              .GroupBy(x => new { x.Chamber}) //, x.StatusDescription 
              .Select(x =>
                new
                {
                    Apps = x.Count(),
                    Learners = x.Sum(y => y.Learners),
                    TotalBudget = x.Sum(y => y.Budget),
                    HDI = x.Sum(y => y.HDI),
                    Female = x.Sum(y => y.Female),
                    Youth = x.Sum(y => y.Youth),
                    Number_Disabled = x.Sum(y => y.Number_Disabled),
                    Rural = x.Sum(y => y.Rural),
                    Chamber = x.Key.Chamber,
                    //StatusDescription = x.Key.StatusDescription
                });

            var appdash = from o in apps
                          select new GrantChamberSummaries()
                          {
                              Apps = o.Apps,
                              Chamber = o.Chamber,
                              //StatusDescription = o.StatusDescription,
                              Learners = o.Learners,
                              TotalBudget = o.TotalBudget,
                              HDI = o.HDI,
                              Female = o.Female,
                              Youth = o.Youth,
                              Number_Disabled = o.Number_Disabled,
                              Rural = o.Rural
                          };

            var totalCount = apps.Count();

            return new PagedResultDto<GrantChamberSummaries>(
                totalCount,
                appdash.ToList()
            );
        }

        public async Task<PagedResultDto<GrantApplicationsSummaries>> ResStatusDashboard(int WindowId)
        {
            var apps = (from p in _discProjRepository.GetAll()
                        join dpd in _discStratDetRepository.GetAll() on p.Id equals dpd.ProjectId
                        //join obj in _stratObjRepository.GetAll() on dpd.Id equals obj.DetailsId
                        join w in _windowRepository.GetAll() on p.GrantWindowId equals w.Id
                        join o in _orgRepository.GetAll() on p.OrganisationId equals o.Id
                        join st in _dgStatusRepository.GetAll() on p.ProjectStatusID equals st.Id
                        select new
                        {
                            Id = dpd.Id,
                            StatusDescription = st.StatusDesc,
                            Learners = 0,
                            Budget = 0,
                            HDI = 0,
                            Female = 0,
                            Youth = 0,
                            Number_Disabled = 0,
                            Rural = 0,
                            WindowId = w.Id
                        })
              .Where(x => x.WindowId == WindowId)
              .GroupBy(x => x.StatusDescription)
              .Select(x =>
                new
                {
                    Apps = x.Count(),
                    Learners = x.Sum(y => y.Learners),
                    TotalBudget = x.Sum(y => y.Budget),
                    HDI = x.Sum(y => y.HDI),
                    Female = x.Sum(y => y.Female),
                    Youth = x.Sum(y => y.Youth),
                    Number_Disabled = x.Sum(y => y.Number_Disabled),
                    Rural = x.Sum(y => y.Rural),
                    StatusDescription = x.Key
                });

            var appdash = from o in apps
                          select new GrantApplicationsSummaries()
                          {
                              StatusDescription = o.StatusDescription,
                              Apps = o.Apps,
                              Learners = o.Learners,
                              TotalBudget = o.TotalBudget,
                              HDI = o.HDI,
                              Female = o.Female,
                              Youth = o.Youth,
                              Number_Disabled = o.Number_Disabled,
                              Rural = o.Rural
                          };

            var totalCount = apps.Count();

            return new PagedResultDto<GrantApplicationsSummaries>(
                totalCount,
                appdash.ToList()
            );
        }

        public async Task<PagedResultDto<GrantRegionsSummaries>> ResRegionDashboard(int WindowId)
        {
            var apps = (from p in _discProjRepository.GetAll()
                        join dpd in _discStratDetRepository.GetAll() on p.Id equals dpd.ProjectId
                        join obj in _stratObjRepository.GetAll() on dpd.Id equals obj.DetailsId
                        join w in _windowRepository.GetAll() on p.GrantWindowId equals w.Id
                        join o in _orgRepository.GetAll() on p.OrganisationId equals o.Id
                        join st in _dgStatusRepository.GetAll() on p.ProjectStatusID equals st.Id
                        join pr in _prCdRepository.GetAll() on dpd.Province equals pr.Description
                        join rp in _rpRepository.GetAll() on pr.Province_Code equals rp.ProvinceId
                        join rgn in _rgnRepository.GetAll() on rp.RegionId equals rgn.Id

                        select new
                        {
                            Id = dpd.Id,
                            Region = rgn.RegionName,
                            StatusDescription = st.StatusDesc,
                            Learners = 0,
                            Budget = obj.Cost,
                            HDI = 0,
                            Female = 0,
                            Youth = 0,
                            Number_Disabled = 0,
                            Rural = 0,
                            WindowId = w.Id
                        })
              .Where(x => x.WindowId == WindowId)
              .GroupBy(x => new { x.Region }) //, x.StatusDescription
              .Select(x =>
                new
                {
                    Apps = x.Count(),
                    Learners = x.Sum(y => y.Learners),
                    TotalBudget = x.Sum(y => y.Budget),
                    HDI = x.Sum(y => y.HDI),
                    Female = x.Sum(y => y.Female),
                    Youth = x.Sum(y => y.Youth),
                    Number_Disabled = x.Sum(y => y.Number_Disabled),
                    Rural = x.Sum(y => y.Rural),
                    Region = x.Key.Region,
                    //StatusDescription = x.Key.StatusDescription
                });

            var appdash = from o in apps
                          select new GrantRegionsSummaries()
                          {
                              Apps = o.Apps,
                              Region = o.Region,
                              Learners = o.Learners,
                              TotalBudget = o.TotalBudget,
                              HDI = o.HDI,
                              Female = o.Female,
                              Youth = o.Youth,
                              Number_Disabled = o.Number_Disabled,
                              Rural = o.Rural,
                              //StatusDescription = o.StatusDescription
                          };

            var totalCount = apps.Count();

            return new PagedResultDto<GrantRegionsSummaries>(
                totalCount,
                appdash.ToList()
            );
        }

        public async Task<PagedResultDto<GrantRSASummaries>> ResRegionalRSADashboard(int WindowId)
        {
            var apps = (from p in _discProjRepository.GetAll()
                        join dpd in _discStratDetRepository.GetAll() on p.Id equals dpd.ProjectId
                        join obj in _stratObjRepository.GetAll() on dpd.Id equals obj.DetailsId
                        join w in _windowRepository.GetAll() on p.GrantWindowId equals w.Id
                        join o in _orgRepository.GetAll() on p.OrganisationId equals o.Id
                        join st in _dgStatusRepository.GetAll() on p.ProjectStatusID equals st.Id
                        join rsa in _rsaRepository.GetAll() on p.RSAId equals rsa.UserID
                        join rgn in _rgnRepository.GetAll() on rsa.RegionID equals rgn.Id

                        select new
                        {
                            Id = dpd.Id,
                            RSA = rsa.RSA_Name,
                            StatusDescription = st.StatusDesc,
                            Learners = 0,
                            Budget = obj.Cost,
                            HDI = 0,
                            Female = 0,
                            Youth = 0,
                            Number_Disabled = 0,
                            Rural = 0,
                            WindowId = w.Id
                        })
              .Where(x => x.WindowId == WindowId)
              .GroupBy(x => new { x.RSA }) //, x.StatusDescription
              .Select(x =>
                new
                {
                    Apps = x.Count(),
                    Learners = x.Sum(y => y.Learners),
                    TotalBudget = x.Sum(y => y.Budget),
                    HDI = x.Sum(y => y.HDI),
                    Female = x.Sum(y => y.Female),
                    Youth = x.Sum(y => y.Youth),
                    Number_Disabled = x.Sum(y => y.Number_Disabled),
                    Rural = x.Sum(y => y.Rural),
                    RSA = x.Key.RSA,
                    //StatusDescription = x.Key.StatusDescription
                });

            var appdash = from o in apps
                          select new GrantRSASummaries()
                          {
                              Apps = o.Apps,
                              RSA = o.RSA,
                              //StatusDescription = o.StatusDescription,
                              Learners = o.Learners,
                              TotalBudget = o.TotalBudget,
                              HDI = o.HDI,
                              Female = o.Female,
                              Youth = o.Youth,
                              Number_Disabled = o.Number_Disabled,
                              Rural = o.Rural
                          };

            var totalCount = apps.Count();

            return new PagedResultDto<GrantRSASummaries>(
                totalCount,
                appdash.ToList()
            );
        }

        public async Task<PagedResultDto<GrantProvinceSummaries>> ResProvinceDashboard(int WindowId)
        {
            var apps = (from p in _discProjRepository.GetAll()
                        join dpd in _discStratDetRepository.GetAll() on p.Id equals dpd.ProjectId
                        join obj in _stratObjRepository.GetAll() on dpd.Id equals obj.DetailsId
                        join w in _windowRepository.GetAll() on p.GrantWindowId equals w.Id
                        join o in _orgRepository.GetAll() on p.OrganisationId equals o.Id
                        join st in _dgStatusRepository.GetAll() on p.ProjectStatusID equals st.Id

                        select new
                        {
                            Id = dpd.Id,
                            Province = dpd.Province,
                            StatusDescription = st.StatusDesc,
                            Learners = 0,
                            Budget = obj.Cost,
                            HDI = 0,
                            Female = 0,
                            Youth = 0,
                            Number_Disabled = 0,
                            Rural = 0,
                            WindowId = w.Id
                        })
              .Where(x => x.WindowId == WindowId)
              .GroupBy(x => new { x.Province }) //, x.StatusDescription
              .Select(x =>
                new
                {
                    Apps = x.Count(),
                    Learners = x.Sum(y => y.Learners),
                    TotalBudget = x.Sum(y => y.Budget),
                    HDI = x.Sum(y => y.HDI),
                    Female = x.Sum(y => y.Female),
                    Youth = x.Sum(y => y.Youth),
                    Number_Disabled = x.Sum(y => y.Number_Disabled),
                    Rural = x.Sum(y => y.Rural),
                    Province = x.Key.Province,
                    //StatusDescription = x.Key.StatusDescription
                });

            var appdash = from o in apps
                          select new GrantProvinceSummaries()
                          {
                              Apps = o.Apps,
                              Province = o.Province,
                              //StatusDescription = o.StatusDescription,
                              Learners = o.Learners,
                              TotalBudget = o.TotalBudget,
                              HDI = o.HDI,
                              Female = o.Female,
                              Youth = o.Youth,
                              Number_Disabled = o.Number_Disabled,
                              Rural = o.Rural
                          };

            var totalCount = apps.Count();

            return new PagedResultDto<GrantProvinceSummaries>(
                totalCount,
                appdash.ToList()
            );
        }

        public async Task<PagedResultDto<GrantChamberSummaries>> ResChamberDashboard(int WindowId)
        {
            var apps = (from p in _discProjRepository.GetAll()
                        join dpd in _discStratDetRepository.GetAll() on p.Id equals dpd.ProjectId
                        join obj in _stratObjRepository.GetAll() on dpd.Id equals obj.DetailsId
                        join w in _windowRepository.GetAll() on p.GrantWindowId equals w.Id
                        join o in _orgRepository.GetAll() on p.OrganisationId equals o.Id
                        join st in _dgStatusRepository.GetAll() on p.ProjectStatusID equals st.Id

                        select new
                        {
                            Id = dpd.Id,
                            Chamber = o.CHAMBER,
                            StatusDescription = st.StatusDesc,
                            Learners = 0,
                            Budget = obj.Cost,
                            HDI = 0,
                            Female = 0,
                            Youth = 0,
                            Number_Disabled = 0,
                            Rural = 0,
                            WindowId = w.Id
                        })
              .Where(x => x.WindowId == WindowId && x.Chamber != null)
              .GroupBy(x => new { x.Chamber }) //, x.StatusDescription 
              .Select(x =>
                new
                {
                    Apps = x.Count(),
                    Learners = x.Sum(y => y.Learners),
                    TotalBudget = x.Sum(y => y.Budget),
                    HDI = x.Sum(y => y.HDI),
                    Female = x.Sum(y => y.Female),
                    Youth = x.Sum(y => y.Youth),
                    Number_Disabled = x.Sum(y => y.Number_Disabled),
                    Rural = x.Sum(y => y.Rural),
                    Chamber = x.Key.Chamber,
                    //StatusDescription = x.Key.StatusDescription
                });

            var appdash = from o in apps
                          select new GrantChamberSummaries()
                          {
                              Apps = o.Apps,
                              Chamber = o.Chamber,
                              //StatusDescription = o.StatusDescription,
                              Learners = o.Learners,
                              TotalBudget = o.TotalBudget,
                              HDI = o.HDI,
                              Female = o.Female,
                              Youth = o.Youth,
                              Number_Disabled = o.Number_Disabled,
                              Rural = o.Rural
                          };

            var totalCount = apps.Count();

            return new PagedResultDto<GrantChamberSummaries>(
                totalCount,
                appdash.ToList()
            );
        }
        public async Task<PagedResultDto<GrantApplicationsSummaries>> LesediStatusDashboard(int WindowId)
        {
            var apps = (from p in _bappRepository.GetAll()
                        join ld in _ldRepository.GetAll() on p.StudentId equals ld.Id
                        join l in _lesediRepository.GetAll() on p.LesediId equals l.Id
                        join w in _windowRepository.GetAll() on p.GrantWindowId equals w.Id
                        join st in _lsdstRepository.GetAll() on p.ApplicationStatusId equals st.Id
                        select new
                        {
                            Id = p.Id,
                            StatusDescription = st.StatusDesc,
                            Learners = 1,
                            Budget = l.Balance,
                            HDI = 0,
                            Female = 0,
                            Youth = 0,
                            Number_Disabled = 0,
                            Rural = 0,
                            WindowId = w.Id
                        })
              .Where(x => x.WindowId == WindowId)
              .GroupBy(x => x.StatusDescription)
              .Select(x =>
                new
                {
                    Apps = x.Count(),
                    Learners = x.Sum(y => y.Learners),
                    TotalBudget = x.Sum(y => y.Budget),
                    HDI = x.Sum(y => y.HDI),
                    Female = x.Sum(y => y.Female),
                    Youth = x.Sum(y => y.Youth),
                    Number_Disabled = x.Sum(y => y.Number_Disabled),
                    Rural = x.Sum(y => y.Rural),
                    StatusDescription = x.Key
                });

            var appdash = from o in apps
                          select new GrantApplicationsSummaries()
                          {
                              StatusDescription = o.StatusDescription,
                              Apps = o.Apps,
                              Learners = o.Learners,
                              TotalBudget = o.TotalBudget,
                              HDI = o.HDI,
                              Female = o.Female,
                              Youth = o.Youth,
                              Number_Disabled = o.Number_Disabled,
                              Rural = o.Rural
                          };

            var totalCount = apps.Count();

            return new PagedResultDto<GrantApplicationsSummaries>(
                totalCount,
                appdash.ToList()
            );
        }

        public async Task<PagedResultDto<GrantRegionsSummaries>> LesediRegionDashboard(int WindowId)
        {
            var apps = (from p in _bappRepository.GetAll()
                        join ld in _ldRepository.GetAll() on p.StudentId equals ld.Id
                        join l in _lesediRepository.GetAll() on p.LesediId equals l.Id
                        join w in _windowRepository.GetAll() on p.GrantWindowId equals w.Id
                        join st in _lsdstRepository.GetAll() on p.ApplicationStatusId equals st.Id
                        join pr in _prCdRepository.GetAll() on ld.Province equals pr.Description
                        join rp in _rpRepository.GetAll() on pr.Province_Code equals rp.ProvinceId
                        join rgn in _rgnRepository.GetAll() on rp.RegionId equals rgn.Id

                        select new
                        {
                            Id = p.Id,
                            Region = rgn.RegionName,
                            StatusDescription = st.StatusDesc,
                            Learners = 1,
                            Budget = l.Balance,
                            HDI = 0,
                            Female = 0,
                            Youth = 0,
                            Number_Disabled = 0,
                            Rural = 0,
                            WindowId = w.Id
                        })
              .Where(x => x.WindowId == WindowId)
              .GroupBy(x => new { x.Region })
              .Select(x =>
                new
                {
                    Apps = x.Count(),
                    Learners = x.Sum(y => y.Learners),
                    TotalBudget = x.Sum(y => y.Budget),
                    HDI = x.Sum(y => y.HDI),
                    Female = x.Sum(y => y.Female),
                    Youth = x.Sum(y => y.Youth),
                    Number_Disabled = x.Sum(y => y.Number_Disabled),
                    Rural = x.Sum(y => y.Rural),
                    Region = x.Key.Region,
                });

            var appdash = from o in apps
                          select new GrantRegionsSummaries()
                          {
                              Apps = o.Apps,
                              Region = o.Region,
                              Learners = o.Learners,
                              TotalBudget = o.TotalBudget,
                              HDI = o.HDI,
                              Female = o.Female,
                              Youth = o.Youth,
                              Number_Disabled = o.Number_Disabled,
                              Rural = o.Rural,
                          };

            var totalCount = apps.Count();

            return new PagedResultDto<GrantRegionsSummaries>(
                totalCount,
                appdash.ToList()
            );
        }
        public async Task<PagedResultDto<GrantProvinceSummaries>> LesediProvinceDashboard(int WindowId)
        {
            var apps = (from p in _bappRepository.GetAll()
                        join ld in _ldRepository.GetAll() on p.StudentId equals ld.Id
                        join l in _lesediRepository.GetAll() on p.LesediId equals l.Id
                        join w in _windowRepository.GetAll() on p.GrantWindowId equals w.Id
                        join st in _lsdstRepository.GetAll() on p.ApplicationStatusId equals st.Id

                        select new
                        {
                            Id = p.Id,
                            Province = ld.Province,
                            StatusDescription = st.StatusDesc,
                            Learners = 1,
                            Budget = l.Balance,
                            HDI = 0,
                            Female = 0,
                            Youth = 0,
                            Number_Disabled = 0,
                            Rural = 0,
                            WindowId = w.Id
                        })
              .Where(x => x.WindowId == WindowId)
              .GroupBy(x => new { x.Province }) //, x.StatusDescription
              .Select(x =>
                new
                {
                    Apps = x.Count(),
                    Learners = x.Sum(y => y.Learners),
                    TotalBudget = x.Sum(y => y.Budget),
                    HDI = x.Sum(y => y.HDI),
                    Female = x.Sum(y => y.Female),
                    Youth = x.Sum(y => y.Youth),
                    Number_Disabled = x.Sum(y => y.Number_Disabled),
                    Rural = x.Sum(y => y.Rural),
                    Province = x.Key.Province,
                });

            var appdash = from o in apps
                          select new GrantProvinceSummaries()
                          {
                              Apps = o.Apps,
                              Province = o.Province,
                              Learners = o.Learners,
                              TotalBudget = o.TotalBudget,
                              HDI = o.HDI,
                              Female = o.Female,
                              Youth = o.Youth,
                              Number_Disabled = o.Number_Disabled,
                              Rural = o.Rural
                          };

            var totalCount = apps.Count();

            return new PagedResultDto<GrantProvinceSummaries>(
                totalCount,
                appdash.ToList()
            );
        }
        public async Task<PagedResultDto<GrantApplicationsSummaries>> GrantStatusApprovalDashboard(int WindowId)
        {
            var apps = (from p in _discProjRepository.GetAll()
                        join dpd in _discProjDetApprRepository.GetAll() on p.Id equals dpd.ProjectId
                        join da in _dgApprRepository.GetAll() on dpd.Id equals da.ApplicationId
                        join w in _windowRepository.GetAll() on p.GrantWindowId equals w.Id
                        join o in _orgRepository.GetAll() on p.OrganisationId equals o.Id
                        join st in _dgStatusRepository.GetAll() on p.ProjectStatusID equals st.Id
                        select new
                        {
                            Id = dpd.Id,
                            StatusDescription = st.StatusDesc,
                            Learners = dpd.Number_New + dpd.Number_Continuing,
                            Budget = (dpd.Number_New + dpd.Number_Continuing) * dpd.CostPerLearner,
                            HDI = dpd.HDI,
                            Female = dpd.Female,
                            Youth = dpd.Youth,
                            Number_Disabled = dpd.Number_Disabled,
                            Rural = dpd.Rural,
                            WindowId = w.Id,
                            ApprovalStatusId = da.ApprovalStatusId
                        })
              .Where(x => x.WindowId == WindowId && x.ApprovalStatusId == 1)
              .GroupBy(x => x.StatusDescription)
              .Select(x =>
                new
                {
                    Apps = x.Count(),
                    Learners = x.Sum(y => y.Learners),
                    TotalBudget = x.Sum(y => y.Budget),
                    HDI = x.Sum(y => y.HDI),
                    Female = x.Sum(y => y.Female),
                    Youth = x.Sum(y => y.Youth),
                    Number_Disabled = x.Sum(y => y.Number_Disabled),
                    Rural = x.Sum(y => y.Rural),
                    StatusDescription = x.Key
                });

            var appdash = from o in apps
                          select new GrantApplicationsSummaries()
                          {
                              StatusDescription = o.StatusDescription,
                              Apps = o.Apps,
                              Learners = o.Learners,
                              TotalBudget = o.TotalBudget,
                              HDI = o.HDI,
                              Female = o.Female,
                              Youth = o.Youth,
                              Number_Disabled = o.Number_Disabled,
                              Rural = o.Rural
                          };

            var totalCount = apps.Count();

            return new PagedResultDto<GrantApplicationsSummaries>(
                totalCount,
                appdash.ToList()
            );
        }
        public async Task<PagedResultDto<GrantRegionsSummaries>> GrantRegionApprovalDashboard(int WindowId)
        {
            var apps = (from p in _discProjRepository.GetAll()
                        join dpd in _discProjDetApprRepository.GetAll() on p.Id equals dpd.ProjectId
                        join da in _dgApprRepository.GetAll() on dpd.Id equals da.ApplicationId
                        join w in _windowRepository.GetAll() on p.GrantWindowId equals w.Id
                        join o in _orgRepository.GetAll() on p.OrganisationId equals o.Id
                        join st in _dgStatusRepository.GetAll() on p.ProjectStatusID equals st.Id
                        join pr in _prCdRepository.GetAll() on dpd.Province equals pr.Description
                        join rp in _rpRepository.GetAll() on pr.Province_Code equals rp.ProvinceId
                        join rgn in _rgnRepository.GetAll() on rp.RegionId equals rgn.Id

                        select new
                        {
                            Id = dpd.Id,
                            Region = rgn.RegionName,
                            StatusDescription = st.StatusDesc,
                            Learners = dpd.Number_New + dpd.Number_Continuing,
                            Budget = (dpd.Number_New + dpd.Number_Continuing) * dpd.CostPerLearner,
                            HDI = dpd.HDI,
                            Female = dpd.Female,
                            Youth = dpd.Youth,
                            Number_Disabled = dpd.Number_Disabled,
                            Rural = dpd.Rural,
                            WindowId = w.Id,
                            ApprovalStatusId = da.ApprovalStatusId
                        })
              .Where(x => x.WindowId == WindowId && x.ApprovalStatusId == 1)
              .GroupBy(x => new { x.Region}) //, x.StatusDescription 
              .Select(x =>
                new
                {
                    Apps = x.Count(),
                    Learners = x.Sum(y => y.Learners),
                    TotalBudget = x.Sum(y => y.Budget),
                    HDI = x.Sum(y => y.HDI),
                    Female = x.Sum(y => y.Female),
                    Youth = x.Sum(y => y.Youth),
                    Number_Disabled = x.Sum(y => y.Number_Disabled),
                    Rural = x.Sum(y => y.Rural),
                    Region = x.Key.Region,
                    //StatusDescription = x.Key.StatusDescription
                });

            var appdash = from o in apps
                          select new GrantRegionsSummaries()
                          {
                              Apps = o.Apps,
                              Region = o.Region,
                              Learners = o.Learners,
                              TotalBudget = o.TotalBudget,
                              HDI = o.HDI,
                              Female = o.Female,
                              Youth = o.Youth,
                              Number_Disabled = o.Number_Disabled,
                              Rural = o.Rural,
                              //StatusDescription = o.StatusDescription
                          };

            var totalCount = apps.Count();

            return new PagedResultDto<GrantRegionsSummaries>(
                totalCount,
                appdash.ToList()
            );
        }

        public async Task<PagedResultDto<GrantRSASummaries>> GrantRegionalRSAApprovalDashboard(int WindowId)
        {
            var apps = (from p in _discProjRepository.GetAll()
                        join dpd in _discProjDetApprRepository.GetAll() on p.Id equals dpd.ProjectId
                        join da in _dgApprRepository.GetAll() on dpd.Id equals da.ApplicationId
                        join w in _windowRepository.GetAll() on p.GrantWindowId equals w.Id
                        join o in _orgRepository.GetAll() on p.OrganisationId equals o.Id
                        join st in _dgStatusRepository.GetAll() on p.ProjectStatusID equals st.Id
                        join rsa in _rsaRepository.GetAll() on p.RSAId equals rsa.UserID
                        join rgn in _rgnRepository.GetAll() on rsa.RegionID equals rgn.Id

                        select new
                        {
                            Id = dpd.Id,
                            RSA = rsa.RSA_Name,
                            StatusDescription = st.StatusDesc,
                            Learners = dpd.Number_New + dpd.Number_Continuing,
                            Budget = (dpd.Number_New + dpd.Number_Continuing) * dpd.CostPerLearner,
                            HDI = dpd.HDI,
                            Female = dpd.Female,
                            Youth = dpd.Youth,
                            Number_Disabled = dpd.Number_Disabled,
                            Rural = dpd.Rural,
                            WindowId = w.Id,
                            ApprovalStatusId = da.ApprovalStatusId
                        })
              .Where(x => x.WindowId == WindowId && x.ApprovalStatusId == 1)
              .GroupBy(x => new { x.RSA}) //, x.StatusDescription 
              .Select(x =>
                new
                {
                    Apps = x.Count(),
                    Learners = x.Sum(y => y.Learners),
                    TotalBudget = x.Sum(y => y.Budget),
                    HDI = x.Sum(y => y.HDI),
                    Female = x.Sum(y => y.Female),
                    Youth = x.Sum(y => y.Youth),
                    Number_Disabled = x.Sum(y => y.Number_Disabled),
                    Rural = x.Sum(y => y.Rural),
                    RSA = x.Key.RSA,
                    //StatusDescription = x.Key.StatusDescription
                });

            var appdash = from o in apps
                          select new GrantRSASummaries()
                          {
                              Apps = o.Apps,
                              RSA = o.RSA,
                              //StatusDescription = o.StatusDescription,
                              Learners = o.Learners,
                              TotalBudget = o.TotalBudget,
                              HDI = o.HDI,
                              Female = o.Female,
                              Youth = o.Youth,
                              Number_Disabled = o.Number_Disabled,
                              Rural = o.Rural
                          };

            var totalCount = apps.Count();

            return new PagedResultDto<GrantRSASummaries>(
                totalCount,
                appdash.ToList()
            );
        }

        public async Task<PagedResultDto<GrantProvinceSummaries>> GrantProvinceApprovalDashboard(int WindowId)
        {
            var apps = (from p in _discProjRepository.GetAll()
                        join dpd in _discProjDetApprRepository.GetAll() on p.Id equals dpd.ProjectId
                        join da in _dgApprRepository.GetAll() on dpd.Id equals da.ApplicationId
                        join w in _windowRepository.GetAll() on p.GrantWindowId equals w.Id
                        join o in _orgRepository.GetAll() on p.OrganisationId equals o.Id
                        join st in _dgStatusRepository.GetAll() on p.ProjectStatusID equals st.Id

                        select new
                        {
                            Id = dpd.Id,
                            Province = dpd.Province,
                            StatusDescription = st.StatusDesc,
                            Learners = dpd.Number_New + dpd.Number_Continuing,
                            Budget = (dpd.Number_New + dpd.Number_Continuing) * dpd.CostPerLearner,
                            HDI = dpd.HDI,
                            Female = dpd.Female,
                            Youth = dpd.Youth,
                            Number_Disabled = dpd.Number_Disabled,
                            Rural = dpd.Rural,
                            WindowId = w.Id,
                            ApprovalStatusId = da.ApprovalStatusId
                        })
              .Where(x => x.WindowId == WindowId && x.ApprovalStatusId == 1)
              .GroupBy(x => new { x.Province}) //, x.StatusDescription 
              .Select(x =>
                new
                {
                    Apps = x.Count(),
                    Learners = x.Sum(y => y.Learners),
                    TotalBudget = x.Sum(y => y.Budget),
                    HDI = x.Sum(y => y.HDI),
                    Female = x.Sum(y => y.Female),
                    Youth = x.Sum(y => y.Youth),
                    Number_Disabled = x.Sum(y => y.Number_Disabled),
                    Rural = x.Sum(y => y.Rural),
                    Province = x.Key.Province,
                    //StatusDescription = x.Key.StatusDescription
                });

            var appdash = from o in apps
                          select new GrantProvinceSummaries()
                          {
                              Apps = o.Apps,
                              Province = o.Province,
                              //StatusDescription = o.StatusDescription,
                              Learners = o.Learners,
                              TotalBudget = o.TotalBudget,
                              HDI = o.HDI,
                              Female = o.Female,
                              Youth = o.Youth,
                              Number_Disabled = o.Number_Disabled,
                              Rural = o.Rural
                          };

            var totalCount = apps.Count();

            return new PagedResultDto<GrantProvinceSummaries>(
                totalCount,
                appdash.ToList()
            );
        }

        public async Task<PagedResultDto<GrantChamberSummaries>> GrantChamberApprovalDashboard(int WindowId)
        {
            var apps = (from p in _discProjRepository.GetAll()
                        join dpd in _discProjDetApprRepository.GetAll() on p.Id equals dpd.ProjectId
                        join da in _dgApprRepository.GetAll() on dpd.Id equals da.ApplicationId
                        join w in _windowRepository.GetAll() on p.GrantWindowId equals w.Id
                        join o in _orgRepository.GetAll() on p.OrganisationId equals o.Id
                        join st in _dgStatusRepository.GetAll() on p.ProjectStatusID equals st.Id

                        select new
                        {
                            Id = dpd.Id,
                            Chamber = o.CHAMBER,
                            StatusDescription = st.StatusDesc,
                            Learners = (int)(dpd.GC_New + dpd.GC_Continuing),
                            Budget = (int)((dpd.GC_New + dpd.GC_Continuing) * dpd.GC_CostPerLearner),
                            HDI = dpd.HDI,
                            Female = dpd.Female,
                            Youth = dpd.Youth,
                            Number_Disabled = dpd.Number_Disabled,
                            Rural = dpd.Rural,
                            WindowId = w.Id,
                            ApprovalStatusId = da.ApprovalStatusId
                        })
              .Where(x => x.WindowId == WindowId && x.ApprovalStatusId == 1 && x.Chamber != null)
              .GroupBy(x => new { x.Chamber }) //, x.StatusDescription
              .Select(x =>
                new
                {
                    Apps = x.Count(),
                    Learners = x.Sum(y => y.Learners),
                    TotalBudget = x.Sum(y => y.Budget),
                    HDI = x.Sum(y => y.HDI),
                    Female = x.Sum(y => y.Female),
                    Youth = x.Sum(y => y.Youth),
                    Number_Disabled = x.Sum(y => y.Number_Disabled),
                    Rural = x.Sum(y => y.Rural),
                    Chamber = x.Key.Chamber,
                    //StatusDescription = x.Key.StatusDescription
                });

            var appdash = from o in apps
                          select new GrantChamberSummaries()
                          {
                              Apps = o.Apps,
                              Chamber = o.Chamber,
                              //StatusDescription = o.StatusDescription,
                              Learners = o.Learners,
                              TotalBudget = o.TotalBudget,
                              HDI = o.HDI,
                              Female = o.Female,
                              Youth = o.Youth,
                              Number_Disabled = o.Number_Disabled,
                              Rural = o.Rural
                          };

            var totalCount = apps.Count();

            return new PagedResultDto<GrantChamberSummaries>(
                totalCount,
                appdash.ToList()
            );
        }

        public async Task<PagedResultDto<GrantApplicationsSummaries>> GrantStatusAwardDashboard(int WindowId)
        {
            var apps = (from p in _discProjRepository.GetAll()
                        join dpd in _discProjDetApprRepository.GetAll() on p.Id equals dpd.ProjectId
                        join da in _dgApprRepository.GetAll() on dpd.Id equals da.ApplicationId
                        join gc in _gcRepository.GetAll() on dpd.Id equals gc.ApplicationId
                        join w in _windowRepository.GetAll() on p.GrantWindowId equals w.Id
                        join o in _orgRepository.GetAll() on p.OrganisationId equals o.Id
                        join st in _dgStatusRepository.GetAll() on p.ProjectStatusID equals st.Id
                        select new
                        {
                            Id = dpd.Id,
                            StatusDescription = st.StatusDesc,
                            Learners = (int)(dpd.GC_New + dpd.GC_Continuing),
                            Budget = (int)((dpd.GC_New + dpd.GC_Continuing) * dpd.GC_CostPerLearner),
                            HDI = dpd.HDI,
                            Female = dpd.Female,
                            Youth = dpd.Youth,
                            Number_Disabled = dpd.Number_Disabled,
                            Rural = dpd.Rural,
                            WindowId = w.Id,
                            ApplicationStatusId = da.ApprovalStatusId,
                            GCStatusId = gc.ApprovalStatusId
                        })
              .Where(x => x.WindowId == WindowId && x.ApplicationStatusId == 1 && x.GCStatusId == 1)
              .GroupBy(x => x.StatusDescription)
              .Select(x =>
                new
                {
                    Apps = x.Count(),
                    Learners = x.Sum(y => y.Learners),
                    TotalBudget = x.Sum(y => y.Budget),
                    HDI = x.Sum(y => y.HDI),
                    Female = x.Sum(y => y.Female),
                    Youth = x.Sum(y => y.Youth),
                    Number_Disabled = x.Sum(y => y.Number_Disabled),
                    Rural = x.Sum(y => y.Rural),
                    StatusDescription = x.Key
                });

            var appdash = from o in apps
                          select new GrantApplicationsSummaries()
                          {
                              StatusDescription = o.StatusDescription,
                              Apps = o.Apps,
                              Learners = o.Learners,
                              TotalBudget = o.TotalBudget,
                              HDI = o.HDI,
                              Female = o.Female,
                              Youth = o.Youth,
                              Number_Disabled = o.Number_Disabled,
                              Rural = o.Rural
                          };

            var totalCount = apps.Count();

            return new PagedResultDto<GrantApplicationsSummaries>(
                totalCount,
                appdash.ToList()
            );
        }

        public async Task<PagedResultDto<GrantRegionsSummaries>> GrantRegionAwardDashboard(int WindowId)
        {
            var apps = (from p in _discProjRepository.GetAll()
                        join dpd in _discProjDetApprRepository.GetAll() on p.Id equals dpd.ProjectId
                        join da in _dgApprRepository.GetAll() on dpd.Id equals da.ApplicationId
                        join gc in _gcRepository.GetAll() on dpd.Id equals gc.ApplicationId
                        join w in _windowRepository.GetAll() on p.GrantWindowId equals w.Id
                        join o in _orgRepository.GetAll() on p.OrganisationId equals o.Id
                        join st in _dgStatusRepository.GetAll() on p.ProjectStatusID equals st.Id
                        join pr in _prCdRepository.GetAll() on dpd.Province equals pr.Description
                        join rp in _rpRepository.GetAll() on pr.Province_Code equals rp.ProvinceId
                        join rgn in _rgnRepository.GetAll() on rp.RegionId equals rgn.Id

                        select new
                        {
                            Id = dpd.Id,
                            Region = rgn.RegionName,
                            StatusDescription = st.StatusDesc,
                            Learners = (int)(dpd.GC_New + dpd.GC_Continuing),
                            Budget = (int)((dpd.GC_New + dpd.GC_Continuing) * dpd.GC_CostPerLearner),
                            HDI = dpd.HDI,
                            Female = dpd.Female,
                            Youth = dpd.Youth,
                            Number_Disabled = dpd.Number_Disabled,
                            Rural = dpd.Rural,
                            WindowId = w.Id,
                            ApplicationStatusId = da.ApprovalStatusId,
                            GCStatusId = gc.ApprovalStatusId
                        })
              .Where(x => x.WindowId == WindowId && x.ApplicationStatusId == 1 && x.GCStatusId == 1)
              .GroupBy(x => new { x.Region}) //, x.StatusDescription 
              .Select(x =>
                new
                {
                    Apps = x.Count(),
                    Learners = x.Sum(y => y.Learners),
                    TotalBudget = x.Sum(y => y.Budget),
                    HDI = x.Sum(y => y.HDI),
                    Female = x.Sum(y => y.Female),
                    Youth = x.Sum(y => y.Youth),
                    Number_Disabled = x.Sum(y => y.Number_Disabled),
                    Rural = x.Sum(y => y.Rural),
                    Region = x.Key.Region,
                    //StatusDescription = x.Key.StatusDescription
                });

            var appdash = from o in apps
                          select new GrantRegionsSummaries()
                          {
                              Apps = o.Apps,
                              Region = o.Region,
                              Learners = o.Learners,
                              TotalBudget = o.TotalBudget,
                              HDI = o.HDI,
                              Female = o.Female,
                              Youth = o.Youth,
                              Number_Disabled = o.Number_Disabled,
                              Rural = o.Rural,
                              //StatusDescription = o.StatusDescription
                          };

            var totalCount = apps.Count();

            return new PagedResultDto<GrantRegionsSummaries>(
                totalCount,
                appdash.ToList()
            );
        }

        public async Task<PagedResultDto<GrantRSASummaries>> GrantRegionalRSAAwardDashboard(int WindowId)
        {
            var apps = (from p in _discProjRepository.GetAll()
                        join dpd in _discProjDetApprRepository.GetAll() on p.Id equals dpd.ProjectId
                        join da in _dgApprRepository.GetAll() on dpd.Id equals da.ApplicationId
                        join gc in _gcRepository.GetAll() on dpd.Id equals gc.ApplicationId
                        join w in _windowRepository.GetAll() on p.GrantWindowId equals w.Id
                        join o in _orgRepository.GetAll() on p.OrganisationId equals o.Id
                        join st in _dgStatusRepository.GetAll() on p.ProjectStatusID equals st.Id
                        join rsa in _rsaRepository.GetAll() on p.RSAId equals rsa.UserID
                        join rgn in _rgnRepository.GetAll() on rsa.RegionID equals rgn.Id

                        select new
                        {
                            Id = dpd.Id,
                            RSA = rsa.RSA_Name,
                            StatusDescription = st.StatusDesc,
                            Learners = (int)(dpd.GC_New + dpd.GC_Continuing),
                            Budget = (int)((dpd.GC_New + dpd.GC_Continuing) * dpd.GC_CostPerLearner),
                            HDI = dpd.HDI,
                            Female = dpd.Female,
                            Youth = dpd.Youth,
                            Number_Disabled = dpd.Number_Disabled,
                            Rural = dpd.Rural,
                            WindowId = w.Id,
                            ApplicationStatusId = da.ApprovalStatusId,
                            GCStatusId = gc.ApprovalStatusId
                        })
              .Where(x => x.WindowId == WindowId && x.ApplicationStatusId == 1 && x.GCStatusId == 1)
              .GroupBy(x => new { x.RSA}) //, x.StatusDescription
              .Select(x =>
                new
                {
                    Apps = x.Count(),
                    Learners = x.Sum(y => y.Learners),
                    TotalBudget = x.Sum(y => y.Budget),
                    HDI = x.Sum(y => y.HDI),
                    Female = x.Sum(y => y.Female),
                    Youth = x.Sum(y => y.Youth),
                    Number_Disabled = x.Sum(y => y.Number_Disabled),
                    Rural = x.Sum(y => y.Rural),
                    RSA = x.Key.RSA,
                    //StatusDescription = x.Key.StatusDescription
                });

            var appdash = from o in apps
                          select new GrantRSASummaries()
                          {
                              Apps = o.Apps,
                              RSA = o.RSA,
                              //StatusDescription = o.StatusDescription,
                              Learners = o.Learners,
                              TotalBudget = o.TotalBudget,
                              HDI = o.HDI,
                              Female = o.Female,
                              Youth = o.Youth,
                              Number_Disabled = o.Number_Disabled,
                              Rural = o.Rural
                          };

            var totalCount = apps.Count();

            return new PagedResultDto<GrantRSASummaries>(
                totalCount,
                appdash.ToList()
            );
        }

        public async Task<PagedResultDto<GrantProvinceSummaries>> GrantProvinceAwardDashboard(int WindowId)
        {
            var apps = (from p in _discProjRepository.GetAll()
                        join dpd in _discProjDetApprRepository.GetAll() on p.Id equals dpd.ProjectId
                        join da in _dgApprRepository.GetAll() on dpd.Id equals da.ApplicationId
                        join gc in _gcRepository.GetAll() on dpd.Id equals gc.ApplicationId
                        join w in _windowRepository.GetAll() on p.GrantWindowId equals w.Id
                        join o in _orgRepository.GetAll() on p.OrganisationId equals o.Id
                        join st in _dgStatusRepository.GetAll() on p.ProjectStatusID equals st.Id

                        select new
                        {
                            Id = dpd.Id,
                            Province = dpd.Province,
                            StatusDescription = st.StatusDesc,
                            Learners = (int)(dpd.GC_New + dpd.GC_Continuing),
                            Budget = (int)((dpd.GC_New + dpd.GC_Continuing) * dpd.GC_CostPerLearner),
                            HDI = dpd.HDI,
                            Female = dpd.Female,
                            Youth = dpd.Youth,
                            Number_Disabled = dpd.Number_Disabled,
                            Rural = dpd.Rural,
                            WindowId = w.Id,
                            ApplicationStatusId = da.ApprovalStatusId,
                            GCStatusId = gc.ApprovalStatusId
                        })
              .Where(x => x.WindowId == WindowId && x.ApplicationStatusId == 1 && x.GCStatusId == 1)
              .GroupBy(x => new { x.Province}) //, x.StatusDescription 
              .Select(x =>
                new
                {
                    Apps = x.Count(),
                    Learners = x.Sum(y => y.Learners),
                    TotalBudget = x.Sum(y => y.Budget),
                    HDI = x.Sum(y => y.HDI),
                    Female = x.Sum(y => y.Female),
                    Youth = x.Sum(y => y.Youth),
                    Number_Disabled = x.Sum(y => y.Number_Disabled),
                    Rural = x.Sum(y => y.Rural),
                    Province = x.Key.Province,
                    //StatusDescription = x.Key.StatusDescription
                });

            var appdash = from o in apps
                          select new GrantProvinceSummaries()
                          {
                              Apps = o.Apps,
                              Province = o.Province,
                              //StatusDescription = o.StatusDescription,
                              Learners = o.Learners,
                              TotalBudget = o.TotalBudget,
                              HDI = o.HDI,
                              Female = o.Female,
                              Youth = o.Youth,
                              Number_Disabled = o.Number_Disabled,
                              Rural = o.Rural
                          };

            var totalCount = apps.Count();

            return new PagedResultDto<GrantProvinceSummaries>(
                totalCount,
                appdash.ToList()
            );
        }

        public async Task<PagedResultDto<GrantChamberSummaries>> GrantChamberAwardDashboard(int WindowId)
        {
            var apps = (from p in _discProjRepository.GetAll()
                        join dpd in _discProjDetApprRepository.GetAll() on p.Id equals dpd.ProjectId
                        join da in _dgApprRepository.GetAll() on dpd.Id equals da.ApplicationId
                        join gc in _gcRepository.GetAll() on dpd.Id equals gc.ApplicationId
                        join w in _windowRepository.GetAll() on p.GrantWindowId equals w.Id
                        join o in _orgRepository.GetAll() on p.OrganisationId equals o.Id
                        join st in _dgStatusRepository.GetAll() on p.ProjectStatusID equals st.Id

                        select new
                        {
                            Id = dpd.Id,
                            Chamber = o.CHAMBER,
                            StatusDescription = st.StatusDesc,
                            Learners = (int)(dpd.GC_New + dpd.GC_Continuing),
                            Budget = (int)((dpd.GC_New + dpd.GC_Continuing) * dpd.GC_CostPerLearner),
                            HDI = dpd.HDI,
                            Female = dpd.Female,
                            Youth = dpd.Youth,
                            Number_Disabled = dpd.Number_Disabled,
                            Rural = dpd.Rural,
                            WindowId = w.Id,
                            ApplicationStatusId = da.ApprovalStatusId,
                            GCStatusId = gc.ApprovalStatusId
                        })
              .Where(x => x.WindowId == WindowId && x.ApplicationStatusId == 1 && x.GCStatusId == 1 && x.Chamber != null)
              .GroupBy(x => new { x.Chamber }) //, x.StatusDescription
              .Select(x =>
                new
                {
                    Apps = x.Count(),
                    Learners = x.Sum(y => y.Learners),
                    TotalBudget = x.Sum(y => y.Budget),
                    HDI = x.Sum(y => y.HDI),
                    Female = x.Sum(y => y.Female),
                    Youth = x.Sum(y => y.Youth),
                    Number_Disabled = x.Sum(y => y.Number_Disabled),
                    Rural = x.Sum(y => y.Rural),
                    Chamber = x.Key.Chamber,
                    //StatusDescription = x.Key.StatusDescription
                });

            var appdash = from o in apps
                          select new GrantChamberSummaries()
                          {
                              Apps = o.Apps,
                              Chamber = o.Chamber,
                              //StatusDescription = o.StatusDescription,
                              Learners = o.Learners,
                              TotalBudget = o.TotalBudget,
                              HDI = o.HDI,
                              Female = o.Female,
                              Youth = o.Youth,
                              Number_Disabled = o.Number_Disabled,
                              Rural = o.Rural
                          };

            var totalCount = apps.Count();

            return new PagedResultDto<GrantChamberSummaries>(
                totalCount,
                appdash.ToList()
            );
        }

    }
}
