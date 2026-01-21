using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Lookups
{
    [Table("lkp_Bank_Account_Type")]
    public class Bank_Account_Type : Entity<int>
    {
        public string AccountType { get; set; }
        public string FIntegrateType { get; set; }
        public DateTime DateCreated { get; set; }

    }
}
