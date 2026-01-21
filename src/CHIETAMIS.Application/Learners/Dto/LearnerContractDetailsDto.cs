using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Learners.Dto
{
    public class LearnerContractDetailsDto: EntityDto
    {
        public string ID_Number { get; set; }
        public string Passport_No { get; set; }
        public int ApplicationId { get; set; }
        public int BatchId { get; set; }
        public string Passport_Number { get; set; }
        public string First_Name { get; set; }
        public string Middle_Name { get; set; }
        public string Last_Name { get; set; }
        public string Email { get; set; }
        public string Province { get; set; }
        public int FocusAreaId { get; set; }
        public string FocusArea { get; set; }
        public int SubCategoryId { get; set; }
        public string SubCategory { get; set; }
        public int InterventionId { get; set; }
        public string Intervention { get; set; }
        public string Contract_Number { get; set; }
        public string Status { get; set; }
        public string UploadStatus { get; set; }
        public string LearnerStatus { get; set; }
        public decimal? Amount { get; set; }
        public string Workplace_Legal_Name { get; set; }
        public string Provider_Legal_Name { get; set; }
        public int Id { get; set; }
    }
}
