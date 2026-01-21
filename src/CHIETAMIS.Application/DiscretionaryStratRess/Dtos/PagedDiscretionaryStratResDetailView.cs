using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.DiscretionaryStratRess.Dtos
{
    public class PagedDiscretionaryStratResDetailView
    {
        public DiscretionaryStratResDetailsView DiscretionaryStratResDetails { get; set; }
        public List<DiscretionaryStratResObjectivesDto> DiscretionaryStratResObjectives { get; set; }
    }
}
