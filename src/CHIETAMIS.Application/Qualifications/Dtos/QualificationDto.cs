using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Qualifications.Dtos
{
    public class QualificationDto: EntityDto
    {
        public int QUALIFICATION_ID { get; set; }
        public string QUALIFICATION_TITLE { get; set; }
        public int PROVIDER_ID { get; set; }
        public string PROVIDER_NAME { get; set; }
        public int PROVIDER_ETQA_ID { get; set; }
        public string QUALIFICATION_TYPE_DESC { get; set; }
        public int QUALIFICATION_MINIMUM_CREDITS { get; set; }
        public string NQF_LEVEL_DESCRIPTION { get; set; }
        public DateTime QUAL_REGISTRATION_START_DATE { get; set; }
        public DateTime QUAL_REGISTRATION_END_DATE { get; set; }
        public DateTime LAST_DATE_FOR_ENROLMENT { get; set; }
        public DateTime LAST_DATE_FOR_ACHIEVEMENT { get; set; }
        public int ETQA_ID { get; set; }
    }
}
