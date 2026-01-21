using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Lookups.DTOs
{
    public class SETADto: EntityDto
    {
        public int Id { get; set; }
        public int SETA_Id { get; set; }
        public string Abrev { get; set; }
        public string Description { get; set; }
    }
}
