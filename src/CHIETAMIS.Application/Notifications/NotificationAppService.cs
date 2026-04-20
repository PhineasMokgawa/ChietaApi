// CHIETAMIS/Notifications/NotificationAppService.cs
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Timing;
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
        // CREATE NOTIFICATIONS (Single + Multiple Users)
        // ==========================

        public async Task CreateNotificationAsync(CreateNotificationDto input)
        {
            var targetUserIds = ResolveUserIds(input.UserId, input.UserIds);

            if (!targetUserIds.Any())
                throw new UserFriendlyException("No valid UserIds provided.");

            foreach (var userId in targetUserIds)
            {
                await _notificationRepository.InsertAsync(new Notification
                {
                    UserId = userId,
                    Title = input.Title?.Trim() ?? string.Empty,
                    Message = input.Message?.Trim() ?? string.Empty,
                    Source = !string.IsNullOrWhiteSpace(input.Source) ? input.Source.Trim() : "SYSTEM",
                    IsRead = false,
                    IsPushSent = false,
                    CreatedAt = Clock.Now,
                    UpdatedAt = null
                    // ✅ Id is NOT set → EF Core performs INSERT, not UPDATE
                });
            }
        }

        // ==========================
        // CREATE + PUSH NOTIFICATIONS (Single + Multiple Users)
        // ==========================

        public async Task SendAndPushNotificationAsync(CreateNotificationDto input)
        {
            var targetUserIds = ResolveUserIds(input.UserId, input.UserIds);

            if (!targetUserIds.Any())
                throw new UserFriendlyException("No valid UserIds provided.");

            if (string.IsNullOrWhiteSpace(input.Title) || string.IsNullOrWhiteSpace(input.Message))
                throw new UserFriendlyException("Title and message are required");

            // Fetch all tokens for target users in a single efficient query
            var tokensByUser = await _pushNotificationRepository
                .GetAll()
                .Where(t => targetUserIds.Contains(t.UserId))
                .Select(t => new { t.UserId, t.Token })
                .ToListAsync();

            if (!tokensByUser.Any())
                throw new UserFriendlyException("No registered device tokens for the specified users.");

            var processed = new HashSet<(int, string)>(); // Track (UserId, Token) to avoid duplicates

            foreach (var userId in targetUserIds)
            {
                var userTokens = tokensByUser
                    .Where(t => t.UserId == userId)
                    .Select(t => t.Token)
                    .Distinct();

                foreach (var token in userTokens)
                {
                    if (processed.Add((userId, token)))
                    {
                        await _notificationRepository.InsertAsync(new Notification
                        {
                            UserId = userId,
                            Title = input.Title.Trim(),
                            Message = input.Message.Trim(),
                            Source = !string.IsNullOrWhiteSpace(input.Source) ? input.Source.Trim() : "SYSTEM",
                            IsRead = false,
                            IsPushSent = true,
                            CreatedAt = Clock.Now,
                            UpdatedAt = null
                        });
                    }
                }
            }

            // TODO: Integrate with Firebase/APNS here to actually send push notifications
            // Example: await _pushService.SendAsync(tokensByUser.Select(t => t.Token).Distinct(), input.Title, input.Message);
        }

        // ==========================
        // HELPER: Resolve UserIds (Single + Multiple)
        // ==========================

        private List<int> ResolveUserIds(int singleUserId, IEnumerable<int> multipleUserIds)
        {
            var result = new HashSet<int>(); // Auto-deduplicate

            if (singleUserId > 0)
                result.Add(singleUserId);

            if (multipleUserIds?.Any() == true)
            {
                foreach (var id in multipleUserIds.Where(id => id > 0))
                    result.Add(id);
            }

            return result.ToList();
        }

        // ==========================
        // PUSH TOKEN MANAGEMENT
        // ==========================

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
                    Token = request.Token,
                    CreatedAt = Clock.Now
                });
            }
        }

        // ==========================
        // GET NOTIFICATIONS
        // ==========================

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

            notification.UpdatedAt = Clock.Now;

            await CurrentUnitOfWork.SaveChangesAsync();
        }

        public async Task MarkAsReadAsync(int notificationId)
        {
            var notification = await _notificationRepository.FirstOrDefaultAsync(notificationId);
            if (notification == null)
                throw new UserFriendlyException("Notification not found.");

            notification.IsRead = true;
            notification.UpdatedAt = Clock.Now;

            await CurrentUnitOfWork.SaveChangesAsync();
        }

        // ==========================
        // DELETE NOTIFICATION
        // ==========================

        public async Task DeleteNotificationAsync(int notificationId)
        {
            var notification = await _notificationRepository.FirstOrDefaultAsync(notificationId);
            if (notification == null)
                throw new UserFriendlyException("Notification not found.");

            await _notificationRepository.DeleteAsync(notification);
        }
    }
}