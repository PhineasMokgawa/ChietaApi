using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Workflows.Dto
{
    public class wfRequestComments
    {
        public string Comment { get; set; }
        public string User { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
