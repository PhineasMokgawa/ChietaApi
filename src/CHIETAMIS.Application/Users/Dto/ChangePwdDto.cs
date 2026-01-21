using System.ComponentModel.DataAnnotations;

namespace CHIETAMIS.Users.Dto
{
    public class ChangePwdDto
    {
        [Required]
        public string CurrentPassword { get; set; }

        [Required]
        public string NewPassword { get; set; }
    }
}
