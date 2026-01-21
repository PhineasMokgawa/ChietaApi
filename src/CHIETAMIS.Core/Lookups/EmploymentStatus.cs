using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Lookups
{
    [Table("lkp_EmploymentStatus")]
    public class EmploymentStatus: Entity
    {
        public string Employment_Status { get; set; }
    }
}
