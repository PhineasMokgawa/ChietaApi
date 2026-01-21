using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Sdfs
{
    [Table("tbl_Organisation_Sdf")]
    public class Organisation_Sdf: Entity<int>
    {
        public int OrganisationId { get; set; }
        public int SdfId { get; set; }
        public int UserId { get; set; }
        public int PersonId { get; set; }
        public Int16 StatusId { get; set; }
        public DateTime StatusDate { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
