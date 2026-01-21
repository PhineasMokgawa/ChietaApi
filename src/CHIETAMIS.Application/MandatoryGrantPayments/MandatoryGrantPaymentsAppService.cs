using System.Data;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using CHIETAMIS.MandatoryGrantPayments.Dtos;
using Abp.Application.Services.Dto;
using Microsoft.EntityFrameworkCore;
using CHIETAMIS.Finance;

namespace CHIETAMIS.MandatoryGrantPayments
{
    //[AbpAuthorize(AppPermissions.Pages_LevyFileList)]
    public class MandatoryGrantPaymentsAppService : CHIETAMISAppServiceBase, IMandatoryGrantPaymentsAppService
    {
        private readonly IRepository<MandatoryGrantsPayments> _mgRepository;
        private readonly IRepository<LevyFile> _lfRepository;
        private readonly IRepository<LevyFileList> _lflRepository;
        private readonly IRepository<LevyFileDetails> _lfdRepository;

        public MandatoryGrantPaymentsAppService(
            IRepository<MandatoryGrantsPayments> mgRepository,
            IRepository<LevyFile> lfRepository,
            IRepository<LevyFileList> lflRepository,
            IRepository<LevyFileDetails> lfdRepository)
        {
            _mgRepository = mgRepository;
            _lfRepository = lfRepository;
            _lflRepository = lflRepository;
            _lfdRepository = lfdRepository;
        }
        public async Task<PagedResultDto<GetMandatoryGrantPaymentsForViewDto>> GetMandatoryGrantPayments(string sdl)
        {
            int mgyear = _mgRepository.GetAll().Max(a => a.GrantYear);

            var filteredMandatoryGrantPayments = (from mg in _mgRepository.GetAll()
            join lf in _lfRepository.GetAll() on mg.zipfileid equals lf.Id
            join lfl in _lflRepository.GetAll() on lf.Id equals lfl.DHETZipFileID
            join lfd in _lfdRepository.GetAll() on lfl.Id equals lfd.LevyFileId
            select new
            {
                mg = mg,
                lfd = lfd,
                lf = lf
            })
           .Where(a => a.mg.GrantYear == mgyear && a.lfd.SDLNumber == a.mg.SDL_Number && a.mg.zipfileid == a.lf.Id && a.mg.SDL_Number == sdl);

            var cnt = filteredMandatoryGrantPayments.Count();

            var pagedAndFilteredMandatoryGrantPayments = filteredMandatoryGrantPayments
                .OrderBy(a => a.mg.SDL_Number);
                //.OrderByDescending(a => a.lfd.Id);

            var MandatoryGrantPayments = from o in pagedAndFilteredMandatoryGrantPayments
                select new GetMandatoryGrantPaymentsForViewDto()
                {
                    MandatoryGrantsPayments = new MandatoryGrantPaymentsDto
                    {
                        SDL_Number = o.mg.SDL_Number,
                        GrantYear = o.lfd.LevyYear,
                        Month = o.mg.Month,
                        ChietaAccount = o.mg.ChietaAccount,
                        CHIETA_Code1 = o.mg.CHIETA_Code1,
                        OrgName_Code = o.mg.OrgName_Code,
                        BANK_NAME = o.mg.BANK_NAME,
                        Bank_Account_NUmber = o.mg.Bank_Account_NUmber,
                        Code = o.mg.Code,
                        Bank_Account_Code = o.mg.Bank_Account_Code,
                        Organisation_Name = o.mg.Organisation_Name,
                        SDLCode = o.mg.SDLCode,
                        Amount = (double)o.lfd.GrantAmount,
                        LevyAmount = (double)o.lfd.DhetAmount
                    }
                };

            var totalCount = await pagedAndFilteredMandatoryGrantPayments.CountAsync();

            return new PagedResultDto<GetMandatoryGrantPaymentsForViewDto>(
                totalCount,
                await MandatoryGrantPayments.Distinct().ToListAsync()
            );
        }

        public async Task<PagedResultDto<MandatoryGrantBeneficiariesView>> getMandatoryBeneficiaries()
        {
            int mgyear = _mgRepository.GetAll().Max(a => a.GrantYear);

            var filteredMandatoryGrantPayments = (from mg in _mgRepository.GetAll()
                join lf in _lfRepository.GetAll() on mg.zipfileid equals lf.Id
                join lfl in _lflRepository.GetAll() on lf.Id equals lfl.DHETZipFileID
                join lfd in _lfdRepository.GetAll() on lfl.Id equals lfd.LevyFileId
                select new
                {
                    mg = mg,
                    lfd = lfd,
                    lf = lf
                })
           .Where(a => a.mg.GrantYear == mgyear && a.lfd.SDLNumber == a.mg.SDL_Number && a.mg.zipfileid == a.lf.Id);

            var cnt = filteredMandatoryGrantPayments.Distinct().Count();

            var pagedAndFilteredMandatoryGrantPayments = filteredMandatoryGrantPayments
                .OrderBy(a => a.mg.SDL_Number).ThenByDescending(a=>a.mg.SDLCode).ThenByDescending(a => a.lfd.LevyYear);

            var MandatoryGrantBenefit = from o in pagedAndFilteredMandatoryGrantPayments
                select new MandatoryGrantBeneficiariesView()
                {
                    Beneficiaries = new MandatoryGrantBeneficiariesDto
                    {
                        SDL_Number = o.mg.SDL_Number,
                    }
                };

            MandatoryGrantBenefit = MandatoryGrantBenefit.Distinct();

            var totalCount = filteredMandatoryGrantPayments.Count();

            return new PagedResultDto<MandatoryGrantBeneficiariesView>(
                totalCount,
                MandatoryGrantBenefit.ToList()
            );
        }
    }
}
