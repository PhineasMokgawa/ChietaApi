using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.PaymentPortals
{
    [Table("tbl_ApplicationTrancheDetails")]
    public class ApplicationTrancheDetails : Entity<int>
    {
        public int ApplicationTrancheId { get; set; }
        public int? LearnerDetailsId { get; set; }
        public decimal? Amount { get; set; }
        public string ApplicationTranceStatus { get; set; }
        public string? Current_Approver { get; set; }
        public int UserId { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
