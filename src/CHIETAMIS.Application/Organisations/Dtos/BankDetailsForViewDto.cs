using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Organisations.Dtos
{
    public class BankDetailsForViewDto
    {
        public BankDetailsDto BankDetails { get; set; }
        public string BankName { get; set; }
        public string Account_Type { get; set; }
    }
}
