using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.MandatoryGrants
{
    [Table("lkp_Mandatory_Approval_Status")]
    public class MandatoryApprovalStatus: Entity
    {
        public string StatusDescription { get; set; }
    }
}
