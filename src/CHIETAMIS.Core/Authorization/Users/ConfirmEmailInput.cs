using Abp.Authorization.Users;
using System.ComponentModel.DataAnnotations;

namespace CHIETAMIS.Authorization.Users
{
    public class ConfirmationEmailInput
    {
        [Required]
        [MaxLength(AbpUserBase.MaxEmailAddressLength)]
        public string EmailAddress { get; set; }
        public string projectCode { get; set; }
        public string projectName { get; set; }
        public string sdlNumber { get; set; }
        public string organisationName { get; set; }
    }
}