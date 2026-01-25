using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.DiscretionaryProjects.Dtos
{
    // For changing project status
    public class ChangeProjectStatusDto
    {
        public int ProjectId { get; set; }
        public int NewStatusId { get; set; }
        public string ChangedBy { get; set; } = null!; // user who changed the status
    }
}
