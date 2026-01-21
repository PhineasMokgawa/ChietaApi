using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Lesedis.Dtos
{
    public class BursaryApplicationssView
    {
        public int Id { get; set; }
        public string ApplicationStatus { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime LauncheDte { get; set; }
        public DateTime DeadlineTime { get; set; }
        public string SubmittedBy { get; set; }
        public DateTime? SubmissionDte { get; set; }
        public string? StudentSAId { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
