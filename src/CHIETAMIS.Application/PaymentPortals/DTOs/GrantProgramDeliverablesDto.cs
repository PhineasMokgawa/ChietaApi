using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.PaymentPortals.DTOs
{
    public class GrantProgramDeliverablesDto: EntityDto
    {
        public int TrancheScheduleId { get; set; }
        public int FocusAreaId { get; set; }
        public int SubcategoryId { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
