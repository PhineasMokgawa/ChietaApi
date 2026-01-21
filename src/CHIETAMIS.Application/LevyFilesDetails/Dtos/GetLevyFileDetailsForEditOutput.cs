using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
using CHIETAMIS.LevyFilesDetails.Dtos;

namespace CHIETAMIS.LevyFilesDetails.Dtos
{
    public class GetLevyFileDetailsForEditOutput
    {
        public CreateOrEditLevyFileDetailsDto LevyFileDetails { get; set; }
    }
}
