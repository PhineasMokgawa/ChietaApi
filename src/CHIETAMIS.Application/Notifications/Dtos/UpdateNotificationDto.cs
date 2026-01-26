using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Notifications.Dtos
{
    public class UpdateNotificationDto
    {
        public int Id { get; set; }          // Notification to update
        public string Title { get; set; }    // Optional: new title
        public string Body { get; set; }     // Optional: new body
        public string Data { get; set; }     // Optional: new data
        public string Source { get; set; }   // Optional: new source
        public bool? Read { get; set; }      // Optional: mark read/unread
    }
}
