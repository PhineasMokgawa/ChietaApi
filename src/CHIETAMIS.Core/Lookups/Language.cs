using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace CHIETAMIS.Lookups
{
    [Table("Ikp_Home_Language_Code")]
    public class Language : Entity
    {
        public Language() { }
        public string Description { get; set; }
        public string Home_Language_Code { get; set; }
    }
}
