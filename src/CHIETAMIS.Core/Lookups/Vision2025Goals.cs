using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace CHIETAMIS.Lookups
{
    [Table("lkp_Vision2025Goals")]
    public class Vision2025Goals: Entity
    {
        public Vision2025Goals() { }
        public string Goal { get; set; }
    }
}
