using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Workplaces.Dto
{
    public class WorkplaceDetailsDto: EntityDto
    {
        public string Workplace_Trading_Name { get; set; }
        public string Workplacement_Name { get; set; }
        public string Workplace_Type { get; set; }
        public string SDL_No { get; set; }
        public string SIC_Code { get; set; }
        public string ETQA_ID { get; set; }
        public string Workplacement_Approval_No { get; set; }
        public string Physical_Address_1 { get; set; }
        public string Physical_Address_2 { get; set; }
        public string Physical_Address_3 { get; set; }
        public string Physical_Postal_Code { get; set; }
        public string Postal_Address_1 { get; set; }
        public string Postal_Address_2 { get; set; }
        public string Postal_Address_3 { get; set; }
        public string Postal_Code { get; set; }
        public string Workplace_Tel_No { get; set; }
        public string Workplace_Fax_No { get; set; }
        public string Workplace_Cell_No { get; set; }
        public string Workplace_Email { get; set; }
        public string Workplace_SARS_No { get; set; }
        public string Contact_Name { get; set; }
        public string Contact_Tel_No { get; set; }
        public string Contact_FAX_No { get; set; }
        public string Contact_Cell_No { get; set; }
        public string Contact_Email { get; set; }
        public string Web_Address { get; set; }
    }
}
