using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CHIETAMIS.DiscretionaryWindows
{
    [Table("lkp_FocusArea")]
    public class FocusArea : Entity
    {
        public string FocusAreaCd { get; set; }
        public string FocusAreaDesc { get; set; }
        public string EmpStatus { get; set; }

        public int FundinglImit {get; set;}
        public bool ActiveYN { get; set; }
    }
}
