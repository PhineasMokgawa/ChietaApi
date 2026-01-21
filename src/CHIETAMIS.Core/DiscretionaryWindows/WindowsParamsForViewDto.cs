using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.DiscretionaryWindows
{
    public  class WindowsParamsForViewDto
    {
        public int Id { get; set; }
        public string ProgCd { get; set; }
        public string Reference { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public DateTime LaunchDte { get; set; }
        public DateTime DeadlineTime { get; set; }
        public decimal TotBdgt { get; set; }
        public bool ActiveYN { get; set; }
        public DateTime DteUpd { get; set; }
        public int UsrUpd { get; set; }
        public DateTime DteCreated { get; set; }
    }
}
