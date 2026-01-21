using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
using CHIETAMIS.LevyFiles.Dtos;

namespace CHIETAMIS.LevyFiles.Dtos
{
    public class GetLevyFileForEditOutput
    {
        public CreateOrEditLevyFileDto LevyFile { get; set; }
    }
}
