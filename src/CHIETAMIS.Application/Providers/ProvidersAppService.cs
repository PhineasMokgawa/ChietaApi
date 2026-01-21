using Abp.Domain.Repositories;
using CHIETAMIS.Authorization.Users;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CHIETAMIS.Providers.Dto;
using Abp.Application.Services.Dto;
using CHIETAMIS.Learners.Dto;
using CHIETAMIS.Learners;
using Microsoft.EntityFrameworkCore;
using CHIETAMIS.Qualifications.Dtos;
using CHIETAMIS.Qualifications;
using System;

namespace CHIETAMIS.Providers
{
    public class ProvidersAppService : CHIETAMISAppServiceBase
    {
        private readonly IRepository<ProviderDetails> _provRepository;
        private readonly IRepository<LearnerDetails> _learnerRepository;
        private readonly IUserEmailer _userEmailer;
        private readonly IRepository<Qualification> _qual;
        private readonly IRepository<Discretionary_Universtity_College> _college;

        public ProvidersAppService(IRepository<ProviderDetails> provRepository,
                                   IRepository<LearnerDetails> learnerRepository,
                                   IRepository<Qualification> qual,
                                 IRepository<Discretionary_Universtity_College> college)
        {
            _provRepository = provRepository;
            _learnerRepository = learnerRepository;
            _qual = qual;
            _college = college;
        }

        public async Task<string> CreateEditProvider(ProviderDetailsDto input)
        {
            var output = "";

            var prov = _provRepository.GetAll().Where(a => a.Provider_Accreditation_NO == input.Provider_Accreditation_NO && a.Provider_legal_name == input.Provider_legal_name);

            if (prov.Count() == 0)
            {
                var prv = ObjectMapper.Map<ProviderDetails>(input);

                await _provRepository.InsertAsync(prv);
            }
            else
            {
                var lrn = await _provRepository.GetAsync(prov.FirstOrDefault().Id);
                await _provRepository.UpdateAsync(lrn);
            }

            return output;
        }

        public async Task<PagedResultDto<ProviderDetailsListDto>> GetDGApplicationProviders(int ApplicationId, string TrancheType, string Status)
        {

            var providers = (from lrn in _learnerRepository.GetAll()
                             join prv in _provRepository.GetAll() on lrn.Provider_Legal_Name equals prv.Provider_legal_name
                             select new
                             {
                                 Provider_Trading_name = prv.Provider_Trading_name,
                                 Provider_SDL_NO = prv.Provider_SDL_NO,
                                 Chieta_Accredited = prv.Chieta_Accredited,
                                 Provider_Accreditation_NO = prv.Provider_Accreditation_NO,
                                 Provider_Legal_Name = prv.Provider_legal_name,
                                 ApplicationId = lrn.ApplicationId,
                                 Id = prv.Id
                             })
                    .Where(a => a.ApplicationId == ApplicationId).Distinct().ToList();

            var prds = from o in providers
                       select new ProviderDetailsListDto()
                       {
                           Providers = new ProviderDetailsDto

                           {
                               Provider_Trading_name = o.Provider_Trading_name,
                               Provider_SDL_NO = o.Provider_SDL_NO,
                               Chieta_Accredited = o.Chieta_Accredited,
                               Provider_Accreditation_NO = o.Provider_Accreditation_NO,
                               Provider_legal_name = o.Provider_Legal_Name,
                               Id = o.Id
                           }
                       };

            var totalCount = prds.Distinct().Count();

            return new PagedResultDto<ProviderDetailsListDto>(
                totalCount,
                prds.Distinct().ToList()
            );
        }

        public async Task<ProviderDetailsDto> GetProviderById(int id)
        {
            var provider = await _provRepository.GetAll()
                .Where(prv => prv.Id == id)
                .Select(prv => new ProviderDetailsDto
                {
                    Provider_Trading_name = prv.Provider_Trading_name,
                    Provider_legal_name = prv.Provider_legal_name,
                    Provider_SDL_NO = prv.Provider_SDL_NO,
                    Chieta_Accredited = prv.Chieta_Accredited,
                    Public_Private = prv.Public_Private,
                    Provider_Accreditation_NO = prv.Provider_Accreditation_NO,
                    Provider_Accredit_Review_Date = prv.Provider_Accredit_Review_Date,
                    Accred_NO_Knowledge_Component = prv.Accred_NO_Knowledge_Component,
                    Accred_NO_Practical_Component = prv.Accred_NO_Practical_Component,
                    SIC_Code = prv.SIC_Code,
                    ETQA_ID = prv.ETQA_ID,
                    Physical_Address_1 = prv.Physical_Address_1,
                    Physical_Address_2 = prv.Physical_Address_2,
                    Physical_Address_3 = prv.Physical_Address_3,
                    Physical_Postal_Code = prv.Physical_Postal_Code,
                    Postal_Address_1 = prv.Postal_Address_1,
                    Postal_Address_2 = prv.Postal_Address_2,
                    Postal_Address_3 = prv.Postal_Address_3,
                    Postal_Code = prv.Postal_Code,
                    Provider_Tel_No = prv.Provider_Tel_No,
                    Provider_Cell_No = prv.Provider_Cell_No,
                    Provider_Email = prv.Provider_Email,
                    Provider_SARS_No = prv.Provider_SARS_No,
                    Contact_Name = prv.Contact_Name,
                    Contact_Tel_No = prv.Contact_Tel_No,
                    Contact_FAX_No = prv.Contact_FAX_No,
                    Contact_Cell_No = prv.Contact_Cell_No,
                    Contact_Email = prv.Contact_Email,
                    Web_Address = prv.Web_Address,

                    Id = prv.Id
                })
                .FirstOrDefaultAsync();

            return provider;
        }

        public async Task<PagedResultDto<Discretionary_Universtity_CollegeList>> GetCollegesByName(string SearchName)
        {

            var ccolls = (from c in _college.GetAll()
                          select new
                          {
                              UniversityCollegeName = c.UniversityCollegeName,
                              Type = c.Type,
                              Id = c.Id
                          }).Distinct()
                          .OrderBy(o=>o.UniversityCollegeName)
                          .ToList();

            var colls = from o in ccolls
                        select new Discretionary_Universtity_CollegeList()
                        {
                            Colleges = new Discretionary_Universtity_CollegeDto

                            {
                                UniversityCollegeName = o.UniversityCollegeName,
                                Type = o.Type,
                                Id = o.Id
                            }
                        };

            var totalCount = colls.Distinct().Count();

            return new PagedResultDto<Discretionary_Universtity_CollegeList>(
                totalCount,
                colls.Distinct().ToList()
            );
        }

        public async Task<Discretionary_Universtity_CollegeDto> GetCollegesById(int Id)
        {
            var clg = await _college.GetAll()
                .Where(q => q.Id == Id)
                .Select(cl => new Discretionary_Universtity_CollegeDto
                {
                    UniversityCollegeName = cl.UniversityCollegeName,
                    Id = cl.Id
                })
                .FirstOrDefaultAsync();

            return clg;
        }
    }
}
