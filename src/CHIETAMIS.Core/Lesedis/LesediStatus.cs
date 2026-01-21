using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Lesedis
{
    [Table("lkp_Lesedi_Status")]
    public class LesediStatus : Entity
    {
        public string StatusDesc { get; set; }
        public string Typ { get; set; }
    }
}
