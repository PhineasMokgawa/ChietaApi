using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Lookups
{
    [Table("lkp_Mandatoty_Grants_Impact")]
    public class Mandatoty_Grants_Impact: Entity
    {
        public string Impact { get; set; }
    }
}
