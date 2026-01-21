using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace CHIETAMIS.Lookups
{
    [Table("lkp_Bank")]
    public class Bank: Entity
    {
        public Bank() { }
        public string Bank_Name { get; set; }
        public string Branch_Code { get; set; }

    }
}
