using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Learners
{
    public class ProjectLearner: Entity
    {
        public int ApplicationId { get; set; }
        public int LearnerId { get; set; }
        public int WorkplaceId { get; set; }
        public int ProviderId { get; set; }
        public int UserId { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
