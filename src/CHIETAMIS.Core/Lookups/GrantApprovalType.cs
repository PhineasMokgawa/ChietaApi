using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace CHIETAMIS.Lookups
{
    [Table("lkp_Grant_Approval_Types")]
    public class GrantApprovalType: Entity
    {
        public GrantApprovalType() { }

        public int Id { get; set; }
        public string ApprovalDescription { get; set; }
    }
}
