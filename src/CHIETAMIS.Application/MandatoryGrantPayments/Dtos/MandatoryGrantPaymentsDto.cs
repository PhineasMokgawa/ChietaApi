using System;
using Abp.Application.Services.Dto;

namespace CHIETAMIS.MandatoryGrantPayments.Dtos
{
    public class MandatoryGrantPaymentsDto : EntityDto<long>
    {
        public string SDL_Number { get; set; }
        public int GrantYear { get; set; }
        public int Month { get; set; }
        public int zipfileid { get; set; }
        public string ChietaAccount { get; set; }
        public string CHIETA_Code1 { get; set; }
        public string OrgName_Code { get; set; }
        public string BANK_NAME { get; set; }
        public string Bank_Account_NUmber { get; set; }
        public int Code { get; set; }
        public string Bank_Account_Code { get; set; }
        public string Organisation_Name { get; set; }
        public string SDLCode { get; set; }
        public double Amount { get; set; }
        public double LevyAmount { get; set; }
        
    }
}
