using Abp.Authorization.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Authorization.Users
{
    public class RSAAssignEmailInput
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
