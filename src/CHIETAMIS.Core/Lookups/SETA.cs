using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Lookups
{
    [Table("Ikp_SETA_")]
    public class SETA: Entity
    {
        public int Id { get; set; }
        public int SETA_Id { get; set; }
        public string Abrev { get; set; }
        public string Description { get; set; }
    }
}
