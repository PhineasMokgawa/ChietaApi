using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using CHIETAMIS.Organisations.Dtos;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Application.Services.Dto;
using CHIETAMIS.Sdfs;
using CHIETAMIS.DiscretionaryProjects;
using CHIETAMIS.Counters;
using CHIETAMIS.Counters.Dtos;
using CHIETAMIS.MandatoryGrants.Dtos;
using CHIETAMIS.Lookups;

namespace CHIETAMIS.Organisations
{
    public class OrganisationAppService : CHIETAMISAppServiceBase
    {
        private readonly IRepository<Organisation> _organisationRepository;
        private readonly IRepository<Organisation_Sdf> _orgSdfRepository;
        private readonly IRepository<DiscretionaryProject> _discretionaryProjectsRepository;
        private readonly IRepository<OrganisationPhysicalAddress> _physicalAddressRepository;
        private readonly IRepository<OrganisationPostalAddress> _postalAddressRepository;
        private readonly IRepository<BankDetails> _bankDetailsRepository;
        private readonly IRepository<Bank> _bankRepository;
        private readonly IRepository<Bank_Account_Type> _accTypeRepository;
        private readonly IRepository<Counter> _counterRepository;
        private readonly IRepository<SETA> _setaRepository;
        public OrganisationAppService(IRepository<Organisation> organisationRepository,
                                      IRepository<Organisation_Sdf> orgSdfRepository,
                                      IRepository<DiscretionaryProject> discretionaryProjectsRepository,
                                      IRepository<OrganisationPhysicalAddress> physicalAddressRepository,
                                      IRepository<OrganisationPostalAddress> postAddressRepository,
                                      IRepository<BankDetails> bankDetailsRepository,
                                      IRepository<Counter> counterRepository,
                                      IRepository<Bank> bankRepository,
                                      IRepository<Bank_Account_Type> accTypeRepository,
                                      IRepository<SETA> setaRepository)
        {
            _organisationRepository = organisationRepository;
            _orgSdfRepository = orgSdfRepository;
            _discretionaryProjectsRepository = discretionaryProjectsRepository;
            _physicalAddressRepository = physicalAddressRepository;
            _postalAddressRepository = postAddressRepository;
            _bankDetailsRepository = bankDetailsRepository;
            _counterRepository = counterRepository;
            _bankRepository= bankRepository;
            _accTypeRepository= accTypeRepository;
            _setaRepository= setaRepository;
        }

        public async Task<PagedResultDto<GetOrganisationsForViewDto>> getAll(int first, int rows)
        {
            var orgs = (from o in _organisationRepository.GetAll()
                        join s in _setaRepository.GetAll() on o.SETA_Id equals s.SETA_Id into so
                        from sto in so.DefaultIfEmpty()
                        select new
                        {
                            SDL_No = o.SDL_No,
                            Organisation_Registration_Number = o.Organisation_Registration_Number,
                            Organisation_Name = o.Organisation_Name,
                            Organisation_Trading_Name = o.Organisation_Trading_Name,
                            SETA_Id = o.SETA_Id,
                            SETA = sto.Description,
                            SIC_Code = o.SIC_Code,
                            Organisation_Fax_Number = o.Organisation_Fax_Number,
                            Organisation_Contact_Name = o.Organisation_Contact_Name,
                            Organisation_Contact_Email_Address = o.Organisation_Contact_Email_Address,
                            Organisation_Contact_Phone_Number = o.Organisation_Contact_Phone_Number,
                            Organisation_Contact_Cell_Number = o.Organisation_Contact_Cell_Number,
                            COMPANY_SIZE = o.COMPANY_SIZE,
                            NUMBER_OF_EMPLOYEES = o.NUMBER_OF_EMPLOYEES,
                            TYPE_OF_ENTITY = o.TYPE_OF_ENTITY,
                            CORE_BUSINESS = o.CORE_BUSINESS,
                            PARENT_SDL_NUMBER = o.PARENT_SDL_NUMBER,
                            BBBEE_Status = o.BBBEE_Status,
                            BBBEE_LEVEL = o.BBBEE_LEVEL,
                            DATEBUSINESSCOMMENCED = o.DATEBUSINESSCOMMENCED,
                            STATUS = o.STATUS,
                            EXMPTIONCODE = o.EXMPTIONCODE,
                            CHAMBER = o.CHAMBER,
                            CEO_Name = o.CEO_Name,
                            CEO_Surname = o.CEO_Surname,
                            CEO_Email = o.CEO_Email,
                            CEO_GenderId = o.CEO_GenderId,
                            CEO_RaceId = o.CEO_RaceId,
                            Senior_Rep_Name = o.Senior_Rep_Name,
                            Senior_Rep_Surname = o.Senior_Rep_Surname,
                            Senior_Rep_Email = o.Senior_Rep_Email,
                            Senior_Rep_GenderId = o.Senior_Rep_GenderId,
                            Senior_Rep_RaceId = o.Senior_Rep_RaceId,
                            Id = o.Id
                        })
            .Distinct()
            .OrderByDescending(a => a.Id)
            .Skip(first)
            .Take(rows);

            var orgDetails = from o in orgs
                             select new GetOrganisationsForViewDto()
                             {
                                 Organisation = new OrganisationDto
                                 {
                                     SDL_No = o.SDL_No,
                                     SETA_Id = o.SETA_Id,
                                     SETA = o.SETA,
                                     SIC_Code = o.SIC_Code,
                                     Organisation_Registration_Number = o.Organisation_Registration_Number,
                                     Organisation_Name = o.Organisation_Name,
                                     Organisation_Trading_Name = o.Organisation_Trading_Name,
                                     Organisation_Fax_Number = o.Organisation_Fax_Number,
                                     Organisation_Contact_Name = o.Organisation_Contact_Name,
                                     Organisation_Contact_Email_Address = o.Organisation_Contact_Email_Address,
                                     Organisation_Contact_Phone_Number = o.Organisation_Contact_Phone_Number,
                                     Organisation_Contact_Cell_Number = o.Organisation_Contact_Cell_Number,
                                     COMPANY_SIZE = o.COMPANY_SIZE,
                                     NUMBER_OF_EMPLOYEES = o.NUMBER_OF_EMPLOYEES,
                                     TYPE_OF_ENTITY = o.TYPE_OF_ENTITY,
                                     CORE_BUSINESS = o.CORE_BUSINESS,
                                     PARENT_SDL_NUMBER = o.PARENT_SDL_NUMBER,
                                     BBBEE_Status = o.BBBEE_Status,
                                     BBBEE_LEVEL = o.BBBEE_LEVEL,
                                     DATEBUSINESSCOMMENCED = o.DATEBUSINESSCOMMENCED,
                                     STATUS = o.STATUS,
                                     EXMPTIONCODE = o.EXMPTIONCODE,
                                     CHAMBER = o.CHAMBER,
                                     CEO_Name = o.CEO_Name,
                                     CEO_Surname = o.CEO_Surname,
                                     CEO_Email = o.CEO_Email,
                                     CEO_GenderId = o.CEO_GenderId,
                                     CEO_RaceId = o.CEO_RaceId,
                                     Senior_Rep_Name = o.Senior_Rep_Name,
                                     Senior_Rep_Surname = o.Senior_Rep_Surname,
                                     Senior_Rep_Email = o.Senior_Rep_Email,
                                     Senior_Rep_GenderId = o.Senior_Rep_GenderId,
                                     Senior_Rep_RaceId = o.Senior_Rep_RaceId,
                                     Id = o.Id
                                 }
                             };

            var totalCount = _organisationRepository.GetAll().Count();

            return new PagedResultDto<GetOrganisationsForViewDto>(
                totalCount,
                await orgDetails.ToListAsync()
            );
        }

        public async Task<PagedResultDto<GetOrganisationsForViewDto>> getAllLazy(int first, int rows, string SDLNoFilter, string SDLNoFilterMode, 
            string OrganisationNameFilter, string OrganisationNameFilterMode, string TradingNameFilter, string TradingNameFilterMode, string RegistrationNumberFilter, 
            string RegistrationNumberFilterMode, string SetaFilter, string SetaFilterMode, string SicFilter, string SicFilterMode, string StatusFilter, string StatusFilterMode)
        {
            var orgs =  (from o in _organisationRepository.GetAll()
                              join s in _setaRepository.GetAll() on o.SETA_Id equals s.SETA_Id into so
                              from sto in so.DefaultIfEmpty()
                              select new
                              {
                                  SDL_No = o.SDL_No,
                                  Organisation_Registration_Number = o.Organisation_Registration_Number,
                                  Organisation_Name = o.Organisation_Name,
                                  Organisation_Trading_Name = o.Organisation_Trading_Name,
                                  SETA_Id = o.SETA_Id,
                                  SETA = sto.Abrev,
                                  SIC_Code = o.SIC_Code,
                                  Organisation_Fax_Number = o.Organisation_Fax_Number,
                                  Organisation_Contact_Name = o.Organisation_Contact_Name,
                                  Organisation_Contact_Email_Address = o.Organisation_Contact_Email_Address,
                                  Organisation_Contact_Phone_Number = o.Organisation_Contact_Phone_Number,
                                  Organisation_Contact_Cell_Number = o.Organisation_Contact_Cell_Number,
                                  COMPANY_SIZE = o.COMPANY_SIZE,
                                  NUMBER_OF_EMPLOYEES = o.NUMBER_OF_EMPLOYEES,
                                  TYPE_OF_ENTITY = o.TYPE_OF_ENTITY,
                                  CORE_BUSINESS = o.CORE_BUSINESS,
                                  PARENT_SDL_NUMBER = o.PARENT_SDL_NUMBER,
                                  BBBEE_Status = o.BBBEE_Status,
                                  BBBEE_LEVEL = o.BBBEE_LEVEL,
                                  DATEBUSINESSCOMMENCED = o.DATEBUSINESSCOMMENCED,
                                  STATUS = o.STATUS,
                                  EXMPTIONCODE = o.EXMPTIONCODE,
                                  CHAMBER = o.CHAMBER,
                                  CEO_Name = o.CEO_Name,
                                  CEO_Surname = o.CEO_Surname,
                                  CEO_Email = o.CEO_Email,
                                  CEO_GenderId = o.CEO_GenderId,
                                  CEO_RaceId = o.CEO_RaceId,
                                  Senior_Rep_Name = o.Senior_Rep_Name,
                                  Senior_Rep_Surname = o.Senior_Rep_Surname,
                                  Senior_Rep_Email = o.Senior_Rep_Email,
                                  Senior_Rep_GenderId = o.Senior_Rep_GenderId,
                                  Senior_Rep_RaceId = o.Senior_Rep_RaceId,
                                  Id = o.Id
                              });

            if (SDLNoFilter != null)
            {
                if (SDLNoFilterMode == "startsWith")
                {
                    orgs = orgs.Where(a => a.SDL_No.StartsWith(SDLNoFilter));
                }
                if (SDLNoFilterMode == "endsWith")
                {
                    orgs = orgs.Where(a => a.SDL_No.EndsWith(SDLNoFilter));
                }
                if (SDLNoFilterMode == "contains")
                {
                    orgs = orgs.Where(a => a.SDL_No.Contains(SDLNoFilter));
                }
                if (SDLNoFilterMode == "notContains")
                {
                    orgs = orgs.Where(a => !(a.SDL_No.Contains(SDLNoFilter)));
                }
                if (SDLNoFilterMode == "equals")
                {
                    orgs = orgs.Where(a => a.SDL_No == SDLNoFilter);
                }
            }

            if (OrganisationNameFilter != null)
            {
                if (OrganisationNameFilterMode == "startsWith")
                {
                    orgs = orgs.Where(a => a.Organisation_Name.StartsWith(OrganisationNameFilter));
                }
                if (OrganisationNameFilterMode == "endsWith")
                {
                    orgs = orgs.Where(a => a.Organisation_Name.EndsWith(OrganisationNameFilter));
                }
                if (OrganisationNameFilterMode == "contains")
                {
                    orgs = orgs.Where(a => a.Organisation_Name.Contains(OrganisationNameFilter));
                }
                if (OrganisationNameFilterMode == "notContains")
                {
                    orgs = orgs.Where(a => !(a.Organisation_Name.Contains(OrganisationNameFilter)));
                }
                if (OrganisationNameFilterMode == "equals")
                {
                    orgs = orgs.Where(a => a.Organisation_Name == OrganisationNameFilter);
                }
            }

            if (TradingNameFilter != null)
            {
                if (TradingNameFilterMode == "startsWith")
                {
                    orgs = orgs.Where(a => a.Organisation_Trading_Name.StartsWith(TradingNameFilter));
                }
                if (TradingNameFilterMode == "endsWith")
                {
                    orgs = orgs.Where(a => a.Organisation_Trading_Name.EndsWith(TradingNameFilter));
                }
                if (TradingNameFilterMode == "contains")
                {
                    orgs = orgs.Where(a => a.Organisation_Trading_Name.Contains(TradingNameFilter));
                }
                if (TradingNameFilterMode == "notContains")
                {
                    orgs = orgs.Where(a => !(a.Organisation_Trading_Name.Contains(TradingNameFilter)));
                }
                if (TradingNameFilterMode == "equals")
                {
                    orgs = orgs.Where(a => a.Organisation_Trading_Name == TradingNameFilter);
                }
            }

            if (RegistrationNumberFilter != null)
            {
                if (RegistrationNumberFilterMode == "startsWith")
                {
                    orgs = orgs.Where(a => a.Organisation_Registration_Number.StartsWith(RegistrationNumberFilter));
                }
                if (RegistrationNumberFilterMode == "endsWith")
                {
                    orgs = orgs.Where(a => a.Organisation_Registration_Number.EndsWith(RegistrationNumberFilter));
                }
                if (RegistrationNumberFilterMode == "contains")
                {
                    orgs = orgs.Where(a => a.Organisation_Registration_Number.Contains(RegistrationNumberFilter));
                }
                if (RegistrationNumberFilterMode == "notContains")
                {
                    orgs = orgs.Where(a => !(a.Organisation_Registration_Number.Contains(RegistrationNumberFilter)));
                }
                if (RegistrationNumberFilterMode == "equals")
                {
                    orgs = orgs.Where(a => a.Organisation_Registration_Number == RegistrationNumberFilter);
                }
            }

            if (SetaFilter != null)
            {
                if (SetaFilterMode == "startsWith")
                {
                    orgs = orgs.Where(a => a.SETA.StartsWith(SetaFilter));
                }
                if (SetaFilterMode == "endsWith")
                {
                    orgs = orgs.Where(a => a.SETA.EndsWith(SetaFilter));
                }
                if (SetaFilterMode == "contains")
                {
                    orgs = orgs.Where(a => a.SETA.Contains(SetaFilter));
                }
                if (SetaFilterMode == "notContains")
                {
                    orgs = orgs.Where(a => !(a.SETA.Contains(SetaFilter)));
                }
                if (SetaFilterMode == "equals")
                {
                    orgs = orgs.Where(a => a.SETA == SetaFilter);
                }
            }

            if (SicFilter != null)
            {
                if (SicFilterMode == "startsWith")
                {
                    orgs = orgs.Where(a => a.SIC_Code.StartsWith(SicFilter));
                }
                if (SicFilterMode == "endsWith")
                {
                    orgs = orgs.Where(a => a.SIC_Code.EndsWith(SicFilter));
                }
                if (SicFilterMode == "contains")
                {
                    orgs = orgs.Where(a => a.SIC_Code.Contains(SicFilter));
                }
                if (SicFilterMode == "notContains")
                {
                    orgs = orgs.Where(a => !(a.SIC_Code.Contains(SicFilter)));
                }
                if (SicFilterMode == "equals")
                {
                    orgs = orgs.Where(a => a.SIC_Code == SicFilter);
                }
            }

            if (StatusFilter != null)
            {
                if (StatusFilterMode == "startsWith")
                {
                    orgs = orgs.Where(a => a.STATUS.StartsWith(StatusFilter));
                }
                if (StatusFilterMode == "endsWith")
                {
                    orgs = orgs.Where(a => a.STATUS.EndsWith(StatusFilter));
                }
                if (StatusFilterMode == "contains")
                {
                    orgs = orgs.Where(a => a.STATUS.Contains(StatusFilter));
                }
                if (StatusFilterMode == "notContains")
                {
                    orgs = orgs.Where(a => !(a.STATUS.Contains(StatusFilter)));
                }
                if (StatusFilterMode == "equals")
                {
                    orgs = orgs.Where(a => a.STATUS == StatusFilter);
                }
            }


            var totalCount = orgs.Count();

            orgs = orgs
            .Distinct()
            .OrderByDescending(a => a.Id)
            .Skip(first)
            .Take(rows);

            var orgDetails = from o in orgs
                select new GetOrganisationsForViewDto()
                {
                    Organisation = new OrganisationDto
                    {
                        SDL_No = o.SDL_No,
                        SETA_Id = o.SETA_Id,
                        SETA = o.SETA,
                        SIC_Code = o.SIC_Code,
                        Organisation_Registration_Number = o.Organisation_Registration_Number,
                        Organisation_Name = o.Organisation_Name,
                        Organisation_Trading_Name = o.Organisation_Trading_Name,
                        Organisation_Fax_Number = o.Organisation_Fax_Number,
                        Organisation_Contact_Name = o.Organisation_Contact_Name,
                        Organisation_Contact_Email_Address = o.Organisation_Contact_Email_Address,
                        Organisation_Contact_Phone_Number = o.Organisation_Contact_Phone_Number,
                        Organisation_Contact_Cell_Number = o.Organisation_Contact_Cell_Number,
                        COMPANY_SIZE = o.COMPANY_SIZE,
                        NUMBER_OF_EMPLOYEES = o.NUMBER_OF_EMPLOYEES,
                        TYPE_OF_ENTITY = o.TYPE_OF_ENTITY,
                        CORE_BUSINESS = o.CORE_BUSINESS,
                        PARENT_SDL_NUMBER = o.PARENT_SDL_NUMBER,
                        BBBEE_Status = o.BBBEE_Status,
                        BBBEE_LEVEL = o.BBBEE_LEVEL,
                        DATEBUSINESSCOMMENCED = o.DATEBUSINESSCOMMENCED,
                        STATUS = o.STATUS,
                        EXMPTIONCODE = o.EXMPTIONCODE,
                        CHAMBER = o.CHAMBER,
                        CEO_Name = o.CEO_Name,
                        CEO_Surname = o.CEO_Surname,
                        CEO_Email = o.CEO_Email,
                        CEO_GenderId = o.CEO_GenderId,
                        CEO_RaceId = o.CEO_RaceId,
                        Senior_Rep_Name = o.Senior_Rep_Name,
                        Senior_Rep_Surname = o.Senior_Rep_Surname,
                        Senior_Rep_Email = o.Senior_Rep_Email,
                        Senior_Rep_GenderId = o.Senior_Rep_GenderId,
                        Senior_Rep_RaceId = o.Senior_Rep_RaceId,
                        Id = o.Id
                    }
                };

            return new PagedResultDto<GetOrganisationsForViewDto>(
                totalCount,
                orgDetails.ToList()
            );
        }

        public async Task<PagedResultDto<GetOrganisationsForViewDto>> GetNew(PagedOrganisationResultRequestDto input)
        {
            var orgs = _organisationRepository.GetAll()
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.SDL_No == input.Keyword)
                .OrderByDescending(x => x.Id)
                .Skip(0)
                .Take(1);


            var orgDetails = from o in orgs
                select new GetOrganisationsForViewDto()
                {
                    Organisation = new OrganisationDto
                    {
                        SDL_No = o.SDL_No,
                        SETA_Id = o.SETA_Id,
                        SIC_Code = o.SIC_Code,
                        Organisation_Registration_Number = o.Organisation_Registration_Number,
                        Organisation_Name = o.Organisation_Name,
                        Organisation_Trading_Name = o.Organisation_Trading_Name,
                        Organisation_Fax_Number = o.Organisation_Fax_Number,
                        Organisation_Contact_Name = o.Organisation_Contact_Name,
                        Organisation_Contact_Email_Address = o.Organisation_Contact_Email_Address,
                        Organisation_Contact_Phone_Number = o.Organisation_Contact_Phone_Number,
                        Organisation_Contact_Cell_Number = o.Organisation_Contact_Cell_Number,
                        COMPANY_SIZE = o.COMPANY_SIZE,
                        NUMBER_OF_EMPLOYEES = o.NUMBER_OF_EMPLOYEES,
                        TYPE_OF_ENTITY = o.TYPE_OF_ENTITY,
                        CORE_BUSINESS = o.CORE_BUSINESS,
                        PARENT_SDL_NUMBER = o.PARENT_SDL_NUMBER,
                        BBBEE_Status = o.BBBEE_Status,
                        BBBEE_LEVEL = o.BBBEE_LEVEL,
                        DATEBUSINESSCOMMENCED = o.DATEBUSINESSCOMMENCED,
                        STATUS = o.STATUS,
                        EXMPTIONCODE = o.EXMPTIONCODE,
                        CHAMBER = o.CHAMBER,
                        CEO_Name = o.CEO_Name,
                        CEO_Surname = o.CEO_Surname,
                        CEO_Email = o.CEO_Email,
                        CEO_GenderId = o.CEO_GenderId,
                        CEO_RaceId = o.CEO_RaceId,
                        Senior_Rep_Name = o.Senior_Rep_Name,
                        Senior_Rep_Surname = o.Senior_Rep_Surname,
                        Senior_Rep_Email = o.Senior_Rep_Email,
                        Senior_Rep_GenderId = o.Senior_Rep_GenderId,
                        Senior_Rep_RaceId = o.Senior_Rep_RaceId,
                        Id = o.Id
                    }
            };

            var totalCount = await orgs.CountAsync();

            return new PagedResultDto<GetOrganisationsForViewDto>(
                totalCount,
                await orgDetails.ToListAsync()
            );
        }

        public async Task<PagedResultDto<GetOrganisationsForViewDto>> GetSdfLinked(int sdfId)
        {
            var orgs = await (from org in _organisationRepository.GetAll()
                              join sdfl in _orgSdfRepository.GetAll() on org.Id equals sdfl.OrganisationId into linkedSdfs from sdf in linkedSdfs.DefaultIfEmpty()
                              select new
                              {
                                  Organisation = org,
                                  SdfId = sdf.SdfId,
                                  StatusId = sdf.StatusId,
                                  OrgSdfId = sdf.Id
                              })
                    .Where(a => a.SdfId == sdfId && a.StatusId != 5)
                    .ToListAsync();

            //var result = new List<OrganisationDto>();

            //foreach (var org in orgs)
            //{
            //    var resultOrg = ObjectMapper.Map<OrganisationDto>(org.Organisation);
            //    result.Add(resultOrg);
            //}

            var orgDetails = from o in orgs
                             select new GetOrganisationsForViewDto()
                             {
                                 Organisation = new OrganisationDto
                                 {
                                     SDL_No = o.Organisation.SDL_No,
                                     SETA_Id = o.Organisation.SETA_Id,
                                     SIC_Code = o.Organisation.SIC_Code,
                                     Organisation_Registration_Number = o.Organisation.Organisation_Registration_Number,
                                     Organisation_Name = o.Organisation.Organisation_Name,
                                     Organisation_Trading_Name = o.Organisation.Organisation_Trading_Name,
                                     Organisation_Fax_Number = o.Organisation.Organisation_Fax_Number,
                                     Organisation_Contact_Name = o.Organisation.Organisation_Contact_Name,
                                     Organisation_Contact_Email_Address = o.Organisation.Organisation_Contact_Email_Address,
                                     Organisation_Contact_Phone_Number = o.Organisation.Organisation_Contact_Phone_Number,
                                     Organisation_Contact_Cell_Number = o.Organisation.Organisation_Contact_Cell_Number,
                                     COMPANY_SIZE = o.Organisation.COMPANY_SIZE,
                                     NUMBER_OF_EMPLOYEES = o.Organisation.NUMBER_OF_EMPLOYEES,
                                     TYPE_OF_ENTITY = o.Organisation.TYPE_OF_ENTITY,
                                     CORE_BUSINESS = o.Organisation.CORE_BUSINESS,
                                     PARENT_SDL_NUMBER = o.Organisation.PARENT_SDL_NUMBER,
                                     BBBEE_Status = o.Organisation.BBBEE_Status,
                                     BBBEE_LEVEL = o.Organisation.BBBEE_LEVEL,
                                     DATEBUSINESSCOMMENCED = o.Organisation.DATEBUSINESSCOMMENCED,
                                     STATUS = o.Organisation.STATUS,
                                     EXMPTIONCODE = o.Organisation.EXMPTIONCODE,
                                     CHAMBER = o.Organisation.CHAMBER,
                                     CEO_Name = o.Organisation.CEO_Name,
                                     CEO_Surname = o.Organisation.CEO_Surname,
                                     CEO_Email = o.Organisation.CEO_Email,
                                     CEO_GenderId = o.Organisation.CEO_GenderId,
                                     CEO_RaceId = o.Organisation.CEO_RaceId,
                                     Senior_Rep_Name = o.Organisation.Senior_Rep_Name,
                                     Senior_Rep_Surname = o.Organisation.Senior_Rep_Surname,
                                     Senior_Rep_Email = o.Organisation.Senior_Rep_Email,
                                     Senior_Rep_GenderId = o.Organisation.Senior_Rep_GenderId,
                                     Senior_Rep_RaceId = o.Organisation.Senior_Rep_RaceId,
                                     Id = o.Organisation.Id
                                 },
                                 OrgSdfId = o.OrgSdfId,
                                 StatusId = o.StatusId,
                             };

            var totalCount = orgs.Count();

            return new PagedResultDto<GetOrganisationsForViewDto>(
                totalCount,
                orgDetails.ToList()
            );
        }

        public async Task<GetOrganisationsForViewDto> Get(int id)
        {
            var orgDetails = await _organisationRepository.GetAsync(id);

            var output = new GetOrganisationsForViewDto { Organisation = ObjectMapper.Map<OrganisationDto>(orgDetails) };

            return output;
        }

        public async Task<GetOrganisationsForViewDto> GetByProject(int id)
        {
            var proj = _discretionaryProjectsRepository.Get(id);

            var orgDetails = await _organisationRepository.GetAsync(proj.OrganisationId);

            var output = new GetOrganisationsForViewDto { Organisation = ObjectMapper.Map<OrganisationDto>(orgDetails) };

            return output;
        }

        public async Task<GetOrganisationsForViewDto> GetBySDL(string sdl)
        {
            var orgDetails = _organisationRepository.GetAll().Where(a=>a.SDL_No == sdl).FirstOrDefault();

            var output = new GetOrganisationsForViewDto { Organisation = ObjectMapper.Map<OrganisationDto>(orgDetails) };

            return output;
        }

        public async Task Delete(EntityDto<int> input)
        {
            await _organisationRepository.DeleteAsync(input.Id);
        }

        public async Task DeLinkSDF(int id, int userid)
        {
            var orgsdf = await _orgSdfRepository.GetAsync(id);
            orgsdf.StatusId = 5;
            orgsdf.UserId = userid;
            await _orgSdfRepository.UpdateAsync(orgsdf);
        }

        public async Task<OrganisationForView> CreateUpdate(CreateOrganisationDto input)
        {
            if (input.Id > 0)
            {
                var cOrg = _organisationRepository.Get(input.Id);

                var output = new OrganisationForView { Organisation = ObjectMapper.Map<OrganisationDto>(cOrg) };
                return output;
            }
            else
            {
                var cOrg = ObjectMapper.Map<Organisation>(input);

                var cnt = _counterRepository.GetAll().FirstOrDefault();
                var sdlval = cnt.N_Last_Number + 1;
                cOrg.SDL_No = "N03" + sdlval.ToString("D7");
                cOrg.DATEBUSINESSCOMMENCED = DateTime.Now;
                cOrg.STATUS = "Active";
                var org = await _organisationRepository.InsertAsync(cOrg);

                cnt.N_Last_Number = sdlval + 1;
                await _counterRepository.UpdateAsync(cnt);

                var output = new OrganisationForView { Organisation = ObjectMapper.Map<OrganisationDto>(org) };

                return output;
            }
        }

        public async Task Update(CreateOrganisationDto input)
        {
            var orgDetails = await _organisationRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, orgDetails);
        }

        public async Task SavePhysicalAddress(OrganisationPhysicalAddressDto input)
        {
            var cPhysicalAddress = _physicalAddressRepository.GetAll().Where(a => a.organisationId == input.organisationId);
            if (cPhysicalAddress.Count() == 0)
            {
                var cPhysAddress = ObjectMapper.Map<OrganisationPhysicalAddress>(input);
                await _physicalAddressRepository.InsertAsync(cPhysAddress);
            }
            else
            {
                var cPhyslAddress = await _physicalAddressRepository.FirstOrDefaultAsync(cPhysicalAddress.First().Id);
                cPhyslAddress.addressline1 = input.addressline1;
                cPhyslAddress.addressline2 = input.addressline2;
                cPhyslAddress.area = input.area;
                cPhyslAddress.district = input.district;
                cPhyslAddress.municipality = input.municipality;
                cPhyslAddress.postcode = input.postcode;
                cPhyslAddress.province = input.province;
                cPhyslAddress.suburb = input.suburb;
                await _physicalAddressRepository.UpdateAsync(cPhyslAddress);
            }
        }

        public async Task SavePostalAddress(OrganisationPostalAddressDto input)
        {
            var cPostalAddress = _postalAddressRepository.GetAll().Where(a => a.organisationId == input.organisationId);
            if (cPostalAddress.Count() == 0)
            {
                var cPosAddress = ObjectMapper.Map<OrganisationPostalAddress>(input);
                await _postalAddressRepository.InsertAsync(cPosAddress);
            }
            else
            {
                var cPoslAddress = await _postalAddressRepository.FirstOrDefaultAsync(cPostalAddress.First().Id);
                cPoslAddress.addressline1 = input.addressline1;
                cPoslAddress.addressline2 = input.addressline2;
                cPoslAddress.area = input.area;
                cPoslAddress.district = input.district;
                cPoslAddress.municipality = input.municipality;
                cPoslAddress.postcode = input.postcode;
                cPoslAddress.province = input.province;
                cPoslAddress.suburb = input.suburb;
                await _postalAddressRepository.UpdateAsync(cPoslAddress);
            }
        }

        public async Task ChangePostalIndicator(int Id, int userId, bool indicator)
        {
            var cPostalAddress = _postalAddressRepository.GetAll().Where(a => a.organisationId == Id);
            if (cPostalAddress.Count() == 0 && indicator == true)
            {
                var postAddr = new OrganisationPostalAddress();
                postAddr.organisationId = Id;
                postAddr.userId = userId;
                postAddr.sameasphysical = indicator;
                await _postalAddressRepository.InsertAsync(postAddr);
            } else {
                if (indicator == true)
                {
                    var cPoslAddress = await _postalAddressRepository.FirstOrDefaultAsync(cPostalAddress.First().Id);
                    cPoslAddress.sameasphysical = indicator;
                    cPoslAddress.addressline1 = null;
                    cPoslAddress.addressline2 = null;
                    cPoslAddress.area = null;
                    cPoslAddress.district = null;
                    cPoslAddress.municipality = null;
                    cPoslAddress.postcode = null;
                    cPoslAddress.province = null;
                    cPoslAddress.suburb = null;

                    await _postalAddressRepository.UpdateAsync(cPoslAddress);
                }
                else
                {
                    var cPoslAddress = await _postalAddressRepository.FirstOrDefaultAsync(cPostalAddress.First().Id);
                    cPoslAddress.sameasphysical = indicator;
                    await _postalAddressRepository.UpdateAsync(cPoslAddress);
                }
            }
        }

        public async Task<OrganisationPhysicalAddressForViewDto> GetOrganisationPhysAddress(int organisationId)
        {
            var address = _physicalAddressRepository.GetAll().Where(a => a.organisationId == organisationId).FirstOrDefault();

            var output = new OrganisationPhysicalAddressForViewDto { OrganisationPhysicalAddress = ObjectMapper.Map<OrganisationPhysicalAddressDto>(address) };

            return output;
        }

        public async Task<OrganisationPostalAddressForViewDto> GetOrganisationPostAddress(int organisationId)
        {
            var address = _postalAddressRepository.GetAll().Where(a => a.organisationId == organisationId).FirstOrDefault();

            var output = new OrganisationPostalAddressForViewDto { OrganisationPostalAddress = ObjectMapper.Map<OrganisationPostalAddressDto>(address) };

            return output;
            
        }

        public async Task<PagedResultDto<GetOrganisationsForViewDto>> GetChildCompanies(string orgsdl)
        {

            var filteredorgs = _organisationRepository.GetAll()
                        .Where(a => a.PARENT_SDL_NUMBER == orgsdl);

            var pagedAndFilteredorgs = filteredorgs;

            var cOrgs = from o in pagedAndFilteredorgs
            select new GetOrganisationsForViewDto()
                {
                    Organisation = new OrganisationDto()
                    {
                        SDL_No = o.SDL_No,
                        Id = o.Id
                    }
                };

            var totalCount = pagedAndFilteredorgs.Count();

            return new PagedResultDto<GetOrganisationsForViewDto>(
                totalCount,
                cOrgs.ToList()
            );
        }

        public async Task BankDetailSave(BankDetailsDto input)
        {
            var cbankdetais = _bankDetailsRepository.GetAll().Where(a => a.OrganisationId == input.OrganisationId);
            if (cbankdetais.Count() == 0)
            {
                var vbank = ObjectMapper.Map<BankDetails>(input);
                await _bankDetailsRepository.InsertAsync(vbank);
            }
            else
            {
                var cbank = await _bankDetailsRepository.FirstOrDefaultAsync(cbankdetais.First().Id);
                cbank.Account_Holder = input.Account_Holder;
                cbank.Account_Number = input.Account_Number;
                cbank.Branch_Code = input.Branch_Code;
                cbank.Branch_Name = input.Branch_Name;
                cbank.AccountType = input.AccountType;
                cbank.UserId = input.UserId;
                await _bankDetailsRepository.UpdateAsync(cbank);
            }
        }

        public async Task<BankDetailsForViewDto> GetOrgBank(int Id)
        {
            var bank = (from bd in _bankDetailsRepository.GetAll() 
                    join b in _bankRepository.GetAll() on bd.Bank_Name equals b.Branch_Code
                    join at in _accTypeRepository.GetAll() on bd.AccountType equals at.Id
                    select new {
                        Id = bd.Id,
                        BankDetails = bd,
                        BankName = b.Bank_Name,
                        Account_Type = at.AccountType
                    })
                .Where(a => a.BankDetails.OrganisationId == Id).FirstOrDefault();

            if (bank != null)
            {
                return new BankDetailsForViewDto
                {
                    BankDetails = ObjectMapper.Map<BankDetailsDto>(bank.BankDetails),
                    BankName = bank.BankName,
                    Account_Type = bank.Account_Type
                };
            } else {
                return null;
            };
        }
    }
}
