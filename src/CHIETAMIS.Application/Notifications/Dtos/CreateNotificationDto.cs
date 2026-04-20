using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace CHIETAMIS.Notifications.Dtos
{
    public class CreateNotificationDto
    {

        /// <summary>
        /// Single user ID (for backward compatibility)
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Multiple user IDs (new feature - takes precedence if provided)
        /// </summary>

        public IEnumerable<int> UserIds { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        [StringLength(1000)]
        public string Message { get; set; }

        [StringLength(50)]
        public string Source { get; set; }
    }
}
