using Abp.Application.Services;
using CHIETAMIS.Notifications.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CHIETAMIS.Notifications
{
    public interface INotificationAppService : IApplicationService
    {
        Task CreateNotificationAsync(CreateNotificationDto input);
        Task SendAndPushNotificationAsync(CreateNotificationDto input);
        Task CreateUserNotificationToken(PushNotificationDto request);
        Task<List<NotificationDto>> GetByUserAsync(int userId);
        Task UpdateNotificationAsync(UpdateNotificationDto input);
        Task MarkAsReadAsync(int notificationId);
        Task DeleteNotificationAsync(int notificationId);
    }
}
