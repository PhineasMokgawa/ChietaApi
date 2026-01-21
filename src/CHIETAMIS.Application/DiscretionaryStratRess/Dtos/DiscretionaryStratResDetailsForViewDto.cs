using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;


namespace CHIETAMIS.DiscretionaryStratRess.Dtos
{
    public class DiscretionaryStratResDetailsForViewDto: EntityDto
    {
        public DiscretionaryStratResDetailsDto DiscretionaryStratResDetails { get; set; }
        public DiscretionaryStratResObjectivesDto DiscretionaryStratResObjectives { get; set; }
    }
}
