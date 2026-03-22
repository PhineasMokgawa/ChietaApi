using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Notifications
{
    /// <summary>
    /// Mobile Notification entity for push notifications to mobile devices.
    /// Separate from web notifications to maintain clean separation of concerns.
    /// </summary>
    [Table("tbl_mobile_notifications")]
    public class Notification : Entity<int>
    {
        public int UserId { get; set; }

        public string Title { get; set; } = null!;

        public string Message { get; set; } = null!;

        public bool IsRead { get; set; } = false;

        public bool IsPushSent { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        public string Source { get; set; } = null!;
    }

}
