using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Lesedis
{
    [Table("tbl_Discretionary_Lesedi")]
    public class Lesedi: Entity
    {
        public int UniversityCollege { get; set; }
        public int Qualification { get; set; }
        public string? OtherQualification { get; set; }
        public string CurrentlyStudying { get; set; }
        public string StudyYear { get; set; }
        public string UnderPostGraduate { get; set; }
        public string NSFASBeneficiary { get; set; }
        public string CurrentHist { get; set; }
        public decimal Balance { get; set; }
        public int PassRate { get; set; }
        public string? ConsentYN { get; set; }
        public DateTime? ConsentDate { get; set; }
        public DateTime DateCreated { get; set; }
        public int UserId { get; set; }
        public DateTime? DteUpd { get; set; }
        public int? UsrUpd { get; set; }
    }
}
