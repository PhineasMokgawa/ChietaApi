using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.DiscretionaryProjects.Dtos
{
    public class CreateProjectNotificationDto
    {
        public int ProjectId { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; } = "";
    }

}
