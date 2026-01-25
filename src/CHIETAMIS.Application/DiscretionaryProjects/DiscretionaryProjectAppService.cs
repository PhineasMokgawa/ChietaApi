using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Abp.Linq.Extensions;
using Abp.Localization;
using Abp.Runtime.Session;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Abp.Application.Services;
using CHIETAMIS.DiscretionaryProjects.Dtos;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Application.Services.Dto;
using Abp.ObjectMapping;
using System.Text;
using Abp.Configuration;
using Abp.Zero.Configuration;
using CHIETAMIS.Organisations;
using CHIETAMIS.Organisations.Dtos;
using CHIETAMIS.Documents;
using CHIETAMIS.Documents.Dtos;
using CHIETAMIS.Sdfs;
using CHIETAMIS.Sdfs.Dtos;
using CHIETAMIS.DiscretionaryProjects;
using CHIETAMIS.DiscretionaryWindows;
using CHIETAMIS.UnitStandards;
using CHIETAMIS.UnitStandards.Dtos;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Configuration;
using CHIETAMIS.Authorization.Users;
using CHIETAMIS.Lookups;
using CHIETAMIS.Commons.DTOs;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto;

namespace CHIETAMIS.DiscretionaryProjects
{
    public class DiscretionaryProjectAppService : CHIETAMISAppServiceBase
    {
        private readonly IUserEmailer _userEmailer;
        private readonly IRepository<ProjectNotification> _projectNotificationRepository;

        private readonly IRepository<DiscretionaryProject> _discProjRepository;
        private readonly IRepository<Organisation> _orgRepository;
        private readonly IRepository<Organisation_Sdf> _orgsdfRepository;
        private readonly IRepository<DiscretionaryStatus> _discStatusRepository;
        private readonly IRepository<DiscretionaryWindow> _windowRepository;
        private readonly IRepository<WindowParams> _windowParamRepository;
        private readonly IRepository<ProjectType> _projTypeRepository;
        private readonly IRepository<FocusArea> _focusAreaRepository;
        private readonly IRepository<AdminCriteria> _adminCritRepository;
        private readonly IRepository<EvaluationMethod> _evalMethodRepository;
        private readonly IRepository<DiscretionaryProjectDetails> _discProjDetRepository;
        private readonly IRepository<DiscretionaryProjectDetailsApproval> _discProjDetApprovalRepository;
        private readonly IRepository<DiscretionaryProjectUS> _discProjUSRepository;
        private readonly IRepository<UnitStandard> _usRepository;
        private readonly IRepository<BankDetails> _bankRepository;
        private readonly IRepository<OrganisationPhysicalAddress> _addressRepository;
        private readonly IRepository<Document> _docRepository;
        private readonly IRepository<DiscretionaryGCApproval> _discGCApprovalRepository;
        public DiscretionaryProjectAppService(IRepository<DiscretionaryProject> dicprojRepository,
                                              IRepository<Organisation> orgRepository,
                                              IRepository<Organisation_Sdf> orgsdfRepository,
                                              IRepository<DiscretionaryStatus> discStatusRepository,
                                              IRepository<DiscretionaryWindow> windowRepository,
                                              IRepository<WindowParams> windowParamRepository,
                                              IRepository<ProjectType> projTypeRepository,
                                              IRepository<FocusArea> focusAreaRepository,
                                              IRepository<AdminCriteria> adminCritRepository,
                                              IRepository<EvaluationMethod> evalMethodRepository,
                                              IRepository<DiscretionaryProjectDetails> discprojDetRepository,
                                              IRepository<DiscretionaryProjectDetailsApproval> discprojDetApprovalRepository,
                                              IRepository<DiscretionaryProjectUS> discProjUSRepository,
                                              IRepository<DiscretionaryGCApproval> discGCApprovalRepository,
                                              IRepository<UnitStandard> usRepository,
                                              IRepository<BankDetails> bankRepository,
                                              IRepository<OrganisationPhysicalAddress> addressRepository,
                                              IRepository<Document> docRepository,
                                              IUserEmailer userEmailer, IRepository<ProjectNotification> projectNotificationRepository)
        {
            _discProjRepository = dicprojRepository;
            _orgRepository = orgRepository;
            _orgsdfRepository = orgsdfRepository;
            _discStatusRepository = discStatusRepository;
            _windowRepository = windowRepository;
            _windowParamRepository = windowParamRepository;
            _projTypeRepository = projTypeRepository;
            _focusAreaRepository = focusAreaRepository;
            _adminCritRepository = adminCritRepository;
            _evalMethodRepository = evalMethodRepository;
            _discProjDetRepository = discprojDetRepository;
            _discProjDetApprovalRepository = discprojDetApprovalRepository;
            _discProjUSRepository = discProjUSRepository;
            _discGCApprovalRepository = discGCApprovalRepository;
            _usRepository = usRepository;
            _bankRepository = bankRepository;
            _addressRepository = addressRepository;
            _docRepository = docRepository;
            _userEmailer = userEmailer;
            _projectNotificationRepository = projectNotificationRepository; 
        }

        public async Task CreateEditApplication(DiscretionaryProjectDto input)
        {
            var appl = _discProjRepository.GetAll().Where(a => a.WindowParamId == input.WindowParamId && a.OrganisationId == input.OrganisationId);

            if (appl.Count() == 0)
            {
                var prms = _windowParamRepository.Get(input.WindowParamId);
                var dg = _windowRepository.Get(prms.DG_Window_Id);
                input.GrantWindowId = prms.DG_Window_Id;
                input.ProjectTypeId = prms.ProjectTypeId;
                input.ProjShortNam = dg.Description;
                input.ProjectNam = dg.Description;
                input.DteCreated = DateTime.Now;
                input.CaptureDte = DateTime.Now;
                input.ProjectStatDte = dg.LaunchDte;

                var app = ObjectMapper.Map<DiscretionaryProject>(input);

                await _discProjRepository.InsertAsync(app);
            } else
            {
                throw new InvalidOperationException("Dupicate entry detected.");
            }
        }

        public async Task AssignRSA(int id, int rsaid, int userid)
        {
            var appl = _discProjRepository.GetAll().Where(a => a.Id == id);

            if (appl.Count() != 0)
            { 
                var cappl = await _discProjRepository.FirstOrDefaultAsync(appl.First().Id);
                //cappl.DteUpd = DateTime.Now;
                //cappl.UsrUpd = input.UsrUpd;
                cappl.RSAId = rsaid;
                cappl.RSAAssignedBy = userid;
                cappl.RSAAssignDate = DateTime.Now;
                //cappl.ProjectStatusID = 247;

                await _discProjRepository.UpdateAsync(cappl);
            }
        }

        public async Task<PagedResultDto<DiscretionaryProjectForViewDto>> GetOrgProjects(int OrganisationId)
        {

            var discprojs = await (from discgrant in _discProjRepository.GetAll()
                                   join org in _orgRepository.GetAll() on discgrant.OrganisationId equals org.Id
                                   join orgsdf in _orgsdfRepository.GetAll() on discgrant.OrganisationId equals orgsdf.OrganisationId
                                   join stat in _discStatusRepository.GetAll() on discgrant.ProjectStatusID equals stat.Id
                                   join wind in _windowRepository.GetAll() on discgrant.GrantWindowId equals wind.Id
                                   join prms in _windowParamRepository.GetAll() on discgrant.WindowParamId equals prms.Id
                                   join projtype in _projTypeRepository.GetAll() on discgrant.ProjectTypeId equals projtype.Id
                                   join focarea in _focusAreaRepository.GetAll() on prms.FocusAreaId equals focarea.Id into foc
                                   from focs in foc.DefaultIfEmpty()
                                   join subcat in _adminCritRepository.GetAll() on prms.SubCategoryId equals subcat.Id into sub
                                   from subs in sub.DefaultIfEmpty()
                                   join interv in _evalMethodRepository.GetAll() on prms.InterventionId equals interv.Id into inter
                                   from inters in inter.DefaultIfEmpty()
                                   select new
                                   {
                                       SDLNo = org.SDL_No,
                                       Organisation_Name = org.Organisation_Name,
                                       OrganisationId = org.Id,
                                       ProjectId = discgrant.Id,
                                       ProjectStatDte = discgrant.ProjectStatDte,
                                       Title = wind.Title,
                                       ProjectEndDate = wind.DeadlineTime,
                                       ProjShortNam = discgrant.ProjShortNam,
                                       ProjectNam = discgrant.ProjectNam,
                                       SubmissionDte = discgrant.SubmissionDte,
                                       SdfId = orgsdf.SdfId,
                                       OrgSdfId = orgsdf.Id,
                                       Status = stat.StatusDesc,
                                       ProjType = projtype.ProjTypDesc,
                                       FocusArea = focs.FocusAreaDesc,
                                       SubCategory = subs.AdminDesc,
                                       Intervention = inters.EvalMthdDesc,
                                       Id = discgrant.Id
                                   })
                    .Where(a => a.OrganisationId == OrganisationId)
                    .OrderByDescending(a=>a.Id)
                    .ToListAsync();


            var discproject = from o in discprojs
                select new DiscretionaryProjectForViewDto()
                {
                    DiscretionaryProject = new DiscretionaryProjectOutputDto
                    {
                        SDLNo = o.SDLNo,
                        Organisation_Name = o.Organisation_Name,
                        OrganisationId = o.OrganisationId,
                        SdfId = o.SdfId,
                        ProjectId = o.ProjectId,
                        Title = o.Title,
                        ProjType = o.ProjType,
                        FocusArea = o.FocusArea,
                        SubCategory = o.SubCategory,
                        ProjectStatDte = o.ProjectStatDte,
                        ProjectEndDate = o.ProjectEndDate,
                        ProjectStatus = o.Status,
                        ProjShortNam = o.ProjShortNam,
                        ProjectNam = o.ProjectNam,
                        SubmissionDte = o.SubmissionDte,
                        Id = o.Id
                    }
            };

            var totalCount = discproject.Count();

            return new PagedResultDto<DiscretionaryProjectForViewDto>(
                totalCount,
                discproject.ToList()
            );
        }


        public async Task<PagedResultDto<ProjectTimelineDto>> GetProjectTimeline(int OrganisationId)
        {
            var projects = await (
                from discgrant in _discProjRepository.GetAll()
                join org in _orgRepository.GetAll() on discgrant.OrganisationId equals org.Id
                join stat in _discStatusRepository.GetAll() on discgrant.ProjectStatusID equals stat.Id
                join wind in _windowRepository.GetAll() on discgrant.GrantWindowId equals wind.Id
                where discgrant.OrganisationId == OrganisationId
                orderby discgrant.ProjectStatDte descending
                select new
                {
                    discgrant.Id,
                    discgrant.ProjectNam,
                    discgrant.ProjShortNam,
                    discgrant.ProjectStatusID,
                    discgrant.ProjectStatDte,
                    OrganisationName = org.Organisation_Name,
                    org.SDL_No,
                    StatusDesc = stat.StatusDesc,
                    WindowTitle = wind.Title,
                    ProjectEndDate = wind.DeadlineTime
                }
            ).ToListAsync();

            var timeline = projects.Select(p =>
            {
                var statusId = p.ProjectStatusID;


                return new ProjectTimelineDto
                {
                    ProjectId = p.Id,
                    ProjectName = p.ProjectNam,
                    ProjectShortName = p.ProjShortNam,

                    OrganisationName = p.OrganisationName,
                    SDLNo = p.SDL_No,

                    Status = p.StatusDesc,
                    StatusChangedDate = p.ProjectStatDte ?? DateTime.Now,

                    WindowTitle = p.WindowTitle,
                    ProjectEndDate = p.ProjectEndDate,

                    // 🔹 Status flags
                    ApplicationStarted = statusId >= 9,
                    ApplicationSubmitted = statusId >= 10,
                    RsaReviewCompleted = statusId >= 239,

                    GrantsCommitteeReview =
                        statusId >= 239 &&
                        statusId != 246 &&
                        statusId != 89,

                    EvaluationCompleted = statusId == 246,
                    RejectedAfterAssessment = statusId == 89,

                    IsFinalStage = statusId == 246 || statusId == 89,

                    CurrentStage =
                        statusId == 9 ? "Application Started" :
                        statusId == 10 ? "Application Submitted" :
                        statusId == 239 ? "RSA Review Completed" :
                        statusId == 246 ? "Evaluation Completed" :
                        statusId == 89 ? "Rejected After Full Assessment" :
                                          "Grants Committee Review"
                };
            }).ToList();

            return new PagedResultDto<ProjectTimelineDto>(
                timeline.Count,
                timeline
            );
        }

        public async Task<ProjectNotificationDto> CreateProjectNotificationAsync(CreateProjectNotificationDto input, int previousStatusId)
        {
            // Fetch project
            var project = await _discProjRepository.GetAll()
                                    .Where(p => p.Id == input.ProjectId)
                                    .Select(p => new { p.ProjectNam, p.ProjectStatusID })
                                    .FirstOrDefaultAsync();

            if (project == null)
                throw new UserFriendlyException("Project not found.");

            // Helper function to convert statusId to friendly text
            string GetStatusMessage(int statusId) => statusId switch
            {
                9 => "Application started",
                10 => "Application submitted",
                239 => "RSA review completed",
                246 => "Evaluation completed",
                89 => "Rejected after full assessment",
                _ => "Status updated"
            };

            var previousStatus = GetStatusMessage(previousStatusId);
            var newStatus = GetStatusMessage(project.ProjectStatusID);

            // Build message
            string message = $"Project '{project.ProjectNam}' status has been updated from '{previousStatus}' to '{newStatus}'.";

            // Create the notification
            var notification = new ProjectNotification
            {
                ProjectId = input.ProjectId,
                UserId = input.UserId,
                Message = message,
                CreationTime = DateTime.Now,
                IsRead = false
            };

            // Insert into repository
            await _projectNotificationRepository.InsertAsync(notification);
            await CurrentUnitOfWork.SaveChangesAsync(); // commit to DB

            // Return DTO
            return new ProjectNotificationDto
            {
                Id = notification.Id,
                ProjectId = notification.ProjectId,
                ProjectName = project.ProjectNam ?? "",
                UserId = notification.UserId,
                Message = notification.Message,
                CreatedDate = notification.CreationTime
            };
        }

        public async Task<List<ProjectNotificationDto>> GetUserNotificationsAsync(string userId)
        {
            // Efficient single-query join with projects
            var notifications = await (from n in _projectNotificationRepository.GetAll()
                                       join p in _discProjRepository.GetAll() on n.ProjectId equals p.Id
                                       where n.UserId == userId
                                       orderby n.CreatedDate descending
                                       select new ProjectNotificationDto
                                       {
                                           Id = n.Id,
                                           ProjectId = n.ProjectId,
                                           ProjectName = p.ProjectNam,
                                           UserId = n.UserId,
                                           Message = n.Message,
                                           CreatedDate = n.CreatedDate
                                       }).ToListAsync();

            return notifications;
        }

        public async Task NotifyUserOnStatusChangeAsync(NotifyStatusChangeDto input)
        {
            var project = await _discProjRepository.GetAll()
                                .FirstOrDefaultAsync(p => p.Id == input.ProjectId);

            if (project == null)
                throw new UserFriendlyException("Project not found");

            var status = await _discStatusRepository.GetAll()
                                .FirstOrDefaultAsync(s => s.Id == input.NewStatusId);

            if (status == null)
                throw new UserFriendlyException("Status not found");

            var notification = new ProjectNotification
            {
                ProjectId = project.Id,
                UserId = input.UserId,
                Message = $"Project '{project.ProjectNam}' status has been updated to '{status.StatusDesc}'",
                CreatedDate = DateTime.Now
            };

            await _projectNotificationRepository.InsertAsync(notification);
            await CurrentUnitOfWork.SaveChangesAsync();
        }


        public async Task<PagedResultDto<DiscretionaryProjectForViewDto>> GetProjectsLinkedtoSdf(DiscretionaryProjectRequestDto input)
        {
            var discprojs = await (from discgrant in _discProjRepository.GetAll()
                join org in _orgRepository.GetAll() on discgrant.OrganisationId equals org.Id
                join orgsdf in _orgsdfRepository.GetAll() on discgrant.OrganisationId equals orgsdf.OrganisationId
                join stat in _discStatusRepository.GetAll() on discgrant.ProjectStatusID equals stat.Id
                join wind in _windowRepository.GetAll() on discgrant.GrantWindowId equals wind.Id
                join prms in _windowParamRepository.GetAll() on discgrant.WindowParamId equals prms.Id
                join projtype in _projTypeRepository.GetAll() on discgrant.ProjectTypeId equals projtype.Id
                join focarea in _focusAreaRepository.GetAll() on prms.FocusAreaId equals focarea.Id into foc
                from focs in foc.DefaultIfEmpty()
                join subcat in _adminCritRepository.GetAll() on prms.SubCategoryId equals subcat.Id into sub
                from subs in sub.DefaultIfEmpty()
                join interv in _evalMethodRepository.GetAll() on prms.InterventionId equals interv.Id into inter
                from inters in inter.DefaultIfEmpty()
                select new
                {
                    SDLNo = org.SDL_No,
                    OrganisationId = org.Id,
                    DiscretionaryProject = discgrant,
                    Title = wind.Title,
                    //Params = prms,
                    SdfId = orgsdf.SdfId,
                    OrgSdfId = orgsdf.Id,
                    Status = stat.StatusDesc,
                    ProjType = projtype.ProjTypDesc,
                    FocusArea = focs.FocusAreaDesc,
                    SubCategory = subs.AdminDesc,
                    Intervention = inters.EvalMthdDesc,
                })
            .Where(a => a.OrgSdfId == input.OrgSdfId)
            .ToListAsync();




            var discproject = from o in discprojs
                              select new DiscretionaryProjectForViewDto()
                              {
                                  DiscretionaryProject = new DiscretionaryProjectOutputDto
                                  {
                                      SDLNo = o.SDLNo,
                                      OrganisationId = o.OrganisationId,
                                      SdfId = o.SdfId,
                                      ProjectId = o.DiscretionaryProject.Id,
                                      Title = o.Title,
                                      ProjType = o.ProjType,
                                      FocusArea = o.FocusArea,
                                      SubCategory = o.SubCategory,
                                      ProjectStatDte = o.DiscretionaryProject.ProjectStatDte,
                                      ProjectStatus = o.Status,
                                      ProjShortNam = o.DiscretionaryProject.ProjShortNam,
                                      ProjectNam = o.DiscretionaryProject.ProjectNam,
                                      SubmissionDte = o.DiscretionaryProject.SubmissionDte,
                                      Id = o.DiscretionaryProject.Id
                                  }
                              };

            var totalCount = discproject.Count();

            return new PagedResultDto<DiscretionaryProjectForViewDto>(
                totalCount,
                discproject.ToList()
            );
        }

        public async Task<PagedResultDto<DiscretionaryProjectForViewDto>> GetProjectForView(int Id)
        {

            var discprojs = await (from discgrant in _discProjRepository.GetAll()
                                   join org in _orgRepository.GetAll() on discgrant.OrganisationId equals org.Id
                                   join orgsdf in _orgsdfRepository.GetAll() on discgrant.OrganisationId equals orgsdf.OrganisationId
                                   join stat in _discStatusRepository.GetAll() on discgrant.ProjectStatusID equals stat.Id
                                   join wind in _windowRepository.GetAll() on discgrant.GrantWindowId equals wind.Id
                                   //join prms in _windowParamRepository.GetAll() on discgrant.WindowParamId equals prms.Id
                                   join dpd in _discProjDetRepository.GetAll() on discgrant.Id equals dpd.ProjectId
                                   join projtype in _projTypeRepository.GetAll() on dpd.ProjectTypeId equals projtype.Id
                                   join focarea in _focusAreaRepository.GetAll() on dpd.FocusAreaId equals focarea.Id
                                   join subcat in _adminCritRepository.GetAll() on dpd.SubCategoryId equals subcat.Id
                                   join interv in _evalMethodRepository.GetAll() on dpd.InterventionId equals interv.Id
                                   select new
                                   {
                                       SDLNo = org.SDL_No,
                                       OrganisationId = org.Id,
                                       ProjectId = discgrant.Id,
                                       ProjectStatDte = discgrant.ProjectStatDte,
                                       ProjectEndDate = wind.DeadlineTime,
                                       Title = wind.Title,
                                       //Params = prms,
                                       SdfId = orgsdf.SdfId,
                                       OrgSdfId = orgsdf.Id,
                                       Status = stat.StatusDesc,
                                       ProjType = projtype.ProjTypDesc,
                                       FocusArea = focarea.FocusAreaDesc,
                                       FundingLimit = focarea.FundinglImit,
                                       SubCategory = subcat.AdminDesc,
                                       Intervention = interv.EvalMthdDesc,
                                       WindowId = wind.Id,
                                       ContractStartDate = wind.ContractStartDate,
                                       ContractEndDate = wind.ContractEndDate,
                                       ProjShortNam = discgrant.ProjShortNam,
                                       ProjectNam = discgrant.ProjectNam,
                                       SubmissionDte = discgrant.SubmissionDte,
                                       RSAId = discgrant.RSAId,
                                       Id = discgrant.Id
                                   })
                    .Where(a => a.Id == Id)
                    .ToListAsync();

            var discproject = from o in discprojs
                              select new DiscretionaryProjectForViewDto()
                              {
                                  DiscretionaryProject = new DiscretionaryProjectOutputDto
                                  {
                                      SDLNo = o.SDLNo,
                                      OrganisationId = o.OrganisationId,
                                      SdfId = o.SdfId,
                                      ProjectId = o.ProjectId,
                                      Title = o.Title,
                                      ProjType = o.ProjType,
                                      FocusArea = o.FocusArea,
                                      SubCategory = o.SubCategory,
                                      FundingLimit = o.FundingLimit,
                                      ProjectStatDte = o.ProjectStatDte,
                                      ProjectEndDate = o.ProjectEndDate,
                                      ProjectStatus = o.Status,
                                      ProjShortNam = o.ProjShortNam,
                                      ProjectNam = o.ProjectNam,
                                      SubmissionDte = o.SubmissionDte,
                                      RSAId = o.RSAId,
                                      WindowId = o.WindowId,
                                      ContractStartDate = o.ContractStartDate,
                                      ContractEndDate = o.ContractEndDate,
                                      Id = o.Id
                                  }
                              };

            var totalCount = discproject.Count();

            return new PagedResultDto<DiscretionaryProjectForViewDto>(
                totalCount,
                discproject.ToList()
            );
        }

        public async Task<PagedResultDto<DiscretionaryProjectForViewDto>> GetProjectsForWindow(int first, int rows)
        {
            var discprojs = await (from discgrant in _discProjRepository.GetAll()
                                   join org in _orgRepository.GetAll() on discgrant.OrganisationId equals org.Id
                                   join orgsdf in _orgsdfRepository.GetAll() on discgrant.OrganisationId equals orgsdf.OrganisationId
                                   join stat in _discStatusRepository.GetAll() on discgrant.ProjectStatusID equals stat.Id
                                   join wind in _windowRepository.GetAll() on discgrant.GrantWindowId equals wind.Id
                                   join prms in _windowParamRepository.GetAll() on discgrant.WindowParamId equals prms.Id
                                   join projtype in _projTypeRepository.GetAll() on discgrant.ProjectTypeId equals projtype.Id
                                   join focarea in _focusAreaRepository.GetAll() on prms.FocusAreaId equals focarea.Id into foc
                                   from focs in foc.DefaultIfEmpty()
                                   join subcat in _adminCritRepository.GetAll() on prms.SubCategoryId equals subcat.Id into sub
                                   from subs in sub.DefaultIfEmpty()
                                   join interv in _evalMethodRepository.GetAll() on prms.InterventionId equals interv.Id into inter
                                   from inters in inter.DefaultIfEmpty()
                                   select new
                    {
                        Id = discgrant.Id,
                        SDLNo = org.SDL_No,
                        Organisation_Name = org.Organisation_Name,
                        OrganisationId = org.Id,
                        SdfId = orgsdf.SdfId,
                        ProjectId = discgrant.Id,
                        Title = wind.Title,
                        OrgSdfId = orgsdf.Id,
                        Status = stat.StatusDesc,
                        ProjType = projtype.ProjTypDesc,
                        FocusArea = focs.FocusAreaDesc,
                        FundingLimit = focs.FundinglImit,
                        SubCategory = subs.AdminDesc,
                        ProjectStatDte = discgrant.ProjectStatDte,
                        ProjShortNam = discgrant.ProjShortNam,
                        ProjectNam = discgrant.ProjectNam,
                        SubmissionDte = discgrant.SubmissionDte,
                        Intervention = inters.EvalMthdDesc,
                    })
                    .OrderByDescending(a=>a.Id)
                    .Skip(first)
                    .Take(rows)
                    .ToListAsync();

            var totalCount = _discProjDetRepository.GetAll().Count();

            var discproject = from o in discprojs
                              select new DiscretionaryProjectForViewDto()
                              {
                                  DiscretionaryProject = new DiscretionaryProjectOutputDto
                                  {
                                      SDLNo = o.SDLNo,
                                      Organisation_Name = o.Organisation_Name,
                                      OrganisationId = o.Id,
                                      SdfId = o.SdfId,
                                      ProjectId = o.ProjectId,
                                      Title = o.Title,
                                      ProjType = o.ProjType,
                                      FocusArea = o.FocusArea,
                                      FundingLimit = o.FundingLimit,
                                      SubCategory = o.SubCategory,
                                      ProjectStatDte = o.ProjectStatDte,
                                      ProjectStatus = o.Status,
                                      ProjShortNam = o.ProjShortNam,
                                      ProjectNam = o.ProjectNam,
                                      SubmissionDte = o.SubmissionDte,
                                      Id = o.Id
                                  }
                              };

            return new PagedResultDto<DiscretionaryProjectForViewDto>(
                totalCount,
            discproject.ToList()
            );
        }

        public async Task<PagedResultDto<DiscretionaryProjectForViewDto>> GetProjectsForWindowLazy(int first, int rows, string SDLNoFilter, string SDLNoFilterMode, string OrganisationNameFilter,
        string OrganisationNameFilterMode, string ProjTypeFilter, string ProjTypeFilterMode, string TitleFilter, string TitleFilterMode,
        string FocusAreaFilter, string FocusAreaFilterMode, string ProjectStatusFilter, string ProjectStatusFilterMode)
        {
            var discprojs = await (from discgrant in _discProjRepository.GetAll()
                                   join org in _orgRepository.GetAll() on discgrant.OrganisationId equals org.Id
                                   join orgsdf in _orgsdfRepository.GetAll() on discgrant.OrganisationId equals orgsdf.OrganisationId
                                   join stat in _discStatusRepository.GetAll() on discgrant.ProjectStatusID equals stat.Id
                                   join wind in _windowRepository.GetAll() on discgrant.GrantWindowId equals wind.Id
                                   join prms in _windowParamRepository.GetAll() on discgrant.WindowParamId equals prms.Id
                                   join projtype in _projTypeRepository.GetAll() on discgrant.ProjectTypeId equals projtype.Id
                                   join focarea in _focusAreaRepository.GetAll() on prms.FocusAreaId equals focarea.Id into foc
                                   from focs in foc.DefaultIfEmpty()
                                   join subcat in _adminCritRepository.GetAll() on prms.SubCategoryId equals subcat.Id into sub
                                   from subs in sub.DefaultIfEmpty()
                                   join interv in _evalMethodRepository.GetAll() on prms.InterventionId equals interv.Id into inter
                                   from inters in inter.DefaultIfEmpty()
                                   select new
                        {
                            Id = discgrant.Id,
                            SDLNo = org.SDL_No,
                            Organisation_Name = org.Organisation_Name,
                            OrganisationId = org.Id,
                            SdfId = orgsdf.SdfId,
                            ProjectId = discgrant.Id,
                            Title = wind.Title,
                            OrgSdfId = orgsdf.Id,
                            Status = stat.StatusDesc,
                            ProjType = projtype.ProjTypDesc,
                            FocusArea = focs.FocusAreaDesc,
                            FundingLimit = focs.FundinglImit,
                            SubCategory = subs.AdminDesc,
                            ProjectStatus = stat.StatusDesc,
                            ProjectStatDte = discgrant.ProjectStatDte,
                            ProjShortNam = discgrant.ProjShortNam,
                            ProjectNam = discgrant.ProjectNam,
                            SubmissionDte = discgrant.SubmissionDte,
                            Intervention = inters.EvalMthdDesc
                        })
                .WhereIf(!SDLNoFilter.IsNullOrWhiteSpace() && SDLNoFilterMode == "startsWith", u => u.SDLNo.StartsWith(SDLNoFilter))
                .WhereIf(!SDLNoFilter.IsNullOrWhiteSpace() && SDLNoFilterMode == "contains", u => u.SDLNo.Contains(SDLNoFilter))
                .WhereIf(!SDLNoFilter.IsNullOrWhiteSpace() && SDLNoFilterMode == "equals", u => u.SDLNo == SDLNoFilter)
                .WhereIf(!(OrganisationNameFilter.IsNullOrWhiteSpace()) && OrganisationNameFilterMode == "startsWith", u => u.Organisation_Name.StartsWith(OrganisationNameFilter))
                .WhereIf(!(OrganisationNameFilter.IsNullOrWhiteSpace()) && OrganisationNameFilterMode == "contains", u => u.Organisation_Name.Contains(OrganisationNameFilter))
                .WhereIf(!(OrganisationNameFilter.IsNullOrWhiteSpace()) && OrganisationNameFilterMode == "equals", u => u.Organisation_Name  == OrganisationNameFilter)
                .WhereIf(!ProjTypeFilter.IsNullOrWhiteSpace() && ProjTypeFilterMode == "startsWith", u => u.ProjType.StartsWith(ProjTypeFilter))
                .WhereIf(!ProjTypeFilter.IsNullOrWhiteSpace() && ProjTypeFilterMode == "contains", u => u.ProjType.Contains(ProjTypeFilter))
                .WhereIf(!ProjTypeFilter.IsNullOrWhiteSpace() && ProjTypeFilterMode == "equals", u => u.ProjType  == ProjTypeFilter)
                .WhereIf(!TitleFilter.IsNullOrWhiteSpace() && TitleFilterMode == "startsWith", u => u.Title.StartsWith(TitleFilter))
                .WhereIf(!TitleFilter.IsNullOrWhiteSpace() && TitleFilterMode == "contains", u => u.Title.Contains(TitleFilter))
                .WhereIf(!TitleFilter.IsNullOrWhiteSpace() && TitleFilterMode == "equals", u => u.Title == TitleFilter)
                .WhereIf(!FocusAreaFilter.IsNullOrWhiteSpace() && FocusAreaFilterMode == "startsWith", u => u.FocusArea.StartsWith(FocusAreaFilter))
                .WhereIf(!FocusAreaFilter.IsNullOrWhiteSpace() && FocusAreaFilterMode == "contains", u => u.FocusArea.Contains(FocusAreaFilter))
                .WhereIf(!FocusAreaFilter.IsNullOrWhiteSpace() && FocusAreaFilterMode == "equals", u => u.FocusArea == FocusAreaFilter)
                .WhereIf(!ProjectStatusFilter.IsNullOrWhiteSpace() && ProjectStatusFilterMode == "startsWith", u => u.ProjectStatus.StartsWith(ProjectStatusFilter))
                .WhereIf(!ProjectStatusFilter.IsNullOrWhiteSpace() && ProjectStatusFilterMode == "contains", u => u.ProjectStatus.Contains(ProjectStatusFilter))
                .WhereIf(!ProjectStatusFilter.IsNullOrWhiteSpace() && ProjectStatusFilterMode == "equals", u => u.ProjectStatus == ProjectStatusFilter)
                .OrderByDescending(a=>a.Id)
                .Skip(first)
                .Take(rows)
                .ToListAsync();

            var totalCount = (from discgrant in _discProjRepository.GetAll()
                              join org in _orgRepository.GetAll() on discgrant.OrganisationId equals org.Id
                              join orgsdf in _orgsdfRepository.GetAll() on discgrant.OrganisationId equals orgsdf.OrganisationId
                              join stat in _discStatusRepository.GetAll() on discgrant.ProjectStatusID equals stat.Id
                              join wind in _windowRepository.GetAll() on discgrant.GrantWindowId equals wind.Id
                              join prms in _windowParamRepository.GetAll() on discgrant.WindowParamId equals prms.Id
                              join projtype in _projTypeRepository.GetAll() on discgrant.ProjectTypeId equals projtype.Id
                              join focarea in _focusAreaRepository.GetAll() on prms.FocusAreaId equals focarea.Id into foc
                              from focs in foc.DefaultIfEmpty()
                              join subcat in _adminCritRepository.GetAll() on prms.SubCategoryId equals subcat.Id into sub
                              from subs in sub.DefaultIfEmpty()
                              join interv in _evalMethodRepository.GetAll() on prms.InterventionId equals interv.Id into inter
                              from inters in inter.DefaultIfEmpty()
                              select new
                {
                    Id = discgrant.Id,
                    SDLNo = org.SDL_No,
                    Organisation_Name = org.Organisation_Name,
                    OrganisationId = org.Id,
                    SdfId = orgsdf.SdfId,
                    ProjectId = discgrant.Id,
                    Title = wind.Title,
                    OrgSdfId = orgsdf.Id,
                    Status = stat.StatusDesc,
                    ProjType = projtype.ProjTypDesc,
                    FocusArea = focs.FocusAreaDesc,
                    FundingLimit = focs.FundinglImit,
                    SubCategory = subs.AdminDesc,
                    ProjectStatus = stat.StatusDesc,
                    ProjectStatDte = discgrant.ProjectStatDte,
                    ProjShortNam = discgrant.ProjShortNam,
                    ProjectNam = discgrant.ProjectNam,
                    SubmissionDte = discgrant.SubmissionDte,
                    Intervention = inters.EvalMthdDesc
                })
                .WhereIf(!SDLNoFilter.IsNullOrWhiteSpace() && SDLNoFilterMode == "startsWith", u => u.SDLNo.StartsWith(SDLNoFilter))
                .WhereIf(!SDLNoFilter.IsNullOrWhiteSpace() && SDLNoFilterMode == "contains", u => u.SDLNo.Contains(SDLNoFilter))
                .WhereIf(!SDLNoFilter.IsNullOrWhiteSpace() && SDLNoFilterMode == "equals", u => u.SDLNo == SDLNoFilter)
                .WhereIf(!(OrganisationNameFilter.IsNullOrWhiteSpace()) && OrganisationNameFilterMode == "startsWith", u => u.Organisation_Name.StartsWith(OrganisationNameFilter))
                .WhereIf(!(OrganisationNameFilter.IsNullOrWhiteSpace()) && OrganisationNameFilterMode == "contains", u => u.Organisation_Name.Contains(OrganisationNameFilter))
                .WhereIf(!(OrganisationNameFilter.IsNullOrWhiteSpace()) && OrganisationNameFilterMode == "equals", u => u.Organisation_Name == OrganisationNameFilter)
                .WhereIf(!ProjTypeFilter.IsNullOrWhiteSpace() && ProjTypeFilterMode == "startsWith", u => u.ProjType.StartsWith(ProjTypeFilter))
                .WhereIf(!ProjTypeFilter.IsNullOrWhiteSpace() && ProjTypeFilterMode == "contains", u => u.ProjType.Contains(ProjTypeFilter))
                .WhereIf(!ProjTypeFilter.IsNullOrWhiteSpace() && ProjTypeFilterMode == "equals", u => u.ProjType == ProjTypeFilter)
                .WhereIf(!TitleFilter.IsNullOrWhiteSpace() && TitleFilterMode == "startsWith", u => u.Title.StartsWith(TitleFilter))
                .WhereIf(!TitleFilter.IsNullOrWhiteSpace() && TitleFilterMode == "contains", u => u.Title.Contains(TitleFilter))
                .WhereIf(!TitleFilter.IsNullOrWhiteSpace() && TitleFilterMode == "equals", u => u.Title == TitleFilter)
                .WhereIf(!FocusAreaFilter.IsNullOrWhiteSpace() && FocusAreaFilterMode == "startsWith", u => u.FocusArea.StartsWith(FocusAreaFilter))
                .WhereIf(!FocusAreaFilter.IsNullOrWhiteSpace() && FocusAreaFilterMode == "contains", u => u.FocusArea.Contains(FocusAreaFilter))
                .WhereIf(!FocusAreaFilter.IsNullOrWhiteSpace() && FocusAreaFilterMode == "equals", u => u.FocusArea == FocusAreaFilter)
                .WhereIf(!ProjectStatusFilter.IsNullOrWhiteSpace() && ProjectStatusFilterMode == "startsWith", u => u.ProjectStatus.StartsWith(ProjectStatusFilter))
                .WhereIf(!ProjectStatusFilter.IsNullOrWhiteSpace() && ProjectStatusFilterMode == "contains", u => u.ProjectStatus.Contains(ProjectStatusFilter))
                .WhereIf(!ProjectStatusFilter.IsNullOrWhiteSpace() && ProjectStatusFilterMode == "equals", u => u.ProjectStatus == ProjectStatusFilter)
                .Count();

            var discproject = from o in discprojs
                              select new DiscretionaryProjectForViewDto()
                              {
                                  DiscretionaryProject = new DiscretionaryProjectOutputDto
                                  {
                                      SDLNo = o.SDLNo,
                                      Organisation_Name = o.Organisation_Name,
                                      OrganisationId = o.Id,
                                      SdfId = o.SdfId,
                                      ProjectId = o.ProjectId,
                                      Title = o.Title,
                                      ProjType = o.ProjType,
                                      FocusArea = o.FocusArea,
                                      FundingLimit = o.FundingLimit,
                                      SubCategory = o.SubCategory,
                                      ProjectStatDte = o.ProjectStatDte,
                                      ProjectStatus = o.Status,
                                      ProjShortNam = o.ProjShortNam,
                                      ProjectNam = o.ProjectNam,
                                      SubmissionDte = o.SubmissionDte,
                                      Id = o.Id
                                  }
                              };

            return new PagedResultDto<DiscretionaryProjectForViewDto>(
                totalCount,
                discproject.ToList()
            );
        }

        public async Task<PagedResultDto<DiscretionaryProjectForViewDto>> GetProjectApprovalForWindow(int first, int rows)
        {
            var discprojs = await (from discgrant in _discProjRepository.GetAll()
                    join org in _orgRepository.GetAll() on discgrant.OrganisationId equals org.Id
                    join orgsdf in _orgsdfRepository.GetAll() on discgrant.OrganisationId equals orgsdf.OrganisationId
                    join stat in _discStatusRepository.GetAll() on discgrant.ProjectStatusID equals stat.Id
                    join wind in _windowRepository.GetAll() on discgrant.GrantWindowId equals wind.Id
                    //join prms in _windowParamRepository.GetAll() on discgrant.WindowParamId equals prms.Id
                    join dpd in _discProjDetRepository.GetAll() on discgrant.Id equals dpd.ProjectId
                    join projtype in _projTypeRepository.GetAll() on dpd.ProjectTypeId equals projtype.Id
                    join focarea in _focusAreaRepository.GetAll() on dpd.FocusAreaId equals focarea.Id
                    join subcat in _adminCritRepository.GetAll() on dpd.SubCategoryId equals subcat.Id
                    join interv in _evalMethodRepository.GetAll() on dpd.InterventionId equals interv.Id
                    select new
                    {
                        SDLNo = org.SDL_No,
                        OrganisationId = org.Id,
                        DiscretionaryProject = discgrant,
                        Title = wind.Title,
                        SdfId = orgsdf.SdfId,
                        OrgSdfId = orgsdf.Id,
                        Status = stat.StatusDesc,
                        ProjType = projtype.ProjTypDesc,
                        FocusArea = focarea.FocusAreaDesc,
                        SubCategory = subcat.AdminDesc,
                        Intervention = interv.EvalMthdDesc,
                    })
                    .Where(a=>a.Status == "Submitted by online user")
                    .Skip(first)
                    .Take(rows)
                    .ToListAsync();

            var totalCount = (from discgrant in _discProjRepository.GetAll()
                              join org in _orgRepository.GetAll() on discgrant.OrganisationId equals org.Id
                              join orgsdf in _orgsdfRepository.GetAll() on discgrant.OrganisationId equals orgsdf.OrganisationId
                              join stat in _discStatusRepository.GetAll() on discgrant.ProjectStatusID equals stat.Id
                              select new { discgrant.Id, Status = stat.StatusDesc })
                              .Where(a => a.Status == "Submitted by online user")
                              .Count();

            var discproject = from o in discprojs
                              select new DiscretionaryProjectForViewDto()
                              {
                                  DiscretionaryProject = new DiscretionaryProjectOutputDto
                                  {
                                      SDLNo = o.SDLNo,
                                      OrganisationId = o.OrganisationId,
                                      SdfId = o.SdfId,
                                      ProjectId = o.DiscretionaryProject.Id,
                                      Title = o.Title,
                                      ProjType = o.ProjType,
                                      FocusArea = o.FocusArea,
                                      SubCategory = o.SubCategory,
                                      ProjectStatDte = o.DiscretionaryProject.ProjectStatDte,
                                      ProjectStatus = o.Status,
                                      ProjShortNam = o.DiscretionaryProject.ProjShortNam,
                                      ProjectNam = o.DiscretionaryProject.ProjectNam,
                                      SubmissionDte = o.DiscretionaryProject.SubmissionDte,
                                      Id = o.DiscretionaryProject.Id
                                  }
                              };

            return new PagedResultDto<DiscretionaryProjectForViewDto>(
                totalCount,
                discproject.ToList()
            );
        }

        public async Task<DiscretionaryProjectDto> GetProjectById(int id)
        {
            var proj = await _discProjRepository.GetAsync(id);

            var output = ObjectMapper.Map<DiscretionaryProjectDto>(proj);

            return output;
        }

        public async Task<DiscretionaryProjectDetailsDto> GetDGProjectDetById(int id)
        {
            var proj = await _discProjDetRepository.GetAsync(id);

            var output = ObjectMapper.Map<DiscretionaryProjectDetailsDto>(proj);

            return output;
        }

        public async Task<PagedResultDto<DiscretionaryProjectForViewDto>> GetProject(int id)
        {

            var discprojs = await (from discgrant in _discProjRepository.GetAll()
                                   join org in _orgRepository.GetAll() on discgrant.OrganisationId equals org.Id
                                   join orgsdf in _orgsdfRepository.GetAll() on discgrant.OrganisationId equals orgsdf.OrganisationId
                                   join stat in _discStatusRepository.GetAll() on discgrant.ProjectStatusID equals stat.Id
                                   join wind in _windowRepository.GetAll() on discgrant.GrantWindowId equals wind.Id
                                   //join prms in _windowParamRepository.GetAll() on discgrant.WindowParamId equals prms.Id
                                   join dpd in _discProjDetRepository.GetAll() on discgrant.Id equals dpd.ProjectId
                                   join projtype in _projTypeRepository.GetAll() on dpd.ProjectTypeId equals projtype.Id
                                   join focarea in _focusAreaRepository.GetAll() on dpd.FocusAreaId equals focarea.Id
                                   join subcat in _adminCritRepository.GetAll() on dpd.SubCategoryId equals subcat.Id
                                   join interv in _evalMethodRepository.GetAll() on dpd.InterventionId equals interv.Id
                                   select new
                                   {
                                       SDLNo = org.SDL_No,
                                       OrganisationId = org.Id,
                                       DiscretionaryProject = discgrant,
                                       Title = wind.Title,
                                       SdfId = orgsdf.SdfId,
                                       OrgSdfId = orgsdf.Id,
                                       Status = stat.StatusDesc,
                                       ProjType = projtype.ProjTypDesc,
                                       FocusArea = focarea.FocusAreaDesc,
                                       FundingLimit = focarea.FundinglImit,
                                       SubCategory = subcat.AdminDesc,
                                       Intervention = interv.EvalMthdDesc,
                                   })
                    .Where(a => a.DiscretionaryProject.Id == id)

                    .ToListAsync();


            var discproject = from o in discprojs
                              select new DiscretionaryProjectForViewDto()
                              {
                                  DiscretionaryProject = new DiscretionaryProjectOutputDto
                                  {
                                      SDLNo = o.SDLNo,
                                      OrganisationId = o.OrganisationId,
                                      SdfId = o.SdfId,
                                      ProjectId = o.DiscretionaryProject.Id,
                                      Title = o.Title,
                                      ProjType = o.ProjType,
                                      FocusArea = o.FocusArea,
                                      FundingLimit = o.FundingLimit,
                                      SubCategory = o.SubCategory,
                                      ProjectStatDte = o.DiscretionaryProject.ProjectStatDte,
                                      ProjectStatus = o.Status,
                                      ProjShortNam = o.DiscretionaryProject.ProjShortNam,
                                      ProjectNam = o.DiscretionaryProject.ProjectNam,
                                      SubmissionDte = o.DiscretionaryProject.SubmissionDte,
                                      Id = o.DiscretionaryProject.Id
                                  }
                              };

            var totalCount = discproject.Count();

            return new PagedResultDto<DiscretionaryProjectForViewDto>(
                totalCount,
                discproject.ToList()
            );
        }

        public async Task CreateEditApplicationDetails(DiscretionaryProjectDetailsDto input)
        {
            var dgprojdet = _discProjDetRepository.GetAll().Where(a => a.Id == input.Id);

            if (dgprojdet.Count() == 0)
            {
                var chk = _discProjDetRepository.GetAll().
                    Where(a => a.ProjectId == input.ProjectId && a.FocusAreaId == input.FocusAreaId && a.ProjectTypeId == input.ProjectTypeId && a.SubCategoryId == input.SubCategoryId
                    && a.InterventionId == input.InterventionId && a.Province == input.Province && a.District == input.District && a.Municipality == input.Municipality);
                if (chk.Count() == 0)
                {
                    input.DateCreated = DateTime.Now;
                    var projdet = ObjectMapper.Map<DiscretionaryProjectDetails>(input);

                    await _discProjDetRepository.InsertAsync(projdet);
                } else
                {
                    throw new InvalidOperationException("Dupicate entry detected.");
                }
            }
            else
            {
                var dgdet = await _discProjDetRepository.FirstOrDefaultAsync(dgprojdet.First().Id);
                //cappl.DteUpd = DateTime.Now;
                dgdet.UserId = input.UserId;
                dgdet.FocusAreaId = input.FocusAreaId;
                dgdet.SubCategoryId = input.SubCategoryId;
                dgdet.InterventionId = input.InterventionId;
                dgdet.OtherIntervention = input.OtherIntervention;
                dgdet.Province = input.Province;
                dgdet.Municipality = input.Municipality;
                dgdet.CostPerLearner = input.CostPerLearner;
                dgdet.Female = input.Female;
                dgdet.HDI = input.HDI;
                dgdet.Number_Continuing = input.Number_Continuing;
                dgdet.Number_Disabled = input.Number_Disabled;
                dgdet.Number_New = input.Number_New;
                dgdet.Rural = input.Rural;
                dgdet.Youth = input.Youth;

                await _discProjDetRepository.UpdateAsync(dgdet);
            }
        }

        public async Task<PagedResultDto<DiscretionaryProjectDetailsForViewDto>> GetProjectDetails(int ProjectId)
        {
            var det = _discProjDetRepository.GetAll().Where(a => a.ProjectId == ProjectId);


            var projdet = await (from o in det
                              select new DiscretionaryProjectDetailsForViewDto
                              {
                                  DiscretionaryProjectDetails = new DiscretionaryProjectDetailsDto
                                  {
                                      ProjectTypeId = o.ProjectTypeId,
                                      FocusAreaId = o.FocusAreaId,
                                      SubCategoryId = o.SubCategoryId,
                                      InterventionId = o.InterventionId,
                                      CostPerLearner = o.CostPerLearner,
                                      Female = o.Female,
                                      HDI = o.HDI,
                                      Province = o.Province,
                                      Municipality = o.Municipality,
                                      Number_Continuing = o.Number_Continuing,
                                      Number_Disabled = o.Number_Disabled,
                                      Number_New = o.Number_New,
                                      Rural = o.Rural,
                                      Youth = o.Youth,
                                      Id = o.Id
                                  }
                              }).ToListAsync();

            var totalCount = det.Count();

            return new PagedResultDto<DiscretionaryProjectDetailsForViewDto>(
                totalCount,
                projdet.ToList()
            );
        }

        public async Task<PagedResultDto<PagedDiscretionaryProjectDetailsView>> GetDGProjectDetails(int ProjectId)
        {
            var discprojs = await (from projdet in _discProjDetApprovalRepository.GetAll()
                                   join proj in _discProjRepository.GetAll() on projdet.ProjectId equals proj.Id
                                   join stat in _discStatusRepository.GetAll() on proj.ProjectStatusID equals stat.Id
                                   join projtype in _projTypeRepository.GetAll() on projdet.ProjectTypeId equals projtype.Id
                                   join focarea in _focusAreaRepository.GetAll() on projdet.FocusAreaId equals focarea.Id
                                   join subcat in _adminCritRepository.GetAll() on projdet.SubCategoryId equals subcat.Id
                                   join interv in _evalMethodRepository.GetAll() on projdet.InterventionId equals interv.Id
                                   join gac in _discGCApprovalRepository.GetAll() on projdet.Id equals gac.ApplicationId into ga
                                   from gcs in ga.DefaultIfEmpty()

                                   select new
                                   {
                                       Application = projdet,
                                       ProjType = projtype.ProjTypDesc,
                                       FocusArea = focarea.FocusAreaDesc,
                                       SubCategory = subcat.AdminDesc,
                                       Intervention = interv.EvalMthdDesc,
                                       Status = stat.StatusDesc,
                                       OrganisationId = proj.OrganisationId,
                                       GCStatus = gcs
                                   })
                    .Where(a => a.Application.ProjectId == ProjectId)
                    .ToListAsync();


            var discproject = from o in discprojs
                              select new PagedDiscretionaryProjectDetailsView()
                              {
                                  ProjectDetails = new DiscretionaryProjectDetailsView
                                  {
                                      ProjectType = o.ProjType,
                                      ApplicationStatusId = o.Application.ApplicationStatusId,
                                      Contract_Number = o.Application.Contract_Number,
                                      SubmittedBy = o.Application.SubmittedBy,
                                      SubmissionDte = o.Application.SubmissionDte,
                                      Current_Approver = o.Application.Current_Approver,
                                      Reason = o.Application.Reason,
                                      FocusArea = o.FocusArea,
                                      SubCategory = o.SubCategory,
                                      Intervention = o.Intervention,
                                      Number_Continuing = o.Application.Number_Continuing,
                                      Number_New = o.Application.Number_New,
                                      CostPerLearner = o.Application.CostPerLearner,
                                      GC_Continuing = o.Application.GC_Continuing,
                                      GC_New = o.Application.GC_New,
                                      GC_CostPerLearner = o.Application.GC_CostPerLearner,
                                      HDI = o.Application.HDI,
                                      Female = o.Application.Female,
                                      Youth = o.Application.Youth,
                                      Number_Disabled = o.Application.Number_Disabled,
                                      Rural = o.Application.Rural,
                                      Province = o.Application.Province,
                                      Municipality = o.Application.Municipality,
                                      Status = o.Status,
                                      GCStatus = o.GCStatus,
                                      OrganisationId = o.OrganisationId,
                                      Id = o.Application.Id
                                  }
                              };

            var totalCount = discproject.Count();

            return new PagedResultDto<PagedDiscretionaryProjectDetailsView>(
                totalCount,
                discproject.ToList()
            );
        }

        public async Task<PagedResultDto<PagedDiscretionaryProjectDetailsView>> GetDGProjectDetailsApp(int ProjectId)
        {
            var discprojs = await (from projdet in _discProjDetRepository.GetAll()
                                   join proj in _discProjRepository.GetAll() on projdet.ProjectId equals proj.Id
                                   join stat in _discStatusRepository.GetAll() on proj.ProjectStatusID equals stat.Id
                                   join projtype in _projTypeRepository.GetAll() on projdet.ProjectTypeId equals projtype.Id
                                   join focarea in _focusAreaRepository.GetAll() on projdet.FocusAreaId equals focarea.Id
                                   join subcat in _adminCritRepository.GetAll() on projdet.SubCategoryId equals subcat.Id
                                   join interv in _evalMethodRepository.GetAll() on projdet.InterventionId equals interv.Id into emp
                                   from emps in emp.DefaultIfEmpty()
                                   join gac in _discGCApprovalRepository.GetAll() on projdet.Id equals gac.ApplicationId into ga
                                   from gcs in ga.DefaultIfEmpty()

                                   select new
                                   {
                                       Application = projdet,
                                       ProjType = projtype.ProjTypDesc,
                                       FocusArea = focarea.FocusAreaDesc,
                                       SubCategory = subcat.AdminDesc,
                                       Intervention = emps.EvalMthdDesc,
                                       Status = stat.StatusDesc,
                                       OrganisationId = proj.OrganisationId,
                                       GCStatus = gcs
                                   })
                    .Where(a => a.Application.ProjectId == ProjectId)
                    .ToListAsync();


            var discproject = from o in discprojs
                              select new PagedDiscretionaryProjectDetailsView()
                              {
                                  ProjectDetails = new DiscretionaryProjectDetailsView
                                  {
                                      ProjectType = o.ProjType,
                                      FocusArea = o.FocusArea,
                                      SubCategory = o.SubCategory,
                                      Intervention = o.Intervention,
                                      Number_Continuing = o.Application.Number_Continuing,
                                      Number_New = o.Application.Number_New,
                                      CostPerLearner = o.Application.CostPerLearner,
                                      HDI = o.Application.HDI,
                                      Female = o.Application.Female,
                                      Youth = o.Application.Youth,
                                      Number_Disabled = o.Application.Number_Disabled,
                                      Rural = o.Application.Rural,
                                      Province = o.Application.Province,
                                      Municipality = o.Application.Municipality,
                                      Status = o.Status,
                                      GCStatus = o.GCStatus,
                                      OrganisationId = o.OrganisationId,
                                      Id = o.Application.Id
                                  }
                              };

            var totalCount = discproject.Count();

            return new PagedResultDto<PagedDiscretionaryProjectDetailsView>(
                totalCount,
                discproject.ToList()
            );
        }

        public async Task<PagedResultDto<PagedDiscretionaryProjectDetailsView>> GetDGProjectDetails_bk(int applicationId)
        {
            var discprojs = await (from projdet in _discProjDetApprovalRepository.GetAll()
                                   join proj in _discProjRepository.GetAll() on projdet.ProjectId equals proj.Id
                                   join stat in _discStatusRepository.GetAll() on proj.ProjectStatusID equals stat.Id
                                   join projtype in _projTypeRepository.GetAll() on projdet.ProjectTypeId equals projtype.Id
                                   join focarea in _focusAreaRepository.GetAll() on projdet.FocusAreaId equals focarea.Id
                                   join subcat in _adminCritRepository.GetAll() on projdet.SubCategoryId equals subcat.Id
                                   join interv in _evalMethodRepository.GetAll() on projdet.InterventionId equals interv.Id into emp
                                   from emps in emp.DefaultIfEmpty()
                                   join gac in _discGCApprovalRepository.GetAll() on projdet.Id equals gac.ApplicationId into ga
                                   from gcs in ga.DefaultIfEmpty()

                                   select new
                                   {
                                       Application = projdet,
                                       ProjType = projtype.ProjTypDesc,
                                       FocusArea = focarea.FocusAreaDesc,
                                       SubCategory = subcat.AdminDesc,
                                       Intervention = emps.EvalMthdDesc,
                                       Status = stat.StatusDesc,
                                       OrganisationId = proj.OrganisationId,
                                       GCStatus = gcs
                                   })
                    .Where(a => a.Application.Id == applicationId)
                    .ToListAsync();


            var discproject = from o in discprojs
                              select new PagedDiscretionaryProjectDetailsView()
                              {
                                  ProjectDetails = new DiscretionaryProjectDetailsView
                                  {
                                      ProjectType = o.ProjType,
                                      ApplicationStatusId = o.Application.ApplicationStatusId,
                                      Contract_Number = o.Application.Contract_Number,
                                      FocusArea = o.FocusArea,
                                      SubCategory = o.SubCategory,
                                      Intervention = o.Intervention,
                                      Number_Continuing = o.Application.Number_Continuing,
                                      Number_New = o.Application.Number_New,
                                      CostPerLearner = o.Application.CostPerLearner,
                                      GC_Continuing = o.Application.GC_Continuing,
                                      GC_New = o.Application.GC_New,
                                      GC_CostPerLearner = o.Application.GC_CostPerLearner,
                                      HDI = o.Application.HDI,
                                      Female = o.Application.Female,
                                      Youth = o.Application.Youth,
                                      Number_Disabled = o.Application.Number_Disabled,
                                      Rural = o.Application.Rural,
                                      Province = o.Application.Province,
                                      Municipality = o.Application.Municipality,
                                      Status = o.Status,
                                      GCStatus = o.GCStatus,
                                      OrganisationId = o.OrganisationId,
                                      Id = o.Application.Id
                                  }
                              };

            var totalCount = discproject.Count();

            return new PagedResultDto<PagedDiscretionaryProjectDetailsView>(
                totalCount,
                discproject.ToList()
            );
        }

        public async Task<PagedResultDto<PagedDiscretionaryProjectDetailsView>> GetDGProjectDetailsId(int applicationId)
        {
            var discprojs = await (from projdet in _discProjDetApprovalRepository.GetAll()
                                   join proj in _discProjRepository.GetAll() on projdet.ProjectId equals proj.Id
                                   join stat in _discStatusRepository.GetAll() on proj.ProjectStatusID equals stat.Id
                                   join projtype in _projTypeRepository.GetAll() on projdet.ProjectTypeId equals projtype.Id
                                   join focarea in _focusAreaRepository.GetAll() on projdet.FocusAreaId equals focarea.Id
                                   join subcat in _adminCritRepository.GetAll() on projdet.SubCategoryId equals subcat.Id
                                   join interv in _evalMethodRepository.GetAll() on projdet.InterventionId equals interv.Id into emp
                                   from emps in emp.DefaultIfEmpty()
                                   join gac in _discGCApprovalRepository.GetAll() on projdet.Id equals gac.ApplicationId into ga
                                   from gcs in ga.DefaultIfEmpty()

                                   select new
                                   {
                                       Application = projdet,
                                       ProjType = projtype.ProjTypDesc,
                                       FocusArea = focarea.FocusAreaDesc,
                                       SubCategory = subcat.AdminDesc,
                                       Intervention = emps.EvalMthdDesc,
                                       Status = stat.StatusDesc,
                                       OrganisationId = proj.OrganisationId,
                                       GCStatus = gcs
                                   })
                    .Where(a => a.Application.Id == applicationId)
                    .ToListAsync();


            var discproject = from o in discprojs
                              select new PagedDiscretionaryProjectDetailsView()
                              {
                                  ProjectDetails = new DiscretionaryProjectDetailsView
                                  {
                                      ProjectType = o.ProjType,
                                      ApplicationStatusId = o.Application.ApplicationStatusId,
                                      Reason = o.Application.Reason,
                                      ApprovalStatus = o.Application.ApprovalStatus,
                                      FocusArea = o.FocusArea,
                                      SubCategory = o.SubCategory,
                                      Intervention = o.Intervention,
                                      Number_Continuing = o.Application.Number_Continuing,
                                      Number_New = o.Application.Number_New,
                                      CostPerLearner = o.Application.CostPerLearner,
                                      GC_Continuing = o.Application.GC_Continuing,
                                      GC_New = o.Application.GC_New,
                                      GC_CostPerLearner = o.Application.GC_CostPerLearner,
                                      HDI = o.Application.HDI,
                                      Female = o.Application.Female,
                                      Youth = o.Application.Youth,
                                      Number_Disabled = o.Application.Number_Disabled,
                                      Rural = o.Application.Rural,
                                      Province = o.Application.Province,
                                      Municipality = o.Application.Municipality,
                                      Status = o.Status,
                                      GCStatus = o.GCStatus,
                                      OrganisationId = o.OrganisationId,
                                      Id = o.Application.Id
                                  }
                              };

            var totalCount = discproject.Count();

            return new PagedResultDto<PagedDiscretionaryProjectDetailsView>(
                totalCount,
                discproject.ToList()
            );
        }


        public async Task SubmitApplication(int id, int userid)
        {
            var dgproj = _discProjRepository.GetAll().Where(a => a.Id == id);

            if (dgproj.Count() != 0)
            {
                var proj = await _discProjRepository.FirstOrDefaultAsync(dgproj.First().Id);
                proj.DteUpd = DateTime.Now;
                proj.SubmittedBy = userid;
                proj.SubmissionDte = DateTime.Now;
                proj.ProjectStatDte = DateTime.Now;
                proj.ProjectStatusID = 10;
                
                await _discProjRepository.UpdateAsync(proj);

            }
        }

        public async Task DeleteApplication(int id, int userid)
        {
            var proj = await _discProjDetRepository.GetAsync(id);
            await _discProjDetRepository.DeleteAsync(proj);
        }

        public async Task<string> AddProjUs(int appId, int usId, int userId)
        {
            string status = "";
            decimal grandtotal = 0;

            var cus = _discProjUSRepository.GetAll().Where(a => a.ApplicationId == appId && a.USId == usId);
            if (cus.Count() == 0)
            {
                var us = _usRepository.Get(usId);
                var empstat = (from dp in _discProjDetRepository.GetAll()
                    join sp in _focusAreaRepository.GetAll() on dp.FocusAreaId equals sp.Id
                    select new
                    {
                        Id = dp.Id,
                        EmpStat = sp.EmpStatus
                    }).Where(a => a.Id == appId).FirstOrDefault().EmpStat;

                var totcredits = (from uses in _discProjUSRepository.GetAll()
                    join uss in _usRepository.GetAll() on uses.USId equals uss.Id
                    select new
                    {
                        uss.UNIT_STD_NUMBER_OF_CREDITS,
                        ApplicationId = uses.ApplicationId
                    })
                    .Where(a => a.ApplicationId == appId)
                    .Sum(a => a.UNIT_STD_NUMBER_OF_CREDITS) + us.UNIT_STD_NUMBER_OF_CREDITS;

                if (totcredits <= 45) 
                {
                    try
                    {
                        decimal amt;
                        if (empstat == "Employed")
                        {
                            grandtotal = _discProjUSRepository.GetAll().Where(a => a.ApplicationId == appId).Sum(a => a.Amount) + us.Amount1;
                            amt = us.Amount1;
                        } else
                        {
                            grandtotal = _discProjUSRepository.GetAll().Where(a => a.ApplicationId == appId).Sum(a => a.Amount) + us.Amount2;
                            amt = us.Amount2;
                        }
                            
                        var inus = new DiscretionaryProjectUS();
                        inus.ApplicationId = appId;
                        inus.USId = usId;
                        inus.UserId = userId;
                        inus.Amount = amt;
                        inus.DateCreated = DateTime.Now;
                        _discProjUSRepository.Insert(inus);

                        var proj = _discProjDetRepository.FirstOrDefault(appId);
                        proj.CostPerLearner = grandtotal;
                        await _discProjDetRepository.UpdateAsync(proj);
                    } catch (Exception ex)
                    {
                        status = "Error";
                    }
                    finally
                    {
                        if (status == "")
                        {
                            status = "Ok";
                        }
                    };
                } else
                {
                    status = "Limit";
                }
            } else
            {
                status = "Duplicate";
            }

            return status;
        }

        public async Task<int> DeleteProjUS(int id, int userId)
        {
            var projus = await _discProjUSRepository.GetAsync(id);
            var appid = projus.ApplicationId;

            _discProjUSRepository.Delete(projus);

            return appid;
        }

        public async Task UpdateProjUS(int ApplicationId, int userid)
        {
            var app = _discProjUSRepository.GetAll().Where(a => a.ApplicationId == ApplicationId).Count();
            if (app > 0)
            {
                var grandtotal = _discProjUSRepository.GetAll().Where(a => a.ApplicationId == ApplicationId).Sum(a => a.Amount);

                var proj = _discProjDetRepository.FirstOrDefault(ApplicationId);
                proj.CostPerLearner = grandtotal;
                await _discProjDetRepository.UpdateAsync(proj);
            }
        }
        
        public async Task<string> AddStrategicUs(int appId, int usId, int userId)
        {
            string status = "";
            decimal grandtotal = 0;

            var cus = _discProjUSRepository.GetAll().Where(a => a.ApplicationId == appId && a.USId == usId);
            if (cus.Count() == 0)
            {
                var us = _usRepository.Get(usId);
                var empstat = (from dp in _discProjDetRepository.GetAll()
                               join sp in _focusAreaRepository.GetAll() on dp.FocusAreaId equals sp.Id
                               select new
                               {
                                   Id = dp.Id,
                                   EmpStat = sp.EmpStatus
                               }).Where(a => a.Id == appId).FirstOrDefault().EmpStat;

                var totcredits = (from uses in _discProjUSRepository.GetAll()
                                  join uss in _usRepository.GetAll() on uses.USId equals uss.Id
                                  select new
                                  {
                                      uss.UNIT_STD_NUMBER_OF_CREDITS,
                                      ApplicationId = uses.ApplicationId
                                  })
                    .Where(a => a.ApplicationId == appId)
                    .Sum(a => a.UNIT_STD_NUMBER_OF_CREDITS) + us.UNIT_STD_NUMBER_OF_CREDITS;

                if (totcredits <= 45)
                {
                    try
                    {
                        decimal amt;
                        if (empstat == "Employed")
                        {
                            grandtotal = _discProjUSRepository.GetAll().Where(a => a.ApplicationId == appId).Sum(a => a.Amount) + us.Amount1;
                            amt = us.Amount1;
                        }
                        else
                        {
                            grandtotal = _discProjUSRepository.GetAll().Where(a => a.ApplicationId == appId).Sum(a => a.Amount) + us.Amount2;
                            amt = us.Amount2;
                        }

                        var inus = new DiscretionaryProjectUS();
                        inus.ApplicationId = appId;
                        inus.USId = usId;
                        inus.UserId = userId;
                        inus.Amount = amt;
                        inus.DateCreated = DateTime.Now;
                        _discProjUSRepository.Insert(inus);

                        var proj = _discProjDetRepository.FirstOrDefault(appId);
                        proj.CostPerLearner = grandtotal;
                        await _discProjDetRepository.UpdateAsync(proj);
                    }
                    catch (Exception ex)
                    {
                        status = "Error";
                    }
                    finally
                    {
                        if (status == "")
                        {
                            status = "Ok";
                        }
                    };
                }
                else
                {
                    status = "Limit";
                }
            }
            else
            {
                status = "Duplicate";
            }

            return status;
        }

        public async Task<PagedResultDto<DisscretionaryProjectUSForViewDto>> GetProjectUS(int projId, int usId)
        {

            var uss = await (from uses in _discProjUSRepository.GetAll()
                                   join us in _usRepository.GetAll() on uses.USId equals us.Id
                                   select new
                                   {
                                       Application = uses,
                                       UNIT_STANDARD_ID = us.UNIT_STANDARD_ID,
                                       UNIT_STD_TITLE = us.UNIT_STD_TITLE,
                                       Amount = uses.Amount,
                                       Id = uses.Id,
                                   })
                    .Where(a => a.Application.Id == projId && a.Application.USId == usId)
                    .ToListAsync();


            var cus = from o in uss
                      select new DisscretionaryProjectUSForViewDto()
                              {
                                  DiscretionaryProjectUS = new DiscretionaryProjectUSForView
                                  {
                                      UNIT_STANDARD_ID = o.UNIT_STANDARD_ID,
                                      Title = o.UNIT_STD_TITLE,
                                      USId = o.Application.USId,
                                      Amount = o.Amount,
                                      ApplicationId = o.Application.Id,
                                      Id = o.Id
                                  }
                              };

            var totalCount = uss.Count();

            return new PagedResultDto<DisscretionaryProjectUSForViewDto>(
                totalCount,
                cus.ToList()
            );
        }

        public async Task<PagedResultDto<DisscretionaryProjectUSForViewDto>> GetAllProjectUS(int projId)
        {

            var uss = await (from uses in _discProjUSRepository.GetAll()
                             join us in _usRepository.GetAll() on uses.USId equals us.Id
                             select new
                             {
                                 Application = uses,
                                 UNIT_STANDARD_ID = us.UNIT_STANDARD_ID,
                                 UNIT_STD_TITLE = us.UNIT_STD_TITLE,
                                 Amount = uses.Amount,
                                 Credits = us.UNIT_STD_NUMBER_OF_CREDITS,
                                 Id = uses.Id,
                             })
                    .Where(a => a.Application.ApplicationId == projId)
                    .ToListAsync();


            var cus = from o in uss
                      select new DisscretionaryProjectUSForViewDto()
                      {
                          DiscretionaryProjectUS = new DiscretionaryProjectUSForView
                          {
                              UNIT_STANDARD_ID = o.UNIT_STANDARD_ID,
                              Title = o.UNIT_STD_TITLE,
                              USId = o.Application.USId,
                              Amount = o.Amount,
                              Credits = o.Credits,
                              ApplicationId = o.Application.Id,
                              Id = o.Id
                          }
                      };

            var totalCount = uss.Count();

            return new PagedResultDto<DisscretionaryProjectUSForViewDto>(
                totalCount,
                cus.ToList()
            );
        }

        public async Task<string> validateProjSubmission(int projId)
        {
            string output = "";
            var proj = await _discProjRepository.FirstOrDefaultAsync(projId);
            var wind = await _windowRepository.FirstOrDefaultAsync(proj.GrantWindowId);
            if (wind.DeadlineTime <= DateTime.Now) { output = "Window is now Closed"; };

            var org = await _orgRepository.FirstOrDefaultAsync(proj.OrganisationId);
            if (org.SIC_Code == "") { output = output + ", Sic Code"; }
            if (org.CORE_BUSINESS == "") { output = output + ", Core Business"; }
            if (org.NUMBER_OF_EMPLOYEES == 0) { output = output + ", Number of Employees"; }
            if (org.BBBEE_Status == "") { output = output + ", BBBEE_Status"; }
            if (org.BBBEE_LEVEL == 0) { output = output + ", BBBEE Level"; }
            if (org.CHAMBER == null) { output = output + ", Sub Sector"; }
            if (org.CEO_Email == null) { output = output + "CEO Email"; }
            if (org.CEO_Name == null) { output = output + ", CEO Name"; }
            if (org.CEO_RaceId == null) { output = output + ", CEO Race"; }
            if (org.CEO_GenderId == null) { output = output + ", CEO Gender"; }
            if (org.CEO_Surname == null) { output = output + ", CEO Surname"; }
            if (org.Organisation_Contact_Cell_Number == null) { output = output + ", Contact Cellphone"; }
            if (org.Organisation_Contact_Email_Address == null) { output = output + ", Contact Email"; }
            if (org.Organisation_Contact_Phone_Number == null) { output = output + ", Contact Phone"; }
            if (org.Organisation_Registration_Number == null) { output = output + ", Registration Number"; }
            if (org.Senior_Rep_Email == null) { output = output + ", Rep Email"; }
            if (org.Senior_Rep_GenderId == null) { output = output + ", Rep Gender"; }
            if (org.Senior_Rep_Name == null) { output = output + ", Rep Name"; }
            if (org.Senior_Rep_RaceId == null) { output = output + ", Rep Race"; }
            if (org.Senior_Rep_Surname == null) { output = output + ", Rep Surname"; }
            if (org.SIC_Code == null) { output = output + ", SIC Code"; }

            var paddress = _addressRepository.GetAll().Where(a => a.organisationId == org.Id).FirstOrDefault();
            if (paddress == null)
            {
                output = output + ", Physical Address";
            }

            var bank = _bankRepository.GetAll().Where(a => a.OrganisationId == org.Id).FirstOrDefault();
            if (bank == null) { 
                output = output + ", Bank Details"; 
            }
            else
            {
                if (bank.AccountType == 0) { output = output + ", Bank Account Type"; }
                if (bank.Branch_Code == null) { output = output + ", Bank Branch Code"; }
                if (bank.Branch_Name == null) { output = output + ", Bank Branch Name"; }
                if (bank.Account_Holder == null) { output = output + ", Bank Account Holder"; }
                if (bank.Account_Number == null) { output = output + ", Bank Account Number"; }
                if (bank.Bank_Name == null) { output = output + ", Bank Name"; }
            }

            var projdet = (from dpd in _discProjDetRepository.GetAll()
                join ac in _adminCritRepository.GetAll() on dpd.SubCategoryId equals ac.Id
                join fa in _focusAreaRepository.GetAll() on dpd.FocusAreaId equals fa.Id
                select new
                {
                    Id = ac.Id,
                    ProjId = dpd.ProjectId,
                    AdminDesc = ac.AdminDesc,
                    FocusAreaDesc = fa.FocusAreaDesc,
                    FundingLimit = fa.FundinglImit,
                    DetailsId = dpd.Id
                }).Where(a => a.ProjId == projId);
             if (projdet.Count() == 0){ output = output + ", Application Details"; }

            var projdetus = (from pd in _discProjDetRepository.GetAll()
                        join em in _evalMethodRepository.GetAll() on pd.InterventionId equals em.Id
                        select new
                        {
                            Id = em.Id,
                            ProjId = pd.ProjectId,
                            EvalMthdDesc = em.EvalMthdDesc,
                            DetailsId = pd.Id
                        }).Where(a => a.ProjId == projId && a.EvalMthdDesc.Contains("Skills Programme") == true).ToList();

            bool hasus = true;
            foreach (var pr in projdetus)
            {
                var prus = _discProjUSRepository.GetAll().Where(a => a.ApplicationId == pr.DetailsId).Count();
                if (prus == 0) { hasus = false; }
            }

            var projdetprojus = (from pd in _discProjDetRepository.GetAll()
                             join ac in _adminCritRepository.GetAll() on pd.SubCategoryId equals ac.Id
                             select new
                             {
                                 Id = ac.Id,
                                 ProjId = pd.ProjectId,
                                 AdminDesc = ac.AdminDesc,
                                 DetailsId = pd.Id
                             }).Where(a => a.ProjId == projId && a.AdminDesc.Contains("Skills Programme") == true).ToList();

            //foreach (var pr in projdetprojus)
            //{
            //    var prus = _discProjUSRepository.GetAll().Where(a => a.ApplicationId == pr.DetailsId).Count();
            //    if (prus == 0) { hasus = false; }
            //}

            //if (!hasus) { output = output + ", Unit Standard Linking"; }

            var docs = _docRepository.GetAll().Where(a=>a.entityid == proj.Id).ToList();

            //Projects
            if (proj.ProjectTypeId == 2)
            {
                var tax = docs.Where(a => a.documenttype == "Tax Clearance").Count();
                if (tax == 0) { output = output + ", Tax Clearance"; }
                var reg = docs.Where(a => a.documenttype == "Company Registration").Count();
                if (reg == 0) { output = output + ", Company Registration"; }
                var bee = docs.Where(a => a.documenttype == "BEE Certificate").Count();
                if (reg == 0) { output = output + ", BBBEE Certificate"; }
                //var accr = docs.Where(a => a.documenttype == "Accreditation").Count();
                //var accchecked = false;
                //if (accr == 0) {
                //    foreach (var pr in projdet)
                //    {
                //        if (pr.AdminDesc.Contains("WIL") || pr.AdminDesc.Contains("AET") || pr.AdminDesc.Contains("Bursaries") || pr.AdminDesc.Contains("Candidacy") || (pr.AdminDesc.Contains("STEM") && !(pr.AdminDesc.Contains("Skills Program"))) || pr.AdminDesc.Contains("Incubation") || pr.AdminDesc.Contains("Short Learning"))
                //        {
                //            //No accreditation needed
                //        } else
                //        {
                //            if (pr.FocusAreaDesc.Contains("WIL") || pr.FocusAreaDesc.Contains("AET") || pr.FocusAreaDesc.Contains("Bursaries") || pr.FocusAreaDesc.Contains("Candidacy") || (pr.FocusAreaDesc.Contains("STEM") && !(pr.FocusAreaDesc.Contains("Skills Program"))) || pr.FocusAreaDesc.Contains("Incubation") || pr.FocusAreaDesc.Contains("Short Learning"))
                //            {
                //                //No accreditation needed
                //            }
                //            else
                //            {
                //                if (!accchecked)
                //                {
                //                    output = output + ", Accreditation";
                //                    accchecked = true;
                //                }
                //            }
                //        }
                //    }
                //};
                var decl = docs.Where(a => a.documenttype == "Declaration").Count();
                if (decl == 0) { output = output + ", Decalaration"; }
                var bankproof = docs.Where(a => a.documenttype == "Bank Proof").Count();
                if (bankproof == 0) { output = output + ", Proof of Bank"; }
            }


            //Strategic
            if (proj.ProjectTypeId == 4)
            {
                var tax = docs.Where(a => a.documenttype == "Tax Clearance").Count();
                if (tax == 0) { output = output + ", Tax Clearance"; }
                var reg = docs.Where(a => a.documenttype == "Company Registration").Count();
                if (reg == 0) { output = output + ", Company Registration"; }
                var bee = docs.Where(a => a.documenttype == "BEE Certificate").Count();
                if (reg == 0) { output = output + ", BBBEE Certificate"; }
                var proposal = docs.Where(a => a.documenttype == "Proposal").Count();
                if (proposal == 0) { output = output + ", Project Proposal"; }
                var decl = docs.Where(a => a.documenttype == "Declaration").Count();
                if (decl == 0) { output = output + ", Decalaration"; }
                var bankproof = docs.Where(a => a.documenttype == "Bank Proof").Count();
                if (bankproof == 0) { output = output + ", Proof of Bank"; }
            }

            //Research
            if (proj.ProjectTypeId == 3)
            {
                var tax = docs.Where(a => a.documenttype == "Tax Clearance").Count();
                if (tax == 0) { output = output + ", Tax Clearance"; }
                var reg = docs.Where(a => a.documenttype == "Company Registration").Count();
                if (reg == 0) { output = output + ", Company Registration"; }
                var bee = docs.Where(a => a.documenttype == "BEE Certificate").Count();
                if (reg == 0) { output = output + ", BBBEE Certificate"; }
                var proposal = docs.Where(a => a.documenttype == "Proposal").Count();
                if (proposal == 0) { output = output + ", Project Proposal"; }
                var decl = docs.Where(a => a.documenttype == "Declaration").Count();
                if (decl == 0) { output = output + ", Decalaration"; }
                var bankproof = docs.Where(a => a.documenttype == "Bank Proof").Count();
                if (bankproof == 0) { output = output + ", Proof of Bank"; }
                var qual = docs.Where(a => a.documenttype == "Qualifications").Count();
                if (qual == 0) { output = output + ", Proof of Qualifications"; }
                var letters = docs.Where(a => a.documenttype == "Reference").Count();
                if (letters == 0) { output = output + ", Reference Letters"; }
                var cv = docs.Where(a => a.documenttype == "CV").Count();
                if (cv == 0) { output = output + ", CV"; }
            }

            if (output.StartsWith(",")) { output = output.Substring(2,output.Length - 2); }

            return output;
        }
    }
}
