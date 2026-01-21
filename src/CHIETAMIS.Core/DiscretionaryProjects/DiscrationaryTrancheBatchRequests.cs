using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.DiscretionaryProjects
{
    [Table("tbl_Discrationary_Tranche_Batch_Requests")]
    public class DiscrationaryTrancheBatchRequests: Entity
    {
        public int TrancheBatchId { get; set; }
        public int RequestId { get; set; }
        public DateTime DateCreated { get; set; }
        public int UserId { get; set; }
    }
}
