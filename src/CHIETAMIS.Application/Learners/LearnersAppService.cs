using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using CHIETAMIS.Authorization.Users;
using CHIETAMIS.DiscretionaryProjects;
using CHIETAMIS.DiscretionaryWindows;
using CHIETAMIS.Learners.Dto;
using CHIETAMIS.PaymentPortals;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using CHIETAMIS.Lookups;
using Microsoft.AspNetCore.Routing;
using OfficeOpenXml.FormulaParsing.Ranges;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;
using System.Data;
using CHIETAMIS.MandatoryGrants;
using CHIETAMIS.DiscretionaryTranches;
using CHIETAMIS.DiscretionaryTanches.Dtos;


namespace CHIETAMIS.Learners
{
    public class LearnersAppService : CHIETAMISAppServiceBase
    {

        private readonly IRepository<WindowParams> _windowParamRepository;
        private readonly IRepository<DiscretionaryWindow> _windowRepository;
        private readonly IRepository<LearnerDetails> _learnerRepository;
        private readonly IRepository<ApplicationTrancheDetails> _trdRepository;
        private readonly IUserEmailer _userEmailer;
        private readonly IRepository<DiscretionaryProjectDetailsApproval> _discProjDetRepository;
        private readonly IRepository<FocusArea> _focusAreaRepository;
        private readonly IRepository<AdminCriteria> _adminCritRepository;
        private readonly IRepository<EvaluationMethod> _evalMethodRepository;
        private readonly IRepository<ApplicationBatch> _appBatchRepository;

        public LearnersAppService(

            IRepository<DiscretionaryWindow> windowRepository,
            IRepository<LearnerDetails> learnerRepository,
            IRepository<DiscretionaryProjectDetailsApproval> discProjDetRepository,
            IRepository<FocusArea> focusAreaRepository,
            IRepository<AdminCriteria> adminCritRepository,
            IRepository<EvaluationMethod> evalMethodRepository,
            IRepository<ApplicationTrancheDetails> trdRepository,
            IRepository<ApplicationBatch> appBatchRepository
          )
        {

            _windowRepository = windowRepository;
            _learnerRepository = learnerRepository;
            _discProjDetRepository = discProjDetRepository;
            _focusAreaRepository = focusAreaRepository;
            _adminCritRepository = adminCritRepository;
            _evalMethodRepository = evalMethodRepository;
            _trdRepository = trdRepository;
            _appBatchRepository = appBatchRepository;
        }

        public async Task<int> AddLearnersToAppBatch(int ApplicationId, int BatchId, string TrancheType, int userid)
        {
            // Find or create a LearnerBatch record with the specified ApplicationId
            var learnerBatch = await _learnerRepository.FirstOrDefaultAsync(ab => ab.BatchId == BatchId);

            if (learnerBatch != null)
            {
                // LearnerBatch does not exist, create a new one
                learnerBatch = new LearnerDetails
                {
                    ApplicationId = ApplicationId,
                    BatchId = learnerBatch.BatchId,
                    MoA_Contract_Number = learnerBatch.MoA_Contract_Number,
                    Funded = learnerBatch.Funded,
                    Contracted_Learning_Achievement_Status = learnerBatch.Contracted_Learning_Achievement_Status,
                    Learner_Enrolment_Number = learnerBatch.Learner_Enrolment_Number,
                    Learning_Programme_Name = learnerBatch.Learning_Programme_Name,
                    Subcategory = learnerBatch.Subcategory,
                    Intervention = learnerBatch.Intervention,
                    Start_Date_of_Training = learnerBatch.Start_Date_of_Training,
                    End_Date_of_Training = learnerBatch.End_Date_of_Training,
                    ID_Type = learnerBatch.ID_Type,
                    Passport_No = learnerBatch.ID_Number,
                    ID_Number = learnerBatch.ID_Number,
                    Age = learnerBatch.Age,
                    Youth = learnerBatch.Youth,
                    Title = learnerBatch.Title,
                    Last_Name = learnerBatch.Last_Name,
                    First_Name = learnerBatch.First_Name,
                    Middle_Name = learnerBatch.Middle_Name,
                    Birth_Year = learnerBatch.Birth_Year,
                    Gender = learnerBatch.Gender,
                    Race = learnerBatch.Race,
                    Disabled = learnerBatch.Disabled,
                    Home_Language = learnerBatch.Home_Language,
                    SA_Citizen = learnerBatch.SA_Citizen,
                    Nationality = learnerBatch.Nationality,
                    Employment_Status = learnerBatch.Employment_Status,
                    Unemployed_Period = learnerBatch.Unemployed_Period,
                    Address_Type = learnerBatch.Address_Type,
                    Home_Address_1 = learnerBatch.Home_Address_1,
                    Home_Address_2 = learnerBatch.Home_Address_2,
                    Home_Address_3 = learnerBatch.Home_Address_3,
                    Home_Address_Postal_Code = learnerBatch.Home_Address_Postal_Code,
                    Postal_Address_1 = learnerBatch.Postal_Address_1,
                    Postal_Address_2 = learnerBatch.Postal_Address_2,
                    Postal_Address_3 = learnerBatch.Postal_Address_3,
                    Postal_Code = learnerBatch.Postal_Code,
                    Guardian_ID_No = learnerBatch.Guardian_ID_No,
                    Guardian_Full_Name = learnerBatch.Guardian_Full_Name,
                    Province = learnerBatch.Province,
                    Municipality = learnerBatch.Municipality,
                    Town = learnerBatch.Town,
                    Urban_Rural = learnerBatch.Urban_Rural,
                    Tel_No = learnerBatch.Tel_No,
                    Cell_No = learnerBatch.Cell_No,
                    Email = learnerBatch.Email,
                    Occupational_Levels_For_Equity_Reporting_Purposes = learnerBatch.Occupational_Levels_For_Equity_Reporting_Purposes,
                    Job_Title = learnerBatch.Job_Title,
                    OFO_Occupation_Code = learnerBatch.OFO_Occupation_Code,
                    OFO_Specialisation = learnerBatch.OFO_Specialisation,
                    OFO_Occupation = learnerBatch.OFO_Occupation,
                    Highest_School_Qualification = learnerBatch.Highest_School_Qualification,
                    Highest_Qualification = learnerBatch.Highest_Qualification,
                    Student_Enrolment_No = learnerBatch.Student_Enrolment_No,
                    Bursary_Academic_Year_of_Study = learnerBatch.Bursary_Academic_Year_of_Study,
                    Bursary_Completion_Status_Final_Year = learnerBatch.Bursary_Completion_Status_Final_Year,
                    POPI_Act_Status = learnerBatch.POPI_Act_Status,
                    POPI_Act_Status_Date = learnerBatch.POPI_Act_Status_Date,
                    Workplace_Legal_Name = learnerBatch.Workplace_Legal_Name,
                    Provider_Legal_Name = learnerBatch.Provider_Legal_Name,
                    Status = learnerBatch.Status,
                    UploadStatus = learnerBatch.UploadStatus,
                    UserId = userid,
                    DateCreated = DateTime.Now
                };

                // Insert the new LearnerBatch record into the database
                learnerBatch = await _learnerRepository.InsertAsync(learnerBatch);
                await CurrentUnitOfWork.SaveChangesAsync(); // Save changes to persist the new LearnerBatch record   
            }

            // Return the BatchId associated with either the inserted ApplicationBatch or LearnerDetails

            return learnerBatch.Id;
        }

        public async Task<string> CreateEditLearner(LearnerDetailsDto input)
        {
            var output = "";
            var lrnc = 0;
            if ((input.ID_Number != null && input.ID_Number.Trim() != "") || (input.Passport_No != null && input.Passport_No.Trim() != ""))
            {
                if (input.ID_Number != null && input.ID_Number.Trim() != "")
                {
                    lrnc = ((from l in _learnerRepository.GetAll()
                             join b in _appBatchRepository.GetAll() on l.ApplicationId equals b.ApplicationId
                             select new
                             {
                                 ID_Number = l.ID_Number,
                                 ApplicationId = l.ApplicationId,
                                 Passport_No = l.Passport_No,
                             })

                                .Where(a => a.ID_Number == input.ID_Number && a.ApplicationId == input.ApplicationId)).Count();
                } else
                {
                    if (input.Passport_No != null && input.Passport_No.Trim() == "")
                    {
                        lrnc = ((from l in _learnerRepository.GetAll()
                                 join b in _appBatchRepository.GetAll() on l.ApplicationId equals b.ApplicationId
                                 select new
                                 {
                                     ID_Number = l.ID_Number,
                                     ApplicationId = l.ApplicationId,
                                     Passport_No = l.Passport_No,
                                 })

                                    .Where(a => a.Passport_No == input.Passport_No && a.ApplicationId == input.ApplicationId)).Count();
                    }
                }

                if (lrnc > 0)
                {
                    output = "Duplicate Id found";
                }
                else
                {
                    if (input.Contracted_Learning_Achievement_Status.Trim() != "")
                    {

                        if (input.Id == 0)
                        {
                            var funded = _learnerRepository.GetAll().Where(a => a.ApplicationId == input.ApplicationId && a.Funded == true).Count();
                            int awardedc = (int)_discProjDetRepository.GetAll().Where(a => a.Id == input.ApplicationId).FirstOrDefault().GC_Continuing;
                            int awardedn = (int)_discProjDetRepository.GetAll().Where(a => a.Id == input.ApplicationId).FirstOrDefault().GC_New;
                            int totalaward = 0;

                            if (awardedc > 0)
                            {
                                totalaward = totalaward + awardedc;
                            }
                            if (awardedn > 0)
                            {
                                totalaward = totalaward + awardedn;
                            }

                            var lrn = ObjectMapper.Map<LearnerDetails>(input);

                            if (funded >= totalaward)
                            {
                                lrn.Funded = false;
                            }
                            else
                            {
                                lrn.Funded = true;
                            }

                            lrn.Status = "Pending";

                            await _learnerRepository.InsertAsync(lrn);
                        }
                        else
                        {
                            var Learn = _learnerRepository.Get(input.Id);
                            Learn.DateCreated = DateTime.Now;
                            Learn.BatchId = input.BatchId;
                            Learn.First_Name = input.First_Name;
                            Learn.Middle_Name = input.Middle_Name;
                            Learn.Last_Name = input.Last_Name;
                            Learn.Learner_Enrolment_Number = input.Learner_Enrolment_Number;
                            Learn.Learning_Programme_Name = input.Learning_Programme_Name;
                            Learn.Subcategory = input.Subcategory;
                            Learn.Intervention = input.Intervention;
                            Learn.Start_Date_of_Training = input.Start_Date_of_Training;
                            Learn.End_Date_of_Training = input.End_Date_of_Training;
                            Learn.Passport_No = input.Passport_No;
                            Learn.ID_Number = input.ID_Number;
                            Learn.Age = input.Age;
                            Learn.Youth = input.Youth;
                            Learn.Title = input.Title;
                            Learn.Birth_Year = input.Birth_Year;
                            Learn.Gender = input.Gender;
                            Learn.Race = input.Race;
                            Learn.Disabled = input.Disabled;
                            Learn.Home_Language = input.Home_Language;
                            Learn.SA_Citizen = input.SA_Citizen;
                            Learn.Nationality = input.Nationality;
                            Learn.Employment_Status = input.Employment_Status;
                            Learn.Unemployed_Period = input.Unemployed_Period;
                            Learn.Address_Type = input.Address_Type;
                            Learn.Home_Address_1 = input.Home_Address_1;
                            Learn.Home_Address_2 = input.Home_Address_2;
                            Learn.Home_Address_3 = input.Home_Address_3;
                            Learn.Home_Address_Postal_Code = input.Home_Address_Postal_Code;
                            Learn.Postal_Address_1 = input.Postal_Address_1;
                            Learn.Postal_Address_2 = input.Postal_Address_2;
                            Learn.Postal_Address_3 = input.Postal_Address_3;
                            Learn.Postal_Code = input.Postal_Code;
                            Learn.Guardian_ID_No = input.Guardian_ID_No;
                            Learn.Guardian_Full_Name = input.Guardian_Full_Name;
                            Learn.Province = input.Province;
                            Learn.Municipality = input.Municipality;
                            Learn.Town = input.Town;
                            Learn.Urban_Rural = input.Urban_Rural;
                            Learn.Tel_No = input.Tel_No;
                            Learn.Cell_No = input.Cell_No;
                            Learn.Email = input.Email;
                            Learn.Occupational_Levels_For_Equity_Reporting_Purposes = input.Occupational_Levels_For_Equity_Reporting_Purposes;
                            Learn.Job_Title = input.Job_Title;
                            Learn.OFO_Occupation_Code = input.OFO_Occupation_Code;
                            Learn.OFO_Specialisation = input.OFO_Specialisation;
                            Learn.OFO_Occupation = input.OFO_Occupation;
                            Learn.Highest_School_Qualification = input.Highest_School_Qualification;
                            Learn.Highest_Qualification = input.Highest_Qualification;
                            Learn.Student_Enrolment_No = input.Student_Enrolment_No;
                            Learn.Bursary_Academic_Year_of_Study = input.Bursary_Academic_Year_of_Study;
                            Learn.Bursary_Completion_Status_Final_Year = input.Bursary_Completion_Status_Final_Year;
                            Learn.POPI_Act_Status = input.POPI_Act_Status;
                            Learn.POPI_Act_Status_Date = input.POPI_Act_Status_Date;
                            Learn.Workplace_Legal_Name = input.Workplace_Legal_Name;
                            Learn.Provider_Legal_Name = input.Provider_Legal_Name;


                            var lrn = ObjectMapper.Map<LearnerDetails>(Learn);

                            await _learnerRepository.UpdateAsync(lrn);
                        }
                    }

                }
            }
            else
            {
                output = "Missing Id/Passport";
            }
            return output;
        }

        public async Task<PagedResultDto<LearnerDetailsListDto>> GetDGApplicationLearners(int ApplicationId, int BatchId, string TrancheType, string Status)
        {
            var learners = (from lrn in _learnerRepository.GetAll()
                            join bl in _appBatchRepository.GetAll() on lrn.BatchId equals bl.Id
                            join prd in _discProjDetRepository.GetAll() on lrn.ApplicationId equals prd.Id
                            join focarea in _focusAreaRepository.GetAll() on prd.FocusAreaId equals focarea.Id
                            join subcat in _adminCritRepository.GetAll() on prd.SubCategoryId equals subcat.Id
                            join interv in _evalMethodRepository.GetAll() on prd.InterventionId equals interv.Id
                            join trd in _trdRepository.GetAll() on lrn.Id equals trd.LearnerDetailsId into t
                            from trds in t.DefaultIfEmpty()


                            select new
                            {
                                ID_Number = lrn.ID_Number,
                                Passport_No = lrn.Passport_No,
                                First_Name = lrn.First_Name,
                                Middle_Name = lrn.Middle_Name,
                                Last_Name = lrn.Last_Name,
                                Email = lrn.Email,
                                Province = lrn.Province,
                                FocusArea = focarea.FocusAreaDesc,
                                SubCategory = subcat.AdminDesc,
                                Intervention = interv.EvalMthdDesc,
                                Contract_Number = lrn.MoA_Contract_Number,
                                LearnerStatus = lrn.Contracted_Learning_Achievement_Status,
                                Status = lrn.Status,
                                UploadStatus = lrn.UploadStatus,
                                ApplicationId = lrn.ApplicationId,
                                BatchId = lrn.BatchId,
                                Funded = lrn.Funded,
                                Amount = trds.Amount,
                                Workplace_Legal_Name = lrn.Workplace_Legal_Name,
                                Provider_Legal_Name = lrn.Provider_Legal_Name,
                                userid = lrn.UserId,
                                Id = lrn.Id
                            })
                    .Where(a => a.ApplicationId == ApplicationId && a.LearnerStatus == "Enrolled" && a.Funded == true && a.Status == Status).ToList();

            var lrns = from o in learners
                       select new LearnerDetailsListDto()
                       {
                           Learner = new LearnerContractDetailsDto

                           {
                               ID_Number = o.ID_Number,
                               Passport_No = o.Passport_No,
                               First_Name = o.First_Name,
                               Middle_Name = o.Middle_Name,
                               Last_Name = o.Last_Name,
                               Email = o.Email,
                               Province = o.Province,
                               FocusArea = o.FocusArea,
                               SubCategory = o.SubCategory,
                               Intervention = o.Intervention,
                               Contract_Number = o.Contract_Number,
                               LearnerStatus = o.LearnerStatus,
                               Amount = o.Amount,
                               Workplace_Legal_Name = o.Workplace_Legal_Name,
                               Provider_Legal_Name = o.Provider_Legal_Name,
                               Status = o.Status,
                               UploadStatus = o.UploadStatus,
                               BatchId = o.BatchId,
                               Id = o.Id
                           }
                       };

            var totalCount = learners.Count();

            return new PagedResultDto<LearnerDetailsListDto>(
                totalCount,
                lrns.Distinct().ToList()
            );
        }

        public async Task<PagedResultDto<LearnerDetailsListDto>> GetDGApplicationUnfLearners(int ApplicationId, int BatchId, string Type, string State)
        {
            var learners = (from lrn in _learnerRepository.GetAll()
                            join bl in _appBatchRepository.GetAll() on lrn.BatchId equals bl.Id
                            join prd in _discProjDetRepository.GetAll() on lrn.ApplicationId equals prd.Id
                            join focarea in _focusAreaRepository.GetAll() on prd.FocusAreaId equals focarea.Id
                            join subcat in _adminCritRepository.GetAll() on prd.SubCategoryId equals subcat.Id
                            join interv in _evalMethodRepository.GetAll() on prd.InterventionId equals interv.Id
                            select new
                            {
                                ID_Number = lrn.ID_Number,
                                First_Name = lrn.First_Name,
                                Middle_Name = lrn.Middle_Name,
                                Last_Name = lrn.Last_Name,
                                Email = lrn.Email,
                                Province = lrn.Province,
                                FocusArea = focarea.FocusAreaDesc,
                                SubCategory = subcat.AdminDesc,
                                Intervention = interv.EvalMthdDesc,
                                Contract_Number = lrn.MoA_Contract_Number,
                                Status = lrn.Contracted_Learning_Achievement_Status,
                                UploadStatus = lrn.UploadStatus,
                                ApplicationId = lrn.ApplicationId,
                                BatchId = lrn.BatchId,
                                Funded = lrn.Funded,
                                Id = lrn.Id
                            })
                    .Where(a => a.ApplicationId == ApplicationId && a.Funded == false).ToList();


            if (Type == "1b")
            {
                if (State == "Pending")
                {
                    learners = learners.Where(a => a.Status == "Enrolled").ToList();
                }
                if (State == "Done")
                {
                    learners = learners.Where(a => a.Status == "Enrolled").ToList();
                }
            }

            if (Type == "2")
            {
                if (State == "Pending")
                {
                    learners = learners.Where(a => a.Status == "Busy").ToList();
                }
                if (State == "Done")
                {
                    learners = learners.Where(a => a.Status == "Busy").ToList();
                }
            }

            if (Type == "3")
            {
                if (State == "Pending")
                {
                    learners = learners.Where(a => a.Status == "Completed").ToList();
                }
                if (State == "Done")
                {
                    learners = learners.Where(a => a.Status == "Completed").ToList();
                }
            }

            var lrns = from o in learners
                       select new LearnerDetailsListDto()
                       {
                           Learner = new LearnerContractDetailsDto

                           {
                               ID_Number = o.ID_Number,
                               First_Name = o.First_Name,
                               Middle_Name = o.Middle_Name,
                               Last_Name = o.Last_Name,
                               Email = o.Email,
                               Province = o.Province,
                               FocusArea = o.FocusArea,
                               SubCategory = o.SubCategory,
                               Intervention = o.Intervention,
                               Contract_Number = o.Contract_Number,
                               Status = o.Status,
                               UploadStatus = o.UploadStatus,
                               BatchId = o.BatchId,
                               Id = o.Id
                           }
                       };

            var totalCount = learners.Count();

            return new PagedResultDto<LearnerDetailsListDto>(
                totalCount,
                lrns.Distinct().ToList()
            );
        }

        public async Task<LearnerDetailsDto> GetLearnerById(int id)
        {
            var learner = (from lrn in _learnerRepository.GetAll()
                           join bl in _appBatchRepository.GetAll() on lrn.BatchId equals bl.Id
                           join prd in _discProjDetRepository.GetAll() on lrn.ApplicationId equals prd.Id
                           join focarea in _focusAreaRepository.GetAll() on prd.FocusAreaId equals focarea.Id
                           join subcat in _adminCritRepository.GetAll() on prd.SubCategoryId equals subcat.Id
                           join interv in _evalMethodRepository.GetAll() on prd.InterventionId equals interv.Id
                           select new LearnerDetailsDto()
                           {

                               ApplicationId = lrn.ApplicationId,
                               BatchId = lrn.BatchId,
                               ID_Number = lrn.ID_Number,
                               Passport_No = lrn.Passport_No,
                               First_Name = lrn.First_Name,
                               Middle_Name = lrn.Middle_Name,
                               Last_Name = lrn.Last_Name,
                               Email = lrn.Email,
                               Gender = lrn.Gender,
                               Province = lrn.Province,
                               MoA_Contract_Number = lrn.MoA_Contract_Number,
                               Contracted_Learning_Achievement_Status = lrn.Contracted_Learning_Achievement_Status,
                               Learner_Enrolment_Number = lrn.Learner_Enrolment_Number,
                               Learning_Programme_Name = lrn.Learning_Programme_Name,
                               Subcategory = lrn.Subcategory,
                               Intervention = lrn.Intervention,
                               Start_Date_of_Training = lrn.Start_Date_of_Training,
                               End_Date_of_Training = lrn.End_Date_of_Training,
                               ID_Type = lrn.ID_Type,
                               Title = lrn.Title,
                               Age = lrn.Age,
                               Home_Language = lrn.Home_Language,
                               Race = lrn.Race,
                               Nationality = lrn.Nationality,
                               SA_Citizen = lrn.SA_Citizen,
                               Municipality = lrn.Municipality,
                               Urban_Rural = lrn.Urban_Rural,
                               Employment_Status = lrn.Employment_Status,
                               Unemployed_Period = lrn.Unemployed_Period,
                               Guardian_Full_Name = lrn.Guardian_Full_Name,
                               Guardian_ID_No = lrn.Guardian_ID_No,
                               Town = lrn.Town,
                               Tel_No = lrn.Tel_No,
                               Cell_No = lrn.Cell_No,
                               Job_Title = lrn.Job_Title,
                               Youth = lrn.Youth,
                               Birth_Year = lrn.Birth_Year,
                               Disabled = lrn.Disabled,
                               Address_Type = lrn.Address_Type,
                               Home_Address_1 = lrn.Home_Address_1,
                               Home_Address_2 = lrn.Home_Address_2,
                               Home_Address_3 = lrn.Home_Address_3,
                               Home_Address_Postal_Code = lrn.Home_Address_Postal_Code,
                               Postal_Address_1 = lrn.Postal_Address_1,
                               Postal_Address_2 = lrn.Postal_Address_2,
                               Postal_Address_3 = lrn.Postal_Address_3,
                               Postal_Code = lrn.Postal_Code,
                               Occupational_Levels_For_Equity_Reporting_Purposes = lrn.Occupational_Levels_For_Equity_Reporting_Purposes,
                               OFO_Occupation = lrn.OFO_Occupation,
                               OFO_Occupation_Code = lrn.OFO_Occupation_Code,
                               OFO_Specialisation = lrn.OFO_Specialisation,
                               Student_Enrolment_No = lrn.Student_Enrolment_No,
                               Highest_Qualification = lrn.Highest_Qualification,
                               Highest_School_Qualification = lrn.Highest_School_Qualification,
                               POPI_Act_Status = lrn.POPI_Act_Status,
                               POPI_Act_Status_Date = lrn.POPI_Act_Status_Date,
                               Bursary_Academic_Year_of_Study = lrn.Bursary_Academic_Year_of_Study,
                               Bursary_Completion_Status_Final_Year = lrn.Bursary_Completion_Status_Final_Year,
                               Provider_Legal_Name = lrn.Provider_Legal_Name,
                               Workplace_Legal_Name = lrn.Workplace_Legal_Name,
                               Status = lrn.Status,
                               UploadStatus = lrn.UploadStatus,
                               Id = lrn.Id
                           })
                    .Where(a => a.Id == id).FirstOrDefault();

            return learner;
        }

        

        public async Task<int> ValidateBatchLearners(int applicationId, int BatchId, string TrancheType)
        {
            var BatchErrors = 0;
            var learner = (from lrn in _learnerRepository.GetAll()
                           join bl in _appBatchRepository.GetAll() on lrn.BatchId equals bl.Id
                           join prd in _discProjDetRepository.GetAll() on lrn.ApplicationId equals prd.Id

                           select new
                           {

                               ApplicationId = lrn.ApplicationId,
                               BatchId = lrn.BatchId,
                               TrancheType = bl.TrancheType,
                               ID_Number = lrn.ID_Number,
                               Passport_No = lrn.Passport_No,

                               Id = lrn.Id,
                           }
                           )
                           .Where(a => a.ApplicationId == applicationId && a.TrancheType == TrancheType).ToList();

            if (learner.Count() > 0)
            {
                foreach (var l in learner)


                {
                    var existingBatchLearner = (from bl in _learnerRepository.GetAll()
                                                join ab in _appBatchRepository.GetAll() on bl.BatchId equals ab.Id

                                                select new
                                                {
                                                    Id = l.Id,
                                                    BatchId = l.BatchId,
                                                    TrancheType = ab.TrancheType
                                                })
                    .Where(a => a.BatchId != BatchId && a.TrancheType == TrancheType)
                    .Count();

                    if (existingBatchLearner > 0)

                    {
                        //delete the learner entry from Learnerdetails
                        _learnerRepository.Delete(l.Id);
                        await CurrentUnitOfWork.SaveChangesAsync();

                        BatchErrors = BatchErrors + 1;
                    }
                }
            }
            return BatchErrors;
        }

        public async Task<string> ValidateData(int BatchId)
        {
            var output = "";
            var err = "";
            var stat = "";
            var outcome = "";

            var learn = _learnerRepository.GetAll().Where(e => e.BatchId == BatchId).ToList();
            foreach (var b in learn)
            {
                output = "";
                err = "";
                stat = "";

                if ((b.Learner_Enrolment_Number == "" || b.Learner_Enrolment_Number == null))
                {
                    err = err + ",Missing Learner Enrolment Number Field ";
                    stat = "Fatal";
                    output = "Error";
                }


                if (b.Learning_Programme_Name == "" || b.Learning_Programme_Name == null)
                {
                    err = err + ",Missing Learning Programme Name ";
                    stat = "Fatal";
                    output = "Error";
                }

                if (b.Subcategory == "" || b.Subcategory == null)
                {
                    err = err + ", Missing Subcategory";
                    stat = "Fatal";
                    output = "Error";
                }


                if (b.Intervention == "" || b.Intervention == null)
                {
                    err = err + ", Missing Intervention";
                    stat = "Fatal";
                    output = "Error";
                }

                if (output == "")
                {

                    b.UploadStatus = "Success";
                    _learnerRepository.Update(b);
                }
                else
                {

                    b.UploadStatus = stat;
                    b.Comment = err;
                    _learnerRepository.Update(b);
                    outcome = "Error";
                }
            }

            return outcome;
        }
        public async Task<string> CheckBioErrors(int BatchId)
        {
            var nerrors = _learnerRepository.GetAll()
                        .Where(a => a.BatchId == BatchId && a.UploadStatus == "Fatal").Count();

            return nerrors.ToString();
        }

        public async Task<PagedResultDto<LearnerDetailsListDto>> GetBioErrors(int BatchId)
        {
            var filteredCBiodatas = _learnerRepository.GetAll()
                        .Where(a => a.BatchId == BatchId && a.UploadStatus == "Fatal");

            var pagedAndFilteredCBiodatas = filteredCBiodatas;

            var cBiodatas = from o in pagedAndFilteredCBiodatas
                            select new LearnerDetailsListDto()
                            {
                                Learner = new LearnerContractDetailsDto
                                {
                                    ID_Number = o.ID_Number,
                                    Passport_No = o.Passport_No,
                                    ApplicationId = o.ApplicationId,
                                    BatchId = o.BatchId,
                                    First_Name = o.First_Name,
                                    Middle_Name = o.Middle_Name,
                                    Last_Name = o.Last_Name,
                                    Email = o.Email,
                                    Province = o.Province,

                                    SubCategory = o.Subcategory,
                                    Intervention = o.Intervention,
                                    Contract_Number = o.MoA_Contract_Number,
                                    Status = o.Status,
                                    UploadStatus = o.UploadStatus,
                                    LearnerStatus = o.Contracted_Learning_Achievement_Status,

                                    Workplace_Legal_Name = o.Workplace_Legal_Name,
                                    Provider_Legal_Name = o.Provider_Legal_Name,


                                    Id = o.Id
                                }
                            };

            var totalCount = filteredCBiodatas.Count();

            return new PagedResultDto<LearnerDetailsListDto>(
                totalCount,
                cBiodatas.ToList()
            );
        }
    }
}
