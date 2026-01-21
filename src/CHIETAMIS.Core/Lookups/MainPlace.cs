using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Lookups
{
    [Table("lkp_MainPlace")]
    public class MainPlace : Entity
    {
        public MainPlace() { }
        public string MP_NAME { get; set; }
        public string MN_NAME { get; set; }
        public string DC_NAME { get; set; }
        public string PR_NAME { get; set; }
    }
}
