using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using CHIETAMIS.LevyFiles.Dtos;
using CHIETAMIS.Dto;
using Abp.Application.Services.Dto;
using CHIETAMIS.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using CHIETAMIS.Authorization;
using CHIETAMIS.LevyFiles.Dtos;
using CHIETAMIS;
using CHIETAMIS.Finance;
using CHIETAMIS.Finance.Dto;
using CHIETAMIS.MandatoryGrantPayments.Dtos;
using CHIETAMIS.MandatoryGrantPayments;

namespace CHIETAMIS.LevyFiles
{
    //[AbpAuthorize(AppPermissions.Pages_LevyFile)]
    public class LevyFileAppService : CHIETAMISAppServiceBase
    {
        private readonly IRepository<LevyFile> _LevyFileRepository;
        private readonly IRepository<LevyFileList> _levyfilelistRepository;
        private readonly IRepository<LevyFileDetails> _levyfiledetRepository;
        private readonly IRepository<MandatoryGrantsPayments> _mgRepository;

        public LevyFileAppService(IRepository<LevyFile> LevyFileRepository, 
            IRepository<LevyFileList> levyfilelistRepository, 
            IRepository<LevyFileDetails> levyfiledetRepository,
            IRepository<MandatoryGrantsPayments> mgRepository)
        {
            _LevyFileRepository = LevyFileRepository;
            _levyfilelistRepository = levyfilelistRepository;
            _levyfiledetRepository = levyfiledetRepository;
            _mgRepository = mgRepository;
        }

        public async Task<PagedResultDto<GetMandatoryGrantPaymentsForViewDto>> GetAll()
        {
            int mgyear = _mgRepository.GetAll().Max(a => a.GrantYear);

            var filteredMandatoryGrantPayments = (from mg in _mgRepository.GetAll()
                join lf in _LevyFileRepository.GetAll() on mg.zipfileid equals lf.Id
                join lfl in _levyfilelistRepository.GetAll() on lf.Id equals lfl.DHETZipFileID
                join lfd in _levyfiledetRepository.GetAll() on lfl.Id equals lfd.LevyFileId
                select new
                {
                    mg = mg,
                    lfd = lfd,
                    lf = lf
                })
           .Where(a => a.mg.GrantYear == 2024 && a.lfd.SDLNumber == a.mg.SDL_Number && a.mg.zipfileid == a.lf.Id);

            var cnt = filteredMandatoryGrantPayments.Count();

            var pagedAndFilteredMandatoryGrantPayments = filteredMandatoryGrantPayments
                .OrderBy(a => a.mg.SDL_Number).OrderByDescending(a => a.lfd.Id);

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

            var totalCount = await filteredMandatoryGrantPayments.CountAsync();

            return new PagedResultDto<GetMandatoryGrantPaymentsForViewDto>(
                totalCount,
                await MandatoryGrantPayments.ToListAsync()
            );
        }

        public async Task<GetLevyFileForViewDto> GetLevyFileForView(int id)
        {
            var LevyFile = await _LevyFileRepository.GetAsync(id);

            var output = new GetLevyFileForViewDto { LevyFile = ObjectMapper.Map<LevyFileDto>(LevyFile) };

            return output;
        }

        //[AbpAuthorize(AppPermissions.Pages_LevyFile_Edit)]
        public async Task<GetLevyFileForEditOutput> GetLevyFileForEdit(EntityDto<int> input)
        {
            var LevyFile = await _LevyFileRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetLevyFileForEditOutput { LevyFile = ObjectMapper.Map<CreateOrEditLevyFileDto>(LevyFile) };

            return output;
        }

        public async Task<int> GetCurrentFinYear()
        {
            var LastDate = _levyfiledetRepository.GetAll().Max(x => x.LevyDate);
            
            var mnth = LastDate.Month;
            var FinYear = LastDate.Year;
            
            if (mnth <= 4) { 
                FinYear = LastDate.Year - 1;
            }

            return FinYear;
        }

        public async Task<bool> CheckDhetFileExists(int finyear)
        {
            bool Exist = false;

            var chk = _levyfilelistRepository.GetAll()
                    .Where(a => a.FileName.StartsWith("03_Fin_" + finyear.ToString()));

            var totalCount = chk.Count();

            if (totalCount > 0)
            {
                Exist = true;
            }

            return Exist;
        }

        public async Task CreateOrEdit(CreateOrEditLevyFileDto input)
        {
            if (input.Id == null)
            {
                await Create(input);
            }
            else
            {
                await Update(input);
            }
        }

        //[AbpAuthorize(AppPermissions.Pages_LevyFile_Create)]
        protected virtual async Task Create(CreateOrEditLevyFileDto input)
        {
            var LevyFile = ObjectMapper.Map<LevyFile>(input);



            await _LevyFileRepository.InsertAsync(LevyFile);
        }

        //[AbpAuthorize(AppPermissions.Pages_LevyFile_Edit)]
        protected virtual async Task Update(CreateOrEditLevyFileDto input)
        {
            var LevyFile = await _LevyFileRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, LevyFile);
        }

        [AbpAuthorize(AppPermissions.Pages_LevyFile_Delete)]
        public async Task Delete(EntityDto<int> input)
        {
            await _LevyFileRepository.DeleteAsync(input.Id);
        }


    }
}
