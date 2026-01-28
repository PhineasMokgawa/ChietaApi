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
        
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Body { get; set; } = null!;
        public bool Read { get; set; }
        public DateTime Timestamp { get; set; }
        public string Source { get; set; } = null!;
<<<<<<< HEAD
        public int UserId { get; set; }
=======
        
>>>>>>> e38fb8d499e8ab88faf5e7471e671baa65525e6b
    }

}
