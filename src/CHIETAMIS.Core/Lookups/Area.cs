using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace CHIETAMIS.Lookups
{
    [Table("lkp_PostCodeMapping")]
    public class Area : Entity
    {
        public Area() { }
        public string TownCity { get; set; }
        public string Municipality_Name { get; set; }
        public int CodeId { get; set; }
    }
}
