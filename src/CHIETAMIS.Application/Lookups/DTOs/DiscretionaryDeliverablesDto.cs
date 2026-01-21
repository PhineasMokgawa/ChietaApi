using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Lookups.DTOs
{
    public class DiscretionaryDeliverablesDto: EntityDto
    {
        public int ProjectTypeId { get; set; }
        public int FocusAreaId { get; set; }
        public int SubCategoryId { get; set; }
        public int TrancheTypeId { get; set; }
        public int DeliverableId { get; set; }
        public string Deliverable { get; set; }
        public int Percent { get; set; }
    }
}
