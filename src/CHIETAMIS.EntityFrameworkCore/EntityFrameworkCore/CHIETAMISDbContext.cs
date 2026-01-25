using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using CHIETAMIS.Authorization.Roles;
using CHIETAMIS.Authorization.Users;
using CHIETAMIS.MultiTenancy;
using CHIETAMIS.Tasks;
using CHIETAMIS.Lookups;
using CHIETAMIS.People;
using CHIETAMIS.Sdfs;
using CHIETAMIS.Organisations;
using CHIETAMIS.DiscretionaryProjects;
using CHIETAMIS.DiscretionaryStratRess;
using CHIETAMIS.DiscretionaryWindows;
using CHIETAMIS.Documents;
using CHIETAMIS.GrantApprovals;
using CHIETAMIS.UnitStandards;
using CHIETAMIS.PaymentPortals;
using CHIETAMIS.Counters;
using CHIETAMIS.Workflows;
using CHIETAMIS.MandatoryGrants;
using CHIETAMIS.Learners;
using CHIETAMIS.Providers;
using CHIETAMIS.Workplaces;
using CHIETAMIS.Schedules;
using CHIETAMIS.LEVYPAYMENTS;
using CHIETAMIS.MandatoryGrantPayments;
using CHIETAMIS.Finance;
using CHIETAMIS.DiscretionaryTranches;
using CHIETAMIS.Lesedis;
using CHIETAMIS.Qualifications;

namespace CHIETAMIS.EntityFrameworkCore
{
    public class CHIETAMISDbContext : AbpZeroDbContext<Tenant, Role, User, CHIETAMISDbContext>
    {
        /* Define a DbSet for each entity of the application */
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Title> Title { get; }
        public DbSet<Designation> Desgignation { get; }
        public DbSet<IdType> IdType { get; }
        public DbSet<Area> Area { get; }
        public DbSet<Citizenship> Citizenship { get; }
        public DbSet<Equity> Equity { get; }
        public DbSet<Gender> Gender { get; }
        public DbSet<Language> Language { get; }
        public DbSet<Nationality> Nationality { get; }
        public DbSet<Province> Province { get; }
        public DbSet<PostalCodeMapping> PostalCodeMapping { get; }
        public DbSet<Person> Person { get; }
        public DbSet<PersonPhysicalAddress> PersonPhysicalAddress { get; }
        public DbSet<PersonPostalAddress> PersonPostalAddresses { get; }
        public DbSet<SdfFileUpload> SdfFileUpload { get; }
        public DbSet<SdfDetails> SdfDetails { get; set; }
        public DbSet<Organisation> Organisation { get; set; }
        public DbSet<Organisation_Sdf> Organisation_Sdf { get; set; }
        public DbSet<DiscretionaryProject> DiscretionaryProject { get; set; }
        public DbSet<DiscretionaryWindow> DiscretionaryWindow { get; set; }
        public DbSet<WindowParams> WindowParams { get; set; }
        public DbSet<FocusArea> FocusArea { get; set; }
        public DbSet<ProjectType> ProjectType { get; set; }
        public DbSet<AdminCriteria> AdminCriteria { get; set; }
        public DbSet<EvaluationMethod> EvaluationMethod { get; set; }
        public DbSet<FocusCriteriaEvaluation> FocusCriteriaEvaluation { get; set; }
        public DbSet<DiscretionaryStatus> DiscretionaryStatus { get; set; }
        public DbSet<OrganisationPhysicalAddress> OrganisationPhysicalAddress { get; }
        public DbSet<OrganisationPostalAddress> OrganisationPostalAddresses { get; }
        public DbSet<BankDetails> BankDetails { get; }
        public DbSet<DiscretionaryProjectDetails> DiscretionaryProjectDetails { get; }
        public DbSet<DiscretionaryProjectDetailsApproval> DiscretionaryProjectDetailsApproval { get; }
        public DbSet<DiscretionaryStratResDetails> DiscretionaryStratResDetails { get; }
        public DbSet<DiscretionaryStratResDetailsApproval> DiscretionaryStratResDetailsApproval { get; }
        public DbSet<DiscretionaryStratResObjectives> DiscretionaryStratResObjectives { get; }
        public DbSet<DiscretionaryStratResObjectivesApproval> DiscretionaryStratResObjectivesApproval { get; }
        public DbSet<Document> Document { get; }
        public DbSet<DiscretionaryGrantApproval> DiscretionaryGrantApproval { get; }
        public DbSet<GrantApprovalStatus> GrantApprovalStatus { get; }
        public DbSet<GrantApprovalType> GrantApprovalType { get; }
        public DbSet<Bank_Account_Type> Bank_Account_Type { get; }
        public DbSet<BBBStatuses> BBBStatuses { get; }
        public DbSet<BBBeeLevels> BBBeeLevels { get; }
        public DbSet<Chambers> Chambers { get; }
        public DbSet<ProjectNotification> ProjectNotifications { get; set; }

        public DbSet<CompanySizes> CompanySizes { get; }
        public DbSet<Fintegrate_Payment_Status> Fintegrate_Payment_Status { get; }
        public DbSet<Payment_Tranches> Payment_Tranches { get; }
        public DbSet<Tranche_Approval_Status> Tranche_Approval_Status { get; }
        public DbSet<Counter> Counter { get; }
        public DbSet<Bank> Bank { get; }
        public DbSet<UnitStandard> UnitStandard { get; }
        public DbSet<DiscretionaryProjectUS> DiscretionaryProjectUS { get; }
        public DbSet<DiscretionaryProjectUSApproval> DiscretionaryProjectUSApproval { get; }
        public DbSet<DiscretionaryDocumentApproval> DiscretionaryDocumentApproval { get; }
        public DbSet<DiscretionaryDetailApproval> DiscretionaryDetailApproval { get; }
        public DbSet<DiscretionaryGECApproval> DiscretionaryGECApproval { get; }
        public DbSet<DiscretionaryGECRApproval> DiscretionaryGECRApproval { get; }
        public DbSet<DiscretionaryGACApproval> DiscretionaryGACApproval { get; }
        public DbSet<DiscretionaryGACRApproval> DiscretionaryGACRApproval { get; }
        public DbSet<DiscretionaryGCApproval> DiscretionaryGCApproval { get; }
        public DbSet<DiscretionaryGCRApproval> DiscretionaryGCRApproval { get; }
        public DbSet<ResearchDocumentApproval> ResearchDocumentApproval { get; }
        public DbSet<DiscretionaryResearchApproval> DiscretionaryResearchApproval { get; }
        public DbSet<Vision2025Goals> Vision2025Goals { get; }
        public DbSet<SqmrAppIndicators> SqmrAppIndicators { get; }
        public DbSet<ApplicationTranche> ApplicationTranche { get; }
        public DbSet<ApplicationTrancheDetails> ApplicationTrancheDetails { get; }
        public DbSet<Payment_Message_Transactions> Payment_Message_Transactions { get; }
        public DbSet<PaymentMessages> PaymentMessages { get; }
        public DbSet<Tranche_Approvals> Tranche_Approvals { get; }
        public DbSet<wfAction> wfAction { get; }
        public DbSet<wfActionTarget> wfActionTarget { get; }
        public DbSet<wfActionType> wfActionType { get; }
        public DbSet<wfActivity> wfActivity { get; }
        public DbSet<wfActivityTarget> wfActivityTarget { get; }
        public DbSet<wfActivityType> wfActivityType { get; }
        public DbSet<wfGroup> wfGroup { get; }
        public DbSet<wfGroupMember> wfGroupMember { get; }
        public DbSet<wfProcess> wfProcess { get; }
        public DbSet<wfProcessAdmins> wfProcessAdmins { get; }
        public DbSet<wfRequest> wfRequest { get; }
        public DbSet<wfRequestAction> wfRequestAction { get; }
        public DbSet<wfRequestData> wfRequestData { get; }
        public DbSet<wfRequestFile> wfRequestFile { get; }
        public DbSet<wfRequestNote> wfRequestNote { get; }
        public DbSet<wfRequestStakeholder> wfRequestStakeholder { get; }
        public DbSet<wfState> wfState { get; }
        public DbSet<wfStateActivity> wfStateActivity { get; }
        public DbSet<wfStateType> wfStateType { get; }
        public DbSet<wfTarget> wfTarget { get; }
        public DbSet<wfTimer> wfTimer { get; }
        public DbSet<wfTimerDuration> wfTimerDuration { get; }
        public DbSet<wfTimerResult> wfTimerResult { get; }
        public DbSet<wfTransition> wfTransition { get; }
        public DbSet<wfTransitionAction> wfTransitionAction { get; }
        public DbSet<wfTransitionActivity> wfTransitionActivity { get; }
        public DbSet<wfTransitionTimer> wfTransitionTimer { get; }
        public DbSet<GrantDeliverableSchedule> GrantDeliverableSchedule { get; }
        public DbSet<GrantProgramDeliverables> GrantProgramDeliverables { get; }
        public DbSet<DgPaymentTrancheType> DgPaymentTrancheType { get; }
        public DbSet<MandatoryApplication> MandatoryApplication { get; }
        public DbSet<MandatoryWindow> MandatoryWindow { get; }
        public DbSet<MandatoryStatus> MandatoryStatus { get; }
        public DbSet<Biodata> Biodata { get; }
        public DbSet<Training> Training { get; }
        public DbSet<HTVF> HTVF { get; }
        public DbSet<SkillGab> SkillGab { get; }
        public DbSet<FINANCE_AND_TRAINING_COMPARISON> FINANCE_AND_TRAINING_COMPARISON { get; }
        public DbSet<MainPlace> MainPlace { get; }
        public DbSet<SubPlace> SubPlace { get; }
        public DbSet<Mandatory_Programmes> Mandatory_Programmes { get; }
        public DbSet<Mandatory_Grants_Gap_Reason> Mandatory_Grants_Gap_Reason { get; }
        public DbSet<EmploymentStatus> EmploymentStatus { get; }
        public DbSet<Mandatory_Grants_Target_Beneficiary> Mandatory_Grants_Target_Beneficiary { get; }
        public DbSet<Mandatory_Grants_Scarce_Reason> Mandatory_Grants_Scarce_Reason { get; }
        public DbSet<Mandatory_Grant_Qualification_Type> Mandatory_Grant_Qualification_Type { get; }
        public DbSet<Mandatoty_Grants_Impact> Mandatoty_Grants_Impact { get; }
        public DbSet<Mandatory_Grant_Achievement_Status> Mandatory_Grant_Achievement_Status { get; }
        public DbSet<Learning_Programme> Learning_Programme { get; }
        public DbSet<Occupation_Level> Occupation_Level { get; }
        public DbSet<OFO> OFO { get; }
        public DbSet<OFO_Specialization> OFO_Specialization { get; }
        public DbSet<DiscretionaryTrancheBatch> DiscretionaryTrancheBatch { get; }
        public DbSet<DiscrationaryTrancheBatchRequests> DiscrationaryTrancheBatchRequests { get; }
        public DbSet<Mandatory_Pivotal_Programmes> Mandatory_Pivotal_Programmes { get; }
        public DbSet<Extensions> Extensions { get; }
        public DbSet<Mandatory_Extension_Status> Mandatory_Extension_Status { get; }
        public DbSet<MandatoryApproval> MandatoryApproval { get; }
        public DbSet<MandatoryApprovalStatus> MandatoryApprovalStatus { get; }
        public DbSet<MandatoryDocumentApproval> MandatoryDocumentApproval { get; }
        public DbSet<RegionRSA> RegionRSA { get; }
        public DbSet<RegionRM> RegionRM { get; }
        public DbSet<SpecialistProject> SpecialistProject { get; }
        public DbSet<LearnerDetails> LearnerDetails { get; }
        public DbSet<ProviderDetails> ProviderDetails { get; }
        public DbSet<WorkplaceDetails> WorkplaceDetails { get; }
        public DbSet<ProjectLearner> ProjectLearner { get; }
        public DbSet<DiscretionaryLearnerSchedule> DiscretionaryLearnerSchedule { get; }
        public DbSet<ProgrammeDeliverables> ProgrammeDeliverables { get; }
        public DbSet<Regions> Regions { get; }
        public DbSet<SICCodes> SICCodes { get; }
        public virtual DbSet<LEVY_PAYMENTS> LEVY_PAYMENTSs { get; set; }
        public virtual DbSet<LevyFile> LevyFile { get; set; }
        public virtual DbSet<LevyFileList> LevyFileList { get; set; }
        public virtual DbSet<LevyFileDetails> LevyFileDetails { get; set; }
        public virtual DbSet<MandatoryGrantsPayments> MandatoryGrantsPayments { get; set; }
        public virtual DbSet<MandatoryGrantPayment> MandatoryGrantPayment { get; set; }
        public virtual DbSet<SETA> SETA { get; set; }
        public virtual DbSet<BankingList> BankingList { get; set; }
        public virtual DbSet<ImportBatch> ImportBatch { get; set; }
        public virtual DbSet<LeviesRecon> LeviesRecon { get; set; }
        public virtual DbSet<MandatoryBankDetails> MandatoryBankDetails { get; set; }
        public virtual DbSet<ApplicationBatch> ApplicationBatch { get; set; }
        public virtual DbSet<Lesedi> Lesedi { get; set; }
        public virtual DbSet<LesediAddress> LesediAddress { get; set; }
        public virtual DbSet<LesediDetails> LesediDetails { get; set; }
        public virtual DbSet<LesediStatus> LesediStatus { get; set; }
        public virtual DbSet<BursaryApplications> BursaryApplications { get; set; }
        public virtual DbSet<Discretionary_Universtity_College> Discretionary_Universtity_College { get; set; }
        public virtual DbSet<Qualification> Qualification { get; set; }
        public virtual DbSet<Lesedi_Qualification> Lesedi_Qualification { get; set; }
        public virtual DbSet<RegionProvince> RegionProvince { get; set; }
        public virtual DbSet<ProvinceCode> ProvinceCode { get; set; }
        public virtual DbSet<BursaryApprovals> BursaryApprovals { get; set; }
        public virtual DbSet<BursaryDocumentApprovals> BursaryDocumentApprovals { get; set; }
        public virtual DbSet<CompanyType> CompanyType { get; set; }
        public virtual DbSet<CompanyCompliance> CompanyCompliance { get; set; }
        public virtual DbSet<DiscLearnerType> DiscLearnerType { get; set; }
        public virtual DbSet<HistoricalPerformance> HistoricalPerformance { get; set; }


        public CHIETAMISDbContext(DbContextOptions<CHIETAMISDbContext> options)
            : base(options)
        {

        }
    }
}
