using System.ComponentModel.DataAnnotations;

namespace CHIETAMIS.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}