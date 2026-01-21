using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.DiscretionaryTranches
{
    [Table("tbl_ApplicationBatch")]
    public class ApplicationBatch: Entity
    {
        public int ApplicationId {  get; set; }
        public string TrancheType { get; set; }
        public DateTime DateCreated { get; set; }
        public int UserId { get; set; }
    }
}
