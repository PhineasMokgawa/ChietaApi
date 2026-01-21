using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CHIETAMIS.DiscretionaryWindows
{
    [Table("lkp_AdminCrit")]
    public class AdminCriteria : Entity
    {
        public string AdminDesc { get; set; }
        public Boolean UsBased { get; set; }
        public bool ActiveYN { get; set; }
    }
}
