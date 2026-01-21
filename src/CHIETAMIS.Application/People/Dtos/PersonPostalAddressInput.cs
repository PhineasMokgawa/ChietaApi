using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.People.Dtos
{
    public class PersonPostalAddressInput
    {
        public int personId { get; set; }
        public string addressline1 { get; set; }
        public string addressline2 { get; set; }
        public string suburb { get; set; }
        public string area { get; set; }
        public string district { get; set; }
        public string municipality { get; set; }
        public string province { get; set; }
        public string postcode { get; set; }
    }
}
