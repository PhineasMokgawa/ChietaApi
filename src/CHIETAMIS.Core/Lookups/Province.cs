using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace CHIETAMIS.Lookups
{
    [Table("lkp_PostalCodeProvince")]
    public class Province : Entity
    {
        public Province() { }
        public string Province_Name { get; set; }
        public Int16 Province_Code { get; set; }
    }
}
