using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace CHIETAMIS.Lookups
{
    [Table("lkp_Alternate_Id_Type")]
    public class IdType: Entity<int>
    {
        public IdType() { }
        public Int16 Alternate_Id_Type_Id { get; set; }
        public string Description { get; set; }
        public bool DGInd { get; set; }
    }
}
