using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace CHIETAMIS.Lookups
{
    [Table("lkp_Gender_Code")]
    public class Gender : Entity
    {
        public Gender() { }
        public string Description { get; set; }
        public string Gender_Code { get; set; }
    
   }
}
