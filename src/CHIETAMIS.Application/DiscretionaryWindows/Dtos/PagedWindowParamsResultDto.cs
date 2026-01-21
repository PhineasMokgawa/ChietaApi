using Abp.Application.Services.Dto;
using System;

namespace CHIETAMIS.DiscretionaryWindows.Dtos
{
    public class PagedWindowParamsResultDto: PagedResultRequestDto
    {
        public int Id { get; set; }
        public string DG_Window { get; set; }
        public string ProjectType { get; set; }
        public string Title { get; set; }
        public string ProjType { get; set; }
        public string FocusArea { get; set; }
        public string SubCategory { get; set; }
        public string Intervention { get; set; }
        public bool ActiveYN { get; set; }
    }
}
