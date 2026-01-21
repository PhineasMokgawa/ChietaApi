using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Lookups
{
    [Table("lkp_Region_Managers")]
    public class RegionRM: Entity
    {
        public int UserId { get; set; }
        public int RegionId { get; set; }
        public string ManagerName { get; set; }

    }
}
