using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.DiscretionaryTanches.Dtos
{
    public class ApplicationBatchDto: EntityDto
    {
        public int ApplicationId { get; set; }
        public int BatchId { get; set; }
        public string TrancheType { get; set; }
        public string Description { get; set; }
        public string TrancheStatus { get; set; }
        public int FocusAreaId { get; set; }
        public int SubCategoryId { get; set; }
        public int NoLearners { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime DateCreated { get; set; }
        public int UserId { get; set; }
    }
}