using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace CHIETAMIS.Lookups.DTOs
{
    public class GrantApprovalStatusDto:EntityDto
    {
        public int Id { get; set; }
        public string GrantStatusDescription { get; set; }
    }
}
