using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.ProjectNotifications
{
    [Table("ProjectNotifications")]
    public class ProjectNotification : Entity
    {
       
        public int UserId { get; set; }

        public int ProjectId { get; set; }
        public string Message { get; set; } = null!;
        
        public DateTime CreatedDate { get; set; }
        public bool Read { get; set; }

        public DateTime CreationTime { get; set; }
    }

}
