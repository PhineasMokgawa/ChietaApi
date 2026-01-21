using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Lookups.DTOs
{
    public class SpecialistProjectDto: EntityDto
    {
        public int UserId { get; set; }
        public int ProjectTypeId { get; set; }
    }
}
