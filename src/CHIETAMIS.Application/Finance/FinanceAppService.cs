using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using CHIETAMIS.Documents.Dtos;
using CHIETAMIS.Finance.Dto;
using CHIETAMIS.Organisations;
using CHIETAMIS.PaymentPortals;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Finance
{
    public class FinanceAppService: CHIETAMISAppServiceBase
    {
        private readonly IRepository<MandatoryGrantPayment> _mandPaymentRepository;
        private readonly IRepository<Organisation> _orgRepository;
        private readonly IRepository<LevyFileDetails> _levyfiledetRepository;
        private readonly IRepository<LevyFileList> _levyfilelistRepository;
        private readonly IRepository<LevyFile> _levyfileRepository;

        public FinanceAppService(IRepository<MandatoryGrantPayment> mandPaymentRepository,
                                 IRepository<Organisation> orgRepository,
                                 IRepository<LevyFile> levyfileRepository,
                                 IRepository<LevyFileList> levyfilelistRepository,
                                 IRepository<LevyFileDetails> levyfiledetRepository)
        {
            _mandPaymentRepository = mandPaymentRepository;
            _orgRepository = orgRepository;
            _levyfileRepository = levyfileRepository;
            _levyfilelistRepository = levyfilelistRepository;
            _levyfiledetRepository = levyfiledetRepository;
        }
        public async Task<PagedResultDto<LevySummaryList>> GetLeviesDetails(int organisationId)
        {
            var SDLNo = _orgRepository.GetAll().Where(a => a.Id == organisationId).FirstOrDefault().SDL_No;
            var lvs = (from lfl in _levyfilelistRepository.GetAll()
                             join lfd in _levyfiledetRepository.GetAll() on lfl.Id equals lfd.LevyFileId
                             select new
                             {
                                 Id = lfd.Id,
                                 SDLNumber = lfd.SDLNumber,
                                 LevyDate = lfd.LevyDate,
                                 LevyFile = lfl.FileName.Replace("03_Fin_", "").Replace(".SDL", ""),
                                 LevyYear = lfd.LevyYear,
                                 GrantAmount = lfd.GrantAmount * -1,
                                 GrantAmount2 = lfd.GrantAmount2 * -1,
                                 AdminAmount = lfd.AdminAmount * -1,
                                 InterestAmount = lfd.InterestAmount * -1,
                                 PenaltyAmount = lfd.PenaltyAmount * -1,
                                 DhetAmount = lfd.DhetAmount * -1
                             })
                    .Where(a => a.SDLNumber == SDLNo);

            var levies = await (from o in lvs
                        select new LevySummaryList
                        {
                            LeviesSummary = new LeviesSummaryDto
                            {
                                Id = o.Id,
                                SDLNumber = o.SDLNumber,
                                LevyFile = o.LevyFile,
                                LevyDate = o.LevyDate,
                                LevyYear = o.LevyYear,
                                GrantAmount = o.GrantAmount,
                                GrantAmount2 = o.GrantAmount2,
                                AdminAmount = o.AdminAmount,
                                InterestAmount = o.InterestAmount,
                                PenaltyAmount = o.PenaltyAmount,
                                DhetAmount = o.DhetAmount
                            }
                        }).OrderByDescending(a=>a.LeviesSummary.Id)
                        .ToListAsync()
;

            var totalCount = levies.Count();

            return new PagedResultDto<LevySummaryList>(
                totalCount,
                levies
            );
        }
    }
}
