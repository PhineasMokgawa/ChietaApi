using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Notifications.Dtos
{
    public class CreateNotificationDto
    {

        public int UserId { get; set; }
        public string Title { get; set; } = null!;
        public string Message { get; set; } = null!; // maps to DB Message
    }
}
