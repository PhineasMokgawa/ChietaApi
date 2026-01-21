using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.DiscretionaryProjects.Dtos
{
    public class DiscretionaryProjectOutputDto: EntityDto
    {
        public string Title { get; set; }   
        public string ProjType { get; set; }
        public string FocusArea { get; set; }
        public int FundingLimit { get; set; }
        public string SubCategory { get; set; }
        public int ProjectId { get; set; }
        public int ApplicationId { get; set; }
        public int OrganisationId { get; set; }
        public string ProjectStatus { get; set; }
        public DateTime? ProjectStatDte { get; set; }
        public DateTime? ProjectEndDate { get; set; }
        public string ProjShortNam { get; set; }
        public string ProjectNam { get; set; }
        public DateTime? SubmissionDte { get; set; }
        public string SDLNo { get; internal set; }
        public string Organisation_Name { get; internal set; }
        public int SdfId { get; internal set; }
        public int? RSAId { get; internal set; }
        public int WindowId { get; internal set; }
        public DateTime? ContractStartDate { get; internal set; }
        public DateTime? ContractEndDate { get; internal set; }
    }
}
