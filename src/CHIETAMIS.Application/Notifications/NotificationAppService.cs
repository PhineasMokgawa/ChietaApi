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

        public NotificationAppService(IRepository<Notification, int> notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        /// <summary>
        /// Create notification for any user
        /// </summary>
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

        /// <summary>
        /// Get all notifications for a specific user
        /// </summary>
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
        public async Task UpdateNotificationAsync(UpdateNotificationDto input)
        {
            if (input.Id <= 0)
                throw new UserFriendlyException("Invalid notification Id.");

            var notification = await _notificationRepository.FirstOrDefaultAsync(input.Id);
            if (notification == null)
                throw new UserFriendlyException("Notification not found.");

            // Update only fields that are provided
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

        /// <summary>
        /// Mark a notification as read
        /// </summary>
        public async Task MarkAsReadAsync(int notificationId)
        {
            var notification = await _notificationRepository.GetAsync(notificationId);
            if (notification == null)
                throw new UserFriendlyException("Notification not found.");

            notification.Read = true;
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        /// <summary>
        /// Get unread notifications for a specific user
        /// </summary>
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

        /// <summary>
        /// Delete a notification by Id
        /// </summary>
        public async Task DeleteNotificationAsync(int notificationId)
        {
            var notification = await _notificationRepository.FirstOrDefaultAsync(notificationId);
            if (notification == null)
                throw new UserFriendlyException("Notification not found.");

            await _notificationRepository.DeleteAsync(notification);
        }

        public async Task<int> GetUnreadCountByUserAsync(int userId)
        {
            if (userId <= 0)
                throw new UserFriendlyException("Invalid UserId");
            return await _notificationRepository
                .GetAll()
                .Where(n => n.UserId == userId && !n.Read)
                .CountAsync();
        }

        public async Task CreateUserNotificationToken(PushNotificationRegisterDto request)
        {
            if(request.UserId <= 0)
                throw new UserFriendlyException("Invalid UserId");

            // Implementation for storing the push notification token
            
        }
    }
}