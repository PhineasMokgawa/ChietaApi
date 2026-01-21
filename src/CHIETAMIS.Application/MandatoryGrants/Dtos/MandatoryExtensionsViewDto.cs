using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.MandatoryGrants.Dtos
{
    public class MandatoryExtensionsViewDto
    {
        public int Id { get; set; }
        public string OrganisationSDL { get; set; }
        public string Organisation_Name { get; set; }
        public string Organisation_Trading_Name { get; set; }
        public string Province { get; set; }
        public string Region { get; set; }
        public string GrantStatus { get; set; }
        public DateTime DateRequested { get; set; }
        public DateTime? SubmissionDte { get; set; }
        public string Reason { get; set; }
        public string ReferenceNo { get; set; }
        public string Description { get; set; }
        public DateTime? ClosingDate { get; set; }
        public string RSA { get; set; }

    }
}
