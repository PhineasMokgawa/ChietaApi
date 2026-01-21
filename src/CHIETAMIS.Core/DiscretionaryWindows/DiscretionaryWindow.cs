using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CHIETAMIS.DiscretionaryWindows
{
    [Table("tbl_Discretionary_Grant_Window")]
    public class DiscretionaryWindow: Entity
    {
        public string ProgCd { get; set; }
        public string Reference { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public DateTime LaunchDte { get; set; }
        public DateTime DeadlineTime { get; set; }
        public decimal TotBdgt { get; set; }
        public DateTime? ContractStartDate { get; set; }
        public DateTime? ContractEndDate { get; set; }
        public bool ActiveYN { get; set; }
        public DateTime DteUpd { get; set; }
        public int UsrUpd { get; set; }
        public DateTime DteCreated { get; set; }
    }
}
