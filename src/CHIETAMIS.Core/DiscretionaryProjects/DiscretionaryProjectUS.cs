using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.DiscretionaryProjects
{
    [Table("tbl_Discretionary_Project_US")]
    public class DiscretionaryProjectUS: Entity
    {
        public int ApplicationId { get; set; }
        public int USId { get; set; }
        public decimal Amount { get; set; }
        public int UserId { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
