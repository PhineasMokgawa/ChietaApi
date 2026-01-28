using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Notifications
{
    [Table("tbl_Notifications")]
    public class Notification : Entity<int>
    {

        public int UserId { get; set; }
        public string Title { get; set; } = null!;
        public string Message { get; set; } = null!; // was Body
        public bool IsRead { get; set; } = false; // was Read
        public bool IsPushSent { get; set; } = false; // optional
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // was Timestamp
        public DateTime? UpdatedAt { get; set; }
    }

}
