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

        // ==========================
        // CREATE / PUSH NOTIFICATIONS
        // ==========================

        // Create a notification in DB without pushing
        public async Task CreateNotificationAsync(CreateNotificationDto input)
        {
            if (input.UserId <= 0)
                throw new UserFriendlyException("Invalid UserId");

            var notification = new Notification
            {
                UserId = input.UserId,
                Title = input.Title,
                Message = input.Message,
                Source = input.Source ?? "SYSTEM",
                IsRead = false,
                IsPushSent = false,
                CreatedAt = DateTime.UtcNow
            };

            await _notificationRepository.InsertAsync(notification);
        }

        // Create and push a notification to all tokens for a user
        public async Task SendAndPushNotificationAsync(CreateNotificationDto input)
        {
            if (input.UserId <= 0)
                throw new UserFriendlyException("Invalid UserId");

            if (string.IsNullOrWhiteSpace(input.Title) || string.IsNullOrWhiteSpace(input.Message))
                throw new UserFriendlyException("Title and message are required");

            // Get all registered tokens for the user
            var tokens = await _pushNotificationRepository
                .GetAll()
                .Where(t => t.UserId == input.UserId)
                .Select(t => t.Token)
                .ToListAsync();

            if (!tokens.Any())
                throw new UserFriendlyException("No registered device tokens for this user.");

            // Insert a notification per token (simulate push)
            foreach (var token in tokens)
            {
                // Ensure token exists (redundant check)
                var tokenExists = await _pushNotificationRepository
                    .GetAll()
                    .AnyAsync(t => t.UserId == input.UserId && t.Token == token);

                if (!tokenExists)
                {
                    await _pushNotificationRepository.InsertAsync(new PushNotification
                    {
                        UserId = input.UserId,
                        Token = token
                    });
                }

                // Create the notification
                var notification = new Notification
                {
                    UserId = input.UserId,
                    Title = input.Title,
                    Message = input.Message,
                    Source = input.Source ?? "SYSTEM",
                    IsRead = false,
                    IsPushSent = true, // mark as pushed
                    CreatedAt = DateTime.UtcNow
                };

                await _notificationRepository.InsertAsync(notification);
            }
        }

        // ==========================
        // PUSH TOKEN MANAGEMENT
        // ==========================

        // Save a push token for a user
        public async Task CreateUserNotificationToken(PushNotificationDto request)
        {
            if (request.UserId <= 0)
                throw new UserFriendlyException("Invalid UserId");

            if (string.IsNullOrWhiteSpace(request.Token))
                throw new UserFriendlyException("Invalid push token");

            var exists = await _pushNotificationRepository
                .GetAll()
                .AnyAsync(x => x.UserId == request.UserId && x.Token == request.Token);

            if (!exists)
            {
                await _pushNotificationRepository.InsertAsync(new PushNotification
                {
                    UserId = request.UserId,
                    Token = request.Token
                });
            }
        }

        // ==========================
        // GET NOTIFICATIONS
        // ==========================

        // Get all notifications for a user
        public async Task<List<NotificationDto>> GetByUserAsync(int userId)
        {
            if (userId <= 0)
                throw new UserFriendlyException("Invalid UserId");

            return await _notificationRepository
                .GetAll()
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.CreatedAt)
                .Select(n => new NotificationDto
                {
                    Id = n.Id,
                    UserId = n.UserId,
                    Title = n.Title,
                    Message = n.Message,
                    IsRead = n.IsRead,
                    IsPushSent = n.IsPushSent,
                    CreatedAt = n.CreatedAt,
                    UpdatedAt = n.UpdatedAt
                })
                .ToListAsync();
        }

        // ==========================
        // UPDATE / MARK AS READ
        // ==========================

        // Update a notification
        public async Task UpdateNotificationAsync(UpdateNotificationDto input)
        {
            if (input.Id <= 0)
                throw new UserFriendlyException("Invalid notification Id.");

            var notification = await _notificationRepository.FirstOrDefaultAsync(input.Id);
            if (notification == null)
                throw new UserFriendlyException("Notification not found.");

            if (!string.IsNullOrWhiteSpace(input.Title))
                notification.Title = input.Title;

            if (!string.IsNullOrWhiteSpace(input.Message))
                notification.Message = input.Message;

            if (input.IsRead.HasValue)
                notification.IsRead = input.IsRead.Value;

            notification.UpdatedAt = DateTime.UtcNow;

            await CurrentUnitOfWork.SaveChangesAsync();
        }

        // Mark a notification as read
        public async Task MarkAsReadAsync(int notificationId)
        {
            var notification = await _notificationRepository.FirstOrDefaultAsync(notificationId);
            if (notification == null)
                throw new UserFriendlyException("Notification not found.");

            notification.IsRead = true;
            notification.UpdatedAt = DateTime.UtcNow;

            await CurrentUnitOfWork.SaveChangesAsync();
        }

        // ==========================
        // DELETE NOTIFICATION
        // ==========================

        // Delete a notification
        public async Task DeleteNotificationAsync(int notificationId)
        {
            var notification = await _notificationRepository.FirstOrDefaultAsync(notificationId);
            if (notification == null)
                throw new UserFriendlyException("Notification not found.");

            await _notificationRepository.DeleteAsync(notification);
        }
    }
}
