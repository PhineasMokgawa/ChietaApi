using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using CHIETAMIS.DiscretionaryWindows.Dtos;
using Abp.Application.Services.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Configuration;
using Abp.Zero.Configuration;
using CHIETAMIS.PaymentPortals;
using CHIETAMIS.DiscretionaryProjects;
using CHIETAMIS.DiscretionaryTanches.Dtos;

namespace CHIETAMIS.DiscretionaryTranches
{
    public class DiscretionaryTranchesAppService : CHIETAMISAppServiceBase
    {
        private readonly IRepository<ApplicationBatch> _appBatchRepository;
        private readonly IRepository<ApplicationTranche> _appTrancheRepository;
        private readonly IRepository<DiscretionaryProject> _discProjRepository;
        private readonly IRepository<DiscretionaryProjectDetailsApproval> _discProjDetApprRepository;

        public DiscretionaryTranchesAppService(IRepository<ApplicationBatch> appBatchRepository,
                                             IRepository<ApplicationTranche> appTrancheRepository,
                                             IRepository<DiscretionaryProject> discProjRepository,
                                             IRepository<DiscretionaryProjectDetailsApproval> discProjDetApprRepository)
        {
            _appBatchRepository = appBatchRepository;
            _appBatchRepository = appBatchRepository;
            _discProjDetApprRepository = discProjDetApprRepository;
            _discProjRepository = discProjRepository;
        }

        public async Task CreateEditAppBatch(int ApplicationId, string TrancheType, int UserId)
        {
            var abo = new ApplicationBatchDto();
            abo.ApplicationId = ApplicationId;
            abo.TrancheType = TrancheType;
            abo.UserId = UserId;
            var ab = ObjectMapper.Map<ApplicationBatch>(abo);

            await _appBatchRepository.InsertAsync(ab);
            
        }
        public async Task<PagedResultDto<ApplicationBatchList>> GetApplicationBatches(int applicationId)
        {
            var ab = _appBatchRepository.GetAll().Where(a => a.ApplicationId == applicationId);


            var abs = await (from o in ab
                              select new ApplicationBatchList
                              {
                                  ApplicationBatch = new ApplicationBatchDto
                                  {
                                      ApplicationId = o.ApplicationId,
                                      TrancheType = o.TrancheType,
                                      UserId = o.UserId,
                                      DateCreated = o.DateCreated,
                                      Id = o.Id

                                  }
                              }).ToListAsync();

            var totalCount = abs.Count();

            return new PagedResultDto<ApplicationBatchList>(
                totalCount,
                abs
            );
        }

        public async Task<ApplicationBatchDto> GetApplicationBatchId(int id)
        {
            var ab = await _appBatchRepository.GetAsync(id);
            var output = ObjectMapper.Map<ApplicationBatchDto>(ab);

            return output;
        }

        public async Task<ApplicationBatchDto> GetLatestApplicationBatch(int applicatonid)
        {
            var ab = _appBatchRepository.GetAll().Where(a=>a.ApplicationId == applicatonid)
                .OrderByDescending(a=>a.Id).FirstOrDefault();
            var output = ObjectMapper.Map<ApplicationBatchDto>(ab);

            return output;
        }
    }
}
