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
        public string ProjectName { get; set; } = null!;
        public string ProjectShortName { get; set; } = null!;
        public string Status { get; set; } = null!;
        public DateTime StatusChangedDate { get; set; }
        public string OrganisationName { get; set; } = null!;
        public string SDLNo { get; set; } = null!;
        public string? FocusArea { get; set; }
        public string? SubCategory { get; set; }
        public string? Intervention { get; set; }
        public string ProjectType { get; set; } = null!;
        public string WindowTitle { get; set; } = null!;
        public DateTime ProjectEndDate { get; set; }
    }


}
