using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Lookups
{
    [Table("lkp_CompanySizes")]
    public class CompanySizes : Entity<int>
    {
        public int Code { get; set; }
        public string Size { get; set; }
    }
}
