using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Lookups.DTOs
{
    public class SubPlaceDto: EntityDto
    {
        public int SP_CODE { get; set; }
        public string SP_NAME { get; set; }
        public int MN_CODE { get; set; }
        public string MN_NAME { get; set; }
        public int DC_MN_C { get; set; }
        public string DC_NAME { get; set; }
        public int PR_CODE { get; set; }
        public string PR_NAME { get; set; }
    }
}
