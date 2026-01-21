using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.DiscretionaryProjects.Dtos
{
    public class BatchApprovalRequestsDto
    {
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public int RequestId { get; set; }
        public int TrancheId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Learners { get; set; }
        public decimal Cost { get; set; }
        public DateTime DateRequested { get; set; }
        public string Province { get; set; }
    }
}
