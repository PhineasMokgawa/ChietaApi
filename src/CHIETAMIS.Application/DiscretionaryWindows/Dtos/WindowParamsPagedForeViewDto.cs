using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.DiscretionaryWindows.Dtos
{
    public class WindowParamsPagedForeViewDto
    {
        public int Id { get; set; }
        public string Reference { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public virtual ICollection<PagedWindowParamsResultDto> Parameters { get; set; }
    }
}