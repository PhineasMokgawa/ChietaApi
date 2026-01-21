using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Lookups
{
    [Table("lkp_SubPlace")]
    public class SubPlace: Entity
    {
        public int SP_CODE { get; set; }
        public string SP_NAME { get; set; }
        public int MN_CODE { get; set; }
        public string MN_NAME { get;set; }
        public int DC_MN_C { get; set; }
        public string DC_NAME { get; set; }
        public int PR_CODE { get; set; }
        public string PR_NAME { get; set;}
    }
}
