using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.DiscretionaryWindows
{
    public  class WindowsParamsOutputDto
    {
        public int Id { get; set; }
        public int DG_Window_Id { get; set; }
        public int ProjectTypeId { get; set; }
        public int FocusAreaId { get; set; }
        public int SubCategoryId { get; set; }
        public int InterventionId { get; set; }
        public bool ActiveYN { get; set; }
        public int UserId { get; set; }
    }
}
