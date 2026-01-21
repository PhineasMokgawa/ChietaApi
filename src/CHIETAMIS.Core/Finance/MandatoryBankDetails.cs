using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Finance
{
    [Table("tbl_Mandatory_BankDetails")]
    public class MandatoryBankDetails: Entity
    {
        public int BankId { get; set; }
        public string SDL_NO { get; set; }
        public string PARENT_SDL_NO { get; set;}
        public string ORGANISATION_NAME { get; set;}
        public string TRADING_NAME { get; set;}
        public string ORGANISATION_TYPE { get; set;}
        public string APPROVAL_STATUS { get;set;}
        public string BANK_NAME { get;set;}
        public string BANK_ACCOUNT_NUMBER { get;set;}
        public string BANK_ACCOUNT_HOLDER { get;set;}
        public string BANK_ACCOUNT_TYPE { get;set;}
        public string BANK_BRANCH_NAME { get; set;}
        public string BANK_BRANCH_CODE { get; set; }
        public string ORGANISATION_EMAIL { get; set; }
        public string PHYSICAL_ADDRESS_1 { get; set;}
        public string PHYSICAL_ADDRESS_2 { get; set; }
        public string PHYSICAL_ADDRESS_3 { get; set; }
        public string PHYSICAL_ADDRESS_POST_CODE { get; set; }
        public string POSTAL_ADDRESS_1 { get; set; }
        public string POSTAL_ADDRESS_2 { get; set; }
        public string POSTAL_ADDRESS_3 { get; set; }
        public string SDF_ID { get; set; }
        public string TITLE { get; set; }
        public string FIRST_NAME { get; set; }
        public string LAST_NAME { get; set; }
        public string EMAIL { get; set; }
        public string COMPANY_SIZE { get; set; }
        public string PROVINCE { get; set; }
        public string CHIETA_REPORTING_REGION { get; set; }
        public string MUNICIPALITY { get; set; }
        public string NUMBER_OF_EMPLOYEES { get; set; }
        public string BBBEE_LEVEL { get; set; }
        public decimal GRANT_YEAR { get; set; }
        public DateTime CreationTime { get; set; }


    }
}
