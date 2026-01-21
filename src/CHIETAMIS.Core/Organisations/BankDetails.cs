using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace CHIETAMIS.Organisations
{
    [Table("tbl_BankDetails")]
    public class BankDetails : Entity<int>
    {
        public int OrganisationId { get; set; }
        public string Account_Holder { get; set; }
        public string Branch_Code { get; set; }
        public string Account_Number { get; set; }
        public string Branch_Name { get; set; }
        public string Bank_Name { get; set; }
        public int AccountType { get; set; }
        public int UserId { get; set; }

    }
}
