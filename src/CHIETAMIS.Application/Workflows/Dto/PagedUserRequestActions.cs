using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Workflows.Dto
{
    public class PagedUserRequestActions
    {
        public UserRequestActionsDto UserRequestActions { get; set; }
        public List<wfRequestDataDto> RequestData { get; set; }
        public List<wfRequestNoteDto> RequestNote { get; set; }
    }
}
