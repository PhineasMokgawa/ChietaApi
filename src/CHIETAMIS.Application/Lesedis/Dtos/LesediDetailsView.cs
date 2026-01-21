using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Lesedis.Dtos
{
    public class LesediDetailsView
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string Lastname { get; set; }
        public string SAIdNumber { get; set; }
        public int GrantWindowId { get; set; }
        public string Description { get; set; }
        public string GrantStatus {  get; set; }
        public string Reference { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public string Municipality { get; set; }
        public DateTime? SubmissionDte { get; set; }
        public DateTime DeadlineDate { get; set; }
    }
}
