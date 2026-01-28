using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Notifications.Dtos
{
    public class NotificationDto : EntityDto<int>
    {

        public int UserId { get; set; }
        public string Title { get; set; } = null!;
        public string Message { get; set; } = null!;  // maps to DB Message
        public bool IsRead { get; set; }              // maps to DB IsRead
        public bool IsPushSent { get; set; }          // maps to DB IsPushSent
        public DateTime CreatedAt { get; set; }       // maps to DB CreatedAt
        public DateTime? UpdatedAt { get; set; }      // maps to DB UpdatedAt
    }

}
