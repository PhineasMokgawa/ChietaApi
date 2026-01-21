using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.DiscretionaryProjects
{
    [Table("tbl_Discretionary_Tranche_Batch")]
    public class DiscretionaryTrancheBatch: Entity
    {
        public string Description { get; set; }
        public string BatchNumber { get; set; }
        public string Status { get; set; }
        public DateTime DateCreated { get; set; }
        public int UserId { get; set; }
        public DateTime? DteUpd { get; set; }
        public int? UsrUpd { get; set; }

    }
}
