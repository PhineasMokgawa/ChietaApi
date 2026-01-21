using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace CHIETAMIS.Lookups
{
    [Table("lkp_PostalCodeMapping")]
    public class PostalCodeMapping: Entity<int>
    {
        public PostalCodeMapping() { }
        public string PostalCode { get; set; }
        public string Suburb { get; set; }
        public string Area { get; set; }
        public string District { get; set; }
        public string Municipality { get; set; }
        public string Province { get; set; }
        public string RuralUrban { get; set; }
    }
}
