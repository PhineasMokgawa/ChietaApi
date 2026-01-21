using System.ComponentModel.DataAnnotations;

namespace SYSTEMMIS.Authorization.Accounts.Dto
{
    public class SendEmailActivationLinkInput
    {
        [Required]
        public string EmailAddress { get; set; }
    }
}