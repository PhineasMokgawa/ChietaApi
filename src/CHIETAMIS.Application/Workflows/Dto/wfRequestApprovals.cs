using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Workflows.Dto
{
    public class wfRequestApprovals
    {
        public string Status { get; set; }
        public string User { get; set; }
        public DateTime? DateActioned { get; set; }
    }
}
