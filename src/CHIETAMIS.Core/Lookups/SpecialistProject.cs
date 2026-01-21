using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Lookups
{
    [Table("lkp_Specialist_Project")]
    public class SpecialistProject: Entity
    {
        public int UserId { get; set; }
        public int ProjectTypeId { get; set; }
    }
}
