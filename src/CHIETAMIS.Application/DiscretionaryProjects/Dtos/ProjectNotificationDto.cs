using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.DiscretionaryProjects.Dtos
{
    public class ProjectNotificationDto
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; } = "";
        public int UserId { get; set; }
        public string Message { get; set; } = "";
        public DateTime CreatedDate { get; set; }
    }
}
