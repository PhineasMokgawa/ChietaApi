using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Notifications
{
<<<<<<< HEAD
    [Table("tbl_Notifications")]
    public class Notification : Entity
    {
       
        public int UserId { get; set; }

        public string Title { get; set; } = null!;
        public string Body { get; set; } = null!;
        public string? Data { get; set; }

        public DateTime Timestamp { get; set; }
        public bool Read { get; set; }

        public string Source { get; set; } = null!;
    }

=======
        [Table("tbl_GeneralNotifications")]
        public class GeneralNotification : Entity<string> // string Id
        {
            public string Title { get; set; } = null!;      // Notification title
            public string Body { get; set; } = null!;       // Notification body
            public string? DataJson { get; set; }           // Optional JSON for extra info
            public long Timestamp { get; set; }             // Unix timestamp
            public bool Read { get; set; } = false;         // Read/unread
            public string Source { get; set; } = "local";   // 'local' or 'push'

            public GeneralNotification()
            {
                Id = Guid.NewGuid().ToString();
                Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            }
        }
>>>>>>> 2f4722d0c92878c10b768af41989fa0b63173151
}
