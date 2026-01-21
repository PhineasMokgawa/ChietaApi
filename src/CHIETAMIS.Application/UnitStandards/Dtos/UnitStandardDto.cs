using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.UnitStandards.Dtos
{
    public class UnitStandardDto: EntityDto
    {
        public double UNIT_STANDARD_ID { get; set; }
        public string UNIT_STD_TITLE { get; set; }
        public int UNIT_STD_NUMBER_OF_CREDITS { get; set; }
        public decimal Amount1 { get; set; }
        public decimal Amount2 { get; set; }
    }
}
