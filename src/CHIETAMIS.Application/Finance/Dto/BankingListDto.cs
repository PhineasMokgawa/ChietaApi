using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Finance.Dto
{
    public class BankingListDto: EntityDto
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
