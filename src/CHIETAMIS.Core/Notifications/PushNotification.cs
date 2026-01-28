using System.ComponentModel.DataAnnotations.Schema;

namespace CHIETAMIS.Notifications
{
    [Table("PushNotifications")]
    public class PushNotification
    {
        public int UserId { get; set; }
        public string Token { get; set; }
    }
}
