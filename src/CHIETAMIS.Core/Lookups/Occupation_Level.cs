using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Lookups
{
    [Table("lkp_Occupation_Level")]
    public class Occupation_Level: Entity
    {
        public string Occupational_Levels { get; set; }


    }
}
