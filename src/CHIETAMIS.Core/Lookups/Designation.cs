using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace CHIETAMIS.Lookups
{
    [Table("lkp_sdf_designation")]
    public class Designation: Entity
    {
        public Designation() { }
        public string Designation_Name { get; set; }
        public Int16 Designation_Code { get; set; }
    }
}
