using Abp.Domain.Repositories;
using CHIETAMIS.Lookups.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Newtonsoft.Json.Linq;
using System.Xml.Linq;
using Abp.Extensions;

namespace CHIETAMIS.Lookups
{
    public class LookupAppService : CHIETAMISAppServiceBase
    {
        private readonly IRepository<Area> _areaRepository;
        private readonly IRepository<Bank> _bankRepository;
        private readonly IRepository<Bank_Account_Type> _accTypeRepository;
        private readonly IRepository<GrantApprovalType> _grantApprovalType;
        private readonly IRepository<DgPaymentTrancheType> _trancheType;
        private readonly IRepository<Mandatory_Programmes> _mandProg;
        private readonly IRepository<Mandatory_Grants_Gap_Reason> _gapReason;
        private readonly IRepository<EmploymentStatus> _empStatus;
        private readonly IRepository<Mandatory_Grants_Target_Beneficiary> _targetBeneficiaries;
        private readonly IRepository<Mandatory_Grants_Scarce_Reason> _scarceReason;
        private readonly IRepository<Mandatory_Grant_Qualification_Type> _qualType;
        private readonly IRepository<Mandatoty_Grants_Impact> _grantImpact;
        private readonly IRepository<Mandatory_Grant_Achievement_Status> _achievementStatus;
        private readonly IRepository<Learning_Programme> _learningProg;
        private readonly IRepository<Occupation_Level> _occLevel;
        private readonly IRepository<OFO_Specialization> _specializations;
        private readonly IRepository<Mandatory_Extension_Status> _extensions;
        private readonly IRepository<ProgrammeDeliverables> _dgprogdel;
        private readonly IRepository<SICCodes> _sic;

        public LookupAppService(IRepository<Area> areaRepository,
                                IRepository<Bank> bankRepository,
                                IRepository<Bank_Account_Type> accTypeRepository,
                                IRepository<GrantApprovalType> grantApprovalType,
                                IRepository<DgPaymentTrancheType> paymentTrancheType,
                                IRepository<DgPaymentTrancheType> trancheType,
                                IRepository<Mandatory_Programmes> mandProg,
                                IRepository<Mandatory_Grants_Gap_Reason> gapReason,
                                IRepository<EmploymentStatus> empStatus,
                                IRepository<Mandatory_Grants_Target_Beneficiary> targetBeneficiaries,
                                IRepository<Mandatory_Grants_Scarce_Reason> scarceReason,
                                IRepository<Mandatory_Grant_Qualification_Type> qualType,
                                IRepository<Mandatoty_Grants_Impact> grantImpact,
                                IRepository<Mandatory_Grant_Achievement_Status> achievementStatus,
                                IRepository<Learning_Programme> learningProg,
                                IRepository<Occupation_Level> occLevel,
                                IRepository<OFO_Specialization> specializations,
                                IRepository<Mandatory_Extension_Status> extensions,
                                IRepository<ProgrammeDeliverables> dgprogdel,
                                IRepository<SICCodes> sic)
            
        {
            _areaRepository = areaRepository;
            _bankRepository = bankRepository;
            _accTypeRepository = accTypeRepository;
            _grantApprovalType = grantApprovalType;
            _trancheType = trancheType;
            _mandProg = mandProg;
            _gapReason = gapReason;
            _empStatus= empStatus;
            _gapReason = gapReason;
            _targetBeneficiaries= targetBeneficiaries;
            _scarceReason= scarceReason;
            _qualType= qualType;
            _grantImpact= grantImpact;
            _achievementStatus=achievementStatus;
            _learningProg= learningProg;
            _occLevel= occLevel;
            _specializations= specializations;
            _dgprogdel= dgprogdel;
            _sic= sic;
        }

        public async Task<AreaDto> Areas()
        {
            var values = _areaRepository.GetAll();
            return ObjectMapper.Map<AreaDto>(values);
        }

        public async Task<BankDto> Banks()
        {
            var values = _bankRepository.GetAll().ToList();

            return ObjectMapper.Map<BankDto>(values);
        }

        public async Task<GrantApprovalTypeDto> GrantApprovalTypes()
        {
            var values = _grantApprovalType.GetAll();

            return ObjectMapper.Map<GrantApprovalTypeDto>(values);
        }

        public async Task<DgPaymentTrancheTypeDto> PaymentTrancheTypes()
        {
            var values = _grantApprovalType.GetAll();

            return ObjectMapper.Map<DgPaymentTrancheTypeDto>(values);
        }

        public async Task<PagedResultDto<Mandatory_ProgrammesDtoForView>> Mandatory_Programmes()
        {
            var tasks = _mandProg.GetAll();

            var query = (from o in tasks
                         select new Mandatory_ProgrammesDtoForView()
                         {
                             Mandatory_Programmes = new Mandatory_ProgrammesDto
                             {
                                 Id = o.Id,
                                 Programme_Type = o.Programme_Type,
                                 Programme = o.Programme
                             }
                         });

            var output = await query.ToListAsync();
            var totalCount = await query.CountAsync();


            return new PagedResultDto<Mandatory_ProgrammesDtoForView>(totalCount, output);
        }

        public async Task<PagedResultDto<Mandatory_Grants_Gap_ReasonDtoForView>> Gap_Reasons()
        {
            var tasks = _gapReason.GetAll();

            var query = (from o in tasks
                         select new Mandatory_Grants_Gap_ReasonDtoForView()
                         {
                             Mandatory_Grants_Gap_Reason = new Mandatory_Grants_Gap_ReasonDto
                             {
                                 Gap_Reason = o.Gap_Reason,
                                 Id = o.Id
                             }
                         });

            var output = await query.ToListAsync();
            var totalCount = await query.CountAsync();


            return new PagedResultDto<Mandatory_Grants_Gap_ReasonDtoForView>(totalCount, output);
        }

        public async Task<PagedResultDto<EmploymentStatusDtoForView>> EmploymentStatuses()
        {
            var tasks = _empStatus.GetAll();

            var query = (from o in tasks
                         select new EmploymentStatusDtoForView()
                         {
                             EmploymentStatus = new EmploymentStatusDto
                             {
                                 Employment_Status = o.Employment_Status,
                                 Id = o.Id
                             }
                         });

            var output = await query.ToListAsync();
            var totalCount = await query.CountAsync();


            return new PagedResultDto<EmploymentStatusDtoForView>(totalCount, output);
        }

        public async Task<PagedResultDto<Mandatory_Grants_Target_BeneficiaryDtoForView>> Target_Beneficiaries()
        {
            var tasks = _targetBeneficiaries.GetAll();

            var query = (from o in tasks
                         select new Mandatory_Grants_Target_BeneficiaryDtoForView()
                         {
                             Mandatory_Grants_Target_Beneficiary = new Mandatory_Grants_Target_BeneficiaryDto
                             {
                                 Target_Beneficiary = o.Target_Beneficiary,
                                 Id = o.Id
                             }
                         });

            var output = await query.ToListAsync();
            var totalCount = await query.CountAsync();


            return new PagedResultDto<Mandatory_Grants_Target_BeneficiaryDtoForView>(totalCount, output);
        }

        public async Task<PagedResultDto<Mandatory_Grants_Scarce_ReasonDtoForView>> Scarce_Reasons()
        {
            var tasks = _scarceReason.GetAll();

            var query = (from o in tasks
                         select new Mandatory_Grants_Scarce_ReasonDtoForView()
                         {
                             Mandatory_Grants_Scarce_Reason = new Mandatory_Grants_Scarce_ReasonDto
                             {
                                 Scarce_Reason = o.Scarce_Reason,
                                 Id = o.Id
                             }
                         });

            var AreasDtos = await query.ToListAsync();
            var totalCount = await query.CountAsync();


            return new PagedResultDto<Mandatory_Grants_Scarce_ReasonDtoForView>(totalCount, AreasDtos);
        }

        public async Task<PagedResultDto<Mandatory_Grant_Qualification_TypeDtoForView>> Qualification_Types()
        {
            var tasks = _qualType.GetAll();

            var query = (from o in tasks
                         select new Mandatory_Grant_Qualification_TypeDtoForView()
                         {
                             Mandatory_Grant_Qualification_Type = new Mandatory_Grant_Qualification_TypeDto
                             {
                                 Qualification_Type = o.Qualification_Type,
                                 Id = o.Id
                             }
                         });

            var output = await query.ToListAsync();
            var totalCount = await query.CountAsync();

            return new PagedResultDto<Mandatory_Grant_Qualification_TypeDtoForView>(totalCount, output);
        }

        public async Task<PagedResultDto<Mandatoty_Grants_ImpactDtoForView>> Grants_Impacts()
        {
            var tasks = _grantImpact.GetAll();

            var query = (from o in tasks
                         select new Mandatoty_Grants_ImpactDtoForView()
                         {
                             Mandatoty_Grants_Impact = new Mandatoty_Grants_ImpactDto
                             {
                                 Impact = o.Impact,
                                 Id = o.Id
                             }
                         });

            var output = await query.ToListAsync();
            var totalCount = await query.CountAsync();


            return new PagedResultDto<Mandatoty_Grants_ImpactDtoForView>(totalCount, output);
        }

        public async Task<PagedResultDto<Mandatory_Grant_Achievement_StatusDtoForView>> Achievement_Status()
        {
            var tasks = _achievementStatus.GetAll();

            var query = (from o in tasks
                         select new Mandatory_Grant_Achievement_StatusDtoForView()
                         {
                             Mandatory_Grant_Achievement_Status = new Mandatory_Grant_Achievement_StatusDto
                             {
                                 Achievement_Status = o.Achievement_Status,
                                 Id = o.Id
                             }
                         });

            var output = await query.ToListAsync();
            var totalCount = await query.CountAsync();


            return new PagedResultDto<Mandatory_Grant_Achievement_StatusDtoForView>(totalCount, output);
        }

        public async Task<PagedResultDto<Learning_ProgrammeDtoForView>> Learning_Programmes()
        {
            var tasks = _learningProg.GetAll();

            var query = (from o in tasks
                         select new Learning_ProgrammeDtoForView()
                         {
                             Learning_Programme = new Learning_ProgrammeDto
                             {
                                 Learning_Programmes = o.Learning_Programmes,
                                 Id = o.Id
                             }
                         });

            var output = await query.ToListAsync();
            var totalCount = await query.CountAsync();


            return new PagedResultDto<Learning_ProgrammeDtoForView>(totalCount, output);
        }

        public async Task<PagedResultDto<Occupation_LevelDtoForView>> Occupation_Levels()
        {
            var tasks = _occLevel.GetAll();

            var query = (from o in tasks
                         select new Occupation_LevelDtoForView()
                         {
                             Occupation_Level = new Occupation_LevelDto
                             {
                                 Occupational_Levels = o.Occupational_Levels,
                                 Id = o.Id
                             }
                         });

            var output = await query.ToListAsync();
            var totalCount = await query.CountAsync();


            return new PagedResultDto<Occupation_LevelDtoForView>(totalCount, output);
        }

        public async Task<PagedResultDto<OFO_SpecializationDtoForView>> Specializations(string code)
        {
            var tasks = _specializations.GetAll().Where(e=>e.OFO_Code == code);

            var query = (from o in tasks
                         select new OFO_SpecializationDtoForView()
                         {
                             Specializations = new OFO_SpecializationDto
                             {
                                 OFO_Code = o.OFO_Code,
                                 Specilization = o.Specilization
                             }
                         });

            var output = await query.ToListAsync();
            var totalCount = await query.CountAsync();


            return new PagedResultDto<OFO_SpecializationDtoForView>(totalCount, output);
        }

        public async Task<PagedResultDto<Mandatory_Extension_StatusDtoView>> Mandatory_Extension_Status()
        {
            var tasks = _extensions.GetAll();

            var query = (from o in tasks
                         select new Mandatory_Extension_StatusDtoView()
                         {
                             Extensions = new Mandatory_Extension_StatusDto
                             {
                                 Description = o.StatusDescription,
                                 Id = o.Id
                             }
                         });

            var output = await query.ToListAsync();
            var totalCount = await query.CountAsync();


            return new PagedResultDto<Mandatory_Extension_StatusDtoView>(totalCount, output);
        }

        public async Task<PagedResultDto<DGProgrammeDeliverablesList>> GetDGProgrammeDeliverables(int focusareaid, int subcategoryid, string tranchetype, string source, int projecttypeid)
        {
            var tasks = _dgprogdel.GetAll()
                .Where(a => a.AppliesTo == source && a.TrancheTypeId == tranchetype && a.ProjectTypeId == projecttypeid && a.FocusAreaId == focusareaid);

            if (tasks.Count() == 0) {
                tasks = _dgprogdel.GetAll()
                    .Where(a => a.AppliesTo == source && a.TrancheTypeId == tranchetype && a.ProjectTypeId == projecttypeid && a.SubCategoryId == subcategoryid);
            } 

            var query = (from o in tasks
                         select new DGProgrammeDeliverablesList()
                         {
                             Deliverables = new ProgrammeDeliverablesDto
                             {
                                 DeliverableId = o.DeliverableId,
                                 ProjectTypeId= o.ProjectTypeId,
                                 TrancheTypeId= o.TrancheTypeId,
                                 Deliverable = o.Deliverable,
                                 DocumentType = o.DocumentType,
                                 AppliesTo = o.AppliesTo,
                                 TranchePercent = o.TranchePercent
                             }
                         });

            query = query.Distinct();
            var totalCount = query.Count();
            return new PagedResultDto<DGProgrammeDeliverablesList>(totalCount, query.ToList());
        }

        public async Task<PagedResultDto<SICCodesView>> GetSicResults(string search)
        {
            var sics = _sic.GetAll()
                .Where(a=> a.SIC_Code != null && (a.Description.Contains(search) || a.SIC_Code.ToString().Contains(search)));

            var query = (from o in sics
                         select new SICCodesView()
                         {
                             SICCodes = new SICCodesDto
                             {
                                 SIC_Code = o.SIC_Code,
                                 Description = o.Description
                             }
                         });

            var siccodes = query.ToList();
            var totalCount = await query.CountAsync();

            return new PagedResultDto<SICCodesView>(totalCount, siccodes);
        }
    }
}
