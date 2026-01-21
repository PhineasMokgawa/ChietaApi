using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Finance
{
    [Table("tbl_BankingList")]
    public class BankingList: Entity
    {
        public int ZipFileId { get; set; }
        public string SDL_No { get; set; }
        public string ChietaAccount { get; set; }
        public string Chieta_Code1 { get; set; }
        public string OrgName_Cde { get; set; }
        public string Bank_Name { get; set; }
        public string Bank_Account_Number { get; set; }
        public string SDLCode { get; set; }
        public decimal Amount { get; set; }
        public int CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
