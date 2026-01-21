using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.PaymentPortals
{
    [Table("tbl_ApplicationTranche")]
    public class ApplicationTranche : Entity<int>
    {
        public int ApplicationId { get; set; }
        public string TrancheType { get; set; }
        public string Description { get; set; }
        public string? TrancheStatus { get; set; }
        public string? Current_Approver { get; set; }
        public int? BatchId { get; set; }
        public int? ProgrammeTypeId { get; set; }
        public int? FocusAreaId { get; set; }
        public int? SubCategoryId { get; set; }
        public decimal TrancheAmount { get; set; }
        public int? New_Learners { get; set; }
        public int? Continuing { get; set; }
        public int? Number_of_Learners { get; set; }
        public decimal? CostPerLearner { get; set; }
        public DateTime DateCreated { get; set; }
        public int UserId { get; set; }
        public int? Usrupd { get; set; }
        public DateTime? DteUpd { get; set; }

    }
}
