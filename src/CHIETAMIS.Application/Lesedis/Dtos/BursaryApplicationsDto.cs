using Abp.Application.Services.Dto;
using Castle.MicroKernel.Registration.Interceptor;
using OfficeOpenXml.Drawing.Chart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Lesedis.Dtos
{
    public class BursaryApplicationsDto: EntityDto
    {
        public int? LesediId { get; set; }
        public int GrantWindowId { get; set; }
        public int ApplicationStatusId { get; set; }
        public int? StudentId { get; set; }
        public int? AddressId { get; set; }
        public int? SubmittedBy { get; set; }
        public DateTime? SubmissionDte { get; set; }
        public DateTime DateCreated { get; set; }
        public int UserId { get; set; }
        public DateTime? DteUpd { get; set; }
        public int? UsrUpd { get; set; }
    }
}
