using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace CHIETAMIS.Lookups
{
    [Table("lkp_Citizen_Resident_Status")]
    public class Citizenship : Entity
    {
        public Citizenship() { }
        public string Description { get; set; }
        public string Citizen_Resident_Status_Code { get; set; }
    
    }
}
