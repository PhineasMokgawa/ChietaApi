using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Castle.MicroKernel.Internal;
using CHIETAMIS.Authorization.Users;
using CHIETAMIS.DiscretionaryProjects;
using CHIETAMIS.DiscretionaryProjects.Dtos;
using CHIETAMIS.DiscretionaryWindows;
using CHIETAMIS.Documents;
using CHIETAMIS.Learners;
using CHIETAMIS.Lookups;
using CHIETAMIS.Organisations;
using CHIETAMIS.PaymentPortals;
using CHIETAMIS.PaymentPortals.DTOs;
using CHIETAMIS.Providers;
using CHIETAMIS.Sdfs;
using CHIETAMIS.Workflows;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CHIETAMIS.Tranches
{
    public class TranchesAppService : CHIETAMISAppServiceBase
    {
        private readonly IRepository<ProviderDetails> _provRepository;
        private readonly IRepository<ProgrammeDeliverables> _delrRepository;
        private readonly IUserEmailer _userEmailer;
        private readonly IRepository<DiscretionaryProject> _discProjRepository;
        private readonly IRepository<Organisation> _orgRepository;
        private readonly IRepository<DiscretionaryStatus> _discStatusRepository;
        private readonly IRepository<ProjectType> _projTypeRepository;
        private readonly IRepository<FocusArea> _focusAreaRepository;
        private readonly IRepository<AdminCriteria> _adminCritRepository;
        private readonly IRepository<EvaluationMethod> _evalMethodRepository;
        private readonly IRepository<DiscretionaryProjectDetailsApproval> _discProjDetApprovalRepository;
        private readonly IRepository<BankDetails> _bankRepository;
        private readonly IRepository<Bank_Account_Type> _accTypeRepository;
        private readonly IRepository<DiscrationaryTrancheBatchRequests> _trReqRepository;
        private readonly IRepository<DiscretionaryTrancheBatch> _tbRepository;
        private readonly IRepository<wfRequest> _wfReqRepository;
        private readonly IRepository<wfRequestData> _wfDataRepository;
        private readonly IRepository<Document> _docRepository;
        private readonly IRepository<ApplicationTranche> _appTranche;
        private readonly IRepository<Tranche_Approvals> _trancheApprovals;
        private readonly IRepository<ApplicationTrancheDetails> _appTrancheDetails;
        private readonly IRepository<LearnerDetails> _learnerDetails;
        private readonly IRepository<DgPaymentTrancheType> _trtype;

        public TranchesAppService(  IRepository<ProviderDetails> provRepository,
            IRepository<ProgrammeDeliverables> delrRepository,
            IRepository<DiscretionaryProject> dicprojRepository,
            IRepository<Organisation> orgRepository,
            IRepository<Organisation_Sdf> orgsdfRepository,
            IRepository<DiscretionaryStatus> discStatusRepository,
            IRepository<DiscretionaryWindow> windowRepository,
            IRepository<WindowParams> windowParamRepository,
            IRepository<ProjectType> projTypeRepository,
            IRepository<FocusArea> focusAreaRepository,
            IRepository<AdminCriteria> adminCritRepository,
            IRepository<EvaluationMethod> evalMethodRepository,
            IRepository<DiscretionaryProjectDetailsApproval> discprojDetApprovalRepository,
            IRepository<BankDetails> bankRepository,
            IRepository<Bank_Account_Type> accTypeRepository,
            IRepository<wfRequestData> wfDataRepository,
            IRepository<DiscrationaryTrancheBatchRequests>  trReqRepository,
            IRepository<DiscretionaryTrancheBatch> tbRepository,
            IRepository<wfRequest> wfReqRepository,
            IRepository<DiscretionaryProject> discProjRepository,
            IRepository<Document> docRepository,
            IRepository<ApplicationTranche> appTranche,
            IRepository<Tranche_Approvals> trancheApprovals,
            IRepository<ApplicationTrancheDetails> appTrancheDetails,
            IRepository<LearnerDetails> learnerDetails,
            IRepository<DgPaymentTrancheType> trtype)
        {
            _provRepository = provRepository;
            _delrRepository = delrRepository;
            _bankRepository = bankRepository;
            _accTypeRepository = accTypeRepository;
            _wfDataRepository = wfDataRepository;
            _trReqRepository = trReqRepository;
            _wfReqRepository = wfReqRepository;
            _tbRepository = tbRepository;
            _discProjDetApprovalRepository= discprojDetApprovalRepository;
            _discProjRepository = discProjRepository;
            _orgRepository = orgRepository;
            _docRepository = docRepository;
            _appTranche = appTranche;
            _trancheApprovals = trancheApprovals;
            _appTrancheDetails = appTrancheDetails;
            _learnerDetails = learnerDetails;
            _trtype = trtype;
        }

        public async Task<PagedResultDto<DiscreationaryBatchBanksView>> GetBankBatchDetails(string BatchNumber)
        {
            var bl = (from tr in _trReqRepository.GetAll()
            join tb in _tbRepository.GetAll() on tr.TrancheBatchId equals tb.Id
            join r in _wfReqRepository.GetAll() on tr.RequestId equals r.Id
            join rd in _wfDataRepository.GetAll().Where(a=>a.Name == "ApplicationId") on r.Id equals rd.RequestId
            join pd in _discProjDetApprovalRepository.GetAll() on rd.Value equals pd.Id.ToString()
            join p in _discProjRepository.GetAll() on pd.ProjectId equals p.Id
            join o in _orgRepository.GetAll() on p.OrganisationId equals o.Id
            join b in _bankRepository.GetAll() on o.Id equals b.OrganisationId
            join at in _accTypeRepository.GetAll() on b.AccountType equals at.Id into aty
            from act in aty.DefaultIfEmpty()
            select new
            {
                SDL_Number = o.SDL_No,
                Organisation_Name = o.Organisation_Name,
                Trading_Name = o.Organisation_Trading_Name,
                Bank_Name = b.Bank_Name,
                Account_Number = b.Account_Number,
                Branch_Code = b.Branch_Code,
                Trache_BatchNo = tb.BatchNumber,
            })
.Where(a => a.Trache_BatchNo == BatchNumber)
.Distinct()
.ToList();


            var banks = from o in bl
            select new DiscreationaryBatchBanksView()
            {
                BatchBanks = new DiscreationaryBatchBanksDto
                {
                    SDL_Number = o.SDL_Number,
                    Organisation_Name = o.Organisation_Name,
                    Trading_Name = o.Trading_Name,
                    Bank_Name = o.Bank_Name,
                    Account_Number = o.Account_Number,
                    Branch_Code = o.Branch_Code,
                }
            };

            var totalCount = banks.Count();

            return new PagedResultDto<DiscreationaryBatchBanksView>(
                totalCount,
                banks.ToList()
            );
        }
        public async Task<string> SubmitTranche1bApproval(int ApplicationId, string TrancheType, int UserId)
        {
            var dgproj = _discProjDetApprovalRepository.GetAll().Where(a => a.Id == ApplicationId).FirstOrDefault();
            var output = "";
            if (dgproj.Id != 0)
            {
                var proj = _discProjRepository.GetAll().Where(a=>a.Id == dgproj.ProjectId).FirstOrDefault();
                
                var docs = (from lrns in _learnerDetails.GetAll().Where(a=>a.ApplicationId == ApplicationId && a.Funded == true && a.Status == "Pending")
                    join dcs in _docRepository.GetAll().Where(a => a.module == "Tranches - 1b") on lrns.Id equals dcs.entityid
                    select new
                    {
                        Docs = dcs,
                        LearnerId = lrns.Id
                    }).ToList();
                var compdocs = _docRepository.GetAll().Where(a => a.entityid == ApplicationId && a.module == "Tranches - 1b");
                var learners = _learnerDetails.GetAll().Where(a => a.ApplicationId == ApplicationId && a.Funded == true && a.Status == "Pending").ToList();

                if (learners.Count() > 0)
                {
                    var delv = _delrRepository.GetAll().Where(a => a.FocusAreaId == dgproj.FocusAreaId && a.SubCategoryId == dgproj.SubCategoryId && a.TrancheTypeId == "1b" && a.AppliesTo == "Company").ToList();

                    if (delv.Count() == 0)
                    {
                        delv = _delrRepository.GetAll().Where(a => a.SubCategoryId == dgproj.SubCategoryId && a.FocusAreaId == 0 && a.TrancheTypeId == "1b" && a.AppliesTo == "Company").ToList();
                    }

                    if (delv.Count() == 0)
                    {
                        delv = _delrRepository.GetAll().Where(a => a.FocusAreaId == dgproj.FocusAreaId && a.SubCategoryId == 0 && a.TrancheTypeId == "1b" && a.AppliesTo == "Company").ToList();
                    }

                    if (compdocs.Count() != 0)
                    {
                        if (delv.Count() > 0)
                        {
                            var cdocs = true;
                            foreach (var dlv in delv)
                            {
                                if (dlv.AppliesTo.Trim() == "Company")
                                {
                                    var docfound = false;
                                    foreach (var d in compdocs)
                                    {
                                        if (d.entityid == ApplicationId && d.documenttype == dlv.DocumentType && d.module == "Tranches - 1b") { docfound = true; }
                                    }
                                    if (!docfound) { cdocs = false; }
                                }
                            }

                            var lrndocs = true;
                            //foreach (var lrn in learners)
                            //{
                            //    var ldocs = docs.Where(a => a.LearnerId == lrn.Id);
                            //    foreach (var dlv in delv)
                            //    {
                            //        if (dlv.AppliesTo.Trim() == "Learner")
                            //        {
                            //            var docfound = false;
                            //            foreach (var d in ldocs)
                            //            {
                            //                if (d.Docs.entityid == lrn.Id && d.Docs.documenttype == dlv.DocumentType && d.Docs.module == "Tranches - 1b") { docfound = true; }
                            //            }

                            //            if (!docfound) { lrndocs = false; }
                            //        }
                            //    }
                            //}

                            if (!cdocs) { output = output + ", Missing Company Uploads, please review and retry."; }
                            if (!lrndocs) { output = output + ", Missing Learner Documents"; }

                        }
                        else
                        {
                            output = output + ", Deliverables setup error. Please consult CHIETA.";
                        }
                    }
                    else
                    {
                        output = "No documents uploaded, please all required Company Uploads before continuing.";
                    }
                } else
                {
                    output = "No learners to be submitted.";
                }

                if (output == "")
                {
                    var intr = new ApplicationTrancheDto();
                    intr.ApplicationId = ApplicationId;
                    intr.ProgrammeTypeId = proj.ProjectTypeId;
                    intr.Description = "Tranche 1b";
                    intr.TrancheType = "1b";
                    intr.FocusAreaId = dgproj.FocusAreaId;
                    intr.SubCategoryId = dgproj.SubCategoryId;
                    intr.TrancheStatus = "Processing";
                    intr.New_Learners = dgproj.GC_New;
                    intr.Continuing = dgproj.GC_Continuing;
                    intr.CostPerLearner = dgproj.GC_CostPerLearner;
                    intr.Current_Approver = "RSA";
                    intr.TrancheAmount = 0;
                    intr.DateCreated = DateTime.Now;
                    intr.UserId = UserId;
                    var t = ObjectMapper.Map<ApplicationTranche>(intr);
                    var appt = _appTranche.InsertAsync(t);

                    var tra = new Tranche_Approvals();
                    tra.ApplicationId = ApplicationId;
                    tra.TrancheId = 2;
                    tra.Approval_Status = "Pending";
                    tra.Comment = "";
                    tra.ApprovalLevel = "RSA";
                    tra.UserId = UserId;
                    tra.DateApproved = DateTime.Now;
                    tra.DateCreated = DateTime.Now;

                    _trancheApprovals.Insert(tra);
                }
            } else
            {
                output = output + "Project selection error. Please retry";
            }

            return output;
        }

        public async Task<int> TranchDetailsSave(int ApplicationId, string TrancheType, int UserId)
        {
            var dgproj = _discProjDetApprovalRepository.GetAll().Where(a => a.Id == ApplicationId).FirstOrDefault();
            var learners = _learnerDetails.GetAll().Where(a => a.ApplicationId == ApplicationId && a.Funded == true && a.Status == "Pending").ToList();
            //var ttype = _trtype.GetAll().Where(a=>a.TrancheCode== TrancheType).FirstOrDefault();
            var ido = _appTranche.GetAll().Where(a => a.ApplicationId == ApplicationId && a.TrancheType == TrancheType).Max(b=>b.Id);            
            var tottrancheamount = 0.0;
            foreach (var lrn in learners)
            {
                decimal amt = (decimal)(dgproj.GC_CostPerLearner);
                decimal perc = _delrRepository.GetAll().Where(a => a.FocusAreaId == dgproj.FocusAreaId || a.SubCategoryId == dgproj.SubCategoryId).FirstOrDefault().TranchePercent / 100;
                amt = amt * perc;
                var apptr = new ApplicationTrancheDetails();
                apptr.LearnerDetailsId = lrn.Id;
                apptr.ApplicationTrancheId = ido;
                apptr.Amount = amt;
                apptr.ApplicationTranceStatus = "Processing";
                apptr.Current_Approver = "RSA";
                apptr.UserId = UserId;
                apptr.DateCreated = DateTime.Now;
                apptr.UserId = UserId;
                _appTrancheDetails.Insert(apptr);

                tottrancheamount = tottrancheamount + (double)amt;
            }

            var at = _appTranche.Get(ido);
            at.TrancheAmount = (decimal)tottrancheamount;
            var l = ObjectMapper.Map<ApplicationTranche>(at);
            await _appTranche.UpdateAsync(l);

            return ido;
        }

        public async void Tranche1bAStatusChange(int ApplicationId, string TrancheType, int UserId)
        {
            var ldet = _learnerDetails.GetAll().Where(a => a.ApplicationId == ApplicationId && a.Funded == true && a.Status == "Pending").ToList();
            foreach (var ld in ldet)
            {
                var ldto = _learnerDetails.Get(ld.Id);
                ldto.Status = "Batched";
                var ldt = ObjectMapper.Map<LearnerDetails>(ldto);
                _learnerDetails.Update(ldt);
            }
        }
    }
}
