using System;
using Abp.Application.Services.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.PaymentPortals.DTOs
{
    public class BatchTrackingOutputDto
    {
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public string Description { get; set; }
        public string BatchNumber { get; set; }
        public string Status { get; set; }
        public DateTime Datedcreated { get; set; }

    }
}
