using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.MandatoryGrants.Dtos
{
    public class MandWorkforceProfile: EntityDto
    {
        public string OFOCode { get; set; }
        public  string OFODescription { get; set; }
        public int AM { get; set; }
        public int AF { get; set; }
        public int CM { get; set; }
        public int CF { get; set; }
        public int IM { get; set; }
        public int IF { get; set; }
        public int WM { get; set; }
        public int WF { get; set; }
        public int TM { get; set; }
        public int TF { get; set; }
        public int TD { get; set; }
        public int TNSA { get; set; }
        public int A35 { get; set; }
        public int A55 { get; set; }
        public int A55P { get; set; }
        public string Municipality { get; set; }

    }
}
