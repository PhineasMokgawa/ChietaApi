using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Workflows
{
    [Table("wf_ActionType")]
    public class wfActionType: Entity
    {
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
