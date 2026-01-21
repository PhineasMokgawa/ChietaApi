using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Lesedis
{
    [Table("lkp_Discretionary_Lesedi_Qualification")]
    public class Lesedi_Qualification: Entity
    {
        public string QualificationName { get; set; }
    }
}
