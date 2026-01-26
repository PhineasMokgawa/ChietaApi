using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using CHIETAMIS.DiscretionaryProjects;
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

        public NotificationAppService(
            IRepository<Notification, int> notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        /// <summary>
        /// PUT: Create notification for a user
        /// </summary>
        public async Task CreateNotificationAsync(CreateNotificationDto input)
        {
            var notification = new Notification
            {
                UserId = input.UserId,
                Title = input.Title,
                Body = input.Body,
                Data = input.Data,
                Source = input.Source,
                Timestamp =DateTime.Now,
                Read = false
            };

            await _notificationRepository.InsertAsync(notification);
        }

        /// <summary>
        /// GET: Get notifications for a user
        /// </summary>
        public async Task<List<NotificationDto>> GetByUserAsync(int userId)
        {
            var notifications = await _notificationRepository
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

            return notifications;
        }

        /// <summary>
        /// PUT: Mark notification as read
        /// </summary>
        public async Task MarkAsReadAsync(int notificationId)
        {
            var notification = await _notificationRepository.GetAsync(notificationId);
            notification.Read = true;
        }
    }
}
