using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Commons.DTOs
{
    public class LazyFilter
    {
        public string FieldName { get; set; }
        public string MatchMode { get; set; }
        public string Value { get; set; }
        public int First { get; set; } 
        public int Rows { get; set; }

    }
}
