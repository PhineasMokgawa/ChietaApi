using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.UI;
using CHIETAMIS.Notifications.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CHIETAMIS.Notifications
{
    public class NotificationAppService : ApplicationService
    {
        private readonly IRepository<Notification, int> _notificationRepository;
        private readonly IRepository<PushNotification, int> _pushNotificationRepository;

        public NotificationAppService(
            IRepository<Notification, int> notificationRepository,
            IRepository<PushNotification, int> pushNotificationRepository)
        {
            _notificationRepository = notificationRepository;
            _pushNotificationRepository = pushNotificationRepository;
        }

        ///  Create notification
        public async Task CreateNotificationAsync(CreateNotificationDto input)
        {
            if (input.UserId <= 0)
                throw new UserFriendlyException("Invalid UserId");

            var notification = new Notification
            {
                UserId = input.UserId,
                Title = input.Title,
                Body = input.Body,
                Data = input.Data,
                Source = input.Source,
                Read = false,
                Timestamp = DateTime.UtcNow
            };

            await _notificationRepository.InsertAsync(notification);
        }

        ///  Get all notifications for a user
        public async Task<List<NotificationDto>> GetByUserAsync(int userId)
        {
            if (userId <= 0)
                throw new UserFriendlyException("Invalid UserId");

            return await _notificationRepository
                .GetAll()
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.Timestamp)
                .Select(n => new NotificationDto
                {
                    Id = n.Id,
                    Title = n.Title,
                    Body = n.Body,
                    Read = n.Read,
                    Timestamp = n.Timestamp,
                    Source = n.Source
                })
                .ToListAsync();
        }

        ///  Update notification
        public async Task UpdateNotificationAsync(UpdateNotificationDto input)
        {
            if (input.Id <= 0)
                throw new UserFriendlyException("Invalid notification Id.");

            var notification = await _notificationRepository.FirstOrDefaultAsync(input.Id);
            if (notification == null)
                throw new UserFriendlyException("Notification not found.");

            if (!string.IsNullOrWhiteSpace(input.Title))
                notification.Title = input.Title;

            if (!string.IsNullOrWhiteSpace(input.Body))
                notification.Body = input.Body;

            if (!string.IsNullOrWhiteSpace(input.Data))
                notification.Data = input.Data;

            if (!string.IsNullOrWhiteSpace(input.Source))
                notification.Source = input.Source;

            if (input.Read.HasValue)
                notification.Read = input.Read.Value;

            await CurrentUnitOfWork.SaveChangesAsync();
        }

        ///  Mark as read
        public async Task MarkAsReadAsync(int notificationId)
        {
            var notification = await _notificationRepository.FirstOrDefaultAsync(notificationId);
            if (notification == null)
                throw new UserFriendlyException("Notification not found.");

            notification.Read = true;
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        ///  Get unread notifications
        public async Task<List<NotificationDto>> GetUnreadByUserAsync(int userId)
        {
            if (userId <= 0)
                throw new UserFriendlyException("Invalid UserId");

            return await _notificationRepository
                .GetAll()
                .Where(n => n.UserId == userId && !n.Read)
                .OrderByDescending(n => n.Timestamp)
                .Select(n => new NotificationDto
                {
                    Id = n.Id,
                    Title = n.Title,
                    Body = n.Body,
                    Read = n.Read,
                    Timestamp = n.Timestamp,
                    Source = n.Source
                })
                .ToListAsync();
        }

        ///  Delete notification
        public async Task DeleteNotificationAsync(int notificationId)
        {
            var notification = await _notificationRepository.FirstOrDefaultAsync(notificationId);
            if (notification == null)
                throw new UserFriendlyException("Notification not found.");

            await _notificationRepository.DeleteAsync(notification);
        }

        ///  Unread count
        public async Task<int> GetUnreadCountByUserAsync(int userId)
        {
            if (userId <= 0)
                throw new UserFriendlyException("Invalid UserId");

            return await _notificationRepository
                .GetAll()
                .CountAsync(n => n.UserId == userId && !n.Read);
        }

        ///  Register push notification token
        public async Task CreateUserNotificationToken(PushNotificationDto request)
        {
            if (request.UserId <= 0)
                throw new UserFriendlyException("Invalid UserId");

            if (string.IsNullOrWhiteSpace(request.Token))
                throw new UserFriendlyException("Invalid push token");

            var exists = await _pushNotificationRepository
                .GetAll()
                .AnyAsync(x =>
                    x.UserId == request.UserId &&
                    x.Token == request.Token);

            if (!exists)
            {
                await _pushNotificationRepository.InsertAsync(new PushNotification
                {
                    UserId = request.UserId,
                    Token = request.Token
                });
            }
        }
    }
}
