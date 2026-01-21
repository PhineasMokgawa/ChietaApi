using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Providers.Dto
{
    public class Discretionary_Universtity_CollegeDto: EntityDto
    {
        public string UniversityCollegeName { get; set; }
        public string Type { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
