using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CHIETAMIS.DiscretionaryWindows
{
    [Table("tbl_Discretionary_Window_Params")]
    public class WindowParams: Entity
    {
        public int DG_Window_Id { get; set; }
        public int ProjectTypeId { get; set; }
        public int? FocusAreaId { get; set; }
        public int? SubCategoryId { get; set; }
        public int? InterventionId { get; set; }
        public bool ActiveYN { get; set; }
        public int UserId { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
