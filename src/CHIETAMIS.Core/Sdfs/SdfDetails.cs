using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CHIETAMIS.Sdfs
{
    [Table("tbl_Sdf_Details")]
    public  class SdfDetails: Entity<int>
    {
        public int personId { get; set; }
        public int userId { get; set; }
        public Int16 designation { get; set; }
        public DateTime dateCreated { get; set; }
        public int status { get; set; }
        public DateTime statusDate { get; set; }
        public int statusUserId { get; set; }
    }
}
