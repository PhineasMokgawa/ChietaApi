using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.MandatoryGrants.Dtos
{
    public class MandatoryApplicationViewDto
    {
        public int Id { get; set; }
        public int GrantWindowId { get; set; }
        public int OrganisationId { get; set; }
        public string OrganisationSDL { get; set; }
        public string Organisation_Name { get; set; }
        public string Organisation_Trading_Name { get; set; }
        public string Province { get; set; }
        public string Region { get; set; }
        public string GrantStatus { get; set; }
        public string ReferenceNo { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public DateTime DteCreated { get; set; }
        public DateTime CaptureDte { get; set; }
        public DateTime? SubmissionDte { get; set; }
        public DateTime ClosingDate { get; set; }
        public int RSAId { get; set; }
        public string RSA {  get; set; }
        public int RMId { get; set; } 
        public bool SubmittedPrevious { get; set; }
    }
}
