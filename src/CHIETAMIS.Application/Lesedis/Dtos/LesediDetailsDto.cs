using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Lesedis.Dtos
{
    public class LesediDetailsDto: EntityDto
    {
        public string Firstname { get; set; }
        public string Middlename { get; set; }
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
