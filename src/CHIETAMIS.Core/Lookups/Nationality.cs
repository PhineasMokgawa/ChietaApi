using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace CHIETAMIS.Lookups
{
    [Table("Ikp_Nationality_Code")]
    public class Nationality : Entity
    {
        public Nationality() { }
        public string Description { get; set; }
        public string Nationality_Code { get; set; }
    }
}
