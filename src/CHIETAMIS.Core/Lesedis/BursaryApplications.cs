using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Lesedis
{
    [Table("tbl_Discretionary_Bursary_Applications")]
    public class BursaryApplications: Entity
    {
        public int GrantWindowId { get; set; }
        public int ApplicationStatusId { get; set; }
        public int? LesediId { get; set; }
        public int? StudentId { get; set; }
        public int? AddressId { get; set; }
        public int? SubmittedBy { get; set; }
        public DateTime? SubmissionDte { get; set; }
        public DateTime DateCreated { get; set; }
        public int UserId { get; set; }
        public DateTime? DteUpd {  get; set; }
        public int? UsrUpd { get; set; }
    }
}
