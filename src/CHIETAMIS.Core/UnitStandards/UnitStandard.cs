using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace CHIETAMIS.UnitStandards
{
    [Table("tbl_SAQA_Unitstandard")]
    public class UnitStandard: Entity
    {
        public double UNIT_STANDARD_ID { get; set; }
        public string UNIT_STD_TITLE { get; set; }
        public int UNIT_STD_NUMBER_OF_CREDITS { get; set; }
        public decimal Amount1 { get; set; }
        public decimal Amount2 { get; set; }
    }
}
