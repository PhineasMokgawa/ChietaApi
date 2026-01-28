using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace CHIETAMIS.Notifications
{
    [Table("tbl_PushNotifications")]
    public class PushNotification : Entity<int>
    {
        public int UserId { get; set; }
        public string Token { get; set; }
    }
}
