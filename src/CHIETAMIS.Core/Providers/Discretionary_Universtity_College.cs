using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Providers
{
    [Table("lkp_Discretionary_Universtity_College")]
    public class Discretionary_Universtity_College: Entity
    {
        public string UniversityCollegeName { get; set; }
        public string Type { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
