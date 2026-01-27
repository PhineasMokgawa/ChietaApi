namespace CHIETAMIS.Notifications;

public class PushNotificationRegisterDto
{
    public int UserId { get; set; }

    public string Token { get; set; } = string.Empty;
}
