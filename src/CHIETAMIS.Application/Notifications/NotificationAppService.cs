using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
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
        /// Create notification for a user
        /// </summary>
        public async Task CreateNotificationAsync(Dtos.CreateNotificationDto input)
        {
            if (!AbpSession.UserId.HasValue)
            {
                throw new AbpAuthorizationException("User must be logged in");
            }

            var notification = new Notification
            {
                Title = input.Title,
                Body = input.Body,
                Data = input.Data,
                Source = input.Source,
                Read = false,
                Timestamp = DateTime.Now,
                UserId = (int)AbpSession.UserId.Value
            };

            await _notificationRepository.InsertAsync(notification);
        }

        /// <summary>
        /// Get notifications for a user
        /// </summary>
        public async Task<List<Dtos.NotificationDto>> GetByUserAsync(int userId)
        {
            return await _notificationRepository
                .GetAll()
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.Timestamp)
                .Select(n => new Dtos.NotificationDto
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
        /// Mark notification as read
        /// </summary>
        public async Task MarkAsReadAsync(int notificationId)
        {
            var notification = await _notificationRepository.GetAsync(notificationId);
            notification.Read = true;

            await CurrentUnitOfWork.SaveChangesAsync();
        }
    }
}
