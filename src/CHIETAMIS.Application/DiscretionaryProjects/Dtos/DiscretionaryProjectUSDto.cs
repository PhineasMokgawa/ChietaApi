using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.DiscretionaryProjects.Dtos
{
    public class DiscretionaryProjectUSDto:EntityDto
    {
        public int ApplicationId { get; set; }
        public int USId { get; set; }
        public decimal Amount { get; set; }
        public int UserId { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
