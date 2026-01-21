using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.DiscretionaryProjects.Dtos
{
    public class DiscretionaryProjectUSForView
    {
        
        public int ApplicationId { get; set; }
        public int USId { get; set; }
        public double UNIT_STANDARD_ID { get; set; }
        public string Title { get; set; }
        public decimal Amount { get; set; }
        public int Credits { get; set; }
        public int UserId { get; set; }
        public int Id { get; set; }
    }
}
