using Abp.Application.Services.Dto;
using CHIETAMIS.Workflows;
using CHIETAMIS.Workflows.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CHIETAMIS.DiscretionaryProjects.Dtos
{
    public class DiscretionaryAppTranchRequestOutputDto : EntityDto
    {

        public string ProjType { get; set; }
        public string ProjectStatus { get; set; }
        public string SDL_No { get; set; }
        public string Organisation_name { get; set; }
        public decimal Tranche_amount { get; set; }
        public string State_name { get; set; }
        public string Contract_Number { get; set; }
        public string Description { get; set; }
        public List<ApprovalsTrackingStates> States { get; set; }


    }
}
