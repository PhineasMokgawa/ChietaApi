using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.PaymentPortals.DTOs
{
    public class GrantDeliverableScheduleDto: EntityDto
    {
        public string Delivertable { get; set; }
        public string Obligation { get; set; }
        public int TrancheTypeId { get; set; }
        public int Percentage { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
