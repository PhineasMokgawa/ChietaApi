using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Workflows.Dto
{
    public class UserRequestActionsDto
    {
        public int ProcessId { get; set; }
        public string ProcessName { get; set; }
        public int ActionId { get; set; }
        public string ActionName { get; set; }
        public int RequestId { get; set; }
        public string RequestTitle { get; set; }
        public int? UserId { get; set; }
    }
}
