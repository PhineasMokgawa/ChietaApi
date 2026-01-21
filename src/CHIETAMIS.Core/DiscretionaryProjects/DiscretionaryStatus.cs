using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.DiscretionaryProjects
{
    [Table("lkp_Discretionary_Status")]
    public class DiscretionaryStatus: Entity
    {
        public int Status { get; set; }
        public string StatusDesc { get; set; }
        public string Typ { get; set; }
    }
}
