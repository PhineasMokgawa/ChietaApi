using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Lesedis.Dtos
{
    public class LesediStatusDto: EntityDto
    {
        public string StatusDesc { get; }
        public string Typ { get; }
    }
}
