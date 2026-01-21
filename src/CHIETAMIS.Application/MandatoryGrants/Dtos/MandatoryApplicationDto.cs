using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.MandatoryGrants.Dtos
{
    public class MandatoryApplicationDto: EntityDto
    {
        public int Id { get; set; }
        public int GrantWindowId { get; set; }
        public int OrganisationId { get; set; }
        public int GrantStatusID { get; set; }
        public string ReferenceNo { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public DateTime DteCreated { get; set; }
        public DateTime CaptureDte { get; set; }
        public DateTime? SubmissionDte { get; set; }
        public int? UserSubmitted { get; set; }
        public int? RSAId { get; set; }
        public int? RMId { get; set; }
        public bool? SubmittedPrevious { get; set; }
        public int? PreviousSubmissionId { get; set; }
        public DateTime? DteUpd { get; set; }
        public int? UsrUpd { get; set; }
    }
}
