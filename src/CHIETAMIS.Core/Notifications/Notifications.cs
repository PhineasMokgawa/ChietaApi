using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Notifications
{
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
}
