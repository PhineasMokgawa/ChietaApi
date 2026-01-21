using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Sdfs.Dtos
{
    public class SdfDetailInput
    {
        public int personId { get; set; }
        public int userId { get; set; }
        public Int16 designation { get; set; }
        public DateTime dateCreated { get; set; }
        public int status { get; set; }
        public DateTime statusDate { get; set; }
        public int statusUser { get; set; }
    }
}
