using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.DiscretionaryStratRess.Dtos
{
    public class DiscretionaryGECRApprovalForView
    {
        public int ApplicationId { get; set; }
        public string ApprovalType { get; set; }
        public string ApprovalStatus { get; set; }
        public string Comments { get; set; }
        public DateTime DateCreated { get; set; }
        public int UserId { get; set; }
        public DateTime DteUpd { get; set; }
        public int UserUpd { get; set; }
    }
}
