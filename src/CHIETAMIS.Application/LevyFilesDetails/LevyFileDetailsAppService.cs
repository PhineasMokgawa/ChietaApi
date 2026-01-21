using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using CHIETAMIS.LevyFilesDetails.Dtos;
using CHIETAMIS.MandatoryGrantPayments;
using CHIETAMIS.MandatoryGrantPayments.Dtos;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using CHIETAMIS.Authorization;
using CHIETAMIS;
using CHIETAMIS.Finance;
using CHIETAMIS.Finance.Dto;

namespace MIS.LevyFilesDetails
{
    [AbpAuthorize(AppPermissions.Pages_LevyFileDetails)]
    public class LevyFileDetailsAppService : CHIETAMISAppServiceBase, ILevyFileDetailsAppService
    {
        private readonly IRepository<LevyFileDetails, int> _levyFileDetailsRepository;
        private readonly IRepository<MandatoryGrantsPayments> _mandGrantPaymentsRepository;

        public LevyFileDetailsAppService(IRepository<LevyFileDetails, int> LevyFileDetailsRepository,
            //IRepository<BankingList, long> BankingListRepository,
            IRepository<MandatoryGrantsPayments> mandatoryGrantsPayments)
        {
            _levyFileDetailsRepository = LevyFileDetailsRepository;
            _mandGrantPaymentsRepository = mandatoryGrantsPayments;

        }

        //public async Task<PagedResultDto<GetLevyFileDetailsForViewDto>> GetApproved(GetAllLevyFileDetailsInput input)
        //{

        //    var filteredLevyFileDetails = _levyFileDetailsRepository.GetAll()
        //        //.WhereIf(input.LevyDateFilter != null, e => e.LevyDate == input.LevyDateFilter)
        //        .WhereIf(!string.IsNullOrWhiteSpace(input.SDLNumberFilter), e => e.SDLNumber == input.SDLNumberFilter)
        //        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => e.SDLNumber.Contains(input.Filter) || e.LevyYear.ToString() == input.Filter);

        //    var cnt = await filteredLevyFileDetails.CountAsync();

        //    var allPayments = _mandGrantPaymentsRepository.GetAll();

        //    cnt = await allPayments.CountAsync();

        //    var pagedAndFilteredLevyFileDetails = filteredLevyFileDetails
        //        .Join(allPayments,
        //            levyfile => new { levyfile.SDLNumber },
        //            approvals => new { approvals.SDL_Number },
        //            (levyfile, approvals) => new GetLevyFileDetailsForViewDto()
        //            {
        //                LevyFileDetails = new LevyFileDetailsDto
        //                {
        //                    LevyFileId = levyfile.LevyFileId,
        //                    LevyDate = levyfile.LevyDate,
        //                    SETA = levyfile.SETA,
        //                    SDLNumber = levyfile.SDLNumber,
        //                    TransactionDate = levyfile.TransactionDate,
        //                    GrantAmount = levyfile.GrantAmount,
        //                    GrantAmount2 = levyfile.GrantAmount2,
        //                    AdminAmount = levyfile.AdminAmount,
        //                    InterestAmount = levyfile.InterestAmount,
        //                    PenaltyAmount = levyfile.PenaltyAmount,
        //                    DhetAmount = levyfile.DhetAmount,
        //                    ReturnsOutstanding = levyfile.ReturnsOutstanding,
        //                    OutstandingDebt = levyfile.OutstandingDebt,
        //                    LevyYear = levyfile.LevyYear,
        //                    Id = levyfile.Id
        //                }
        //            })
        //        .Distinct()
        //        .OrderBy(input.Sorting ?? "LevyFileDetails.SDLNumber, LevyFileDetails.LevyDate Desc, LevyFileDetails.LevyYear Desc");

        //    var LevyFileDetails = pagedAndFilteredLevyFileDetails;


        //    var totalCount = await pagedAndFilteredLevyFileDetails.CountAsync();

        //    return new PagedResultDto<GetLevyFileDetailsForViewDto>(
        //        totalCount,
        //        await LevyFileDetails.ToListAsync()
        //    );
        //}

        public async Task<PagedResultDto<GetLevyFileDetailsForViewDto>> GetAll(GetAllLevyFileDetailsInput input)
        {

            var filteredLevyFileDetails = _levyFileDetailsRepository.GetAll()
                //.WhereIf(input.LevyDateFilter != null, e => e.LevyDate == input.LevyDateFilter)
                .WhereIf(!string.IsNullOrWhiteSpace(input.SDLNumberFilter), e => e.SDLNumber == input.SDLNumberFilter)
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => e.SDLNumber.Contains(input.Filter) || e.LevyYear.ToString() == input.Filter);

            var pagedAndFilteredLevyFileDetails = filteredLevyFileDetails
                .OrderBy(input.Sorting ?? "SDLNumber, LevyDate Desc, LevyYear Desc")
                .PageBy(input);

            var LevyFileDetails = from o in pagedAndFilteredLevyFileDetails
                                  select new GetLevyFileDetailsForViewDto()
                                  {
                                      LevyFileDetails = new LevyFileDetailsDto
                                      {
                                          LevyFileId = o.LevyFileId,
                                          LevyDate = o.LevyDate,
                                          SETA = o.SETA,
                                          SDLNumber = o.SDLNumber,
                                          TransactionDate = o.TransactionDate,
                                          GrantAmount = o.GrantAmount,
                                          GrantAmount2 = o.GrantAmount2,
                                          AdminAmount = o.AdminAmount,
                                          InterestAmount = o.InterestAmount,
                                          PenaltyAmount = o.PenaltyAmount,
                                          DhetAmount = o.DhetAmount,
                                          ReturnsOutstanding = o.ReturnsOutstanding,
                                          OutstandingDebt = o.OutstandingDebt,
                                          LevyYear = o.LevyYear,
                                          Id = o.Id
                                      }
                                  };

            var totalCount = await filteredLevyFileDetails.CountAsync();

            return new PagedResultDto<GetLevyFileDetailsForViewDto>(
                totalCount,
                await LevyFileDetails.ToListAsync()
            );
        }

        public async Task<GetLevyFileDetailsForViewDto> GetLevyFileDetailsForView(int id)
        {
            var LevyFileDetails = await _levyFileDetailsRepository.GetAsync(id);

            var output = new GetLevyFileDetailsForViewDto { LevyFileDetails = ObjectMapper.Map<LevyFileDetailsDto>(LevyFileDetails) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_LevyFileDetails_Edit)]
        public async Task<GetLevyFileDetailsForEditOutput> GetLevyFileDetailsForEdit(EntityDto<int> input)
        {
            var LevyFileDetails = await _levyFileDetailsRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetLevyFileDetailsForEditOutput { LevyFileDetails = ObjectMapper.Map<CreateOrEditLevyFileDetailsDto>(LevyFileDetails) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditLevyFileDetailsDto input)
        {
            if (input.Id == null || input.Id == 0)
            {
                await Create(input);
            }
            else
            {
                await Update(input);
            }
        }

        [AbpAuthorize(AppPermissions.Pages_LevyFileDetails_Create)]
        protected virtual async Task Create(CreateOrEditLevyFileDetailsDto input)
        {
            var LevyFileDetails = ObjectMapper.Map<LevyFileDetails>(input);



            await _levyFileDetailsRepository.InsertAsync(LevyFileDetails);
        }

        [AbpAuthorize(AppPermissions.Pages_LevyFileDetails_Edit)]
        protected virtual async Task Update(CreateOrEditLevyFileDetailsDto input)
        {
            var LevyFileDetails = await _levyFileDetailsRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, LevyFileDetails);
        }

        [AbpAuthorize(AppPermissions.Pages_LevyFileDetails_Delete)]
        public async Task Delete(EntityDto<int> input)
        {
            await _levyFileDetailsRepository.DeleteAsync(input.Id);
        }
    }
}
