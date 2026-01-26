using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Notifications
{
    [Table("tbl_Notifications")]
    public class Notification : Entity
    {
       
        public int UserId { get; set; }

        public string Title { get; set; } = null!;
        public string Body { get; set; } = null!;
        public string? Data { get; set; }

        public DateTime Timestamp { get; set; }
        public bool Read { get; set; }

        public string Source { get; set; } = null!;
    }

}
