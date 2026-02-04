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
using CHIETAMIS.People;
using CHIETAMIS.People.Dtos;
using CHIETAMIS.Sdfs.Dtos;
using CHIETAMIS.DiscretionaryProjects;
using CHIETAMIS.DiscretionaryWindows;
using CHIETAMIS.UnitStandards;
using CHIETAMIS.UnitStandards.Dtos;
using CHIETAMIS.Lookups;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Configuration;
using CHIETAMIS.Authorization.Users;
using CHIETAMIS.Users;
using CHIETAMIS.PaymentPortals;
using CHIETAMIS.PaymentPortals.DTOs;
using Abp.Domain.Entities;
using CHIETAMIS.Workflows;
using Org.BouncyCastle.Ocsp;
using CHIETAMIS.Workflows.Dto;
using System.Threading.Tasks;
using CHIETAMIS.Workflows;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq.Dynamic.Core;

namespace CHIETAMIS.DiscretionaryProjects
{
    public class DiscretionaryProjectApprovalAppService : CHIETAMISAppServiceBase
    {
        private readonly IUserEmailer _userEmailer;

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
        private readonly IRepository<DiscretionaryProjectDetailsApproval> _discProjDetRepository;
        private readonly IRepository<DiscretionaryProjectUSApproval> _discProjUSRepository;
        private readonly IRepository<UnitStandard> _usRepository;
        private readonly IRepository<BankDetails> _bankRepository;
        private readonly IRepository<OrganisationPhysicalAddress> _addressRepository;
        private readonly IRepository<Document> _docRepository;
        private readonly IRepository<DiscretionaryDocumentApproval> _docApprovalRepository;
        private readonly IRepository<GrantApprovalStatus> _grantApprovalStatusRepository;
        private readonly IRepository<GrantApprovalType> _grantApprovalTypeRepository;
        private readonly IRepository<DiscretionaryDetailApproval> _discDetailApprovalRepository;
        private readonly IRepository<DiscretionaryGECApproval> _discGECApprovalRepository;
        private readonly IRepository<DiscretionaryGACApproval> _discGACApprovalRepository;
        private readonly IRepository<DiscretionaryGCApproval> _discGCApprovalRepository;
        private readonly IRepository<Organisation_Sdf> _orgSdfRepository;
        private readonly IRepository<SdfDetails> _sdfRepository;
        private readonly IRepository<Person> _personRepository;
        private readonly IRepository<ApplicationTranche> _appTranche;
        private readonly IRepository<Tranche_Approvals> _trancheApprovals;
        private readonly IRepository<ApplicationTrancheDetails> _appTrancheDetails;
        private readonly IRepository<GrantDeliverableSchedule> _grantDeliverableScheduleRepository;
        private readonly IRepository<GrantProgramDeliverables> _grantProgramDeliverablesRepository;
        private readonly IRepository<wfRequest> _requestRepository;
        private readonly IRepository<wfRequestData> _requestDataRepository;
        private readonly IRepository<DiscretionaryTrancheBatch> _trBatchRepository;
        private readonly IRepository<DiscrationaryTrancheBatchRequests> _trBatchRequestsRepository;
        private readonly IRepository<DgPaymentTrancheType> _trancheType;
        private readonly IRepository<ApplicationTranche> _applicationTranche;
        private readonly IRepository<GrantDeliverableSchedule> _grantDeliverableSchedule;
        private readonly IRepository<GrantProgramDeliverables> _grantProgramDeliverables;
        private readonly IRepository<wfTimer> _timerRepository;
        private readonly IRepository<wfTransitionAction> _transitionActionRepository;
        private readonly IRepository<wfRequestAction> _requestActionRepository;
        private readonly IRepository<wfTransition> _transitionRepository;
        private readonly IRepository<SpecialistProject> _specRepository;
        private readonly IRepository<wfState> _stateRepository;
        

        public DiscretionaryProjectApprovalAppService(IRepository<DiscretionaryProject> dicprojRepository,
                                              IRepository<Organisation> orgRepository,
                                              IRepository<Organisation_Sdf> orgsdfRepository,
                                              IRepository<DiscretionaryStatus> discStatusRepository,
                                              IRepository<DiscretionaryWindow> windowRepository,
                                              IRepository<WindowParams> windowParamRepository,
                                              IRepository<ProjectType> projTypeRepository,
                                              IRepository<FocusArea> focusAreaRepository,
                                              IRepository<AdminCriteria> adminCritRepository,
                                              IRepository<EvaluationMethod> evalMethodRepository,
                                              IRepository<DiscretionaryProjectDetailsApproval> discprojDetRepository,
                                              IRepository<DiscretionaryProjectUSApproval> discProjUSRepository,
                                              IRepository<UnitStandard> usRepository,
                                              IRepository<BankDetails> bankRepository,
                                              IRepository<OrganisationPhysicalAddress> addressRepository,
                                              IRepository<Document> docRepository,
                                              IRepository<DiscretionaryDocumentApproval> docApprovalRepository,
                                              IRepository<GrantApprovalStatus> grantApprovalStatusRepository,
                                              IRepository<GrantApprovalType> grantApprovalTypeRepository,
                                              IRepository<DiscretionaryDetailApproval> discDetailApprovalRepository,
                                              IRepository<DiscretionaryGECApproval> discGECApprovalRepository,
                                              IRepository<DiscretionaryGACApproval> discGACApprovalRepository,
                                              IRepository<DiscretionaryGCApproval> discGCApprovalRepository,
                                              IRepository<Organisation_Sdf> orgSdfRepository,
                                              IRepository<SdfDetails> sdfRepository,
                                              IRepository<Person> personRepository,
                                              IRepository<ApplicationTranche> appTranche,
                                              IRepository<ApplicationTrancheDetails> appTrancheDetails,
                                              IRepository<Tranche_Approvals> trancheApprovals,
                                              IRepository<GrantDeliverableSchedule> grantDeliverableScheduleRepository,
                                              IRepository<GrantProgramDeliverables> grantProgramDeliverablesRepository,
                                              IRepository<wfRequest> requestRepository,
                                              IRepository<wfRequestData> requestDataRepository,
                                              IRepository<DiscretionaryTrancheBatch> trBatchRepository,
                                              IRepository<DiscrationaryTrancheBatchRequests> trBatchRequestsRepository,
                                              IRepository<ApplicationTranche> applicationTranche,
                                              IRepository<DgPaymentTrancheType> trancheType,
                                              IRepository<GrantDeliverableSchedule> grantDeliverableSchedule,
                                              IRepository<GrantProgramDeliverables> grantProgramDeliverables,
                                              IRepository<wfTransitionAction> transitionActionRepository,
                                              IRepository<wfTimer> timerRepository,
                                              IRepository<wfTransition> transitionRepository,
                                              IRepository<wfRequestAction> requestActionRepository,
                                              IUserEmailer userEmailer,
                                              IRepository<SpecialistProject> specRepository,
                                              IRepository<wfState> stateRepository)
        {
            _timerRepository = timerRepository;
            _transitionRepository = transitionRepository;
            _transitionActionRepository = _transitionActionRepository;
            _requestActionRepository = requestActionRepository;
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
            _discProjUSRepository = discProjUSRepository;
            _usRepository = usRepository;
            _bankRepository = bankRepository;
            _addressRepository = addressRepository;
            _docRepository = docRepository;
            _docApprovalRepository = docApprovalRepository;
            _grantApprovalStatusRepository = grantApprovalStatusRepository;
            _grantApprovalTypeRepository = grantApprovalTypeRepository;
            _userEmailer = userEmailer;
            _discDetailApprovalRepository = discDetailApprovalRepository;
            _discGECApprovalRepository = discGECApprovalRepository;
            _discGACApprovalRepository = discGACApprovalRepository;
            _discGCApprovalRepository = discGCApprovalRepository;
            _orgSdfRepository = orgSdfRepository;
            _sdfRepository = sdfRepository;
            _personRepository = personRepository;
            _appTranche = appTranche;
            _appTrancheDetails = appTrancheDetails;
            _trancheApprovals = trancheApprovals;
            _grantDeliverableScheduleRepository = grantDeliverableScheduleRepository;
            _grantProgramDeliverablesRepository = grantProgramDeliverablesRepository;
            _requestRepository = requestRepository;
            _requestDataRepository = requestDataRepository;
            _trBatchRepository = trBatchRepository;
            _trBatchRequestsRepository = trBatchRequestsRepository;
            _applicationTranche = applicationTranche;
            _trancheType = trancheType;
            _grantDeliverableSchedule = grantDeliverableSchedule;
            _grantProgramDeliverables = grantProgramDeliverables;
            _transitionActionRepository = transitionActionRepository;
            _specRepository = specRepository;
            _stateRepository = stateRepository;
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
            }
            else
            {
                throw new InvalidOperationException("Duplicate entry detected.");
            }
        }

        public async Task UpdateGECApplication(int applicationId, int cont, int newl, decimal cost, int userid)
        {
            var appl = _discProjDetRepository.GetAll().Where(a => a.Id == applicationId);

            if (appl.Count() == 1)
            {
                var dg = _discProjDetRepository.Get(applicationId);
                dg.GEC_Continuing = cont;
                dg.GEC_New = newl;
                dg.GEC_CostPerLearner = cost;
                dg.GAC_Continuing = cont;
                dg.GAC_New = newl;
                dg.GAC_CostPerLearner = cost;
                dg.UsrUpd = userid;
                dg.DteUpd = DateTime.Now;

                var app = ObjectMapper.Map<DiscretionaryProjectDetailsApproval>(dg);

                await _discProjDetRepository.UpdateAsync(app);
            }
            else
            {
                throw new InvalidOperationException("Not found.");
            }
        }

        public async Task UpdateGACApplication(int applicationId, int cont, int newl, decimal cost, int userid)
        {
            var appl = _discProjDetRepository.GetAll().Where(a => a.Id == applicationId);

            if (appl.Count() == 1)
            {
                var dg = _discProjDetRepository.Get(applicationId);
                dg.GAC_Continuing = cont;
                dg.GAC_New = newl;
                dg.GAC_CostPerLearner = cost;
                dg.GC_Continuing = cont;
                dg.GC_New = newl;
                dg.GC_CostPerLearner = cost;
                dg.UsrUpd = userid;
                dg.DteUpd = DateTime.Now;

                var app = ObjectMapper.Map<DiscretionaryProjectDetailsApproval>(dg);
            }
            else
            {
                throw new InvalidOperationException("Not found.");
            }
        }

        public async Task UpdateGCApplication(int applicationId, int cont, int newl, decimal cost, int userid)
        {
            var appl = _discProjDetRepository.GetAll().Where(a => a.Id == applicationId);

            if (appl.Count() == 1)
            {
                var dg = _discProjDetRepository.Get(applicationId);
                dg.GC_Continuing = cont;
                dg.GC_New = newl;
                dg.GC_CostPerLearner = cost;
                dg.UsrUpd = userid;
                dg.DteUpd = DateTime.Now;

                var app = ObjectMapper.Map<DiscretionaryProjectDetailsApproval>(dg);

                await _discProjDetRepository.UpdateAsync(app);
            }
            else
            {
                throw new InvalidOperationException("Not found.");
            }
        }

        public async Task ChangeApplicationStatus(int applicationId, int statusId, int userId)
        {
            var appr = _discProjDetRepository.GetAll().Where(a => a.Id == applicationId);

            if (appr.Count() != 0)
            {
                var cappr = appr.FirstOrDefault();
                cappr.DteUpd = DateTime.Now;
                cappr.UsrUpd = userId;
                cappr.ApplicationStatusId = statusId;
                _discProjDetRepository.Update(cappr);
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

                //var rsa = 
            }
        }

        public async Task<PagedResultDto<DiscretionaryProjectForViewDto>> GetOrgProjects(int OrganisationId)
        {
            var discprojs = await (from discgrant in _discProjRepository.GetAll()
                                   join org in _orgRepository.GetAll() on discgrant.OrganisationId equals org.Id
                                   join orgsdf in _orgsdfRepository.GetAll() on discgrant.OrganisationId equals orgsdf.OrganisationId
                                   join stat in _discStatusRepository.GetAll() on discgrant.ProjectStatusID equals stat.Id
                                   join dg in _windowRepository.GetAll() on discgrant.GrantWindowId equals dg.Id
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
                                       DiscretionaryProject = discgrant,
                                       DiscretionaryGrant = dg,
                                       //Params = prms,
                                       SdfId = orgsdf.SdfId,
                                       OrgSdfId = orgsdf.Id,
                                       Status = stat.StatusDesc,
                                       ProjType = projtype.ProjTypDesc,
                                       FocusArea = focs.FocusAreaDesc,
                                       SubCategory = subs.AdminDesc,
                                       Intervention = inters.EvalMthdDesc,
                                   })
                    .Where(a => a.OrganisationId == OrganisationId)
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
                                      ProjectId = o.DiscretionaryProject.Id,
                                      Title = o.DiscretionaryGrant.Title,
                                      ProjType = o.ProjType,
                                      FocusArea = o.FocusArea,
                                      SubCategory = o.SubCategory,
                                      ProjectStatDte = o.DiscretionaryProject.ProjectStatDte,
                                      ProjectEndDate = o.DiscretionaryGrant.DeadlineTime,
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

        public async Task<PagedResultDto<DiscretionaryProjectForViewDto>> GetProjectsLinkedtoSdf(DiscretionaryProjectRequestDto input)
        {

            var discprojs = await (from discgrant in _discProjRepository.GetAll()
                                   join projdet in _discProjDetRepository.GetAll() on discgrant.Id equals projdet.ProjectId
                                   join org in _orgRepository.GetAll() on discgrant.OrganisationId equals org.Id
                                   join orgsdf in _orgsdfRepository.GetAll() on discgrant.OrganisationId equals orgsdf.OrganisationId
                                   join stat in _discStatusRepository.GetAll() on discgrant.ProjectStatusID equals stat.Id
                                   join dg in _windowRepository.GetAll() on discgrant.GrantWindowId equals dg.Id
                                   //join prms in _windowParamRepository.GetAll() on discgrant.WindowParamId equals prms.Id
                                   join dpd in _discProjDetRepository.GetAll() on discgrant.WindowParamId equals dpd.Id
                                   join projtype in _projTypeRepository.GetAll() on dpd.ProjectTypeId equals projtype.Id
                                   join focarea in _focusAreaRepository.GetAll() on dpd.FocusAreaId equals focarea.Id
                                   join subcat in _adminCritRepository.GetAll() on dpd.SubCategoryId equals subcat.Id
                                   join interv in _evalMethodRepository.GetAll() on dpd.InterventionId equals interv.Id
                                   select new
                                   {
                                       SDLNo = org.SDL_No,
                                       OrganisationId = org.Id,
                                       DiscretionaryProject = discgrant,
                                       DiscretionaryGrant = dg,
                                       SdfId = orgsdf.SdfId,
                                       //Params = prms,
                                       OrgSdfId = orgsdf.Id,
                                       Status = stat.StatusDesc,
                                       ProjType = projtype.ProjTypDesc,
                                       FocusArea = focarea.FocusAreaDesc,
                                       SubCategory = subcat.AdminDesc,
                                       Intervention = interv.EvalMthdDesc,
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
                                      Title = o.DiscretionaryGrant.Title,
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
                                   join projdet in _discProjDetRepository.GetAll() on discgrant.Id equals projdet.ProjectId
                                   join org in _orgRepository.GetAll() on discgrant.OrganisationId equals org.Id
                                   join orgsdf in _orgsdfRepository.GetAll() on discgrant.OrganisationId equals orgsdf.OrganisationId
                                   join stat in _discStatusRepository.GetAll() on discgrant.ProjectStatusID equals stat.Id
                                   join wind in _windowRepository.GetAll() on discgrant.GrantWindowId equals wind.Id
                                   //join prms in _windowParamRepository.GetAll() on discgrant.WindowParamId equals prms.Id
                                   join projtype in _projTypeRepository.GetAll() on projdet.ProjectTypeId equals projtype.Id
                                   join focarea in _focusAreaRepository.GetAll() on projdet.FocusAreaId equals focarea.Id
                                   join subcat in _adminCritRepository.GetAll() on projdet.SubCategoryId equals subcat.Id
                                   join interv in _evalMethodRepository.GetAll() on projdet.InterventionId equals interv.Id
                                   select new
                                   {
                                       SDLNo = org.SDL_No,
                                       OrganisationId = org.Id,
                                       DiscretionaryProject = discgrant,
                                       DiscretionaryGrant = wind,
                                       SdfId = orgsdf.SdfId,
                                       OrgSdfId = orgsdf.Id,
                                       Status = stat.StatusDesc,
                                       ProjType = projtype.ProjTypDesc,
                                       FocusArea = focarea.FocusAreaDesc,
                                       SubCategory = subcat.AdminDesc,
                                       Intervention = interv.EvalMthdDesc
                                   })
                    .Where(a => a.DiscretionaryProject.Id == Id)
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
                                      Title = o.DiscretionaryGrant.Title,
                                      ProjType = o.ProjType,
                                      FocusArea = o.FocusArea,
                                      SubCategory = o.SubCategory,
                                      ProjectStatDte = o.DiscretionaryProject.ProjectStatDte,
                                      ProjectEndDate = o.DiscretionaryGrant.DeadlineTime,
                                      ProjectStatus = o.Status,
                                      ProjShortNam = o.DiscretionaryProject.ProjShortNam,
                                      ProjectNam = o.DiscretionaryProject.ProjectNam,
                                      SubmissionDte = o.DiscretionaryProject.SubmissionDte,
                                      RSAId = o.DiscretionaryProject.RSAId,
                                      WindowId = o.DiscretionaryGrant.Id,
                                      Id = o.DiscretionaryProject.Id
                                  }
                              };

            var totalCount = discproject.Count();

            return new PagedResultDto<DiscretionaryProjectForViewDto>(
                totalCount,
                discproject.ToList()
            );
        }

        public async Task<PagedResultDto<DiscretionaryProjectForViewDto>> GetProjectsForWindow(int userid, string currentprofile)
        {
            var filterappstatus = 0;
            var filterappstatus2 = 0;
            if (currentprofile == "REGIONAL MANAGER") { filterappstatus = 2; }
            if (filterappstatus == 0) { filterappstatus = 7; }

            var discprojs = await (from discgrant in _discProjRepository.GetAll()
                                   join dpda in _discProjDetRepository.GetAll() on discgrant.Id equals dpda.ProjectId
                                   join org in _orgRepository.GetAll() on discgrant.OrganisationId equals org.Id
                                   join orgsdf in _orgsdfRepository.GetAll() on discgrant.OrganisationId equals orgsdf.OrganisationId
                                   join stat in _discStatusRepository.GetAll() on discgrant.ProjectStatusID equals stat.Id
                                   join dg in _windowRepository.GetAll() on discgrant.GrantWindowId equals dg.Id
                                   //join prms in _windowParamRepository.GetAll() on discgrant.WindowParamId equals prms.Id
                                   join projtype in _projTypeRepository.GetAll() on dpda.ProjectTypeId equals projtype.Id
                                   join focarea in _focusAreaRepository.GetAll() on dpda.FocusAreaId equals focarea.Id
                                   join subcat in _adminCritRepository.GetAll() on dpda.SubCategoryId equals subcat.Id
                                   join interv in _evalMethodRepository.GetAll() on dpda.InterventionId equals interv.Id
                                   select new
                                   {
                                       Organisation = org,
                                       SDLNo = org.SDL_No,
                                       Organisation_Name = org.Organisation_Name,
                                       DiscretionaryProject = discgrant,
                                       DiscretionaryGrant = dg,
                                       SdfId = orgsdf.SdfId,
                                       OrgSdfId = orgsdf.Id,
                                       Status = stat.StatusDesc,
                                       ProjType = projtype.ProjTypDesc,
                                       FocusArea = focarea.FocusAreaDesc,
                                       SubCategory = subcat.AdminDesc,
                                       Intervention = interv.EvalMthdDesc,
                                       ApplicationStatus = dpda.ApplicationStatusId,
                                       CurrentApprover = dpda.Current_Approver,
                                       ApplicationId = dpda.Id
                                   })
                    .Where(a => a.CurrentApprover == currentprofile & (a.ApplicationStatus == filterappstatus || (currentprofile == "RSA" && a.ApplicationStatus == 1)))
                    .ToListAsync();


            var discproject = from o in discprojs
                              select new DiscretionaryProjectForViewDto()
                              {
                                  DiscretionaryProject = new DiscretionaryProjectOutputDto
                                  {
                                      SDLNo = o.SDLNo,
                                      Organisation_Name = o.Organisation.Organisation_Name,
                                      OrganisationId = o.Organisation.Id,
                                      SdfId = o.SdfId,
                                      ProjectId = o.DiscretionaryProject.Id,
                                      ApplicationId = o.ApplicationId,
                                      Title = o.DiscretionaryGrant.Title,
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
                discproject.Distinct().ToList()
            );
        }

        public async Task<PagedResultDto<DiscretionaryProjectForViewDto>> GetProjectsForWindowRegManager(int userid)
        {

            var discprojs = await (from discgrant in _discProjRepository.GetAll()
                                   join projdet in _discProjDetRepository.GetAll() on discgrant.Id equals projdet.ProjectId
                                   join org in _orgRepository.GetAll() on discgrant.OrganisationId equals org.Id
                                   join orgsdf in _orgsdfRepository.GetAll() on discgrant.OrganisationId equals orgsdf.OrganisationId
                                   join stat in _discStatusRepository.GetAll() on discgrant.ProjectStatusID equals stat.Id
                                   join dg in _windowRepository.GetAll() on discgrant.GrantWindowId equals dg.Id
                                   //join prms in _windowParamRepository.GetAll() on discgrant.WindowParamId equals GetProjectsForWindowId
                                   join projtype in _projTypeRepository.GetAll() on projdet.ProjectTypeId equals projtype.Id
                                   join focarea in _focusAreaRepository.GetAll() on projdet.FocusAreaId equals focarea.Id
                                   join subcat in _adminCritRepository.GetAll() on projdet.SubCategoryId equals subcat.Id
                                   join interv in _evalMethodRepository.GetAll() on projdet.InterventionId equals interv.Id
                                   select new
                                   {
                                       SDLNo = org.SDL_No,
                                       Organisation_Name = org.Organisation_Name,
                                       OrganisationId = org.Id,
                                       DiscretionaryProject = discgrant,
                                       DiscretionaryGrant = dg,
                                       SdfId = orgsdf.SdfId,
                                       OrgSdfId = orgsdf.Id,
                                       Status = stat.StatusDesc,
                                       ProjType = projtype.ProjTypDesc,
                                       FocusArea = focarea.FocusAreaDesc,
                                       SubCategory = subcat.AdminDesc,
                                       Intervention = interv.EvalMthdDesc,
                                   })
                    .Where(a => (a.Status == "RSA Review Completed" || a.Status == "Rejected after Full Assessment") && a.DiscretionaryProject.RegManagerId == userid)
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
                                      ProjectId = o.DiscretionaryProject.Id,
                                      Title = o.DiscretionaryGrant.Title,
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
                discproject.Distinct().ToList()
            );
        }

        public async Task<PagedResultDto<DiscretionaryProjectForViewDto>> GetProjectsApprovalRSA(int userid)
        {
            var discprojs = await (from discgrant in _discProjRepository.GetAll()
                                   join projdet in _discProjDetRepository.GetAll() on discgrant.Id equals projdet.ProjectId
                                   join org in _orgRepository.GetAll() on discgrant.OrganisationId equals org.Id
                                   join stat in _discStatusRepository.GetAll() on discgrant.ProjectStatusID equals stat.Id
                                   join dg in _windowRepository.GetAll() on discgrant.GrantWindowId equals dg.Id
                                   join projtype in _projTypeRepository.GetAll() on discgrant.ProjectTypeId equals projtype.Id

                                   select new
                                   {
                                       SDLNo = org.SDL_No,
                                       Organisation_Name = org.Organisation_Name,
                                       OrganisationId = org.Id,
                                       DiscretionaryProject = discgrant,
                                       DiscretionaryGrant = dg,
                                       Status = stat.StatusDesc,
                                       ProjType = projtype.ProjTypDesc
                                   })
                    .Where(a => (a.Status == "Submitted by online user" || a.Status == "Allocated to RSA") && a.DiscretionaryProject.RSAId == userid)
                    .ToListAsync();


            var discproject = from o in discprojs
                              select new DiscretionaryProjectForViewDto()
                              {
                                  DiscretionaryProject = new DiscretionaryProjectOutputDto
                                  {
                                      SDLNo = o.SDLNo,
                                      Organisation_Name = o.Organisation_Name,
                                      OrganisationId = o.OrganisationId,
                                      ProjectId = o.DiscretionaryProject.Id,
                                      Title = o.DiscretionaryGrant.Title,
                                      ProjType = o.ProjType,
                                      //FocusArea = o.FocusArea,
                                      //SubCategory = o.SubCategory,
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
                discproject.Distinct().ToList()
            );
        }

        public async Task<PagedResultDto<DiscretionaryProjectForViewDto>> GetDiscProjectPaymentApprovalsView(int userid)
        {
            var discprojs = (from discgrant in _discProjRepository.GetAll()
                             join projdet in _discProjDetRepository.GetAll() on discgrant.Id equals projdet.ProjectId
                             join org in _orgRepository.GetAll() on discgrant.OrganisationId equals org.Id
                             join stat in _discStatusRepository.GetAll() on discgrant.ProjectStatusID equals stat.Id
                             join dg in _windowRepository.GetAll() on discgrant.GrantWindowId equals dg.Id
                             join projtype in _projTypeRepository.GetAll() on discgrant.ProjectTypeId equals projtype.Id
                             join gc in _discGCApprovalRepository.GetAll().Where(a => a.ApprovalStatusId == 1) on projdet.Id equals gc.ApplicationId
                             //join focarea in _focusAreaRepository.GetAll() on projdet.FocusAreaId equals focarea.Id into fap
                             //from faps in fap.DefaultIfEmpty()
                             //join subcat in _adminCritRepository.GetAll() on projdet.SubCategoryId equals subcat.Id into scp
                             //from scps in scp.DefaultIfEmpty()
                             //join interv in _evalMethodRepository.GetAll() on projdet.InterventionId equals interv.Id into emp
                             //from emps in emp.DefaultIfEmpty()
                             select new
                             {
                                 OrganisationSDL = org.SDL_No,
                                 OrgName = org.Organisation_Name,
                                 OrgId = org.Id,
                                 DiscretionaryProject = discgrant,
                                 DiscretionaryGrant = dg,
                                 SDLNo = org.SDL_No,
                                 Status = stat.StatusDesc,
                                 ProjType = projtype.ProjTypDesc,
                                 //FocusArea = faps.FocusAreaDesc,
                                 //SubCategory = scps.AdminDesc,
                                 //Intervention = emps.EvalMthdDesc,
                                 AppStatus = projdet.ApplicationStatusId,
                                 Id = dg.Id
                             })
                    .Where(a => (a.Status == "Evaluations Complete" || a.Status == "RSA Review Completed") && a.Id > 8 && a.AppStatus == 1 && a.DiscretionaryProject.RSAId == userid)
                    .ToList()
                    .Distinct();

            //var trcheck = (from dp in discprojs
            //               join ta in _trancheApprovals.GetAll() on dp.Id equals ta.ApplicationId into trch
            //               from trchs in trch.DefaultIfEmpty()
            //               select new { dp = dp, Tranche = trchs.Approval_Status }).ToList()
            //               //.Where(a => a.Tranche == null)
            //               .ToList();


            var discproject = from o in discprojs
                              select new DiscretionaryProjectForViewDto()
                              {
                                  DiscretionaryProject = new DiscretionaryProjectOutputDto
                                  {
                                      SDLNo = o.OrganisationSDL,
                                      Organisation_Name = o.OrgName,
                                      OrganisationId = o.OrgId,
                                      ProjectId = o.DiscretionaryProject.Id,
                                      Title = o.DiscretionaryGrant.Title,
                                      ProjType = o.ProjType,
                                      //FocusArea = o.FocusArea,
                                      //SubCategory = o.SubCategory,
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
                discproject.Distinct().ToList()
            );
        }

        public async Task<PagedResultDto<DiscretionaryProjectForViewDto>> GetProjectsAdminForWindow()
        {

            var discprojs = await (from discgrant in _discProjRepository.GetAll()
                                   join projdet in _discProjDetRepository.GetAll() on discgrant.Id equals projdet.ProjectId
                                   join org in _orgRepository.GetAll() on discgrant.OrganisationId equals org.Id
                                   join orgsdf in _orgsdfRepository.GetAll() on discgrant.OrganisationId equals orgsdf.OrganisationId
                                   join stat in _discStatusRepository.GetAll() on discgrant.ProjectStatusID equals stat.Id
                                   join dg in _windowRepository.GetAll() on discgrant.GrantWindowId equals dg.Id
                                   //join prms in _windowParamRepository.GetAll() on discgrant.WindowParamId equals prms.Id
                                   join projtype in _projTypeRepository.GetAll() on projdet.ProjectTypeId equals projtype.Id
                                   join focarea in _focusAreaRepository.GetAll() on projdet.FocusAreaId equals focarea.Id
                                   join subcat in _adminCritRepository.GetAll() on projdet.SubCategoryId equals subcat.Id
                                   join interv in _evalMethodRepository.GetAll() on projdet.InterventionId equals interv.Id
                                   select new
                                   {
                                       SDLNo = org.SDL_No,
                                       Organisation_Name = org.Organisation_Name,
                                       OrganisationId = org.Id,
                                       DiscretionaryProject = discgrant,
                                       DiscretionaryGrant = dg,
                                       SdfId = orgsdf.SdfId,
                                       OrgSdfId = orgsdf.Id,
                                       Status = stat.StatusDesc,
                                       ProjType = projtype.ProjTypDesc,
                                       FocusArea = focarea.FocusAreaDesc,
                                       SubCategory = subcat.AdminDesc,
                                       Intervention = interv.EvalMthdDesc,
                                   })
                    .Where(a => a.Status == "Submitted by online user")
                    //.Where(a => a.OrgSdfId == input.OrgSdfId && a.DiscretionaryProject.WindowParamId == a.Params.Id)
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
                                      ProjectId = o.DiscretionaryProject.Id,
                                      Title = o.DiscretionaryGrant.Title,
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
                discproject.Distinct().ToList()
            );
        }

        public async Task<DiscretionaryProjectDto> GetProjectById(int id)
        {
            var proj = await _discProjRepository.GetAsync(id);

            var output = ObjectMapper.Map<DiscretionaryProjectDto>(proj);

            return output;
        }

        public async Task<DiscretionaryProjectDetailsApprovalDto> GetDGProjectDetById(int id)
        {
            var proj = await _discProjDetRepository.GetAsync(id);

            var output = ObjectMapper.Map<DiscretionaryProjectDetailsApprovalDto>(proj);

            return output;
        }

        public async Task<DiscretionaryProjectDetailsApprovalsView> GetProjectDetailsView(int id)
        {
            var proj = (from prdet in _discProjDetRepository.GetAll()
                        join p in _discProjRepository.GetAll() on prdet.ProjectId equals p.Id
                        join o in _orgRepository.GetAll() on p.OrganisationId equals o.Id
                        join prtype in _projTypeRepository.GetAll() on prdet.ProjectTypeId equals prtype.Id
                        join focarea in _focusAreaRepository.GetAll() on prdet.FocusAreaId equals focarea.Id
                        join subcat in _adminCritRepository.GetAll() on prdet.SubCategoryId equals subcat.Id
                        join interv in _evalMethodRepository.GetAll() on prdet.InterventionId equals interv.Id
                        select new DiscretionaryProjectDetailsApprovalsView()
                            {
                                Id = prdet.Id,
                                SDL = o.SDL_No,
                                Organisation_Name = o.Organisation_Name,
                                Organisation_Trade_Name = o.Organisation_Trading_Name,
                                ProjectId = prdet.ProjectId,
                                Contract_Number = prdet.Contract_Number,
                                ProjectType = prtype.ProjTypDesc,
                                FocusArea = focarea.FocusAreaDesc,
                                SubCategory = subcat.AdminDesc,
                                Intervention = interv.EvalMthdDesc,
                                OtherIntervention = prdet.OtherIntervention,
                                Province = prdet.Province,
                                Municipality = prdet.Municipality,
                                GC_New = prdet.GC_New,
                                GC_Continuing = prdet.GC_Continuing,
                                GC_CostPerLearner = prdet.GC_CostPerLearner
                            })
                .Where(a => a.Id == id)
                .FirstOrDefault();

            

            return proj;
        }
        public async Task<List<DiscretionaryProjectDetailsApprovalsView>> GetProjectDetailsListView(int projectId)
        {
            var proj = (from prdet in _discProjDetRepository.GetAll()
                        join p in _discProjRepository.GetAll() on prdet.ProjectId equals p.Id
                        join o in _orgRepository.GetAll() on p.OrganisationId equals o.Id
                        join prtype in _projTypeRepository.GetAll() on prdet.ProjectTypeId equals prtype.Id
                        join focarea in _focusAreaRepository.GetAll() on prdet.FocusAreaId equals focarea.Id
                        join subcat in _adminCritRepository.GetAll() on prdet.SubCategoryId equals subcat.Id
                        join interv in _evalMethodRepository.GetAll() on prdet.InterventionId equals interv.Id
                        select new DiscretionaryProjectDetailsApprovalsView
                        {
                            Id = prdet.Id,
                            SDL = o.SDL_No,
                            Organisation_Name = o.Organisation_Name,
                            Organisation_Trade_Name = o.Organisation_Trading_Name,
                            ProjectId = prdet.ProjectId,
                            Contract_Number = prdet.Contract_Number,
                            ProjectType = prtype.ProjTypDesc,
                            FocusArea = focarea.FocusAreaDesc,
                            SubCategory = subcat.AdminDesc,
                            Intervention = interv.EvalMthdDesc,
                            OtherIntervention = prdet.OtherIntervention,
                            Province = prdet.Province,
                            Municipality = prdet.Municipality,
                            GC_New = prdet.GC_New,
                            GC_Continuing = prdet.GC_Continuing,
                            GC_CostPerLearner = prdet.GC_CostPerLearner
                        })
                        
                        .Where(a => a.ProjectId == projectId)
                        .ToList();

            return proj;
        }



        public async Task<PagedResultDto<DiscretionaryProjectForViewDto>> GetProject(int id)
        {

            var discprojs = await (from discgrant in _discProjRepository.GetAll()
                                   join projdet in _discProjDetRepository.GetAll() on discgrant.Id equals projdet.ProjectId
                                   join org in _orgRepository.GetAll() on discgrant.OrganisationId equals org.Id
                                   join orgsdf in _orgsdfRepository.GetAll() on discgrant.OrganisationId equals orgsdf.OrganisationId
                                   join stat in _discStatusRepository.GetAll() on discgrant.ProjectStatusID equals stat.Id
                                   join dg in _windowRepository.GetAll() on discgrant.GrantWindowId equals dg.Id
                                   //join prms in _windowParamRepository.GetAll() on discgrant.WindowParamId equals prms.Id
                                   join projtype in _projTypeRepository.GetAll() on projdet.ProjectTypeId equals projtype.Id
                                   join focarea in _focusAreaRepository.GetAll() on projdet.FocusAreaId equals focarea.Id
                                   join subcat in _adminCritRepository.GetAll() on projdet.SubCategoryId equals subcat.Id
                                   join interv in _evalMethodRepository.GetAll() on projdet.InterventionId equals interv.Id
                                   select new
                                   {
                                       SDLNo = org.SDL_No,
                                       OrganisationId = org.Id,
                                       DiscretionaryProject = discgrant,
                                       DiscretionaryGrant = dg,
                                       SdfId = orgsdf.SdfId,
                                       OrgSdfId = orgsdf.Id,
                                       Status = stat.StatusDesc,
                                       ProjType = projtype.ProjTypDesc,
                                       FocusArea = focarea.FocusAreaDesc,
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
                                      Title = o.DiscretionaryGrant.Title,
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

        public async Task CreateEditApplicationDetails(DiscretionaryProjectDetailsApprovalDto input)
        {
            var dgprojdet = _discProjDetRepository.GetAll().Where(a => a.Id == input.Id);

            if (dgprojdet.Count() == 0)
            {
                var chk = _discProjDetRepository.GetAll().
                    Where(a => a.ProjectId == input.ProjectId && a.FocusAreaId == input.FocusAreaId && a.ProjectTypeId == input.ProjectTypeId && a.SubCategoryId == input.SubCategoryId
                    && a.InterventionId == input.InterventionId && a.Municipality == input.Municipality);
                if (chk.Count() == 0)
                {
                    input.DateCreated = DateTime.Now;
                    var projdet = ObjectMapper.Map<DiscretionaryProjectDetailsApproval>(input);

                    await _discProjDetRepository.InsertAsync(projdet);
                }
                else
                {
                    throw new InvalidOperationException("Dupicate entry detected.");
                }

            }
            else
            {
                var dgdet = await _discProjDetRepository.FirstOrDefaultAsync(dgprojdet.First().Id);
                dgdet.DteUpd = DateTime.Now;
                dgdet.UsrUpd = input.UserId;
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
                dgdet.vision2025goal = input.vision2025goal;
                dgdet.SqmrAppIndicator = input.SqmrAppIndicator;

                await _discProjDetRepository.UpdateAsync(dgdet);
            }
        }

        public async Task<PagedResultDto<DiscretionaryProjectDetailsApprovalPagedDto>> GetProjectDetails(int ProjectId)
        {
            var det = _discProjDetRepository.GetAll().Where(a => a.ProjectId == ProjectId);


            var projdet = await (from o in det
                                 select new DiscretionaryProjectDetailsApprovalPagedDto
                                 {
                                     projectDetailsApproval = new DiscretionaryProjectDetailsApprovalDto
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
                                         vision2025goal = o.vision2025goal,
                                         SqmrAppIndicator = o.SqmrAppIndicator,
                                         Id = o.Id
                                     }
                                 }).ToListAsync();

            var totalCount = det.Count();

            return new PagedResultDto<DiscretionaryProjectDetailsApprovalPagedDto>(
                totalCount,
                projdet.ToList()
            );
        }

        public async Task<PagedResultDto<PagedDiscretionaryProjectDetailsApprovalView>> GetDGProjectDetails(int ProjectId)
        {
            var discprojs = await (from projdet in _discProjDetRepository.GetAll()
                                   join proj in _discProjRepository.GetAll() on projdet.ProjectId equals proj.Id
                                   join stat in _discStatusRepository.GetAll() on proj.ProjectStatusID equals stat.Id
                                   join projtype in _projTypeRepository.GetAll() on projdet.ProjectTypeId equals projtype.Id
                                   join focarea in _focusAreaRepository.GetAll() on projdet.FocusAreaId equals focarea.Id
                                   join subcat in _adminCritRepository.GetAll() on projdet.SubCategoryId equals subcat.Id
                                   join interv in _evalMethodRepository.GetAll() on projdet.InterventionId equals interv.Id into emp
                                   from intervs in emp.DefaultIfEmpty()
                                   join appr in _discDetailApprovalRepository.GetAll() on projdet.Id equals appr.ApplicationId into pr
                                   from apprs in pr.DefaultIfEmpty()

                                   select new
                                   {
                                       Application = projdet,
                                       ProjType = projtype.ProjTypDesc,
                                       FocusArea = focarea.FocusAreaDesc,
                                       SubCategory = subcat.AdminDesc,
                                       Intervention = intervs.EvalMthdDesc,
                                       Status = stat.StatusDesc,
                                       ApprovalStatus = apprs
                                   })
                    .Where(a => a.Application.ProjectId == ProjectId)
                    .ToListAsync();


            var discproject = from o in discprojs
                              select new PagedDiscretionaryProjectDetailsApprovalView()
                              {
                                  ProjectDetailsApproval = new DiscretionaryProjectDetailsApprovalsView
                                  {
                                      ProjectId = o.Application.ProjectId,
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
                                      ApprovalStatus = o.ApprovalStatus,
                                      vision2025goal = o.Application.vision2025goal,
                                      SqmrAppIndicator = o.Application.SqmrAppIndicator,
                                      Id = o.Application.Id
                                  }
                              };

            var totalCount = discproject.Count();

            return new PagedResultDto<PagedDiscretionaryProjectDetailsApprovalView>(
                totalCount,
                discproject.ToList()
            );
        }

        public async Task<PagedResultDto<PagedDiscretionaryProjectDetailsApprovalView>> GetDGProjectDetailsId(int ApplicationId)
        {
            var discprojs = await (from projdet in _discProjDetRepository.GetAll()
                                   join proj in _discProjRepository.GetAll() on projdet.ProjectId equals proj.Id
                                   join org in _orgRepository.GetAll() on proj.OrganisationId equals org.Id
                                   join stat in _discStatusRepository.GetAll() on proj.ProjectStatusID equals stat.Id
                                   join projtype in _projTypeRepository.GetAll() on projdet.ProjectTypeId equals projtype.Id
                                   join focarea in _focusAreaRepository.GetAll() on projdet.FocusAreaId equals focarea.Id
                                   join subcat in _adminCritRepository.GetAll() on projdet.SubCategoryId equals subcat.Id
                                   join interv in _evalMethodRepository.GetAll() on projdet.InterventionId equals interv.Id into emp
                                   from intervs in emp.DefaultIfEmpty()
                                   join appr in _discDetailApprovalRepository.GetAll() on projdet.Id equals appr.ApplicationId into pr
                                   from apprs in pr.DefaultIfEmpty()
                                   join gec in _discGECApprovalRepository.GetAll() on projdet.Id equals gec.ApplicationId into ge
                                   from gecs in ge.DefaultIfEmpty()
                                   join gac in _discGACApprovalRepository.GetAll() on projdet.Id equals gac.ApplicationId into ga
                                   from gacs in ga.DefaultIfEmpty()
                                   join gc in _discGCApprovalRepository.GetAll() on projdet.Id equals gc.ApplicationId into g
                                   from gcs in g.DefaultIfEmpty()

                                   select new
                                   {
                                       Application = projdet,
                                       ProjType = projtype.ProjTypDesc,
                                       FocusArea = focarea.FocusAreaDesc,
                                       SubCategory = subcat.AdminDesc,
                                       Intervention = intervs.EvalMthdDesc,
                                       Status = stat.StatusDesc,
                                       ApprovalStatus = apprs,
                                       GECStatus = gecs,
                                       GACStatus = gacs,
                                       GCStatus = gcs,
                                       SDL_No = org.SDL_No,
                                       Organisation_Name = org.Organisation_Name,
                                       Organisation_Trade_Name = org.Organisation_Trading_Name,
                                       OrganisationId = proj.OrganisationId,
                                       ProjectId = proj.Id
                                   })
                    .Where(a => a.Application.Id == ApplicationId)
                    .ToListAsync();


            var discproject = from o in discprojs
                              select new PagedDiscretionaryProjectDetailsApprovalView()
                              {
                                  ProjectDetailsApproval = new DiscretionaryProjectDetailsApprovalsView
                                  {
                                      ProjectType = o.ProjType,
                                      FocusArea = o.FocusArea,
                                      SubCategory = o.SubCategory,
                                      Intervention = o.Intervention,
                                      Number_Continuing = o.Application.Number_Continuing,
                                      Number_New = o.Application.Number_New,
                                      CostPerLearner = o.Application.CostPerLearner,
                                      SDL = o.SDL_No,
                                      Organisation_Name = o.Organisation_Name,
                                      Organisation_Trade_Name = o.Organisation_Trade_Name,
                                      GEC_Continuing = o.Application.GEC_Continuing,
                                      GEC_New = o.Application.GEC_New,
                                      GEC_CostPerLearner = o.Application.GEC_CostPerLearner,
                                      GAC_Continuing = o.Application.GAC_Continuing,
                                      GAC_New = o.Application.GAC_New,
                                      GAC_CostPerLearner = o.Application.GAC_CostPerLearner,
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
                                      ApprovalStatus = o.ApprovalStatus,
                                      GECStatus = o.GECStatus,
                                      GACStatus = o.GACStatus,
                                      GCStatus = o.GCStatus,
                                      vision2025goal = o.Application.vision2025goal,
                                      SqmrAppIndicator = o.Application.SqmrAppIndicator,
                                      OrganisationId = o.OrganisationId,
                                      Id = o.Application.Id
                                  }
                              };

            var totalCount = discproject.Count();

            return new PagedResultDto<PagedDiscretionaryProjectDetailsApprovalView>(
                totalCount,
                discproject.ToList()
            );
        }

        public async Task<ApplicationTrancheDto> GetDGApplicationTranchesId(int ApplicationId)
        {
            var tr = _appTranche.GetAll().Where(a => a.ApplicationId == ApplicationId);


            return ObjectMapper.Map<ApplicationTrancheDto>(tr);
        }

        public async Task SubmitApplication(int id, int userid)
        {
            var dgproj = _discProjRepository.GetAll().Where(a => a.Id == id);

            if (dgproj.Count() != 0)
            {
                var proj = await _discProjRepository.FirstOrDefaultAsync(dgproj.First().Id);
                proj.UsrUpd = userid;
                proj.DteUpd = DateTime.Now;
                proj.SubmittedBy = userid;
                proj.SubmissionDte = DateTime.Now;
                proj.ProjectStatDte = DateTime.Now;
                proj.ProjectStatusID = 10;

                await _discProjRepository.UpdateAsync(proj);

            }
        }

        public async Task<GrantDeliverableScheduleDto> GrantProgramDeliverables(int applicationId, int FocusAreaId, int SubCategoryId)
        {
            var scheds = (from del in _grantDeliverableSchedule.GetAll()
                          join pdel in _grantProgramDeliverables.GetAll() on del.Id equals pdel.TrancheScheduleId
                          select new
                          {
                              Schedule = del,
                              ProgDel = pdel,
                              Id = pdel.Id,
                          })
                    .Where(a => a.ProgDel.FocusAreaId == FocusAreaId && (a.ProgDel.SubcategoryId == SubCategoryId || a.ProgDel.SubcategoryId == null));

            return ObjectMapper.Map<GrantDeliverableScheduleDto>(scheds);

        }

        public async Task<string> SubmitMOAApproval(int id, int userid)
        {
            var dgproj = _discProjDetRepository.GetAll().Where(a => a.Id == id);
            var output = "";
            if (dgproj.Count() != 0)
            {
                var docs = _docRepository.GetAll().Where(a => a.entityid == id);
                var moa = false;
                var tranche = false;
                if (docs.Count() != 0)
                {
                    foreach (var d in docs)
                    {
                        if (d.entityid == id && d.documenttype == "MOA") { moa = true; }
                        if (d.entityid == id && d.documenttype == "Tranche Deliverable Plan") { tranche = true; }
                    }

                    if (!moa) { output = output + ", MOA"; }
                    if (!tranche) { output = output + ", Tranche Deliverable Plan"; }
                    if (output == "")
                    {
                        var proj = await _discProjDetRepository.FirstOrDefaultAsync(dgproj.First().Id);
                        proj.UsrUpd = userid;
                        proj.DteUpd = DateTime.Now;
                        proj.SubmittedBy = userid;
                        proj.SubmissionDte = DateTime.Now;
                        proj.ApplicationStatusId = 1;

                        await _discProjDetRepository.UpdateAsync(proj);
                    }
                }
                else
                {
                    output = "MOA and Tranche Deliverable Plan.";
                }
            }

            return output;
        }

        public async Task<string> AcceptMOAPlan(int id, int userid, string reason)
        {
            var dgproj = _discProjDetRepository.GetAll().Where(a => a.Id == id);
            var output = "";
            if (dgproj.Count() != 0)
            {
                var docs = _docRepository.GetAll().Where(a => a.entityid == id);
                var moa = false;
                var tranche = false;
                if (docs.Count() != 0)
                {
                    foreach (var d in docs)
                    {
                        if (d.entityid == id && d.documenttype == "MOA") { moa = true; }
                        if (d.entityid == id && d.documenttype == "Tranche Deliverable Plan") { tranche = true; }
                    }

                    if (!moa) { output = output + ", MOA"; }
                    if (!tranche) { output = output + ", Tranche Deliverable Plan"; }
                    if (output == "")
                    {
                        var proj = await _discProjDetRepository.FirstOrDefaultAsync(dgproj.First().Id);
                        proj.UsrUpd = userid;
                        proj.DteUpd = DateTime.Now;
                        proj.ApplicationStatusId = 2;
                        proj.Reason = reason;
                        proj.ApprovalStatus = "Approved";
                        proj.Current_Approver = "REGIONAL MANAGER";

                        await _discProjDetRepository.UpdateAsync(proj);

                        var tr = _appTranche.GetAll().Where(a => a.ApplicationId == id);
                        if (tr.Count() > 0)
                        {
                            var updtr = tr.SingleOrDefault();
                            //updtr.Current_Approver = "REGIONAL MANAGER";
                            updtr.Usrupd = userid;
                            updtr.DteUpd = DateTime.Now;

                            _appTranche.Update(updtr);
                            output = "Exists";

                        }
                        else
                        {
                            var intr = new ApplicationTrancheDto();
                            intr.ApplicationId = id;
                            if (proj.ProjectTypeId == 2) { intr.ProgrammeTypeId = 2; }
                            if (proj.ProjectTypeId == 4) { intr.ProgrammeTypeId = 4; }
                            intr.Description = "Tranche 1a";
                            intr.TrancheType = "1a";
                            intr.FocusAreaId = proj.FocusAreaId;
                            intr.SubCategoryId = proj.SubCategoryId;
                            intr.TrancheStatus = "Processing";
                            intr.New_Learners = proj.GC_New;
                            intr.Continuing = proj.GC_Continuing;
                            intr.CostPerLearner = proj.GC_CostPerLearner;
                            intr.Current_Approver = "RSA";
                            intr.TrancheAmount = (decimal)((proj.GC_Continuing + proj.GC_New) * proj.GC_CostPerLearner * 15 / 100);
                            intr.DateCreated = DateTime.Now;
                            intr.UserId = userid;
                            var t = ObjectMapper.Map<ApplicationTranche>(intr);
                            var request = _appTranche.InsertAsync(t);
                        }


 
                    }
                }
                else
                {
                    output = "MOA and Tranche Deliverable Plan.";
                }
            }

            return output;
        }

        public async Task<string> SaveMOATrancheData(int id, int userid, string reason)
        {
            var dgproj = _discProjDetRepository.GetAll().Where(a => a.Id == id).FirstOrDefault();
            var ido = _applicationTranche.GetAll().Where(a => a.ApplicationId == id).Max(b => b.Id);
            var output = "";
            if (dgproj.Id > 0)
            {
                var tra = new Tranche_Approvals();
                tra.ApplicationId = id;
                tra.TrancheId = 1;
                tra.Approval_Status = "Recommended";
                tra.Comment = reason;
                tra.ApprovalLevel = "RSA";
                tra.UserId = userid;
                tra.DateApproved = DateTime.Now;
                tra.DateCreated = DateTime.Now;

                _trancheApprovals.Insert(tra);

                var trd = _appTrancheDetails.GetAll().Where(a => a.ApplicationTrancheId == ido).ToList();

                if (trd.Count() > 0)
                {
                    var updtr = trd.SingleOrDefault();
                    //updtr.Current_Approver = "REGIONAL MANAGER";
                    updtr.UserId = userid;
                    _appTrancheDetails.Update(updtr);
                }
                else
                {
                    var intr = new ApplicationTrancheDetails();
                    intr.ApplicationTrancheId = ido;
                    intr.Amount = ((dgproj.GC_Continuing + dgproj.GC_New) * dgproj.GC_CostPerLearner);
                    intr.ApplicationTranceStatus = "Processing";
                    intr.Current_Approver = "RSA";
                    intr.UserId = userid;
                    intr.DateCreated = DateTime.Now;
                    intr.UserId = userid;
                    _appTrancheDetails.Insert(intr);
                }
            }

            return ido.ToString();
        }

        public async Task<string> CreateGrantApprovalBatch(int UserId)
        {
            var result = "";
            var newr = new CreateRequestDto();
            var req = await (from r in _requestRepository.GetAll()
                             join rd in _requestDataRepository.GetAll().Where(a => a.Name == "ApplicationId") on r.Id equals rd.RequestId
                             join pd in _discProjDetRepository.GetAll() on rd.Value equals pd.Id.ToString()
                             join sp in _specRepository.GetAll() on pd.ProjectTypeId equals sp.ProjectTypeId
                             join pt in _projTypeRepository.GetAll() on pd.ProjectTypeId equals pt.Id
                             join p in _discProjRepository.GetAll() on pd.ProjectId equals p.Id
                             join w in _windowRepository.GetAll() on p.GrantWindowId equals w.Id
                             select new
                             {
                                 r = r,
                                 UsrId = sp.UserId,
                                 CurrentStateId = r.CurrentStateId,
                                 ProjectType = pt.ProjTypDesc,
                                 ProjCd = w.ProgCd
                             })
                    .Where(a => a.UsrId == UserId && (a.CurrentStateId == 46 || a.CurrentStateId == 79))
                    .Distinct()
                    .ToListAsync();

            var reqb = await (from r in _requestRepository.GetAll()
                              join rqd in _requestDataRepository.GetAll().Where(a => a.Name == "BatchId") on r.Id equals rqd.RequestId
                              join pd in _discProjDetRepository.GetAll() on rqd.Value equals pd.Id.ToString()
                              join sp in _specRepository.GetAll() on pd.ProjectTypeId equals sp.ProjectTypeId
                              join pt in _projTypeRepository.GetAll() on pd.ProjectTypeId equals pt.Id
                              join p in _discProjRepository.GetAll() on pd.ProjectId equals p.Id
                              join w in _windowRepository.GetAll() on p.GrantWindowId equals w.Id
                              select new
                              {
                                  r = r,
                                  UsrId = sp.UserId,
                                  CurrentStateId = r.CurrentStateId,
                                  ProjectType = pt.ProjTypDesc,
                                  ProjCd = w.ProgCd
                              })
            .Where(a => a.UsrId == UserId && (a.CurrentStateId == 46 || a.CurrentStateId == 79))
            .Distinct()
            .ToListAsync();

            req.AddRange(reqb);

            if (req.Count() == 0)
            {
                result = "You have not approved any requests to create a Batch.";
            }
            else
            {
                var newbatch = new DiscretionaryTrancheBatch();
                string BatchNo = "";
                newbatch.BatchNumber = BatchNo;
                newbatch.Status = "Approval in progress";
                newbatch.DateCreated = DateTime.Now;
                newbatch.UserId = UserId;
                newbatch.Description = "Discretionary Grant Batch - " + BatchNo;

                _trBatchRepository.Insert(newbatch);
            }

            return result;
        }

        public async Task<CreateRequestDto> CreateGrantApprovalBatchData(int UserId)
        {
            var result = "";

            var req = await (from r in _requestRepository.GetAll()
                        join rqd in _requestDataRepository.GetAll().Where(a => a.Name == "ApplicationId") on r.Id equals rqd.RequestId
                        join pd in _discProjDetRepository.GetAll() on rqd.Value equals pd.Id.ToString()
                        join sp in _specRepository.GetAll() on pd.ProjectTypeId equals sp.ProjectTypeId
                        join pt in _projTypeRepository.GetAll() on pd.ProjectTypeId equals pt.Id
                        join p in _discProjRepository.GetAll() on pd.ProjectId equals p.Id
                        join w in _windowRepository.GetAll() on p.GrantWindowId equals w.Id
                        select new
                        {
                            r = r,
                            UsrId = sp.UserId,
                            CurrentStateId = r.CurrentStateId,
                            ProjectType = pt.ProjTypDesc,
                            ProjCd = w.ProgCd
                        })
            .Where(a => a.UsrId == UserId && (a.CurrentStateId == 46 || a.CurrentStateId == 79))
            .Distinct()
            .ToListAsync();

            var reqb = await (from r in _requestRepository.GetAll()
                             join rqd in _requestDataRepository.GetAll().Where(a => a.Name == "BatchId") on r.Id equals rqd.RequestId
                             join pd in _discProjDetRepository.GetAll() on rqd.Value equals pd.Id.ToString()
                             join sp in _specRepository.GetAll() on pd.ProjectTypeId equals sp.ProjectTypeId
                             join pt in _projTypeRepository.GetAll() on pd.ProjectTypeId equals pt.Id
                             join p in _discProjRepository.GetAll() on pd.ProjectId equals p.Id
                             join w in _windowRepository.GetAll() on p.GrantWindowId equals w.Id
                             select new
                             {
                                 r = r,
                                 UsrId = sp.UserId,
                                 CurrentStateId = r.CurrentStateId,
                                 ProjectType = pt.ProjTypDesc,
                                 ProjCd = w.ProgCd
                             })
            .Where(a => a.UsrId == UserId && (a.CurrentStateId == 46 || a.CurrentStateId == 79))
            .Distinct()
            .ToListAsync();

            req.AddRange(reqb);

            var tbid = _trBatchRepository.GetAll().Where(a=>a.UserId == UserId).Max(b=>b.Id);
            var BatchNo = "";
            if (req.First().ProjectType == "Strategic Projects")
            {
                BatchNo = "SP" + (Int32.Parse(req.First().ProjCd.Right(4)) + 1).ToString() + "M" + DateTime.Now.Month.ToString("00") + "B" + tbid.ToString();
            }
            if (req.First().ProjectType == "Learning Projects")
            {
                BatchNo = "LP" + (Int32.Parse(req.First().ProjCd.Right(4)) + 1).ToString() + "M" + DateTime.Now.Month.ToString("00") + "B" + tbid.ToString();
            }
            if (req.First().ProjectType == "Research Projects")
            {
                BatchNo = "RP" + (Int32.Parse(req.First().ProjCd.Right(4)) + 1).ToString() + "M" + DateTime.Now.Month.ToString("00") + "B" + tbid.ToString();
            }

            var tbupd = _trBatchRepository.Get(tbid);
            tbupd.BatchNumber = BatchNo;
            _trBatchRepository.Update(tbupd);

            foreach (var r in req)
            {
                var tb = new DiscrationaryTrancheBatchRequests();
                tb.RequestId = r.r.Id;
                tb.TrancheBatchId = tbid;
                tb.DateCreated = DateTime.Now;
                tb.UserId = UserId;

                _trBatchRequestsRepository.Insert(tb);

                var rupd = _requestRepository.Get(r.r.Id);
                rupd.CurrentStateId = 99;
                _requestRepository.Update(rupd);

                var reqd = _requestDataRepository.GetAll().Where(a => a.RequestId == r.r.Id && a.Name == "ApplicationId").FirstOrDefault();

                var at = _applicationTranche.GetAll().Where(a => a.ApplicationId.ToString() == reqd.Value && a.TrancheStatus == "Processing").ToList();

                if (at.Count() > 0)
                {
                    foreach (var atr in at)
                    {
                        var atupd = _applicationTranche.Get(atr.Id);
                        atupd.TrancheStatus = "Batched";
                        atupd.BatchId = tbid;
                        atupd.Usrupd = UserId;
                        atupd.DteUpd = DateTime.Now;
                        _applicationTranche.Update(atupd);
                    }
                }
            }

            var newr = new CreateRequestDto();
            newr.ProcessId = 2;
            newr.CurrentStateId = 48;
            newr.Title = "Discretionary Grant Approval Batch - " + tbid.ToString();
            newr.RequestPath = "tranchebatchapproval";
            newr.UserId = UserId;
            newr.Username = "";
            newr.DateRequested = DateTime.Now;
            newr.DateCreated = DateTime.Now;

            //_requestRepository.Insert(newr);

            // reqid = _requestRepository.GetAll().Max(x=>x.Id) + 1;
            var rqdata = new List<wfRequestData>();
            var rd = new wfRequestData();
            rd.Name = "BatchId";
            rd.Value = tbid.ToString();
            rd.UserId = UserId;
            //rd.RequestId = reqid;
            rd.DateCreated = DateTime.Now;
            rqdata.Add(rd);

            newr.RequestData = rqdata;

            // CreateNewRequest(newr);

            return newr;
        }

        public async Task<int> CreateNewRequestbk(CreateRequestDto input)
        {
            var req = new wfRequestDto();

            req.Title = input.Title;
            req.RequestPath = input.RequestPath;
            req.ProcessId = input.ProcessId;
            req.CurrentStateId = input.CurrentStateId;
            req.UserId = input.UserId;
            req.Username = input.Username;
            req.DateCreated = DateTime.Now;
            req.DateRequested = DateTime.Now;

            var rq = ObjectMapper.Map<wfRequest>(req);
            var request = _requestRepository.InsertAsync(rq);
            var reqid = _requestRepository.GetAll().Max(x => x.Id) + 1;

            var trns = _transitionRepository.GetAll().Where(x => x.CurrentStateId == input.CurrentStateId);
            foreach (var trn in trns.ToList())
            {
                var transact = _transitionActionRepository.GetAll().Where(x => x.TransitionId == trn.Id);
                foreach (var tract in transact.ToList())
                {
                    var reqact = new wfRequestActionDto();
                    reqact.DateCreated = DateTime.Now;
                    reqact.RequestId = reqid; // request.Id;
                    reqact.ActionId = tract.ActionId;
                    reqact.DateActioned = DateTime.Now;
                    reqact.IsActive = true;
                    reqact.IsComplete = false;
                    reqact.UserActioned = input.UserId;
                    reqact.UserId = input.UserId;
                    reqact.TransitionId = trn.Id;

                    var ra = ObjectMapper.Map<wfRequestAction>(reqact);
                    await _requestActionRepository.InsertAsync(ra);
                }

                var timer = new wfTimerDto();
                timer.ProcessId = input.ProcessId;
                timer.StartDate = DateTime.Now;
                timer.TransitionId = trn.Id;
                timer.DurationType = "Days";
                timer.Duration = 14;
                timer.RequestId = reqid; // request.Id;
                timer.DateCreated = DateTime.Now;
                timer.UserId = input.UserId;

                var tim = ObjectMapper.Map<wfTimer>(timer);
                await _timerRepository.InsertAsync(tim);
            }

            foreach (var reqdata in input.RequestData)
            {
                var chkrd = _requestDataRepository.GetAll().Where(a=>a.Name == reqdata.Name && a.Value == reqdata.Value).Count();
                if (chkrd == 0)
                {
                    var rd = new wfRequestDataDto();
                    rd.Name = reqdata.Name;
                    rd.Value = reqdata.Value;
                    rd.UserId = input.UserId;
                    rd.RequestId = reqid; // request.Id;
                    rd.DateCreated = DateTime.Now;

                    var rqdata = ObjectMapper.Map<wfRequestData>(rd);
                    await _requestDataRepository.InsertAsync(rqdata);
                }
            }
            return reqid;// request.Id;
        }

        public async Task<PagedResultDto<ApplicationTranchePagedView>> GetApplicationTranches(int ApplicationId)
        {
            var tranches = (from tranch in _applicationTranche.GetAll()
                            join app in _discProjDetRepository.GetAll() on tranch.ApplicationId equals app.Id
                            //join typ in _trancheType.GetAll() on tranch.TrancheTypeId equals typ.Id
                            join fa in _focusAreaRepository.GetAll() on app.FocusAreaId equals fa.Id into fat
                            from fas in fat.DefaultIfEmpty()
                            join ac in _adminCritRepository.GetAll() on app.SubCategoryId equals ac.Id into sb
                            from acs in sb.DefaultIfEmpty()
                            select new
                            {
                                Tranches = tranch,
                                Type = tranch.TrancheType,
                                FocusArea = fas.FocusAreaDesc,
                                AdminCriteria = acs.AdminDesc
                            })
                    .Where(a => a.Tranches.ApplicationId == ApplicationId)
                    .ToList();

            var trancheList = from o in tranches
                              select new ApplicationTranchePagedView()
                              {
                                  ApplicationTranche = new ApplicationTrancheView()
                                  {
                                      ApplicationId = o.Tranches.ApplicationId,
                                      TrancheType = o.Type,
                                      Description = o.Tranches.Description,
                                      TrancheStatus = o.Tranches.TrancheStatus,
                                      FocusArea = o.FocusArea,
                                      SubCategory = o.AdminCriteria,
                                      TrancheAmount = o.Tranches.TrancheAmount,
                                      GC_New_Learners = o.Tranches.New_Learners,
                                      GC_Continuing = o.Tranches.Continuing,
                                      GC_CostPerLearner = o.Tranches.CostPerLearner,
                                      DateCreated = o.Tranches.DateCreated,
                                      Id = o.Tranches.Id
                                  }
                              };

            var totalCount = trancheList.Count();

            return new PagedResultDto<ApplicationTranchePagedView>(
                totalCount,
                trancheList.ToList()
            );
        }

        public async Task<PagedResultDto<BatchApprovalRequestsView>> GetBatchApprovalRequests(int BatchId)
        {
            //var det = await (from tb in _trBatchRepository.GetAll()
            //                 join tbr in _trBatchRequestsRepository.GetAll() on tb.Id equals tbr.TrancheBatchId
            //                 join r in _requestRepository.GetAll() on tbr.RequestId equals r.Id
            //                 join rd in _requestDataRepository.GetAll().Where(a => a.Name == "ApplicationId") on r.Id equals rd.RequestId
            //                 join tr in _applicationTranche.GetAll() on rd.Value equals tr.ApplicationId.ToString()
            //                 join dpa in _discProjDetRepository.GetAll() on apt.ApplicationId equals dpa.Id
            var det = await (from tr in _appTranche.GetAll()
                             join rd in _requestDataRepository.GetAll().Where(a => a.Name == "ApplicationId") on tr.ApplicationId.ToString() equals rd.Value
                             join r in _requestRepository.GetAll() on rd.RequestId equals r.Id
                             join pd in _discProjDetRepository.GetAll() on tr.ApplicationId equals pd.Id
                             join tb in _trBatchRepository.GetAll() on tr.BatchId equals tb.Id
                             join tbr in _trBatchRequestsRepository.GetAll() on tb.Id equals tbr.TrancheBatchId

                             select new
                             {
                                 Id = tb.Id,
                                 ApplicationId = pd.Id,
                                 tAppId = tr.ApplicationId,
                                 RequestId = r.Id,
                                 TranchId = tr.Id,
                                 BatchId = tb.Id,
                                 Title = r.Title,
                                 Description = tb.Description,
                                 Learners = pd.GC_Continuing + pd.GC_New,
                                 Cost = tr.TrancheAmount,
                                 DateRequested = r.DateRequested,
                                 Province = pd.Province,
                                 TBRRequestId = tbr.RequestId,
                                 TRDescription = tr.Description,
                                 RPath = r.RequestPath

                             })
                    .Where(a => a.BatchId == BatchId && a.TBRRequestId == a.RequestId && 
                            ((a.TRDescription == "Tranche 1a" && a.RPath == "tranche1aapproval") ||
                            (a.TRDescription == "Tranche 1b" && a.RPath == "tranche1bapproval")) )
                    .Distinct()
                    .ToListAsync();


            var cus = from o in det
                      select new BatchApprovalRequestsView()
                      {
                          BatchApprovalRequests = new BatchApprovalRequestsDto
                          {
                              ApplicationId = o.ApplicationId,
                              Title = o.Title,
                              Description = o.Description,
                              Learners = (int)o.Learners,
                              Cost = (decimal)o.Cost,
                              DateRequested = o.DateRequested,
                              RequestId = o.RequestId,
                              TrancheId = o.TranchId,
                              Province = o.Province,
                              Id = o.Id
                          }
                      };

            var totalCount = det.Count();

            return new PagedResultDto<BatchApprovalRequestsView>(
                totalCount,
                cus.ToList()
            );
        }

        public async Task<ApplicationTrancheView> GetApplicationTrancheId(int TrancheId)
        {
            var tranches = (from tranch in _applicationTranche.GetAll()
                            join app in _discProjDetRepository.GetAll() on tranch.ApplicationId equals app.Id
                            //join typ in _trancheType.GetAll() on tranch.TrancheTypeId equals typ.Id
                            join fa in _focusAreaRepository.GetAll() on app.FocusAreaId equals fa.Id into fat
                            from fas in fat.DefaultIfEmpty()
                            join ac in _adminCritRepository.GetAll() on app.SubCategoryId equals ac.Id into sb
                            from acs in sb.DefaultIfEmpty()
                            select new
                            {
                                Tranches = tranch,
                                Contract_Number = app.Contract_Number,
                                Type = tranch.TrancheType,
                                FocusArea = fas.FocusAreaDesc,
                                AdminCriteria = acs.AdminDesc,
                                ProjectId = app.ProjectId
                            })
                .Where(a => a.Tranches.Id == TrancheId)
                .FirstOrDefault();

            var tranche = new ApplicationTrancheView()
            {
                ApplicationId = tranches.Tranches.ApplicationId,
                Contract_Number = tranches.Contract_Number,
                ProjectId = tranches.ProjectId,
                TrancheType = tranches.Type,
                Description = tranches.Tranches.Description,
                TrancheStatus = tranches.Tranches.TrancheStatus,
                FocusArea = tranches.FocusArea,
                SubCategory = tranches.AdminCriteria,
                TrancheAmount = tranches.Tranches.TrancheAmount,
                GC_New_Learners = tranches.Tranches.New_Learners,
                GC_Continuing = tranches.Tranches.Continuing,
                GC_CostPerLearner = tranches.Tranches.CostPerLearner,
                DateCreated = tranches.Tranches.DateCreated,
                Id = tranches.Tranches.Id
            };

            return tranche;
        }

        public async Task<ApplicationTrancheView> GetTrancheRequestId(int TrancheId)
        {
            var tranches = (from tranch in _applicationTranche.GetAll()
                            join reqdat in _requestDataRepository.GetAll().Where(a => a.Name == "TrancheId") on tranch.Id.ToString() equals reqdat.Value
                            join app in _discProjDetRepository.GetAll() on tranch.ApplicationId equals app.Id
                            //join typ in _trancheType.GetAll() on tranch.TrancheTypeId equals typ.Id
                            join fa in _focusAreaRepository.GetAll() on app.FocusAreaId equals fa.Id into fat
                            from fas in fat.DefaultIfEmpty()
                            join ac in _adminCritRepository.GetAll() on app.SubCategoryId equals ac.Id into sb
                            from acs in sb.DefaultIfEmpty()
                            select new
                            {
                                TrancheId = tranch.Id,
                                RequestId = reqdat.RequestId,
                                Tranches = tranch,
                                Type = tranch.TrancheType,
                                FocusArea = fas.FocusAreaDesc,
                                AdminCriteria = acs.AdminDesc
                            })
                .Where(a => a.TrancheId == TrancheId)
                .FirstOrDefault();

            var tranche = new ApplicationTrancheView()
            {
                ApplicationId = tranches.Tranches.ApplicationId,
                TrancheType = tranches.Type,
                Description = tranches.Tranches.Description,
                TrancheStatus = tranches.Tranches.TrancheStatus,
                FocusArea = tranches.FocusArea,
                SubCategory = tranches.AdminCriteria,
                TrancheAmount = tranches.Tranches.TrancheAmount,
                GC_New_Learners = tranches.Tranches.New_Learners,
                GC_Continuing = tranches.Tranches.Continuing,
                GC_CostPerLearner = tranches.Tranches.CostPerLearner,
                DateCreated = tranches.Tranches.DateCreated,
                Id = tranches.Tranches.Id
            };

            return tranche;
        }

        public async Task<wfRequestDto> GetRequestByBatchId(int id)
        {
            var reqdat = _requestDataRepository.GetAll().Where(a => a.Name == "BatchId" && a.Value == id.ToString()).FirstOrDefault();
            if (reqdat == null)
            {
                return null;
            }
            var req = _requestRepository.Get(reqdat.RequestId);
            return ObjectMapper.Map<wfRequestDto>(req);
        }

        public async Task<BatchPaymentDetailsDto> GetBatchPaymentDetails(int BatchId)
        {

            var tranche = (from tr in _appTranche.GetAll()
                           join rd in _requestDataRepository.GetAll().Where(a => a.Name == "ApplicationId") on tr.ApplicationId.ToString() equals rd.Value
                           join r in _requestRepository.GetAll() on rd.RequestId equals r.Id
                           join pd in _discProjDetRepository.GetAll() on tr.ApplicationId equals pd.Id
                           join tb in _trBatchRepository.GetAll() on tr.BatchId equals tb.Id
                           join tbr in _trBatchRequestsRepository.GetAll() on tb.Id equals tbr.TrancheBatchId

                           select new
                           {
                               Id = tb.Id,
                               BatchNumber = tb.BatchNumber,
                               RequestId = r.Id,
                               TrancheName = tr.Description,
                               //Title = r.Title,
                               Description = tb.Description,
                               Learners = pd.GC_Continuing + pd.GC_New,
                               Cost = tr.TrancheAmount,
                               Province = pd.Province,
                               TBRRequestId = tbr.RequestId,
                               TRDescription = tr.Description,
                               RPath = r.RequestPath
                           })
            .Where(a => a.Id == BatchId && a.TBRRequestId == a.RequestId &&
                            ((a.TRDescription == "Tranche 1a" && a.RPath == "tranche1aapproval") ||
                            (a.TRDescription == "Tranche 1b" && a.RPath == "tranche1bapproval")))
            .Distinct();
            //.ToList();

            var pymt = new BatchPaymentDetailsDto();
            pymt.BatchId = BatchId;
            pymt.BatchNumber = tranche.FirstOrDefault().BatchNumber;
            pymt.BatchTotal = (decimal)tranche.Sum(a => a.Cost);
            pymt.paymentTranches = new List<PaymentTranches>();
            string[] grps = { };

            foreach(var t in tranche)
            {
                var ind = Array.IndexOf(grps, t.TrancheName);
                if (ind == -1){
                    grps = grps.Append(t.TrancheName).ToArray();
                }
            }

            int i;
            for (i = 0; i < grps.Length; i++)
            {
                var pt = new PaymentTranches();
                string tt = grps[i];
                var amt = tranche.Where(x => x.TrancheName == tt).Sum(a => a.Cost);
                pymt.paymentTranches.Add(new PaymentTranches() { TrancheType = tt, Amount = amt });
            }

            return pymt;
        }

        public async Task<PagedResultDto<BatchApprovalRequestsView>> GetBatchPaymentDetails_(int BatchId)
        {
            var det = await (from tb in _trBatchRepository.GetAll()
                             join tbr in _trBatchRequestsRepository.GetAll() on tb.Id equals tbr.TrancheBatchId
                             join r in _requestRepository.GetAll() on tbr.RequestId equals r.Id
                             join rd in _requestDataRepository.GetAll().Where(a=>a.Name == "ApplicationId") on r.Id equals rd.RequestId
                             join pd in _discProjDetRepository.GetAll() on rd.Value equals pd.Id.ToString()

                             select new
                             {
                                 Id = tb.Id,
                                 RequestId = r.Id,
                                 Title = r.Title,
                                 Description = tb.Description,
                                 DataName = rd.Name,
                                 Learners = pd.GC_Continuing + pd.GC_New,
                                 Cost = (pd.GC_Continuing + pd.GC_New) * pd.GC_CostPerLearner,
                                 DateRequested = r.DateRequested,
                                 Province = pd.Province
                             })
                    .Where(a => a.Id == BatchId)
                    .ToListAsync();


            var cus = from o in det
                      select new BatchApprovalRequestsView()
                      {
                          BatchApprovalRequests = new BatchApprovalRequestsDto
                          {
                              Title = o.Title,
                              Description = o.Description,
                              Learners = (int)o.Learners,
                              Cost = (decimal)o.Cost,
                              DateRequested = o.DateRequested,
                              RequestId = o.RequestId,
                              Province = o.Province,
                              Id = o.Id
                          }
                      };

            var totalCount = det.Count();

            return new PagedResultDto<BatchApprovalRequestsView>(
                totalCount,
                cus.ToList()
            );
        }
        public async Task<string> DeclineMOAPlan(int id, int userid, string reason)
        {
            var dgproj = _discProjDetRepository.GetAll().Where(a => a.Id == id);
            var output = "";
            if (dgproj.Count() != 0)
            {
                var docs = _docRepository.GetAll().Where(a => a.entityid == id);
                var moa = false;
                var tranche = false;
                if (docs.Count() != 0)
                {
                    if (output == "")
                    {
                        var proj = await _discProjDetRepository.FirstOrDefaultAsync(dgproj.First().Id);
                        proj.UsrUpd = userid;
                        proj.DteUpd = DateTime.Now;
                        proj.ApplicationStatusId = 6;
                        proj.Reason = reason;
                        proj.ApprovalStatus = "Declined";

                        await _discProjDetRepository.UpdateAsync(proj);

                        var tra = new Tranche_Approvals();
                        tra.ApplicationId = id;
                        if (proj.ProjectTypeId == 2) { tra.TrancheId = 2; }
                        if (proj.ProjectTypeId == 4) { tra.TrancheId = 4; }
                        tra.Approval_Status = "Declined";
                        tra.Comment = reason;
                        tra.ApprovalLevel = "RSA";
                        tra.UserId = userid;
                        tra.DateApproved = DateTime.Now;
                        tra.DateCreated = DateTime.Now;

                        _trancheApprovals.Insert(tra);

                        var email = _personRepository.GetAll().Where(a => a.Userid == proj.UserId).FirstOrDefault().Email;
                        var contractnum = proj.Contract_Number;
                        await _userEmailer.SendBulkTranche1DocRejectionEmailsAsync(email, reason, contractnum);
                    }
                }
                else
                {
                    output = "MOA and Tranche Deliverable Plan.";
                }
            }

            return output;
        }

        public async Task<string> ApproveGrant1a(int id, int userid, string currentprofile, string reason)
        {
            var dgproj = _discProjDetRepository.GetAll().Where(a => a.Id == id);
            var output = "";
            var TrancheId = 0;
            var nextapprover = "";
            if (dgproj.Count() != 0)
            {
                var proj = await _discProjDetRepository.FirstOrDefaultAsync(dgproj.First().Id);
                proj.UsrUpd = userid;
                proj.DteUpd = DateTime.Now;
                proj.Reason = reason;
                proj.ApprovalStatus = "Approved";

                if (proj.ProjectTypeId == 2) { TrancheId = 2; }
                if (proj.ProjectTypeId == 4) { TrancheId = 4; }

                if (currentprofile == "REGIONAL MANAGER")
                {
                    proj.ApplicationStatusId = 7;
                    nextapprover = "GRANT PRACTITIONER";
                }

                if (currentprofile == "GRANT PRACTITIONER")
                {
                    nextapprover = "GRANT SPECIALIST";
                }

                if (currentprofile == "GRANT SPECIALIST")
                {
                    nextapprover = "GRANT MANAGER";
                }

                if (currentprofile == "GRANT MANAGER")
                {
                    nextapprover = "GRANT EXECUTIVE";
                }

                if (currentprofile == "GRANT EXECUTIVE")
                {
                    nextapprover = "FINANCE ADMINISTRATOR";
                }

                if (currentprofile == "FINANCE ADMINISTRATOR")
                {
                    nextapprover = "FINANCE PRACTITIONER";
                }

                if (currentprofile == "FINANCE PRACTITIONER")
                {
                    nextapprover = "FINANCE MANAGER";
                }

                if (currentprofile == "FINANCE MANAGER")
                {
                    nextapprover = "CFO";
                }

                if (currentprofile == "CFO")
                {
                    nextapprover = "FNB";
                    var app = _discProjDetRepository.Get(id);

                }

                proj.Current_Approver = nextapprover;

                await _discProjDetRepository.UpdateAsync(proj);

                var tr = _appTranche.GetAll().Where(a => a.ApplicationId == id && a.ProgrammeTypeId == TrancheId);
                if (tr.Count() > 0)
                {
                    var updtr = tr.SingleOrDefault();
                    updtr.Current_Approver = currentprofile;
                    updtr.Usrupd = userid;
                    updtr.DteUpd = DateTime.Now;

                    _appTranche.Update(updtr);
                }

                var tra = new Tranche_Approvals();
                tra.ApplicationId = id;
                tra.TrancheId = TrancheId;
                tra.ApprovalLevel = currentprofile;
                tra.Approval_Status = "Approved";
                tra.Comment = reason;
                tra.UserId = userid;
                tra.DateApproved = DateTime.Now;
                tra.DateCreated = DateTime.Now;

                _trancheApprovals.Insert(tra);

                var trd = _appTrancheDetails.GetAll().Where(a => a.ApplicationTrancheId == TrancheId);
                if (trd.Count() > 0)
                {
                    var updtr = trd.SingleOrDefault();
                    updtr.Current_Approver = currentprofile;
                    updtr.UserId = userid;
                    _appTrancheDetails.Update(updtr);
                }
            }

            return output;
        }

        public async Task<string> DeclineGrant1a(int id, int userid, string currentprofile, string reason)
        {
            var dgproj = _discProjDetRepository.GetAll().Where(a => a.Id == id);
            var output = "";
            var TrancheId = 0;
            if (dgproj.Count() != 0)
            {
                var proj = await _discProjDetRepository.FirstOrDefaultAsync(dgproj.First().Id);
                proj.UsrUpd = userid;
                proj.DteUpd = DateTime.Now;
                proj.Reason = reason;
                proj.ApprovalStatus = "Declined";

                if (proj.ProjectTypeId == 2) { TrancheId = 2; }
                if (proj.ProjectTypeId == 4) { TrancheId = 4; }

                if (currentprofile == "RSA")
                {
                    proj.ApplicationStatusId = 6;
                }

                if (currentprofile == "REGIONAL MANAGER")
                {
                    proj.ApplicationStatusId = 1;
                    proj.Current_Approver = "RSA";
                }

                if (currentprofile == "GRANT PRACTITIONER")
                {
                    proj.Current_Approver = "REGIONAL MANAGER";
                }

                if (currentprofile == "GRANT SPECIALIST")
                {
                    proj.Current_Approver = "GRANT PRACTITIONER";
                }

                if (currentprofile == "GRANT MANAGER")
                {
                    proj.Current_Approver = "GRANT SPECIALIST";
                }

                if (currentprofile == "GRANT EXECUTIVE")
                {
                    proj.Current_Approver = "GRANT MANAGER";
                }

                if (currentprofile == "FINANCE ADMINISTRATOR")
                {
                    proj.Current_Approver = "GRANT EXECUTIVE";
                }

                if (currentprofile == "FINANCE PRACTITIONER")
                {
                    proj.Current_Approver = "FINANCE ADMINISTRATOR";
                }

                if (currentprofile == "FINANCE MANAGER")
                {
                    proj.Current_Approver = "FINANCE PRACTITIONER";
                }

                if (currentprofile == "CFO")
                {
                    proj.Current_Approver = "FINANCE MANAGER";
                }

                await _discProjDetRepository.UpdateAsync(proj);

                var tr = _appTranche.GetAll().Where(a => a.ApplicationId == id && a.ProgrammeTypeId == TrancheId);
                if (tr.Count() > 0)
                {
                    var updtr = tr.SingleOrDefault();
                    updtr.Current_Approver = "GRANT PRACTITIONER";
                    updtr.Usrupd = userid;
                    updtr.DteUpd = DateTime.Now;

                    _appTranche.Update(updtr);
                }

                var tra = new Tranche_Approvals();
                tra.ApplicationId = id;
                tra.TrancheId = TrancheId;
                tra.ApprovalLevel = currentprofile;
                tra.Approval_Status = "Rejected";
                tra.Comment = reason;
                tra.UserId = userid;
                tra.DateApproved = DateTime.Now;
                tra.DateCreated = DateTime.Now;

                _trancheApprovals.Insert(tra);

                var trd = _appTrancheDetails.GetAll().Where(a => a.ApplicationTrancheId == TrancheId);
                if (trd.Count() > 0)
                {
                    var updtr = trd.SingleOrDefault();
                    updtr.Current_Approver = currentprofile;
                    updtr.UserId = userid;
                    _appTrancheDetails.Update(updtr);
                }

            }

            return output;
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
                        }
                        else
                        {
                            grandtotal = _discProjUSRepository.GetAll().Where(a => a.ApplicationId == appId).Sum(a => a.Amount) + us.Amount2;
                            amt = us.Amount2;
                        }

                        var inus = new DiscretionaryProjectUSApproval();
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

        public async Task DeleteProjUS(int id, int userid)
        {
            var projus = await _discProjUSRepository.GetAsync(id);
            var appid = projus.ApplicationId;

            _discProjUSRepository.Delete(projus);
        }

        public async Task DeleteTrancheFromBatch(int id, int RequestId)
        {
            var tbr = _trBatchRequestsRepository.GetAll().Where(a => a.RequestId == RequestId && a.TrancheBatchId == id).SingleOrDefault();

            _trBatchRequestsRepository.Delete(tbr);

            var tr = _requestRepository.Get(RequestId);
            tr.CurrentStateId = 44;
            _requestRepository.Update(tr);

            var trns = _transitionRepository.GetAll().Where(x => x.CurrentStateId == 44);
            foreach (var trn in trns.ToList())
            {
                var transact = _transitionActionRepository.GetAll().Where(x => x.TransitionId == trn.Id);
                foreach (var tract in transact.ToList())
                {
                    var reqact = new wfRequestActionDto();
                    reqact.DateCreated = DateTime.Now;
                    reqact.RequestId = RequestId; // request.Id;
                    reqact.ActionId = tract.ActionId;
                    reqact.DateActioned = DateTime.Now;
                    reqact.IsActive = true;
                    reqact.IsComplete = false;
                    reqact.UserActioned = tr.UserId;
                    reqact.UserId = tr.UserId;
                    reqact.TransitionId = trn.Id;

                    var ra = ObjectMapper.Map<wfRequestAction>(reqact);
                    await _requestActionRepository.InsertAsync(ra);
                }

                var timer = new wfTimerDto();
                timer.ProcessId = tr.ProcessId;
                timer.StartDate = DateTime.Now;
                timer.TransitionId = trn.Id;
                timer.DurationType = "Days";
                timer.Duration = 14;
                timer.RequestId = RequestId; // request.Id;
                timer.DateCreated = DateTime.Now;
                timer.UserId = tr.UserId;

                var tim = ObjectMapper.Map<wfTimer>(timer);
                await _timerRepository.InsertAsync(tim);
            }
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

                        var inus = new DiscretionaryProjectUSApproval();
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

        public async Task<PagedResultDto<DisscretionaryProjectUSApprovalForViewDto>> GetAllProjectUS(int projId)
        {
            var uss = await (from uses in _discProjUSRepository.GetAll()
                             join us in _usRepository.GetAll() on uses.USId equals us.Id
                             select new
                             {
                                 Application = uses,
                                 UNIT_STANDARD_ID = us.UNIT_STANDARD_ID,
                                 UNIT_STD_TITLE = us.UNIT_STD_TITLE,
                                 Credits = us.UNIT_STD_NUMBER_OF_CREDITS,
                                 Amount = uses.Amount,
                                 Id = uses.Id,
                             })
                    .Where(a => a.Application.ApplicationId == projId)
                    .ToListAsync();


            var cus = from o in uss
                      select new DisscretionaryProjectUSApprovalForViewDto()
                      {
                          DiscretionaryProjectUSApproval = new DiscretionaryProjectUSApprovalForView
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

            return new PagedResultDto<DisscretionaryProjectUSApprovalForViewDto>(
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
            if (bank == null)
            {
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
                               DetailsId = dpd.Id
                           }).Where(a => a.ProjId == projId);
            if (projdet.Count() == 0) { output = output + ", Application Details"; }

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

            foreach (var pr in projdetprojus)
            {
                var prus = _discProjUSRepository.GetAll().Where(a => a.ApplicationId == pr.DetailsId).Count();
                if (prus == 0) { hasus = false; }
            }

            if (!hasus) { output = output + ", Unit Standard Linking"; }

            var docs = _docRepository.GetAll().Where(a => a.entityid == proj.Id).ToList();

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

            if (output.StartsWith(",")) { output = output.Substring(2, output.Length - 2); }

            return output;
        }

        public async Task<string> validateApproval(int applicationid, int appstatus)
        {
            string output = "";
            var applic = await _discProjDetRepository.FirstOrDefaultAsync(applicationid);
            var projectId = applic.ProjectId;
            var appdocs = _docRepository.GetAll().Where(a => a.entityid == projectId && a.documenttype != "SDF Appointment" && a.module == "Projects").ToList();
            var numdocs = appdocs.Count();
            var docs = _docApprovalRepository.GetAll().Where(a => a.ProjectId == projectId);
            if (docs.Count() < numdocs || docs.Count() < 5)
            {
                output = "Please validate all documents before continuing";
            }

            var errind = false;
            foreach (var d in docs)
            {
                if (new[] { 1, 2, 3, 6, 7, 9 }.Contains(d.DocumentTypeId) && !errind)
                {
                    if (d.ApprovalStatusId == 2 && appstatus == 1)
                    {
                        output = output + ", A mandatory document was declined";
                        errind = true;
                    }
                }
            }

            if (numdocs == 0 && appstatus == 2) { output = ""; }

            return output;
        }

        public async Task<string> FinaliseApproval(DiscretionaryDetailApproval input)
        {
            string output = "";
            var appr = _discDetailApprovalRepository.GetAll().Where(a => a.ApplicationId == input.ApplicationId).FirstOrDefault();
            if (appr == null)
            {
                input.DateCreated = DateTime.Now;
                var app = ObjectMapper.Map<DiscretionaryDetailApproval>(input);

                await _discDetailApprovalRepository.InsertAsync(app);
            }
            else
            {
                output = "Already finalised.";
            }

            return output;
        }

        public async Task<string> FinaliseGECApproval(DiscretionaryGECApproval input)
        {
            string output = "";
            var appr = _discGECApprovalRepository.GetAll().Where(a => a.ApplicationId == input.ApplicationId).FirstOrDefault();
            if (appr == null)
            {
                input.DateCreated = DateTime.Now;
                var app = ObjectMapper.Map<DiscretionaryGECApproval>(input);

                await _discGECApprovalRepository.InsertAsync(app);
            }
            else
            {
                output = "Already finalised.";
            }

            return output;
        }

        public async Task<string> FinaliseGACApproval(DiscretionaryGACApproval input)
        {
            string output = "";
            var appr = _discGACApprovalRepository.GetAll().Where(a => a.ApplicationId == input.ApplicationId).FirstOrDefault();
            if (appr == null)
            {
                input.DateCreated = DateTime.Now;
                var app = ObjectMapper.Map<DiscretionaryGACApproval>(input);

                await _discGACApprovalRepository.InsertAsync(app);
            }
            else
            {
                output = "Already finalised.";
            }

            return output;
        }

        public async Task<string> FinaliseGCApproval(DiscretionaryGCApproval input)
        {
            string output = "";
            var appr = _discGCApprovalRepository.GetAll().Where(a => a.ApplicationId == input.ApplicationId).FirstOrDefault();
            if (appr == null)
            {
                input.DateCreated = DateTime.Now;
                var app = ObjectMapper.Map<DiscretionaryGCApproval>(input);

                await _discGCApprovalRepository.InsertAsync(app);
            }
            else
            {
                output = "Already finalised.";
            }

            return output;
        }

        public async Task ApproveDocument(int doctype, int stat, string comment, int projectId, int userid)
        {
            var appr = _docApprovalRepository.GetAll().Where(a => a.ProjectId == projectId && a.DocumentTypeId == doctype && a.ApprovalTypeId == 1).FirstOrDefault();
            if (appr == null)
            {
                var inappr = new DiscretionaryDocumentApproval();
                inappr.ProjectId = projectId;
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

        public async Task<DiscretionaryDocumentApprovalForView> GetDocumentApproval(int projectId, int doctype)
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
                    .Where(a => a.DocumentApproval.ProjectId == projectId && a.DocumentApproval.DocumentTypeId == doctype)
                    .FirstOrDefault();

            if (approvals != null)
            {
                var approvallist = new DiscretionaryDocumentApprovalForView()
                {
                    ProjectId = approvals.DocumentApproval.ProjectId,
                    ApprovalType = approvals.ApprovalType,
                    ApprovalStatus = approvals.ApprovalStatus,
                    Comments = approvals.DocumentApproval.Comments,
                    Id = approvals.DocumentApproval.Id
                };

                return approvallist;
            }

            return null;
        }

        public async Task ReOpenDocument(int doctype, int projectId, int userid)
        {
            var doc = await _docApprovalRepository.GetAll().Where(a => a.ProjectId == projectId && a.DocumentTypeId == doctype).FirstOrDefaultAsync();
            var proj = await _docApprovalRepository.GetAsync(doc.Id);
            await _docApprovalRepository.DeleteAsync(proj);
        }

        public async Task ReOpenApplication(int applicationId, int userid)
        {
            var det = await _discDetailApprovalRepository.GetAll().Where(a => a.ApplicationId == applicationId).FirstOrDefaultAsync();
            var proj = await _discDetailApprovalRepository.GetAsync(det.Id);
            await _discDetailApprovalRepository.DeleteAsync(proj);
        }

        public async Task ReOpenGECApproval(int applicationId, int userid)
        {
            var det = await _discGECApprovalRepository.GetAll().Where(a => a.ApplicationId == applicationId).FirstOrDefaultAsync();
            var proj = await _discGECApprovalRepository.GetAsync(det.Id);
            await _discGECApprovalRepository.DeleteAsync(proj);
        }

        public async Task ReOpenGACApproval(int applicationId, int userid)
        {
            var det = await _discGACApprovalRepository.GetAll().Where(a => a.ApplicationId == applicationId).FirstOrDefaultAsync();
            var proj = await _discGACApprovalRepository.GetAsync(det.Id);
            await _discGACApprovalRepository.DeleteAsync(proj);
        }

        public async Task ReOpenGCApproval(int applicationId, int userid)
        {
            var det = await _discGCApprovalRepository.GetAll().Where(a => a.ApplicationId == applicationId).FirstOrDefaultAsync();
            var proj = await _discGCApprovalRepository.GetAsync(det.Id);
            await _discGCApprovalRepository.DeleteAsync(proj);
        }

        public async Task<int> GetGCApproval(int applicationId)
        {
            var det = await _discGCApprovalRepository.GetAll().Where(a => a.ApplicationId == applicationId).FirstOrDefaultAsync();
            return det.ApprovalStatusId;
        }

        public async Task AssignRSAEmailSend(Authorization.Users.ConfirmationEmailInput input)
        {
            await _userEmailer.AssignRSAEmailAsync(input.EmailAddress, input.sdlNumber, input.organisationName, input.projectCode, input.projectName);
        }

        public async Task<PagedResultDto<PagedDiscretionaryProjectDetailsApprovalView>> GetCommitteeProjectDetails()
        {
            var discprojs = await (from projdet in _discProjDetRepository.GetAll()
                                   join proj in _discProjRepository.GetAll() on projdet.ProjectId equals proj.Id
                                   join win in _windowRepository.GetAll() on proj.GrantWindowId equals win.Id
                                   join org in _orgRepository.GetAll() on proj.OrganisationId equals org.Id
                                   join stat in _discStatusRepository.GetAll() on proj.ProjectStatusID equals stat.Id
                                   join projtype in _projTypeRepository.GetAll() on projdet.ProjectTypeId equals projtype.Id
                                   join focarea in _focusAreaRepository.GetAll() on projdet.FocusAreaId equals focarea.Id
                                   join subcat in _adminCritRepository.GetAll() on projdet.SubCategoryId equals subcat.Id
                                   join interv in _evalMethodRepository.GetAll() on projdet.InterventionId equals interv.Id
                                   join appr in _discDetailApprovalRepository.GetAll() on projdet.Id equals appr.ApplicationId
                                   join gec in _discGECApprovalRepository.GetAll() on projdet.Id equals gec.ApplicationId into ge
                                   from gecs in ge.DefaultIfEmpty()
                                   join gac in _discGACApprovalRepository.GetAll() on projdet.Id equals gac.ApplicationId into ga
                                   from gacs in ga.DefaultIfEmpty()
                                   join gc in _discGCApprovalRepository.GetAll() on projdet.Id equals gc.ApplicationId into g
                                   from gcs in g.DefaultIfEmpty()
                                   select new
                                   {
                                       Application = projdet,
                                       SdlNo = org.SDL_No,
                                       OrgName = org.Organisation_Name,
                                       ProjectId = proj.Id,
                                       ProjType = projtype.ProjTypDesc,
                                       Project = win.Title,
                                       FocusArea = focarea.FocusAreaDesc,
                                       SubCategory = subcat.AdminDesc,
                                       Intervention = interv.EvalMthdDesc,
                                       Status = stat.StatusDesc,
                                       ApprovalStatus = appr,
                                       GECStatus = gecs,
                                       GACStatus = gacs,
                                       GCStatus = gcs
                                   })
                    .Where(a => a.Status == "GC Committee Review Completed" || a.Status == "GAC Committee Review Completed" || a.Status == "GEC Committee Review Completed" || a.Status == "RSA Review Completed" || a.Status == "Rejected after Full Assessment" || a.Status == "Evaluations Complete")
                    .ToListAsync();


            var discproject = from o in discprojs
                              select new PagedDiscretionaryProjectDetailsApprovalView()
                              {
                                  ProjectDetailsApproval = new DiscretionaryProjectDetailsApprovalsView
                                  {
                                      SDL = o.SdlNo,
                                      Organisation_Name = o.OrgName,
                                      ProjectType = o.ProjType,
                                      Project = o.Project,
                                      ProjectId = o.ProjectId,
                                      FocusArea = o.FocusArea,
                                      SubCategory = o.SubCategory,
                                      Intervention = o.Intervention,
                                      Number_Continuing = o.Application.Number_Continuing,
                                      Number_New = o.Application.Number_New,
                                      CostPerLearner = o.Application.CostPerLearner,
                                      GEC_Continuing = o.Application.GEC_Continuing,
                                      GEC_New = o.Application.GEC_New,
                                      GEC_CostPerLearner = o.Application.GEC_CostPerLearner,
                                      GAC_Continuing = o.Application.GAC_Continuing,
                                      GAC_New = o.Application.GAC_New,
                                      GAC_CostPerLearner = o.Application.GAC_CostPerLearner,
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
                                      ApprovalStatus = o.ApprovalStatus,
                                      GECStatus = o.GECStatus,
                                      GACStatus = o.GACStatus,
                                      GCStatus = o.GCStatus,
                                      vision2025goal = o.Application.vision2025goal,
                                      SqmrAppIndicator = o.Application.SqmrAppIndicator,
                                      Leviesuptodate = o.Application.Leviesuptodate,
                                      PreviousWSP = o.Application.PreviousWSP,
                                      PreviousParticipation = o.Application.PreviousParticipation,
                                      Id = o.Application.Id
                                  }
                              };

            var totalCount = discproject.Count();

            return new PagedResultDto<PagedDiscretionaryProjectDetailsApprovalView>(
                totalCount,
                discproject.ToList()
            );
        }

        public async Task DGSendBulkOutcomeEmails(int WindowId)
        {
            var discprojs = await (from projdet in _discProjDetRepository.GetAll()
                                   join proj in _discProjRepository.GetAll() on projdet.ProjectId equals proj.Id
                                   join orgsdf in _orgSdfRepository.GetAll() on proj.OrganisationId equals orgsdf.OrganisationId
                                   join sdf in _sdfRepository.GetAll() on orgsdf.SdfId equals sdf.Id
                                   join pers in _personRepository.GetAll() on sdf.personId equals pers.Id
                                   join stat in _discStatusRepository.GetAll() on proj.ProjectStatusID equals stat.Id
                                   join projtype in _projTypeRepository.GetAll() on projdet.ProjectTypeId equals projtype.Id
                                   join gc in _discGCApprovalRepository.GetAll() on projdet.Id equals gc.ApplicationId
                                   select new
                                   {
                                       WinId = proj.GrantWindowId,
                                       email = pers.Email,
                                       Status = stat.StatusDesc,
                                       ProjType = projtype.ProjTypDesc
                                   })
                    .Where(a => (a.Status == "GC Committee Review Completed" || a.Status == "GAC Committee Review Completed" || a.Status == "GEC Committee Review Completed" || a.Status == "RSA Review Completed" || a.Status == "Rejected after Full Assessment" || a.Status == "Evaluatons Complete") && (a.WinId == WindowId))
                    .ToListAsync();
            foreach (var d in discprojs.Distinct())
            {
                await _userEmailer.DGSendBulkOutcomeEmailsAsync(d.email);
            }
            await _userEmailer.DGSendBulkOutcomeEmailsAsync("smlotshwa@chieta.org.za");
        }

        public async Task DGSendNotificationEmail()
        {
            var discprojs = await (from sdf in _sdfRepository.GetAll()
                                   join pers in _personRepository.GetAll() on sdf.personId equals pers.Id
                                   select new
                                   {
                                       email = pers.Email,
                                   })
                    .ToListAsync();
            foreach (var d in discprojs.Distinct())
            {
                await _userEmailer.DGSendNotificationEmailsAsync(d.email);
            }
        }

        public async Task OfficeMoveSendEmails()
        {
            //var discprojs = await (from sdf in _sdfRepository.GetAll()
            //                       join pers in _personRepository.GetAll() on sdf.personId equals pers.Id
            //                       select new
            //                       {
            //                           email = pers.Email,
            //                       })
            //        .ToListAsync();
            //foreach (var d in discprojs.Distinct())
            //{
            //    await _userEmailer.OfficeMoveEmailsAsync(d.email);
            //}
            await _userEmailer.OfficeMoveEmailsAsync("smlotshwa@chieta.org.za");

        }

        

        public async Task NotificationEmail()
        {
            var discprojs = await (from sdf in _sdfRepository.GetAll()
                                   join pers in _personRepository.GetAll() on sdf.personId equals pers.Id
                                   select new
                                   {
                                       email = pers.Email,
                                   })
                    .ToListAsync();
            foreach (var d in discprojs.Distinct())
            {
                await _userEmailer.NotificationEmailsAsync(d.email);
            }
        }

        public async Task<DiscretionaryTrancheBatch> GetApprovalBatch(int BatchId)
        {
            return _trBatchRepository.GetAll().Where(a => a.Id == BatchId).ToList().FirstOrDefault();
        }

        public async Task<PagedResultDto<DiscretionaryAppTrancheRequestForViewDto>> GetAppTrancheRequestForView(int first, int rows)
        {

            var tranches =  (from tranch in _applicationTranche.GetAll()
                                  join reqdat in _requestDataRepository.GetAll().Where(a => a.Name == "ApplicationId") on tranch.ApplicationId.ToString() equals reqdat.Value
                                  join req in _requestRepository.GetAll() on reqdat.RequestId equals req.Id
                                  join cstate in _stateRepository.GetAll() on req.CurrentStateId equals cstate.Id
                                  join app in _discProjDetRepository.GetAll() on tranch.ApplicationId equals app.Id
                                  join dpr in _discProjRepository.GetAll() on app.ProjectId equals dpr.Id
                                  join stat in _discStatusRepository.GetAll() on dpr.ProjectStatusID equals stat.Id
                                  join org in _orgRepository.GetAll() on dpr.OrganisationId equals org.Id
                                  join projtype in _projTypeRepository.GetAll() on app.ProjectTypeId equals projtype.Id
                                  //join typ in _trancheType.GetAll() on tranch.TrancheTypeId equals typ.Id
                                  join fa in _focusAreaRepository.GetAll() on app.FocusAreaId equals fa.Id into fat
                                  from fas in fat.DefaultIfEmpty()
                                  join ac in _adminCritRepository.GetAll() on app.SubCategoryId equals ac.Id into sb
                                  from acs in sb.DefaultIfEmpty()

                                  select new
                                  {
                                      ApplicationId = tranch.ApplicationId,
                                      RequestId = reqdat.RequestId,
                                      SDL_No = org.SDL_No,
                                      Organisation_name = org.Organisation_Name,
                                      Contract_Number = app.Contract_Number,
                                      ProjType = projtype.ProjTypDesc,
                                      Description = tranch.Description,
                                      TrancheAmount = tranch.TrancheAmount,
                                      TrancheType = tranch.TrancheType,
                                      State_name = cstate.Description,
                                      CurrentStateId = req.CurrentStateId,
                                      Status = stat.StatusDesc,
                                      Id = tranch.Id,
                                  });

            var totalCount = tranches.Count();
            var ctranches = tranches
            .OrderByDescending(a => a.Id)
            .Skip(first)
            .Take(rows)
            .ToList();


            var tranche = from o in ctranches
                          select new DiscretionaryAppTrancheRequestForViewDto()
                          {
                              DiscretionaryAppTrancheRequest = new DiscretionaryAppTranchRequestOutputDto
                              {
                                  SDL_No = o.SDL_No,
                                  Organisation_name = o.Organisation_name,
                                  Contract_Number = o.Contract_Number,
                                  Description = o.Description,
                                  Tranche_amount = o.TrancheAmount,
                                  State_name = o.State_name,
                                  ProjectStatus = o.Status,
                                  ProjType = o.ProjType,
                                  Id = o.Id,
                                  States = (from t in _transitionRepository.GetAll().Where(a => a.CurrentStateId == o.CurrentStateId)
                                    join s in _stateRepository.GetAll() on t.NextStateId equals s.Id
                                    select new ApprovalsTrackingStates
                                    {
                                        Description = s.Description
                                    }).ToList()

                              }
                          };

            return new PagedResultDto<DiscretionaryAppTrancheRequestForViewDto>(
                totalCount,
                tranche.ToList()
            );
        }

        public async Task<PagedResultDto<DiscretionaryAppTrancheRequestForViewDto>> GetAppTrancheRequestLazy(int first, int rows, string SDLNoFilter, string SDLNoFilterMode, string ContractNumberFilter,
        string ContractNumberFilterMode, string DescriptionFilter, string DescriptionFilterMode)
        {
            var tranches =  (from tranch in _applicationTranche.GetAll()
                join reqdat in _requestDataRepository.GetAll().Where(a => a.Name == "ApplicationId") on tranch.ApplicationId.ToString() equals reqdat.Value
                join req in _requestRepository.GetAll() on reqdat.RequestId equals req.Id
                join cstate in _stateRepository.GetAll() on req.CurrentStateId equals cstate.Id
                join app in _discProjDetRepository.GetAll() on tranch.ApplicationId equals app.Id
                join dpr in _discProjRepository.GetAll() on app.ProjectId equals dpr.Id
                join stat in _discStatusRepository.GetAll() on dpr.ProjectStatusID equals stat.Id
                join org in _orgRepository.GetAll() on dpr.OrganisationId equals org.Id
                join projtype in _projTypeRepository.GetAll() on app.ProjectTypeId equals projtype.Id
                //join typ in _trancheType.GetAll() on tranch.TrancheTypeId equals typ.Id
                join fa in _focusAreaRepository.GetAll() on app.FocusAreaId equals fa.Id into fat
                from fas in fat.DefaultIfEmpty()
                join ac in _adminCritRepository.GetAll() on app.SubCategoryId equals ac.Id into sb
                from acs in sb.DefaultIfEmpty()

                select new
                {
                    ApplicationId = tranch.ApplicationId,
                    RequestId = reqdat.RequestId,
                    SDL_No = org.SDL_No,
                    Organisation_name = org.Organisation_Name,
                    Contract_Number = app.Contract_Number,
                    ProjType = projtype.ProjTypDesc,
                    Description = tranch.Description,
                    TrancheAmount = tranch.TrancheAmount,
                    TrancheType = tranch.TrancheType,
                    State_name = cstate.Description,
                    CurrentStateId = req.CurrentStateId,
                    Status = stat.StatusDesc,
                    Id = tranch.Id,
                });

            if (SDLNoFilter != null)
            {
                if (SDLNoFilterMode == "startsWith")
                {
                    tranches = tranches.Where(a => a.SDL_No.StartsWith(SDLNoFilter));
                }
                if (SDLNoFilterMode == "endsWith")
                {
                    tranches = tranches.Where(a => a.SDL_No.EndsWith(SDLNoFilter));
                }
                if (SDLNoFilterMode == "contains")
                {
                    tranches = tranches.Where(a => a.SDL_No.Contains(SDLNoFilter));
                }
                if (SDLNoFilterMode == "notContains")
                {
                    tranches = tranches.Where(a => !(a.SDL_No.Contains(SDLNoFilter)));
                }
                if (SDLNoFilterMode == "equals")
                {
                    tranches = tranches.Where(a => a.SDL_No == SDLNoFilter);
                }
            }

            if (ContractNumberFilter != null)
            {
                if (ContractNumberFilterMode == "startsWith")
                {
                    tranches = tranches.Where(a => a.Contract_Number.StartsWith(ContractNumberFilter));
                }
                if (ContractNumberFilterMode == "endsWith")
                {
                    tranches = tranches.Where(a => a.Contract_Number.EndsWith(ContractNumberFilter));
                }
                if (ContractNumberFilterMode == "contains")
                {
                    tranches = tranches.Where(a => a.Contract_Number.Contains(ContractNumberFilter));
                }
                if (ContractNumberFilterMode == "notContains")
                {
                    tranches = tranches.Where(a => !(a.Contract_Number.Contains(ContractNumberFilter)));
                }
                if (ContractNumberFilterMode == "equals")
                {
                    tranches = tranches.Where(a => a.Contract_Number == ContractNumberFilter);
                }
            }

            if (DescriptionFilter != null)
            {
                if (DescriptionFilterMode == "startsWith")
                {
                    tranches = tranches.Where(a => a.Description.StartsWith(DescriptionFilter));
                }
                if (DescriptionFilterMode == "endsWith")
                {
                    tranches = tranches.Where(a => a.Description.EndsWith(DescriptionFilter));
                }
                if (DescriptionFilterMode == "contains")
                {
                    tranches = tranches.Where(a => a.Description.Contains(DescriptionFilter));
                }
                if (DescriptionFilterMode == "notContains")
                {
                    tranches = tranches.Where(a => !(a.Description.Contains(DescriptionFilter)));
                }
                if (DescriptionFilterMode == "equals")
                {
                    tranches = tranches.Where(a => a.Description == DescriptionFilter);
                }
            }

            var totalCount = tranches.Count();
            var ctranches = tranches
                .OrderByDescending(a => a.Id)
                .Skip(first)
                .Take(rows)
                .ToList();
                
            var tranche = from o in ctranches
                          select new DiscretionaryAppTrancheRequestForViewDto()
                          {
                              DiscretionaryAppTrancheRequest = new DiscretionaryAppTranchRequestOutputDto
                              {
                                  SDL_No = o.SDL_No,
                                  Organisation_name = o.Organisation_name,
                                  Contract_Number = o.Contract_Number,
                                  Description = o.Description,
                                  Tranche_amount = o.TrancheAmount,
                                  State_name = o.State_name,
                                  ProjectStatus = o.Status,
                                  ProjType = o.ProjType,
                                  Id = o.Id,
                                  States = (from t in _transitionRepository.GetAll().Where(a => a.CurrentStateId == o.CurrentStateId)
                                            join s in _stateRepository.GetAll() on t.NextStateId equals s.Id
                                            select new ApprovalsTrackingStates
                                            {
                                                Description = s.Description
                                            }).ToList()

                              }
                          };

            return new PagedResultDto<DiscretionaryAppTrancheRequestForViewDto>(
                totalCount,
                tranche.ToList()
            );
        }

        public async Task<PagedResultDto<BatchTrackingForViewDto>> GetBatchTrackingForView()
        {

            var tranches = await (from tranch in _applicationTranche.GetAll()
                                  join tr in _trBatchRepository.GetAll() on tranch.BatchId equals tr.Id

                                  select new
                                  {
                                      Id = tranch.Id,
                                      ApplicationId = tranch.ApplicationId,
                                      TranchDescription = tr.Description,
                                      BatchNumber = tr.BatchNumber,
                                      Status = tr.Status,
                                      DateCreated = tr.DateCreated,

                                  })

                          .ToListAsync();


            var tranche = from o in tranches
                          select new BatchTrackingForViewDto()
                          {
                              BatchTracking = new BatchTrackingOutputDto
                              {
                                  Id = o.Id,
                                  ApplicationId = o.ApplicationId,
                                  Description = o.TranchDescription,
                                  BatchNumber = o.BatchNumber,
                                  Status = o.Status,
                                  Datedcreated = o.DateCreated,
                              }
                          };

            var totalCount = tranche.Count();

            return new PagedResultDto<BatchTrackingForViewDto>(
                totalCount,
                tranche.ToList()
            );
        }

    }
}
