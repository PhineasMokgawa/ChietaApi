using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using CHIETAMIS.DiscretionaryWindows.Dtos;
using Abp.Application.Services.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Configuration;
using Abp.Zero.Configuration;

namespace CHIETAMIS.DiscretionaryWindows
{
    public class DiscretionaryWindowAppService: CHIETAMISAppServiceBase
    {
        private readonly IRepository<DiscretionaryWindow> _windowRepository;
        private readonly IRepository<WindowParams> _windowParamRepository;
        private readonly IRepository<FocusArea> _focusAreaRepository;
        private readonly IRepository<ProjectType> _projTypeRepository;
        private readonly IRepository<AdminCriteria> _adminCritRepository;
        private readonly IRepository<EvaluationMethod> _evalMethodRepository;
        private readonly IRepository<FocusCriteriaEvaluation> _focCritEvalRepository;


        public DiscretionaryWindowAppService(IRepository<DiscretionaryWindow> windowRepository,
                                             IRepository<WindowParams> windowParamRepository,
                                             IRepository<FocusArea> focusAreaRepository,
                                             IRepository<ProjectType> projTypeRepository,
                                             IRepository<AdminCriteria> adminCritRepository,
                                             IRepository<EvaluationMethod> evalMethodRepository,
                                             IRepository<FocusCriteriaEvaluation> focCritEvalRepository)
        {
            _windowRepository = windowRepository;
            _windowParamRepository = windowParamRepository;
            _focusAreaRepository = focusAreaRepository;
            _projTypeRepository = projTypeRepository;
            _adminCritRepository = adminCritRepository;
            _evalMethodRepository = evalMethodRepository;
            _focCritEvalRepository = focCritEvalRepository;
        }

        public async Task CreateEditWindow(DiscretionaryWindowDto input)
        {
            var window = _windowRepository.GetAll().Where(a => a.Reference == input.Reference);

            if (window.Count() == 0)
            {
                var wind = ObjectMapper.Map<DiscretionaryWindow>(input);

                await _windowRepository.InsertAsync(wind);
            } else
            {
                var cWind = await _windowRepository.FirstOrDefaultAsync(window.First().Id);
                cWind.Description = input.Description;
                cWind.Title = input.Title;
                cWind.LaunchDte = input.LaunchDte;
                cWind.DeadlineTime = input.DeadlineTime;
                cWind.TotBdgt = input.TotBdgt;

                await _windowRepository.UpdateAsync(cWind);
            }
        }


        public async Task<PagedResultDto<DiscretionaryWindowForViewDto>> GetActiveWindows()
        {
            var window = _windowRepository.GetAll().Where(a => a.LaunchDte <= DateTime.Now && a.DeadlineTime >= DateTime.Now);


            var wind = await (from o in window
                              select new DiscretionaryWindowForViewDto
                              {
                                  DiscretionaryWindow = new DiscretionaryWindowDto
                                  {
                                      ProgCd = o.ProgCd,
                                      Reference = o.Reference,
                                      Description = o.Description,
                                      Title = o.Title,
                                      LaunchDte = o.LaunchDte,
                                      DeadlineTime = o.DeadlineTime,
                                      TotBdgt = o.TotBdgt,
                                      ActiveYN = o.ActiveYN,
                                      Id = o.Id

                                  }
                              }).ToListAsync();

            var totalCount = wind.Count();

            return new PagedResultDto<DiscretionaryWindowForViewDto>(
                totalCount,
                wind.ToList()
            );
        }

        public async Task<DiscretionaryWindowForViewDto> GetWindowById(int id)
        {
            var window = await _windowRepository.GetAsync(id);

            var output = new DiscretionaryWindowForViewDto { DiscretionaryWindow = ObjectMapper.Map<DiscretionaryWindowDto>(window) };
            //var output = ObjectMapper.Map<DiscretionaryWindowDto>(window);

            return output;
        }

        public async Task<PagedResultDto<WindowParamsPagedForeViewDto>> GetActiveWindowsParams_bk()
        {
            var window = _windowRepository.GetAll().Where(a => a.LaunchDte <= DateTime.Now && a.DeadlineTime >= DateTime.Now);
            var wind = new WindowParamsPagedForeViewDto();
            var windpar = new List<WindowParamsPagedForeViewDto>();

            foreach (var w in window)
            {

                var var1 = (from prms in _windowParamRepository.GetAll()
                            join focarea in _focusAreaRepository.GetAll() on prms.FocusAreaId equals focarea.Id
                            join projtype in _projTypeRepository.GetAll() on prms.ProjectTypeId equals projtype.Id
                            join subcat in _adminCritRepository.GetAll() on prms.SubCategoryId equals subcat.Id
                            join interv in _evalMethodRepository.GetAll() on prms.InterventionId equals interv.Id
                            select new
                            {
                                Id = prms.Id,
                                WindId = prms.DG_Window_Id,
                                ProjType = projtype.ProjTypDesc,
                                Title = "Title",
                                FocusArea = focarea.FocusAreaDesc,
                                SubCategory = subcat.AdminDesc,
                                Intervention = interv.EvalMthdDesc,
                                DG_Window = "Window",
                                ProjectType = "Type",
                                ActiveYN = true})
                        .Where(a => a.WindId == w.Id)
                        .ToList();

                var param = (from o in var1
                             select new PagedWindowParamsResultDto
                             {
                                 Id = o.Id,
                                 DG_Window = o.DG_Window,
                                 ProjType = o.ProjType,
                                 Title = o.Title,
                                 FocusArea = o.FocusArea,
                                 SubCategory = o.SubCategory,
                                 Intervention = o.Intervention,
                                 ActiveYN = o.ActiveYN,
                             }).ToList();

                var wind1 = new WindowParamsPagedForeViewDto();

                wind1.Reference = w.Reference;
                wind1.Description = w.Description;
                wind1.Title = w.Title;
                wind1.Id = w.Id;
                wind1.Parameters = param;

                windpar.Append(wind1);

            }
            var totalCount = windpar.Count();
            return new PagedResultDto<WindowParamsPagedForeViewDto>(
                totalCount,
                windpar.ToList()
            );
        }

        public async Task<PagedResultDto<DiscretionaryWindowForViewDto>> GetActiveBursaryWindows()
        {
            var windows = _windowRepository.GetAll().Where(a => a.LaunchDte <= DateTime.Now && a.DeadlineTime >= DateTime.Now && a.Description == "Lesedi Student Fund")
                .Take(1);
            var wins = (from o in windows
                select new DiscretionaryWindowForViewDto
                {
                    DiscretionaryWindow = new DiscretionaryWindowDto
                    {   Id = o.Id,
                        ProgCd = o.ProgCd,
                        Reference = o.Reference,
                        Description = o.Description,
                        Title = o.Title,
                        LaunchDte = o.LaunchDte,
                        DeadlineTime = o.DeadlineTime
                    }
            });

            var totalCount = wins.Count();
            return new PagedResultDto<DiscretionaryWindowForViewDto>(
                totalCount,
                wins.ToList()
            );
        }

        public async Task<PagedResultDto<PagedWindowParamsResultDto>> GetActiveWindowsParams()
        {
            var dgparam = await (from prms in _windowParamRepository.GetAll()
                                 join dg in _windowRepository.GetAll() on prms.DG_Window_Id equals dg.Id
                                 join projtype in _projTypeRepository.GetAll() on prms.ProjectTypeId equals projtype.Id
                                 join focarea in _focusAreaRepository.GetAll() on prms.FocusAreaId equals focarea.Id into fap from faps in fap.DefaultIfEmpty()
                                 join subcat in _adminCritRepository.GetAll() on prms.SubCategoryId equals subcat.Id into scp from scps in scp.DefaultIfEmpty()
                                 join interv in _evalMethodRepository.GetAll() on prms.InterventionId equals interv.Id into emp from emps in emp.DefaultIfEmpty()
                                 select new
                                 {
                                     WindowParams = prms,
                                     DG_Window = dg,
                                     Funding_Window = dg.Description,
                                     Title = dg.Title,
                                     ProjType = projtype.ProjTypDesc,
                                     FocusArea = faps.FocusAreaDesc,
                                     SubCategory = scps.AdminDesc,
                                     Intervention = emps.EvalMthdDesc,
                                     Id = prms.Id,
                                 })
                 .Where(a => a.DG_Window.LaunchDte <= DateTime.Now && a.DG_Window.DeadlineTime >= DateTime.Now)
                .ToListAsync();

            var param = (from o in dgparam
                         select new PagedWindowParamsResultDto
                         {
                             Id = o.WindowParams.Id,
                             DG_Window = o.DG_Window.ProgCd,
                             ProjType = o.ProjType,
                             Title = o.Title,
                             FocusArea = o.FocusArea,
                             SubCategory = o.SubCategory,
                             Intervention = o.Intervention,
                             ActiveYN = o.WindowParams.ActiveYN
                         });

            var totalCount = param.Count();

            return new PagedResultDto<PagedWindowParamsResultDto>(
                totalCount,
                param.ToList()
            );
        }

        public async Task CreateEditParams(WindowParamsDto input)
        {
            var para = _windowParamRepository.GetAll().Where(a => a.DG_Window_Id == input.DG_Window_Id && a.ProjectTypeId == input.ProjectTypeId && a.FocusAreaId == input.FocusAreaId && a.SubCategoryId == input.SubCategoryId);

            if (para.Count() == 0)
            {
                var wind = ObjectMapper.Map<WindowParams>(input);

                await _windowParamRepository.InsertAsync(wind);
            }
            else
            {
                var cPara = await _windowParamRepository.FirstOrDefaultAsync(para.First().Id);
                cPara.FocusAreaId = input.FocusAreaId;
                cPara.InterventionId = input.InterventionId;
                cPara.SubCategoryId = input.SubCategoryId;
                await _windowParamRepository.UpdateAsync(cPara);
            }
        }

        public async Task<PagedResultDto<PagedWindowParamsResultDto>> GetWindowParamsForView(int WinId)
        {
            var dgparam = await (from prms in _windowParamRepository.GetAll()
                                 join dg in _windowRepository.GetAll() on prms.DG_Window_Id equals dg.Id
                                 join projtype in _projTypeRepository.GetAll() on prms.ProjectTypeId equals projtype.Id
                                 join focarea in _focusAreaRepository.GetAll() on prms.FocusAreaId equals focarea.Id into fap from faps in fap.DefaultIfEmpty() 
                                 join subcat in _adminCritRepository.GetAll() on prms.SubCategoryId equals subcat.Id into scp from scps in scp.DefaultIfEmpty()
                                 join interv in _evalMethodRepository.GetAll() on prms.InterventionId equals interv.Id into emp from emps in emp.DefaultIfEmpty()
                                 select new
                                 {
                                     WindowParams = prms,
                                     DG_Window = dg.ProgCd,
                                     Funding_Window = dg.Description,
                                     Title = dg.Title,
                                     ProjType = projtype.ProjTypDesc,
                                     FocusArea = faps.FocusAreaDesc,
                                     SubCategory = scps.AdminDesc,
                                     Intervention = emps.EvalMthdDesc
                                 })

                .Where(a=>a.WindowParams.DG_Window_Id == WinId)
                .ToListAsync();

            var param = (from o in dgparam
                              select new PagedWindowParamsResultDto
                              {
                                    Id = o.WindowParams.Id,
                                    DG_Window = o.DG_Window,
                                    ProjType = o.ProjType,
                                    Title = o.Title,
                                    FocusArea = o.FocusArea,
                                    SubCategory = o.SubCategory,
                                    Intervention = o.Intervention,
                                    ActiveYN = o.WindowParams.ActiveYN
                              });

            var totalCount = param.Count();

            return new PagedResultDto<PagedWindowParamsResultDto>(
                totalCount,
                param.ToList()
            );
        }

        public async Task<PagedResultDto<WindowParamsForViewDto>> GetWindowParamsByWindow(int dgId)
        {
            var dgparam = _windowParamRepository.GetAll()
                .Where(a => a.DG_Window_Id == dgId);

            var param = await (from o in dgparam
                         select new WindowParamsForViewDto
                         {
                             WindowParams = new WindowParamsDto
                             {
                                 Id = o.Id,
                                 DG_Window_Id = o.DG_Window_Id,
                                 ProjectTypeId = o.ProjectTypeId,
                                 FocusAreaId = o.FocusAreaId,
                                 SubCategoryId = o.SubCategoryId,
                                 InterventionId = o.InterventionId,
                                 ActiveYN = o.ActiveYN,
                                 UserId = o.UserId
                             }
                         }).ToListAsync();

            var totalCount = param.Count();

            return new PagedResultDto<WindowParamsForViewDto>(
                totalCount,
                param.ToList()
            );
        }

        public async Task<PagedResultDto<DiscretionaryWindowForViewDto>> GetWindows()
        {
            var window = _windowRepository.GetAll();

            var wind = await (from o in window
                              select new DiscretionaryWindowForViewDto
                              {
                                  DiscretionaryWindow = new DiscretionaryWindowDto
                                  {
                                      ProgCd = o.ProgCd,
                                      Reference = o.Reference,
                                      Description = o.Description,
                                      Title = o.Title,
                                      LaunchDte = o.LaunchDte,
                                      DeadlineTime = o.DeadlineTime,
                                      TotBdgt = o.TotBdgt,
                                      ActiveYN = o.ActiveYN,
                                      Id = o.Id

                                  }
                              }).ToListAsync();

            var totalCount = wind.Count();

            return new PagedResultDto<DiscretionaryWindowForViewDto>(
                totalCount,
                wind.ToList()
            );
        }

        public async Task<PagedResultDto<ProjectTypeDto>> GetProjectType()
        {
            var tasks = _projTypeRepository.GetAll();

            var query = (from o in tasks
                         select new ProjectTypeDto
                         {
                             Id = o.Id,
                            ProjTypCD = o.ProjTypCD,
                             ProjTypDesc = o.ProjTypDesc
                         });

            var totalCount = await query.CountAsync();
            return new PagedResultDto<ProjectTypeDto>(totalCount, query.ToList());
        }

        public async Task<PagedResultDto<FocusAreaDto>> GetWinFocusArea(int progType, int winparid)
        {
            var winparams = _windowParamRepository.GetAll().Where(a => a.Id == winparid).FirstOrDefault();
            if (winparams.FocusAreaId > 0) { 
                var tasks = await (from fa in _focusAreaRepository.GetAll()
                        join fcr in _focCritEvalRepository.GetAll() on fa.Id equals fcr.FocusAreaKey
                        select new
                        {
                            FocusArea = fa,
                            ProjectTypeId = fcr.ProjTypCD,
                            ActiveYN = fa.ActiveYN
                        })
                        .Where(a=>a.ProjectTypeId == progType && a.FocusArea.Id == winparams.FocusAreaId && a.ActiveYN)
                        .Distinct()
                        .ToListAsync();

                var query = (from o in tasks
                             select new FocusAreaDto
                             {
                                 Id = o.FocusArea.Id,
                                 FocusAreaCd = o.FocusArea.FocusAreaCd,
                                 FocusAreaDesc = o.FocusArea.FocusAreaDesc,
                                 FundinglImit = o.FocusArea.FundinglImit
                             }).Distinct();

                var totalCount = query.Count();
                return new PagedResultDto<FocusAreaDto>(totalCount, query.ToList());
            } else
            {
                var tasks = await (from fa in _focusAreaRepository.GetAll()
                    join fcr in _focCritEvalRepository.GetAll() on fa.Id equals fcr.FocusAreaKey
                    select new
                    {
                        FocusArea = fa,
                        ProjectTypeId = fcr.ProjTypCD,
                        ActiveYN = fa.ActiveYN
                    })
                .Where(a => a.ProjectTypeId == progType && a.ActiveYN)
                .Distinct()
                .ToListAsync();

                var query = (from o in tasks
                             select new FocusAreaDto
                             {
                                 Id = o.FocusArea.Id,
                                 FocusAreaCd = o.FocusArea.FocusAreaCd,
                                 FocusAreaDesc = o.FocusArea.FocusAreaDesc,
                                 FundinglImit = o.FocusArea.FundinglImit
                             }).Distinct()
                               .OrderBy(a=>a.FocusAreaDesc);

                var totalCount = query.Count();
                return new PagedResultDto<FocusAreaDto>(totalCount, query.ToList());
            }
        }

        public async Task<PagedResultDto<FocusAreaDto>> GetFocusArea(int projType)
        {
            var tasks = await (from fa in _focusAreaRepository.GetAll()
                                join fcr in _focCritEvalRepository.GetAll() on fa.Id equals fcr.FocusAreaKey
                                select new
                                {
                                    FocusArea = fa,
                                    ProjectTypeId = fcr.ProjTypCD,
                                    ActiveYN = fa.ActiveYN
                                })
                    .Where(a => a.ProjectTypeId == projType && a.ActiveYN)
                    .Distinct()
                    .ToListAsync();

            var query = (from o in tasks
                            select new FocusAreaDto
                            {
                                Id = o.FocusArea.Id,
                                FocusAreaCd = o.FocusArea.FocusAreaCd,
                                FocusAreaDesc = o.FocusArea.FocusAreaDesc,
                                FundinglImit = o.FocusArea.FundinglImit
                            }).Distinct()
                              .OrderBy(a => a.FocusAreaDesc);

            var totalCount = query.Count();
            return new PagedResultDto<FocusAreaDto>(totalCount, query.ToList());
            
        }

        public async Task<PagedResultDto<AdminCriteriaDto>> GetAdminCrit(int projType, int focusId)
        {
            var tasks = await (from ad in _adminCritRepository.GetAll()
                               join fcr in _focCritEvalRepository.GetAll() on ad.Id equals fcr.AdminCritKey
                               select new
                               {
                                   AdminCriteria = ad,
                                   ProjectTypeId = fcr.ProjTypCD,
                                   FocusAreaId = fcr.FocusAreaKey,
                                   ActiveYN = fcr.ActiveYN
                               })
                        .Where(a => a.ProjectTypeId == projType && a.FocusAreaId == focusId && a.ActiveYN)
                        .Distinct()
                        .ToListAsync();

            var query = (from o in tasks
                         select new AdminCriteriaDto
                         {
                             Id = o.AdminCriteria.Id,
                             AdminDesc = o.AdminCriteria.AdminDesc
                         }).Distinct()
                           .OrderBy(a => a.AdminDesc);

            var totalCount = query.Count();
            return new PagedResultDto<AdminCriteriaDto>(totalCount, query.ToList());
        }

        public async Task<PagedResultDto<AdminCriteriaDto>> GetWinAdminCrit(int projType, int focusId, int winparid)
        {
            var winparams = _windowParamRepository.GetAll().Where(a => a.Id == winparid).FirstOrDefault();
            if (winparams.SubCategoryId > 0)
            {
                var tasks = await (from ad in _adminCritRepository.GetAll()
                                   join fcr in _focCritEvalRepository.GetAll() on ad.Id equals fcr.AdminCritKey
                                   join ev in _evalMethodRepository.GetAll() on fcr.EvalMthdCd equals ev.Id
                                   select new
                                   {
                                       AdminCriteria = ad,
                                       ProjectTypeId = fcr.ProjTypCD,
                                       FocusAreaId = fcr.FocusAreaKey,
                                       ActiveYN = ev.ActiveYN
                                   })
                        .Where(a => a.ProjectTypeId == projType && a.FocusAreaId == focusId 
                        && a.AdminCriteria.Id == winparams.SubCategoryId && a.ActiveYN)
                        .Distinct()
                        .ToListAsync();

                var query = (from o in tasks
                             select new AdminCriteriaDto
                             {
                                 Id = o.AdminCriteria.Id,
                                 AdminDesc = o.AdminCriteria.AdminDesc
                             }).Distinct()
                               .OrderBy(a => a.AdminDesc);

                var totalCount = query.Count();
                return new PagedResultDto<AdminCriteriaDto>(totalCount, query.ToList());
            } else
            {
                var tasks = await (from ad in _adminCritRepository.GetAll()
                                   join fcr in _focCritEvalRepository.GetAll() on ad.Id equals fcr.AdminCritKey
                                   select new
                                   {
                                       AdminCriteria = ad,
                                       ProjectTypeId = fcr.ProjTypCD,
                                       FocusAreaId = fcr.FocusAreaKey,
                                       ActiveYN = fcr.ActiveYN
                                   })
                        .Where(a => a.ProjectTypeId == projType && a.FocusAreaId == focusId && a.ActiveYN)
                        .Distinct()
                        .ToListAsync();

                var query = (from o in tasks
                             select new AdminCriteriaDto
                             {
                                 Id = o.AdminCriteria.Id,
                                 AdminDesc = o.AdminCriteria.AdminDesc
                             }).Distinct()
                               .OrderBy(a => a.AdminDesc);

                var totalCount = query.Count();
                return new PagedResultDto<AdminCriteriaDto>(totalCount, query.ToList());
            }
        }

        public async Task<PagedResultDto<EvaluationMethodDto>> GetEvalMeth(int projType, int focusId, int critId)
        {
            var tasks = await ( from eval in _evalMethodRepository.GetAll()
                                join fcr in _focCritEvalRepository.GetAll() on eval.Id equals fcr.EvalMthdCd
                                select new
                                {
                                    EvaluationCriteria = eval,
                                    ProjectTypeId = fcr.ProjTypCD,
                                    FocusAreaId = fcr.FocusAreaKey,
                                    AdminCritId = fcr.AdminCritKey,
                                    ActiveYN = fcr.ActiveYN, 
                                    AllowContinuing = fcr.AllowContinuing,
                                    AllowNew = fcr.AllowNew

                                })
                        .Where(a => a.ProjectTypeId == projType && a.FocusAreaId == focusId && a.AdminCritId == critId && a.ActiveYN)
                        .Distinct()
                        .ToListAsync();

            var query = (from o in tasks
                         select new EvaluationMethodDto
                         {
                             Id = o.EvaluationCriteria.Id,
                             EvalMthdCd = o.EvaluationCriteria.EvalMthdCd,
                             EvalMthdDesc = o.EvaluationCriteria.EvalMthdDesc,
                             AllowContinuing = o.AllowContinuing,
                             AllowNew = o.AllowNew
                         }).Distinct()
                           .OrderBy(a => a.EvalMthdDesc);

            var totalCount = query.Count();
            return new PagedResultDto<EvaluationMethodDto>(totalCount, query.ToList());
        }
    }
}
