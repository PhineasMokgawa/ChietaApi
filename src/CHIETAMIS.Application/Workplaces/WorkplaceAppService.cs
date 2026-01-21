using Abp.Domain.Repositories;
using CHIETAMIS.Authorization.Users;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CHIETAMIS.Workplaces.Dto;
using Abp.Application.Services.Dto;
using CHIETAMIS.Providers.Dto;
using CHIETAMIS.Learners;
using Microsoft.EntityFrameworkCore;

namespace CHIETAMIS.Workplaces
{
    public class WorkplacesAppService : CHIETAMISAppServiceBase
    {
        private readonly IRepository<WorkplaceDetails> _workRepository;
        private readonly IRepository<LearnerDetails> _learnerRepository;
        private readonly IUserEmailer _userEmailer;

        public WorkplacesAppService(IRepository<WorkplaceDetails> workRepository,
                                    IRepository<LearnerDetails> learnerRepository)
        {
            _workRepository = workRepository;
            _learnerRepository = learnerRepository;
        }

        public async Task<string> CreateEditWorkplace(WorkplaceDetailsDto input)
        {
            var output = "";

            var work = _workRepository.GetAll().Where(a => a.Workplacement_Approval_No == input.Workplacement_Approval_No && a.Workplacement_Name == input.Workplacement_Name);

            if (work.Count() == 0)
            {
                var wrk = ObjectMapper.Map<WorkplaceDetails>(input);

                await _workRepository.InsertAsync(wrk);
            }
            else
            {
                var wrk = await _workRepository.GetAsync(work.FirstOrDefault().Id);
                await _workRepository.UpdateAsync(wrk);
            }

            return output;
        }

        public async Task<PagedResultDto<WorkplaceDetailsListDto>> GetDGApplicationWorkplaces(int ApplicationId, string TrancheType, string Status)
        {

            var workplaces = (from lrn in _learnerRepository.GetAll()
                              join wrk in _workRepository.GetAll() on lrn.Workplace_Legal_Name equals wrk.Workplacement_Name
                              select new
                              {
                                  Workplace_Trading_name = wrk.Workplace_Trading_Name,
                                  Workplacement_Name = wrk.Workplacement_Name,
                                  SDL_No = wrk.SDL_No,
                                  Workplacement_Approval_No = wrk.Workplacement_Approval_No,
                                  ETQA_ID = wrk.ETQA_ID,
                                  ApplicationId = lrn.ApplicationId,
                                  Id = wrk.Id
                              })
                    .Where(a => a.ApplicationId == ApplicationId).Distinct().ToList();

            var wkpls = from o in workplaces
                        select new WorkplaceDetailsListDto()
                        {
                            Workplaces = new WorkplaceDetailsDto

                            {
                                Workplace_Trading_Name = o.Workplace_Trading_name,
                                Workplacement_Name = o.Workplacement_Name,
                                SDL_No = o.SDL_No,
                                Workplacement_Approval_No = o.Workplacement_Approval_No,
                                ETQA_ID = o.ETQA_ID,
                                Id = o.Id
                            }
                        };

            var totalCount = wkpls.Distinct().Count();

            return new PagedResultDto<WorkplaceDetailsListDto>(
                totalCount,
                wkpls.Distinct().ToList()
            );
        }

        public async Task<WorkplaceDetailsDto> GetWorkplaceById(int id)
        {
            var work = await _workRepository.GetAll()
                .Where(wrk => wrk.Id == id)
                .Select(wrk => new WorkplaceDetailsDto
                {
                    Workplace_Trading_Name = wrk.Workplace_Trading_Name,
                    Workplacement_Name = wrk.Workplacement_Name,
                    Workplace_Type = wrk.Workplace_Type,
                    SDL_No = wrk.SDL_No,
                    SIC_Code = wrk.SIC_Code,
                    ETQA_ID = wrk.ETQA_ID,
                    Workplacement_Approval_No = wrk.Workplacement_Approval_No,
                    Physical_Address_1 = wrk.Physical_Address_1,
                    Physical_Address_2 = wrk.Physical_Address_2,
                    Physical_Address_3 = wrk.Physical_Address_3,
                    Physical_Postal_Code = wrk.Physical_Postal_Code,
                    Postal_Address_1 = wrk.Postal_Address_1,
                    Postal_Address_2 = wrk.Postal_Address_2,
                    Postal_Address_3 = wrk.Postal_Address_3,
                    Postal_Code = wrk.Postal_Code,
                    Workplace_Tel_No = wrk.Workplace_Tel_No,
                    Workplace_Fax_No = wrk.Workplace_Fax_No,
                    Workplace_Cell_No = wrk.Workplace_Cell_No,
                    Workplace_Email = wrk.Workplace_Email,
                    Workplace_SARS_No = wrk.Workplace_SARS_No,
                    Contact_Name = wrk.Contact_Name,
                    Contact_Tel_No = wrk.Contact_Tel_No,
                    Contact_FAX_No = wrk.Contact_FAX_No,
                    Contact_Cell_No = wrk.Contact_Cell_No,
                    Contact_Email = wrk.Contact_Email,
                    Web_Address = wrk.Web_Address,

                    Id = wrk.Id
                })
                .FirstOrDefaultAsync();

            return work;
        }
    }
}
