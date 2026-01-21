using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Sdfs.Dtos
{
    public class CreateOrganisationSdfDto
    {
        public int OrganisationId { get; set; }
        public int SdfId { get; set; }
        public int UserId { get; set; }
        public Int16 StatusId { get; set; }
        public DateTime StatusDate { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
