using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Learners
{
    [Table("tbl_Discretionary_LearnerDetails")]
    public class LearnerDetails: Entity
    {
        public int ApplicationId { get; set; }
        public int BatchId { get; set; }
        public string MoA_Contract_Number { get; set; }
        public bool Funded { get; set; }
        public string Contracted_Learning_Achievement_Status { get; set; }
        public string Learner_Enrolment_Number { get; set; }
        public string Learning_Programme_Name { get; set; }
        public string Subcategory { get; set; }
        public string Intervention { get; set; }
        public DateTime? Start_Date_of_Training { get; set; }
        public DateTime? End_Date_of_Training { get; set; }
        public string ID_Type { get; set; }
        public string Passport_No { get; set; }
        public string ID_Number { get; set; }
        public int Age { get; set; }
        public string Youth { get; set; }
        public string Title { get; set; }
        public string Last_Name { get; set; }
        public string First_Name { get; set; }
        public string Middle_Name { get; set; }
        public int Birth_Year { get; set; }
        public string Gender { get; set; }
        public string Race { get; set; }
        public string Disabled { get; set; }
        public string Home_Language { get; set; }
        public string SA_Citizen { get; set; }
        public string Nationality { get; set; }
        public string Employment_Status { get; set; }
        public string Unemployed_Period { get; set; }
        public string Address_Type { get; set; }
        public string Home_Address_1 { get; set; }
        public string Home_Address_2 { get; set; }
        public string Home_Address_3 { get; set; }
        public string Home_Address_Postal_Code { get; set; }
        public string Postal_Address_1 { get; set; }
        public string Postal_Address_2 { get; set; }
        public string Postal_Address_3 { get; set; }
        public string Postal_Code { get; set; }
        public string Guardian_ID_No { get; set; }
        public string Guardian_Full_Name { get; set; }
        public string Province { get; set; }
        public string Municipality { get; set; }
        public string Town { get; set; }
        public string Urban_Rural { get; set; }
        public string Tel_No { get; set; }
        public string Cell_No { get; set; }
        public string Email { get; set; }
        public string Occupational_Levels_For_Equity_Reporting_Purposes { get; set; }
        public string Job_Title { get; set; }
        public string OFO_Occupation_Code { get; set; }
        public string OFO_Specialisation { get; set; }
        public string OFO_Occupation { get; set; }
        public string Highest_School_Qualification { get; set; }
        public string Highest_Qualification { get; set; }
        public string Student_Enrolment_No { get; set; }
        public string Bursary_Academic_Year_of_Study { get; set; }
        public string Bursary_Completion_Status_Final_Year { get; set; }
        public string POPI_Act_Status { get; set; }
        public DateTime? POPI_Act_Status_Date { get; set; }
        public string Workplace_Legal_Name { get; set; }
        public string Provider_Legal_Name { get; set; }

        public string Status { get; set; }
        public string UploadStatus { get; set; }
        public string Comment { get; set; }
        public DateTime? DateCreated { get; set; }
        public int UserId { get; set; }
    }
}
