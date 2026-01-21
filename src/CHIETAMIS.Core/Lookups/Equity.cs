using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace CHIETAMIS.Lookups
{
    [Table("lkp_Equity_Code")]
    public class Equity : Entity
    {
        public Equity() { }
        public string Description { get; set; }
        public string Equity_Code { get; set; }
    
    }
}
