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

namespace CHIETAMIS.DiscretionaryStratRess
{
    public class DiscretionaryStratResAppService : CHIETAMISAppServiceBase
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
        private readonly IRepository<DiscretionaryProjectDetails> _discProjDetRepository;
        private readonly IRepository<DiscretionaryStratResDetails> _stratResDetRepository;
        private readonly IRepository<DiscretionaryStratResObjectives> _stratObjRepository;
        private readonly IRepository<BankDetails> _bankRepository;
        private readonly IRepository<OrganisationPhysicalAddress> _addressRepository;
        private readonly IRepository<Document> _docRepository;

        public DiscretionaryStratResAppService(IRepository<DiscretionaryProject> dicprojRepository,
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
                                              IRepository<DiscretionaryStratResDetails> stratResDetRepository,
                                              IRepository<DiscretionaryStratResObjectives> stratObjRepository,
                                              IRepository<BankDetails> bankRepository,
                                              IRepository<OrganisationPhysicalAddress> addressRepository,
                                              IRepository<Document> docRepository)
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
        }
        
        public async Task<DiscretionaryProjectDetailsDto> GetDGProjectDetById(int id)
        {
            var proj = await _discProjDetRepository.GetAsync(id);

            var output = ObjectMapper.Map<DiscretionaryProjectDetailsDto>(proj);

            return output;
        }

        public async Task CreateEditProjectDetails(DiscretionaryProjectDetailsDto input)
        {
            var dgprojdet = _discProjDetRepository.GetAll().Where(a => a.Id == input.Id);

            if (dgprojdet.Count() == 0)
            {
                input.DateCreated = DateTime.Now;
                var projdet = ObjectMapper.Map<DiscretionaryProjectDetails>(input);

                await _discProjDetRepository.InsertAsync(projdet);
            }
            else
            {
                var dgdet = await _discProjDetRepository.FirstOrDefaultAsync(dgprojdet.First().Id);
                //cappl.DteUpd = DateTime.Now;
                dgdet.UserId = input.UserId;
                dgdet.FocusAreaId = input.FocusAreaId;
                dgdet.SubCategoryId = input.SubCategoryId;
                dgdet.InterventionId = input.InterventionId;
                dgdet.Province = input.Province;
                dgdet.Municipality = input.Municipality;
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
                                         Province = o.Province,
                                         Municipality = o.Municipality,
                                         Id = o.Id
                                     }
                                 }).ToListAsync();

            var totalCount = det.Count();

            return new PagedResultDto<DiscretionaryProjectDetailsForViewDto>(
                totalCount,
                projdet.ToList()
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
                                   join dpd in _stratResDetRepository.GetAll() on discgrant.Id equals dpd.ProjectId
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


        public async Task<PagedResultDto<DiscretionaryProjectForViewDto>> GetStratRestForView(int Id)
        {

            var discprojs = await (from discgrant in _discProjRepository.GetAll()
                                   join org in _orgRepository.GetAll() on discgrant.OrganisationId equals org.Id
                                   join orgsdf in _orgsdfRepository.GetAll() on discgrant.OrganisationId equals orgsdf.OrganisationId
                                   join stat in _discStatusRepository.GetAll() on discgrant.ProjectStatusID equals stat.Id
                                   join wind in _windowRepository.GetAll() on discgrant.GrantWindowId equals wind.Id
                                   //join prms in _windowParamRepository.GetAll() on discgrant.WindowParamId equals prms.Id
                                   join dpd in _stratResDetRepository.GetAll() on discgrant.Id equals dpd.ProjectId
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

        public async Task<DiscretionaryStratResDetailsDto> GetStratResDetById(int id)
        {
            var proj = await _stratResDetRepository.GetAsync(id);

            var output = ObjectMapper.Map<DiscretionaryStratResDetailsDto>(proj);

            return output;
        }

        public async Task CreateEditStratResDetails(DiscretionaryStratResDetailsDto input)
        {
            var dgprojdet = _stratResDetRepository.GetAll().Where(a => a.Id == input.Id);

            if (dgprojdet.Count() == 0)
            {
                var chk = _stratResDetRepository.GetAll().
                    Where(a => a.ProjectId == input.ProjectId && a.FocusAreaId == input.FocusAreaId && a.ProjectTypeId == input.ProjectTypeId && a.SubCategoryId == input.SubCategoryId
                    && a.InterventionId == input.InterventionId && a.Province == input.Province && a.District == input.District && a.Municipality == input.Municipality);
                if (chk.Count() == 0)
                {
                    input.DateCreated = DateTime.Now;
                    var projdet = ObjectMapper.Map<DiscretionaryStratResDetails>(input);

                    await _stratResDetRepository.InsertAsync(projdet);
                } else
                {
                    throw new InvalidOperationException("Dupicate entry detected.");
                }
            }
            else
            {
                var dgdet = await _stratResDetRepository.FirstOrDefaultAsync(dgprojdet.First().Id);
                //cappl.DteUpd = DateTime.Now;
                dgdet.UserId = input.UserId;
                dgdet.FocusAreaId = input.FocusAreaId;
                dgdet.SubCategoryId = input.SubCategoryId;
                dgdet.InterventionId = input.InterventionId;
                dgdet.Province = input.Province;
                dgdet.Municipality = input.Municipality;
                await _stratResDetRepository.UpdateAsync(dgdet);
            }
        }

        public async Task<PagedResultDto<PagedDiscretionaryStratResDetailView>> GetDGStratResDetails(int ProjectId)
        {
            var discprojs = await (from projdet in _stratResDetRepository.GetAll()
                                   join proj in _discProjRepository.GetAll() on projdet.ProjectId equals proj.Id
                                   join stat in _discStatusRepository.GetAll() on proj.ProjectStatusID equals stat.Id
                                   join projtype in _projTypeRepository.GetAll() on projdet.ProjectTypeId equals projtype.Id
                                   join focarea in _focusAreaRepository.GetAll() on projdet.FocusAreaId equals focarea.Id
                                   join subcat in _adminCritRepository.GetAll() on projdet.SubCategoryId equals subcat.Id
                                   join interv in _evalMethodRepository.GetAll() on projdet.InterventionId equals interv.Id into emp
                                   from emps in emp.DefaultIfEmpty()
                                   select new
                                   {
                                       Application = projdet,
                                       //Objective = ObjectMapper.Map<List<DiscretionaryStratResObjectivesDto>>(objs),
                                       ProjType = projtype.ProjTypDesc,
                                       FocusArea = focarea.FocusAreaDesc,
                                       SubCategory = subcat.AdminDesc,
                                       Intervention = emps.EvalMthdDesc,
                                       Status = stat.StatusDesc
                                   })
                    .Where(a => a.Application.ProjectId == ProjectId)
                    .ToListAsync();

            var objs = await (from obj in _stratObjRepository.GetAll()
                join projdet in _stratResDetRepository.GetAll() on obj.DetailsId equals projdet.Id
                select new
                {
                    Objectives = obj,
                    projid = projdet.Id
                }).Where(a=>a.Objectives.DetailsId == a.projid)
                  .ToListAsync();

            var objects = from o in objs
                          select new DiscretionaryStratResObjectivesDto()
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
                              select new PagedDiscretionaryStratResDetailView()
                              {
                                  DiscretionaryStratResDetails = new DiscretionaryStratResDetailsView()
                                  {
                                      ProjectType = o.ProjType,
                                      FocusArea = o.FocusArea,
                                      SubCategory = o.SubCategory,
                                      Intervention = o.Intervention,
                                      Province = o.Application.Province,
                                      Municipality = o.Application.Municipality,
                                      Status = o.Status,
                                      Id = o.Application.Id
                                  },
                                  DiscretionaryStratResObjectives = objects.Where(a=>a.DetailsId == o.Application.Id).ToList()
                              };

            var totalCount = discproject.Count();

            return new PagedResultDto<PagedDiscretionaryStratResDetailView>(
                totalCount,
                discproject.ToList()
            );
        }

        public async Task<PagedResultDto<DiscretionaryStratResDetailsForViewDto>> GetStratResDetails(int ProjectId)
        {
            var det = await (from  detls in _stratResDetRepository.GetAll()
                             join obj in _stratObjRepository.GetAll() on detls.Id equals obj.DetailsId
                             select new
                             {
                                 Details = detls,
                                 Objectives = obj
                             }).Where(a => a.Details.ProjectId == ProjectId)
                               .ToListAsync();


            var projdet = (from o in det
                                 select new DiscretionaryStratResDetailsForViewDto
                                 {
                                     DiscretionaryStratResDetails = new DiscretionaryStratResDetailsDto
                                     {
                                         ProjectTypeId = o.Details.ProjectTypeId,
                                         FocusAreaId = o.Details.FocusAreaId,
                                         SubCategoryId = o.Details.SubCategoryId,
                                         InterventionId = o.Details.InterventionId,
                                         Province = o.Details.Province,
                                         Municipality = o.Details.Municipality,
                                         Id = o.Details.Id
                                     },
                                     DiscretionaryStratResObjectives = ObjectMapper.Map<DiscretionaryStratResObjectivesDto>(o.Objectives)
                                 });

            var totalCount = det.Count();

            return new PagedResultDto<DiscretionaryStratResDetailsForViewDto>(
                totalCount,
                projdet.ToList()
            );
        }

        public async Task CreateEditStratResObjective(DiscretionaryStratResObjectivesDto input)
        {
            var dgstratrecdet = _stratObjRepository.GetAll().Where(a => a.Id == input.Id);

            if (dgstratrecdet.Count() == 0)
            {
                input.DateCreated = DateTime.Now;
                var stratrecdet = ObjectMapper.Map<DiscretionaryStratResObjectives>(input);

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

        public async Task<PagedResultDto<DiscretionaryStratResObjectsForViewDto>> GetStratResObjectives(int DetailId)
        {
            var det = _stratObjRepository.GetAll().Where(a => a.DetailsId == DetailId);


            var projdet = await (from o in det
                                 select new DiscretionaryStratResObjectsForViewDto
                                 {
                                     DiscretionaryStratResObjectives = new DiscretionaryStratResObjectivesDto
                                     {
                                         Objectiv = o.Objectiv,
                                         Cost = o.Cost,
                                         Learners = o.Learners,
                                         UserId = o.UserId,
                                         Id = o.Id
                                     }
                                 }).ToListAsync();

            var totalCount = det.Count();

            return new PagedResultDto<DiscretionaryStratResObjectsForViewDto>(
                totalCount,
                projdet.ToList()
            );
        }
        public async Task<DiscretionaryStratResObjectivesDto> GetStratResObjById(int id)
        {
            var proj = await _stratResDetRepository.GetAsync(id);

            var output = ObjectMapper.Map<DiscretionaryStratResObjectivesDto>(proj);

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
                    if (_stratObjRepository.GetAll().Where(a=>a.DetailsId == pr.Id).Count() == 0) { hasind = false; }
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
                if (proj.ProjectNam.Contains("Qualification"))
                {
                    var qual = docs.Where(a => a.documenttype == "Qualifications").Count();
                    if (qual == 0) { output = output + ", Proof of Qualifications"; }
                }
                var letters = docs.Where(a => a.documenttype == "Reference").Count();
                if (letters == 0) { output = output + ", Reference Letters"; }
                var cv = docs.Where(a => a.documenttype == "CV").Count();
                if (cv == 0) { output = output + ", CV"; }
            }

            if (output.StartsWith(",")) { output = output.Substring(2, output.Length - 2); }

            return output;
        }
    }
}
