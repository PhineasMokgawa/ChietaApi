using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Lesedis
{
    [Table("tbl_Student_Address")]
    public class LesediAddress: Entity
    {
      public string? addressline1 {get; set;}
      public string addressline2 {get; set;}
      public string suburb {get; set;}
      public string area {get; set;}
      public string district {get; set;}
      public string municipality {get; set;}
      public string province {get; set;}
      public string postcode {get; set;}
      public DateTime datecreated {get; set;}
      public int userid {get; set;}
      public DateTime? DteUpd { get; set;}
      public int? UsrUpd { get; set;}
    }
}
