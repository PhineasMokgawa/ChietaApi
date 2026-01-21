using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Finance.Dto
{
    public class MandatoryGrantPaymentDto: EntityDto
    {
        public string SDL_Number { get; set; }
        public int ZipFileId { get; set; }
        public int GrantYear { get; set; }
        public int Month { get; set; }
        public string ChietaAccount { get; set; }
        public string Chieta_Code1 { get; set; }
        public string OrgName_Code { get; set; }
        public string Bank_Name { get; set; }
        public string Bank_Account_Number { get; set; }
        public int Code { get; set; }
        public string Bank_Account_Code { get; set; }
        public string Organisation_Name { get; set; }
        public string SDLCode { get; set; }
        public double Amount { get; set; }
        public int CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime LastModificationTime { get; set; }
        public int LastModifierUserId { get; set; }
    }
}
