using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Dashboards.Dtos
{
    public class GrantRSASummaries
    {
        public string RSA { get; set; }
        public string StatusDescription { get; set; }
        public int Apps { get; set; }
        public int Learners { get; set; }
        public decimal TotalBudget { get; set; }
        public int HDI { get; set; }
        public int Female { get; set; }
        public int Youth { get; set; }
        public int Number_Disabled { get; set; }
        public int Rural { get; set; }
    }
}
