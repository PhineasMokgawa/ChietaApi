using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
using CHIETAMIS.LevyFileLists.Dtos;

namespace CHIETAMIS.LevyFileLists.Dtos
{
    public class GetLevyFileListForEditOutput
    {
        public CreateOrEditLevyFileListDto LevyFileList { get; set; }
    }
}
