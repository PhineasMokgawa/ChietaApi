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
    public class Notification : Entity<int>
    {

        public int UserId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Data { get; set; }
        public string Source { get; set; }
        public bool Read { get; set; }
        public DateTime Timestamp { get; set; }
    }

}
