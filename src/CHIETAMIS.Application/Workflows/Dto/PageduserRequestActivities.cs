using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Workflows.Dto
{
    public class PagedUserRequestActivities
    {
        public UserRequestActivitiesDto UserRequestActivities { get; set; }
        public List<wfRequestDataDto> RequestData { get; set; }
    }
}
