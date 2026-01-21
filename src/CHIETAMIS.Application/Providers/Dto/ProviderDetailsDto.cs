using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Providers.Dto
{
    public class ProviderDetailsDto: EntityDto
    {
        public string Provider_Trading_name { get; set; }
        public string Provider_legal_name { get; set; }
        public string Provider_SDL_NO { get; set; }
        public string Chieta_Accredited { get; set; }
        public string Public_Private { get; set; }
        public string Provider_Accreditation_NO { get; set; }
        public string Provider_Accredit_Review_Date { get; set; }
        public string Accred_NO_Knowledge_Component { get; set; }
        public string Accred_NO_Practical_Component { get; set; }
        public string SIC_Code { get; set; }
        public int ETQA_ID { get; set; }
        public string Physical_Address_1 { get; set; }
        public string Physical_Address_2 { get; set; }
        public string Physical_Address_3 { get; set; }
        public string Physical_Postal_Code { get; set; }
        public string Postal_Address_1 { get; set; }
        public string Postal_Address_2 { get; set; }
        public string Postal_Address_3 { get; set; }
        public string Postal_Code { get; set; }
        public string Provider_Tel_No { get; set; }
        public string Provider_Cell_No { get; set; }
        public string Provider_Email { get; set; }
        public string Provider_SARS_No { get; set; }
        public string Contact_Name { get; set; }
        public string Contact_Tel_No { get; set; }
        public string Contact_FAX_No { get; set; }
        public string Contact_Cell_No { get; set; }
        public string Contact_Email { get; set; }
        public string Web_Address { get; set; }
    }
}
