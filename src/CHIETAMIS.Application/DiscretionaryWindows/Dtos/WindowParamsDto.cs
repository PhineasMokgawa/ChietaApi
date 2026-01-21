using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.DiscretionaryWindows.Dtos
{
    public class WindowParamsDto:EntityDto<int>
    {
        public int DG_Window_Id { get; set; }
        public int ProjectTypeId { get; set; }
        public int? FocusAreaId { get; set; }
        public int? SubCategoryId { get; set; }
        public int? InterventionId { get; set; }
        public bool ActiveYN { get; set; }
        public int UserId { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
