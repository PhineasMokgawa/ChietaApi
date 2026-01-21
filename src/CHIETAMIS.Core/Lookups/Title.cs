using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace CHIETAMIS.Lookups
{
    [Table("lkp_Person_Title")]
    public class Title: Entity
    {
        public Title() { }
        public string Title_Name { get; set; }
        public string Title_Code { get; set; }
    }
}
