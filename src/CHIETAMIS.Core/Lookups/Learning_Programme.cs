using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Lookups
{
    [Table("lkp_Learning_Programme")]
    public class Learning_Programme: Entity
    {
        public string Learning_Programmes { get; set; }
    }
}
