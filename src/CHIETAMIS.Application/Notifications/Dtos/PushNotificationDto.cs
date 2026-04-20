using Abp.Application.Services.Dto;

namespace CHIETAMIS.Notifications.Dtos
{
    public class PushNotificationDto : EntityDto
    {
        public int UserId { get; set; }
        public string Token { get; set; }
    }
}
