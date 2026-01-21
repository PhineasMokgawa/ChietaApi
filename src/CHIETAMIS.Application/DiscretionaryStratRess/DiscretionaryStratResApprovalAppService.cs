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
using CHIETAMIS.DiscretionaryStratRess.Dtos;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Application.Services.Dto;
using Abp.ObjectMapping;
using System.Text;
using Abp.Configuration;
using Abp.Zero.Configuration;
using CHIETAMIS.Organisations;
using CHIETAMIS.Organisations.Dtos;
using CHIETAMIS.Sdfs;
using CHIETAMIS.Sdfs.Dtos;
using CHIETAMIS.DiscretionaryProjects;
using CHIETAMIS.DiscretionaryStratRess;
using CHIETAMIS.DiscretionaryWindows;
using CHIETAMIS.Documents;
using CHIETAMIS.Documents.Dtos;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Configuration;
using AutoMapper;
using CHIETAMIS.Lookups;
using CHIETAMIS.GrantApprovals;

namespace CHIETAMIS.DiscretionaryStratRess
{
    public class DiscretionaryStratResApprovalAppService : CHIETAMISAppServiceBase
    {
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
        private readonly IRepository<DiscretionaryStratResDetailsApproval> _stratResDetRepository;
        private readonly IRepository<DiscretionaryStratResObjectivesApproval> _stratObjRepository;
        private readonly IRepository<BankDetails> _bankRepository;
        private readonly IRepository<OrganisationPhysicalAddress> _addressRepository;
        private readonly IRepository<Document> _docRepository;
        private readonly IRepository<ResearchDocumentApproval> _docApprovalRepository;
        private readonly IRepository<DiscretionaryResearchApproval> _discResearchApprovalRepository;
        private readonly IRepository<DiscretionaryGECRApproval> _discGECRApprovalRepository;
        private readonly IRepository<GrantApprovalStatus> _grantApprovalStatusRepository;
        private readonly IRepository<GrantApprovalType> _grantApprovalTypeRepository;
        private readonly IRepository<DiscretionaryGrantApproval> _discGrantApprovalRepository;


        public DiscretionaryStratResApprovalAppService (IRepository<DiscretionaryProject> dicprojRepository,
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
                                              IRepository<DiscretionaryStratResDetailsApproval> stratResDetRepository,
                                              IRepository<DiscretionaryStratResObjectivesApproval> stratObjRepository,
                                              IRepository<BankDetails> bankRepository,
                                              IRepository<OrganisationPhysicalAddress> addressRepository,
                                              IRepository<Document> docRepository,
                                              IRepository<ResearchDocumentApproval> docApprovalRepository,
                                              IRepository<DiscretionaryResearchApproval> discResearchApprovalRepository,
                                              IRepository<GrantApprovalStatus> grantApprovalStatusRepository,
                                              IRepository<GrantApprovalType> grantApprovalTypeRepository,
                                              IRepository<DiscretionaryGECRApproval> discGECRApprovalRepository,
                                              IRepository<DiscretionaryGrantApproval> discGrantApprovalRepository)
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
            _stratResDetRepository = stratResDetRepository;
            _stratObjRepository = stratObjRepository;
            _bankRepository = bankRepository;
            _addressRepository = addressRepository;
            _docRepository = docRepository;
            _docApprovalRepository = docApprovalRepository;
            _discResearchApprovalRepository = discResearchApprovalRepository;
            _grantApprovalStatusRepository = grantApprovalStatusRepository;
            _grantApprovalTypeRepository = grantApprovalTypeRepository;
            _discGECRApprovalRepository = discGECRApprovalRepository;
            _discGrantApprovalRepository = discGrantApprovalRepository;
        }

        public async Task<DiscretionaryProjectDetailsApprovalDto> GetDGProjectDetById(int id)
        {
            var proj = await _discProjDetRepository.GetAsync(id);

            var output = ObjectMapper.Map<DiscretionaryProjectDetailsApprovalDto>(proj);

            return output;
        }

        public async Task CreateEditProjectDetails(DiscretionaryProjectDetailsApprovalDto input)
        {
            var dgprojdet = _discProjDetRepository.GetAll().Where(a => a.Id == input.Id);

            if (dgprojdet.Count() == 0)
            {
                input.DateCreated = DateTime.Now;
                var projdet = ObjectMapper.Map<DiscretionaryProjectDetailsApproval>(input);

                await _discProjDetRepository.InsertAsync(projdet);
            }
            else
            {
                var dgdet = await _discProjDetRepository.FirstOrDefaultAsync(dgprojdet.First().Id);
                dgdet.DteUpd = DateTime.Now;
                dgdet.UsrUpd = input.UserId;
                dgdet.FocusAreaId = input.FocusAreaId;
                dgdet.SubCategoryId = input.SubCategoryId;
                dgdet.InterventionId = input.InterventionId;
                dgdet.Province = input.Province;
                dgdet.Municipality = input.Municipality;
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
                                         Province = o.Province,
                                         Municipality = o.Municipality,
                                         Id = o.Id
                                     }
                                 }).ToListAsync();

            var totalCount = det.Count();

            return new PagedResultDto<DiscretionaryProjectDetailsApprovalPagedDto>(
                totalCount,
                projdet.ToList()
            );
        }
        public async Task<DiscretionaryStratResDetailsApprovalDto> GetStratResDetById(int id)
        {
            var proj = await _stratResDetRepository.GetAsync(id);

            var output = ObjectMapper.Map<DiscretionaryStratResDetailsApprovalDto>(proj);

            return output;
        }

        public async Task CreateEditStratResDetails(DiscretionaryStratResDetailsApprovalDto input)
        {
            var dgprojdet = _stratResDetRepository.GetAll().Where(a => a.Id == input.Id);

            if (dgprojdet.Count() == 0)
            {
                var chk = _stratResDetRepository.GetAll().
                    Where(a => a.ProjectId == input.ProjectId && a.FocusAreaId == input.FocusAreaId && a.ProjectTypeId == input.ProjectTypeId && a.SubCategoryId == input.SubCategoryId
                    && a.InterventionId == input.InterventionId && a.Municipality == input.Municipality);
                if (chk.Count() == 0)
                {
                    input.DateCreated = DateTime.Now;
                    var projdet = ObjectMapper.Map<DiscretionaryStratResDetailsApproval>(input);

                    await _stratResDetRepository.InsertAsync(projdet);
                }
                else
                {
                    throw new InvalidOperationException("Dupicate entry detected.");
                }
            }
            else
            {
                var dgdet = await _stratResDetRepository.FirstOrDefaultAsync(dgprojdet.First().Id);
                dgdet.DteUpd = DateTime.Now;
                dgdet.UsrUpd = input.UserId;
                dgdet.FocusAreaId = input.FocusAreaId;
                dgdet.SubCategoryId = input.SubCategoryId;
                dgdet.InterventionId = input.InterventionId;
                dgdet.Province = input.Province;
                dgdet.Municipality = input.Municipality;
                await _stratResDetRepository.UpdateAsync(dgdet);
            }
        }

        public async Task<PagedResultDto<PagedDiscretionaryStratResDetailApprovalView>> GetDGStratResDetails(int ProjectId)
        { 
            var discprojs = await (from projdet in _stratResDetRepository.GetAll()
                                   join proj in _discProjRepository.GetAll() on projdet.ProjectId equals proj.Id
                                   join stat in _discStatusRepository.GetAll() on proj.ProjectStatusID equals stat.Id
                                   join projtype in _projTypeRepository.GetAll() on projdet.ProjectTypeId equals projtype.Id
                                   join focarea in _focusAreaRepository.GetAll() on projdet.FocusAreaId equals focarea.Id
                                   join subcat in _adminCritRepository.GetAll() on projdet.SubCategoryId equals subcat.Id
                                   join interv in _evalMethodRepository.GetAll() on projdet.InterventionId equals interv.Id into emp
                                   from emps in emp.DefaultIfEmpty()
                                   join appr in _discResearchApprovalRepository.GetAll() on projdet.Id equals appr.ApplicationId into pr
                                   from apprs in pr.DefaultIfEmpty()
                                   select new
                                   {
                                       Application = projdet,
                                       ProjType = projtype.ProjTypDesc,
                                       FocusArea = focarea.FocusAreaDesc,
                                       SubCategory = subcat.AdminDesc,
                                       Intervention = emps.EvalMthdDesc,
                                       Status = stat.StatusDesc,
                                       ApprovalStatus = apprs
                                   })
                    .Where(a => a.Application.ProjectId == ProjectId)
                    .ToListAsync();

            var objs = await (from obj in _stratObjRepository.GetAll()
                              join projdet in _stratResDetRepository.GetAll() on obj.DetailsId equals projdet.Id
                              select new
                              {
                                  Objectives = obj,
                                  projid = projdet.Id
                              }).Where(a => a.Objectives.DetailsId == a.projid)
                  .ToListAsync();

            var objects = from o in objs
                          select new DiscretionaryStratResObjectivesApprovalDto()
                          {
                              Id = o.Objectives.Id,
                              DetailsId = o.Objectives.DetailsId,
                              Objectiv = o.Objectives.Objectiv,
                              Learners = o.Objectives.Learners,
                              Cost = o.Objectives.Cost,
                              UserId = o.Objectives.UserId,
                              DateCreated = o.Objectives.DateCreated
                          };


            var discproject = from o in discprojs
                              select new PagedDiscretionaryStratResDetailApprovalView()
                              {
                                  DiscretionaryStratResDetailsApproval = new DiscretionaryStratResDetailsApprovalView()
                                  {
                                      ProjectType = o.ProjType,
                                      FocusArea = o.FocusArea,
                                      SubCategory = o.SubCategory,
                                      Intervention = o.Intervention,
                                      Province = o.Application.Province,
                                      Municipality = o.Application.Municipality,
                                      Status = o.Status,
                                      ApprovalStatus = o.ApprovalStatus,
                                      Cost = o.Application.Cost,
                                      Id = o.Application.Id
                                  },
                                  DiscretionaryStratResObjectivesApproval = objects.Where(a => a.DetailsId == o.Application.Id).ToList()
                              };

            var totalCount = discproject.Count();

            return new PagedResultDto<PagedDiscretionaryStratResDetailApprovalView>(
                totalCount,
                discproject.ToList()
            );
        }

        public async Task<PagedResultDto<PagedDiscretionaryStratResDetailApprovalView>> GetCommitteeStratResDetails()
        {
            var discprojs = await (from projdet in _stratResDetRepository.GetAll()
                                   join proj in _discProjRepository.GetAll() on projdet.ProjectId equals proj.Id
                                   join org in _orgRepository.GetAll() on proj.OrganisationId equals org.Id
                                   join stat in _discStatusRepository.GetAll() on proj.ProjectStatusID equals stat.Id
                                   join projtype in _projTypeRepository.GetAll() on projdet.ProjectTypeId equals projtype.Id
                                   join focarea in _focusAreaRepository.GetAll() on projdet.FocusAreaId equals focarea.Id
                                   join subcat in _adminCritRepository.GetAll() on projdet.SubCategoryId equals subcat.Id
                                   join interv in _evalMethodRepository.GetAll() on projdet.InterventionId equals interv.Id into emp 
                                   from emps in emp.DefaultIfEmpty()
                                   join appr in _discResearchApprovalRepository.GetAll() on projdet.Id equals appr.ApplicationId into pr
                                   from apprs in pr.DefaultIfEmpty()
                                   join gec in _discGECRApprovalRepository.GetAll() on projdet.Id equals gec.ApplicationId into g
                                   from gecs in g.DefaultIfEmpty()
                                   //join grantappr in _discGrantApprovalRepository.GetAll().Where(a=>a.ApprovalStatusId == apprs.ApprovalStatusId) on proj.Id equals grantappr.ProjectId into ga
                                   //from gaps in g.DefaultIfEmpty()
                                   select new
                                   {
                                       Application = projdet,
                                       SDL_No = org.SDL_No,
                                       Organisation_Name = org.Organisation_Name,
                                       ProjType = projtype.ProjTypDesc,
                                       FocusArea = focarea.FocusAreaDesc,
                                       SubCategory = subcat.AdminDesc,
                                       Intervention = emps.EvalMthdDesc,
                                       Status = stat.StatusDesc,
                                       ApprovalStatus = apprs,
                                       GECStatus = gecs
                                   })
                    .Where(a => a.Status == "GEC Committee Review Completed" || a.Status == "RSA Review Completed" || a.Status == "Rejected after Full Assessment" || a.Status == "Regional Review Completed")
                    .ToListAsync();

            var discproject = from o in discprojs
                              select new PagedDiscretionaryStratResDetailApprovalView()
                              {
                                  DiscretionaryStratResDetailsApproval = new DiscretionaryStratResDetailsApprovalView()
                                  {
                                      SDL_No = o.SDL_No,
                                      Organisation_Name = o.Organisation_Name,
                                      Province = o.Application.Province,
                                      ProjectType = o.ProjType,
                                      FocusArea = o.FocusArea,
                                      SubCategory = o.SubCategory,
                                      Intervention = o.Intervention,
                                      Cost = o.Application.Cost,
                                      GEC_Amount = o.Application.GEC_Amount,
                                      vision2025goal = o.Application.vision2025goal,
                                      SqmrAppIndicator = o.Application.SqmrAppIndicator,
                                      Leviesuptodate = o.Application.Leviesuptodate,
                                      PreviousWSP = o.Application.PreviousWSP,
                                      PreviousParticipation = o.Application.PreviousParticipation,
                                      Municipality = o.Application.Municipality,
                                      Status = o.Status,
                                      ApprovalStatus = o.ApprovalStatus,
                                      GECStatus = o.GECStatus,
                                      Id = o.Application.Id
                                  },                                  
                              };

            var totalCount = discproject.Count();

            return new PagedResultDto<PagedDiscretionaryStratResDetailApprovalView>(
                totalCount,
                discproject.ToList()
            );
        }

        public async Task<PagedResultDto<DiscretionaryStratResDetailsApprovalForViewDto>> GetStratResDetails(int ProjectId)
        {
            var det = await (from detls in _stratResDetRepository.GetAll()
                             join obj in _stratObjRepository.GetAll() on detls.Id equals obj.DetailsId
                             select new
                             {
                                 Details = detls,
                                 Objectives = obj
                             }).Where(a => a.Details.ProjectId == ProjectId)
                               .ToListAsync();


            var projdet = (from o in det
                           select new DiscretionaryStratResDetailsApprovalForViewDto
                           {
                               DiscretionaryStratResDetailsApproval = new DiscretionaryStratResDetailsApprovalDto
                               {
                                   ProjectTypeId = o.Details.ProjectTypeId,
                                   FocusAreaId = o.Details.FocusAreaId,
                                   SubCategoryId = o.Details.SubCategoryId,
                                   InterventionId = o.Details.InterventionId,
                                   Province = o.Details.Province,
                                   Municipality = o.Details.Municipality,
                                   Id = o.Details.Id
                               },
                               DiscretionaryStratResObjectivesApproval = ObjectMapper.Map<DiscretionaryStratResObjectivesApprovalDto>(o.Objectives)
                           });

            var totalCount = det.Count();

            return new PagedResultDto<DiscretionaryStratResDetailsApprovalForViewDto>(
                totalCount,
                projdet.ToList()
            );
        }

        public async Task CreateEditStratResObjective(DiscretionaryStratResObjectivesApprovalDto input)
        {
            var dgstratrecdet = _stratObjRepository.GetAll().Where(a => a.Id == input.Id);

            if (dgstratrecdet.Count() == 0)
            {
                input.DateCreated = DateTime.Now;
                var stratrecdet = ObjectMapper.Map<DiscretionaryStratResObjectivesApproval>(input);

                await _stratObjRepository.InsertAsync(stratrecdet);
            }
            else
            {
                var stratrecdet = await _stratObjRepository.FirstOrDefaultAsync(dgstratrecdet.First().Id);
                //cappl.DteUpd = DateTime.Now;
                stratrecdet.UserId = input.UserId;
                stratrecdet.Objectiv = input.Objectiv;
                stratrecdet.Cost = input.Cost;
                stratrecdet.Learners = input.Learners;
                await _stratObjRepository.UpdateAsync(stratrecdet);
            }
        }

        public async Task<PagedResultDto<DiscretionaryStratResObjectsApprovalForViewDto>> GetStratResObjectives(int DetailId)
        {
            var det = _stratObjRepository.GetAll().Where(a => a.DetailsId == DetailId);


            var projdet = await (from o in det
                                 select new DiscretionaryStratResObjectsApprovalForViewDto
                                 {
                                     DiscretionaryStratResObjectivesApproval = new DiscretionaryStratResObjectivesApprovalDto
                                     {
                                         Objectiv = o.Objectiv,
                                         DetailsId = o.DetailsId,
                                         Cost = o.Cost,
                                         Learners = o.Learners,
                                         UserId = o.UserId,
                                         Id = o.Id
                                     }
                                 }).ToListAsync();

            var totalCount = det.Count();

            return new PagedResultDto<DiscretionaryStratResObjectsApprovalForViewDto>(
                totalCount,
                projdet.ToList()
            );
        }
        public async Task<DiscretionaryStratResObjectivesApprovalDto> GetStratResObjById(int id)
        {
            var proj = await _stratObjRepository.GetAsync(id);

            var output = ObjectMapper.Map<DiscretionaryStratResObjectivesApprovalDto>(proj);

            return output;
        }

        public async Task<string> validateResSubmission(int projId)
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

            var projdet = _stratResDetRepository.GetAll().Where(a => a.ProjectId == projId).ToList();
            if (projdet.Count() == 0) { output = output + ", Application Details"; }
            else
            {
                var hasind = true;
                foreach (var pr in projdet)
                {
                    if (_stratObjRepository.GetAll().Where(a => a.DetailsId == pr.Id).Count() == 0) { hasind = false; }
                }
                if (!hasind) { output = output + ", Indicators"; }
            }

            var docs = _docRepository.GetAll().Where(a => a.entityid == proj.Id).ToList();

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
            var applic = await _stratResDetRepository.FirstOrDefaultAsync(applicationid);
            var projectId = applic.ProjectId;
            var appdocs = _docRepository.GetAll().Where(a => a.entityid == projectId && a.module == "Projects" && a.documenttype != "SDF Appointment").ToList();
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

            return output;
        }

        public async Task<string> FinaliseApproval(DiscretionaryResearchApproval input)
        {
            string output = "";
            var appr = _discResearchApprovalRepository.GetAll().Where(a => a.ApplicationId == input.ApplicationId).FirstOrDefault();
            if (appr == null)
            {
                input.DateCreated = DateTime.Now;
                var app = ObjectMapper.Map<DiscretionaryResearchApproval>(input);

                await _discResearchApprovalRepository.InsertAsync(app);
            }
            else
            {

            }

            return output;
        }

        public async Task ApproveDocument(int doctype, int stat, string comment, int projectId, int userid)
        {
            var appr = _docApprovalRepository.GetAll().Where(a => a.ProjectId == projectId && a.DocumentTypeId == doctype && a.ApprovalTypeId == 1).FirstOrDefault();
            if (appr == null)
            {
                var inappr = new ResearchDocumentApproval();
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
            var det = await _discResearchApprovalRepository.GetAll().Where(a => a.ApplicationId == applicationId).FirstOrDefaultAsync();
            var proj = await _discResearchApprovalRepository.GetAsync(det.Id);
            await _discResearchApprovalRepository.DeleteAsync(proj);
        }

        public async Task<string> FinaliseGECApproval(DiscretionaryGECRApproval input)
        {
            string output = "";
            var appr = _discGECRApprovalRepository.GetAll().Where(a => a.ApplicationId == input.ApplicationId).FirstOrDefault();
            if (appr == null)
            {
                input.DateCreated = DateTime.Now;
                var app = ObjectMapper.Map<DiscretionaryGECRApproval>(input);

                await _discGECRApprovalRepository.InsertAsync(app);
            }
            else
            {
                output = "Already finalised.";
            }

            return output;
        }

        public async Task ReOpenGECApproval(int applicationId, int userid)
        {
            var det = await _discGECRApprovalRepository.GetAll().Where(a => a.ApplicationId == applicationId).FirstOrDefaultAsync();
            var proj = await _discGECRApprovalRepository.GetAsync(det.Id);
            await _discGECRApprovalRepository.DeleteAsync(proj);
        }

        public async Task UpdateGECRApplication(int applicationId, decimal cost, int userid)
        {
            var appl = _stratResDetRepository.GetAll().Where(a => a.Id == applicationId);

            if (appl.Count() == 1)
            {
                var dg = _stratResDetRepository.Get(applicationId);
                dg.GEC_Amount = cost;
                dg.UsrUpd = userid;
                dg.DteUpd = DateTime.Now;

                var app = ObjectMapper.Map<DiscretionaryStratResDetailsApproval>(dg);

                await _stratResDetRepository.UpdateAsync(app);
            }
            else
            {
                throw new InvalidOperationException("Not found.");
            }
        }

        public async Task<PagedResultDto<DiscretionaryProjectForViewDto>> GetStratResApprovalRSA(int userid)
        {
            var discprojs = await (from discgrant in _discProjRepository.GetAll()
                                   join projdet in _stratResDetRepository.GetAll() on discgrant.Id equals projdet.ProjectId
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
    }
}
