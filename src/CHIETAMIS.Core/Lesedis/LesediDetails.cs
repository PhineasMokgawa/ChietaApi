using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Lesedis
{
    [Table("tbl_Discretionary_Lesedi_Details")]
    public class LesediDetails: Entity
    {
        public string Firstname { get; set; }
        public string? Middlename { get; set; }
        public string Lastname { get; set; }
        public string SAIdNumber { get; set; }
        public int Age { get; set; }
        public string Cellphone { get; set; }
        public string Contactnumber { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Race { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public string Municipality { get; set; }
        public DateTime DateCreated { get; set; }
        public int UserId { get; set; }
        public DateTime? DteUpd { get; set; }
        public int? UpdUsr { get; set; }
    }
}
