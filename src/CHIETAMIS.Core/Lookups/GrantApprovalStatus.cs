using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace CHIETAMIS.Lookups
{
    [Table("lkp_Grant_Approval_Status")]
    public class GrantApprovalStatus: Entity
    {
        public GrantApprovalStatus() { }

        public int Id { get; set; }
        public string GrantStatusDescription { get; set; }
    }
}
