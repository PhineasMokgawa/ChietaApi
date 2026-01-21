using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CHIETAMIS.DiscretionaryWindows
{
    [Table("lkp_Project_Type")]
    public class ProjectType : Entity
    {
        public string ProjTypCD { get; set; }
        public string ProjTypDesc { get; set; }
        public bool ActiveYN { get; set; }

    }
}
