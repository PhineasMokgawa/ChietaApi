using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.DiscretionaryProjects
{
    [Table("tbl_ProjectNotifications")]
    public class ProjectNotification : Entity
    {
        public int ProjectId { get; set; }  // The project this notification belongs to

        public int UserId { get; set; }  // The user to notify

        public string Message { get; set; } = null!; // Notification message

        public DateTime CreatedDate { get; set; } = DateTime.Now; // Timestamp
        public bool IsRead { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
