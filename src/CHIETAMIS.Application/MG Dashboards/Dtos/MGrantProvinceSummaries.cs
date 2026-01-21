using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.MG_Dashboards.Dtos
{
    public class MGrantProvinceSummaries
    {
        public string Province { get; set; }
        public string StatusDescription { get; set; }
        public int Apps { get; set; }
        public int Learners { get; set; }

    }
}
