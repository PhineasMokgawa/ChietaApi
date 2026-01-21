using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.MandatoryGrants.Dtos
{
    public class MGStatementDto
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public string Batch_No { get; set; }
        public decimal Levies_Paid { get; set; }
        public decimal Mg_Grant {  get; set; }
        public decimal Mg_Grant_Paid { get; set; }

    }
}
