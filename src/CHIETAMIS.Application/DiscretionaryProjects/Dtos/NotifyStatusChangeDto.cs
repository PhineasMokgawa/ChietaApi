using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.DiscretionaryProjects.Dtos
{
    public class NotifyStatusChangeDto
    {
        public int ProjectId { get; set; }
        public int NewStatusId { get; set; }
        public string UserId { get; set; } = null!;
    }
}
