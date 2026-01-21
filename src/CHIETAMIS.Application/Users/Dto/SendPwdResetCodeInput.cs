using Abp.Authorization.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Users.Dto
{
    public class ResetPwdInput
    {
        public long UserId { get; set; }

        public string ResetCode { get; set; }

        public string Password { get; set; }

        public string ReturnUrl { get; set; }

        public string SingleSignIn { get; set; }
    }
}
