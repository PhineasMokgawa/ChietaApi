using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace CHIETAMIS.Lookups
{
    [Table("lkp_SqmrApp_Indicators")]
    public  class SqmrAppIndicators: Entity
    {
        public SqmrAppIndicators() { }
        public string Indicator { get; set; }
    }
}
