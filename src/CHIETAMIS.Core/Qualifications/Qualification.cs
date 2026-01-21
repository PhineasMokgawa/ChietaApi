using Abp.Domain.Entities;
using MimeKit.Cryptography;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Qualifications
{
    [Table("tbl_SAQA_Qualifications")]
    public class Qualification: Entity
    {
        public int QUALIFICATION_ID { get; set; }
        public string QUALIFICATION_TITLE { get; set; }
        public int PROVIDER_ID { get; set; }
        public string PROVIDER_NAME { get; set; }
        public int PROVIDER_ETQA_ID { get; set; }
        public string QUALIFICATION_TYPE_DESC { get; set; }
        public  int QUALIFICATION_MINIMUM_CREDITS { get; set; }
        public string NQF_LEVEL_DESCRIPTION { get; set; }
        public DateTime QUAL_REGISTRATION_START_DATE { get; set; }
        public DateTime QUAL_REGISTRATION_END_DATE { get; set; }
        public DateTime LAST_DATE_FOR_ENROLMENT {  get; set; }
        public DateTime LAST_DATE_FOR_ACHIEVEMENT { get; set; }
        public int ETQA_ID { get; set; }
    }
}
