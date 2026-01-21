using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.PlugIns;
using CHIETAMIS.Authorization.Users;
using CHIETAMIS.DiscretionaryProjects;
using CHIETAMIS.DiscretionaryTanches.Dtos;
using CHIETAMIS.DiscretionaryTranches;
using CHIETAMIS.DiscretionaryWindows;
using CHIETAMIS.Learners;
using CHIETAMIS.Organisations.Dtos;
using CHIETAMIS.PaymentPortals;
using CHIETAMIS.PaymentPortals.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Batches
{
    
    public class BatchesAppService: CHIETAMISAppServiceBase
    {
        private readonly IRepository<WindowParams> _windowParamRepository;
        private readonly IRepository<DiscretionaryWindow> _windowRepository;
        private readonly IRepository<LearnerDetails> _learnerRepository;
        private readonly IRepository<ApplicationTrancheDetails> _trdRepository;
        private readonly IRepository<ApplicationTranche> _trRepository;
        private readonly IUserEmailer _userEmailer;
        private readonly IRepository<DiscretionaryProjectDetailsApproval> _discProjDetRepository;
        private readonly IRepository<FocusArea> _focusAreaRepository;
        private readonly IRepository<AdminCriteria> _adminCritRepository;
        private readonly IRepository<EvaluationMethod> _evalMethodRepository;
        private readonly IRepository<ApplicationBatch> _appBatchRepository;

        public BatchesAppService(
            IRepository<DiscretionaryWindow> windowRepository,
            IRepository<LearnerDetails> learnerRepository,
            IRepository<DiscretionaryProjectDetailsApproval> discProjDetRepository,
            IRepository<FocusArea> focusAreaRepository,
            IRepository<AdminCriteria> adminCritRepository,
            IRepository<EvaluationMethod> evalMethodRepository,
            IRepository<ApplicationTrancheDetails> trdRepository,
            IRepository<ApplicationBatch> appBatchRepository,
            IRepository<ApplicationTranche> trRepository)
        {
            _windowRepository = windowRepository;
            _learnerRepository = learnerRepository;
            _discProjDetRepository = discProjDetRepository;
            _focusAreaRepository = focusAreaRepository;
            _adminCritRepository = adminCritRepository;
            _evalMethodRepository = evalMethodRepository;
            _trdRepository = trdRepository;
            _appBatchRepository = appBatchRepository;
            _trRepository = trRepository;
        }

        public async Task<int> CreateAppBatchId(int applicationId, string TrancheType, int userid)
        {
            // Check if the provided applicationId is not null
            if (applicationId == null)
            {
                throw new ArgumentNullException(nameof(applicationId), "ApplicationId cannot be null.");
            }


            // Create a new ApplicationBatch record with the specified ApplicationId
            var applicationBatch = new ApplicationBatch
            {
                ApplicationId = applicationId,
                TrancheType = TrancheType,
                UserId = userid,
                DateCreated = DateTime.Now
            };

            // Insert the new ApplicationBatch record into the database
            applicationBatch = await _appBatchRepository.InsertAsync(applicationBatch);
            await CurrentUnitOfWork.SaveChangesAsync(); // Save changes to persist the new ApplicationBatch record
                                                        // 
                                                        // Find or create a LearnerBatch record with the specified ApplicationId

            return applicationBatch.Id;
        }

        public async Task<ApplicationBatchDto> GetRecentAppBatchId(int ApplicationId)
        {
            var appBatch = _appBatchRepository.GetAll()
                        .Where(a => a.ApplicationId == ApplicationId)
                        .OrderByDescending(a => a.Id)
                        .Take(1).FirstOrDefault();

            var cAppBatch = ObjectMapper.Map<ApplicationBatchDto>(appBatch);

            return cAppBatch;
        }

        public async Task<PagedResultDto<ApplicationBatchList>> GetApplicationBatches(int ApplicationId, int UserId)
        {
            var appBatch = _appBatchRepository.GetAll()
              .Where(a => a.ApplicationId == ApplicationId);

            var cBatches = from o in appBatch
                           select new ApplicationBatchList()
                           {
                               ApplicationBatch = new ApplicationBatchDto()
                               {
                                   ApplicationId = o.ApplicationId,
                                   TrancheType = o.TrancheType,
                                   NoLearners = 0,
                                   TotalAmount = 0,
                                   DateCreated = o.DateCreated,
                                   UserId = o.UserId,
                                   Id = o.Id
                               }
                           };

            var totalCount = cBatches.Count();

            return new PagedResultDto<ApplicationBatchList>(
                totalCount,
                cBatches.ToList()
            );
        }

        public async Task<string> CreatePaymentBatch(int ApplicationId, string TrancheType, int UserId)
        {
            string result = "";
            var appBatch = _appBatchRepository.GetAll()
                        .Where(a => a.ApplicationId == ApplicationId && a.TrancheType == TrancheType)
                        .ToList();


            if (appBatch.Count() > 0)
            {
                foreach (var b in appBatch) {
                    var lstLearners = _learnerRepository.GetAll()
                        .Where(a=>a.ApplicationId == ApplicationId && a.BatchId == a.Id)
                        .ToList();

                    if (lstLearners.Count() == 0)
                    {
                        result = "An empty batch already exists, cannot create a new one again.";
                        break;
                    }
                }
            }

            if (result == "")
            {
                var abo = new ApplicationBatchDto();
                abo.ApplicationId = ApplicationId;
                abo.TrancheType = TrancheType;
                abo.UserId = UserId;
                abo.DateCreated = DateTime.Now;
                var ab = ObjectMapper.Map<ApplicationBatch>(abo);

                var abonew = await _appBatchRepository.InsertAsync(ab);
                await CurrentUnitOfWork.SaveChangesAsync();

                var BatchId = abonew.Id;

                //Create the application Batch details (TrancheBatch)
                var ato = new ApplicationTrancheDto();
                ato.ApplicationId = ApplicationId;
                ato.BatchId = BatchId;
                ato.TrancheType = TrancheType;
                ato.Description = "Learner Tranche " + TrancheType;
                ato.TrancheStatus = "Pending";
                var appdet = _discProjDetRepository.GetAll().Where(a => a.Id == ApplicationId).SingleOrDefault();
                ato.FocusAreaId = appdet.FocusAreaId;
                ato.SubCategoryId = appdet.SubCategoryId;
                ato.ProgrammeTypeId = appdet.ProjectTypeId;
                ato.New_Learners = appdet.GC_New;
                ato.Continuing = appdet.GC_Continuing;
                ato.Number_of_Learners = appdet.GC_Continuing + appdet.GC_New;
                ato.CostPerLearner = appdet.GC_CostPerLearner;
                ato.UserId = UserId;
                ato.DateCreated = DateTime.Now;
                var atb = ObjectMapper.Map<ApplicationTranche>(ato);

                await _trRepository.InsertAsync(atb);
            }

            return result;
        }

        public async Task<ApplicationTrancheDto> GetTrancheBatche(int ApplicationId, int BatchId, int UserId)
        {
            var appTr = _trRepository.GetAll()
                        .Where(a => a.ApplicationId == ApplicationId && a.BatchId == BatchId);

            var cAppTr = ObjectMapper.Map<ApplicationTrancheDto>(appTr);

            return cAppTr;
        }

        //public async Task<string> GetToken(string Username, string password)
        //{

        //}

    }
}
