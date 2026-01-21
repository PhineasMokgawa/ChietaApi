using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CHIETAMIS.Counters
{
    [Table("lkp_Counters")]
    public class Counter: Entity
    {
        public int N_Last_Number { get; set; }
    }
}
