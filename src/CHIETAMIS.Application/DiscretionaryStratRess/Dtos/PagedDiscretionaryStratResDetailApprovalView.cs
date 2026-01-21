using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.DiscretionaryStratRess.Dtos
{
    public class PagedDiscretionaryStratResDetailApprovalView
    {
        public DiscretionaryStratResDetailsApprovalView DiscretionaryStratResDetailsApproval { get; set; }
        public List<DiscretionaryStratResObjectivesApprovalDto> DiscretionaryStratResObjectivesApproval { get; set; }
    }
}
