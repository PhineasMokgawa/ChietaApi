using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Organisations.Dtos
{
    public class GetOrganisationsForViewDto
    {
        public int OrgSdfId { get; set; }
        public int StatusId { get; set; }
        public OrganisationDto Organisation { get; set; }
    }
}
