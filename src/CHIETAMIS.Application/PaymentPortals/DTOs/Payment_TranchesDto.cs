using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.PaymentPortals.DTOs
{
    public class Payment_TranchesDto: EntityDto
    {
        public string TrancheCode { get; set; }
        public int ProjectTypeId { get; set; }
        public string Tranche_Description { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
