using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CHIETAMIS.Lookups.DTOs
{
    public class IdTypeDto: EntityDto<int>
    {
        public string IdType_Name { get; set; }
        public Int16 IdType_Code { get; set; }
        public bool DGInd { get; set; }
    }
}
