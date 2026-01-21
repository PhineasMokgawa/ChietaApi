using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.MandatoryGrants
{
    [Table("tbl_Mandatory_Approval")]
    public class MandatoryApproval: Entity
    {
        public int ApplicationId { get; set; }
        public int StatusId { get; set; }
        public int UserReviewed { get; set; }
        public DateTime DateReviewed { get; set; }
        public bool Firstsubmission { get; set; }
        public bool ParentChild { get; set; }
        public bool Sublevies { get; set; }
        public bool Bankdetails { get; set; }
        public bool Employees { get; set; }
        public bool TrainingReceived { get; set; }
        public bool TrainingPlanned { get; set; }
        public bool Finance { get; set; }
        public bool EmployerRep { get;set; }
        public bool UnionSignatory { get; set; }
        public string? Comment { get; set; }
        public string Outcome { get; set; }
        public DateTime DateCreated { get; set; }
        public int UserId { get; set; }
        public DateTime? DteUpd { get; set; }
        public int? UsrUpd { get; set; }
    }
}
