using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.JobTitleOFOs.DTOs
{
    public class JobTitleOFOView: EntityDto
    {
        public int Id { get; set; }
        public string OFO_Code { get; set; }
        public string Occupation { get; set; }
        public List<OFOSpecilaization> Specialization { get; set; }
    }
}
