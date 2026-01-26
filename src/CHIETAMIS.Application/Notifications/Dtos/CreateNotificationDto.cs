using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Notifications.Dtos
{
    public class CreateNotificationDto
    {
       
        public string Title { get; set; } = null!;
        public string Body { get; set; } = null!;
        public string? Data { get; set; }
        public string Source { get; set; } = null!;
        public int UserId { get; set; }
    }
}
