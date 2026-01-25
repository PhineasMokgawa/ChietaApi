using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.DiscretionaryProjects.Dtos
{
    public class ProjectTimelineDto
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectShortName { get; set; }

        public string Status { get; set; }
        public DateTime StatusChangedDate { get; set; }

        public string OrganisationName { get; set; }
        public string SDLNo { get; set; }

        public string FocusArea { get; set; }
        public string SubCategory { get; set; }
        public string Intervention { get; set; }
        public string ProjectType { get; set; }
        public string WindowTitle { get; set; }
        public DateTime ProjectEndDate { get; set; }

        /* ===== Timeline Boolean Flags ===== */

        public bool ApplicationStarted { get; set; }
        public bool ApplicationSubmitted { get; set; }
        public bool RsaReviewCompleted { get; set; }
        public bool GrantsCommitteeReview { get; set; }

        public bool EvaluationCompleted { get; set; }
        public bool RejectedAfterAssessment { get; set; }

        public bool IsFinalStage { get; set; }
        public string CurrentStage { get; set; }
    }



}
