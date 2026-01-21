using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Abp.Configuration;
using Abp.Zero.Configuration;
using Abp.UI;
using CHIETAMIS.Authorization.Accounts.Dto;
using CHIETAMIS.Authorization.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using CHIETAMIS.Url;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace CHIETAMIS.Authorization.Accounts
{
    public class AccountAppService : CHIETAMISAppServiceBase, IAccountAppService
    {
        private readonly IUserEmailer _userEmailer;
        public IAppUrlService AppUrlService { get; set; }

        // from: http://regexlib.com/REDetails.aspx?regexp_id=1923
        public const string PasswordRegex = "(?=^.{8,}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?!.*\\s)[0-9a-zA-Z!@#$%^&*()]*$";

        private readonly UserRegistrationManager _userRegistrationManager;
        //private readonly IHostingEnvironment hostingEnvironmentt;

        public AccountAppService(UserRegistrationManager userRegistrationManager, IUserEmailer userEmailer)
        {
            _userRegistrationManager = userRegistrationManager;
            _userEmailer =  userEmailer;
        }

        public async Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input)
        {
            var tenant = await TenantManager.FindByTenancyNameAsync(input.TenancyName);
            if (tenant == null)
            {
                return new IsTenantAvailableOutput(TenantAvailabilityState.NotFound);
            }

            if (!tenant.IsActive)
            {
                return new IsTenantAvailableOutput(TenantAvailabilityState.InActive);
            }

            return new IsTenantAvailableOutput(TenantAvailabilityState.Available, tenant.Id);
        }

        public async Task<RegisterOutput> Register(RegisterInput input)
        {
            var user = await _userRegistrationManager.RegisterAsync(
                input.Name,
                input.Surname,
                input.EmailAddress,
                input.UserName,
                input.Password,
                true // Assumed email address is always confirmed. Change this if you want to implement email confirmation.
            );

            var isEmailConfirmationRequiredForLogin = await SettingManager.GetSettingValueAsync<bool>(AbpZeroSettingNames.UserManagement.IsEmailConfirmationRequiredForLogin);

            return new RegisterOutput
            {
                CanLogin = user.IsActive && (user.IsEmailConfirmed || !isEmailConfirmationRequiredForLogin)
            };
        }
        public async Task SendPasswordResetCode(SendPasswordResetCodeInput input)
        {
            var user = await GetUserByChecking(input.EmailAddress);
            //user.SetNewPasswordResetCode();
            Random generator = new Random();
            user.PasswordResetCode = generator.Next(0, 1000000).ToString("D6");
            await _userEmailer.SendPasswordResetLinkAsync(
                user,
                AppUrlService.CreatePasswordResetUrlFormat(2)
            );
        }

        public virtual async Task ResetPassword(ResetPasswordInput input)
        {
            var user = await UserManager.FindByEmailAsync(input.EmailAddress);

            if (user == null || user.PasswordResetCode != input.ResetCode)
            {
                throw new UserFriendlyException("The Reset Code cannot be matched, please user the most recent one.");
            }

           // Throws an exception if the token is invalid
           //await UserManager.ResetPasswordAsync(user, input.ResetCode, input.Password);
            //UserManager.ChangePasswordAsync(user, input.Password);
            await UserManager.ChangePasswordAsync(user, input.Password);

            // todo: I would like to automatically confirm the users email after restting their password but.. 
            // can't use 'user.EmailConfirmed = true;' and need email token to confirm the email when using the _userManager.
            // The only way to do it currently is to use 'GenerateChangeEmailTokenAsync'
            //await _userManager.ConfirmEmailAsync(user, await _userManager.GenerateChangeEmailTokenAsync(user, user.Email));

            //user.PasswordResetCode = null;

            //await UserManager.UpdateAsync(user);
        }

        private async Task<User> GetUserByChecking(string inputEmailAddress)
        {
            var user = await UserManager.FindByEmailAsync(inputEmailAddress);
            if (user == null)
            {
                throw new Exception(L("InvalidEmailAddress"));
            }

            return user;
        }

        public Task<string> FileUpload(IFormFile fileupload)
        {
            string message = "";
            try
            {
                string uniquefilename = null;
                if (fileupload.Length > 0)
                {
                    string uploadfolder = "Files";
                    uniquefilename = Guid.NewGuid().ToString() + "_" + fileupload.FileName;
                    string filepath = Path.Combine(uploadfolder, uniquefilename);
                    fileupload.CopyToAsync(new FileStream(filepath, FileMode.Create));
                }
                message = uniquefilename;
            }
            catch (Exception ex)
            {
                message = ex.Message;

            }
            return Task.FromResult(message);
        }

        public HttpResponseMessage DownloadFile(string file)
        {
            file = "ff4f7fae-e65d-4354-a07a-032848a5f05b_img20220302_07021854.pdf";
            HttpResponseMessage response = null;
            string uploadfolder = "Files";
            string filepath = Path.Combine(uploadfolder, file);
            if (!File.Exists(filepath))
                return new HttpResponseMessage(HttpStatusCode.Gone);
            else
            {
                //if file present than read file 
                var fStream = new FileStream(filepath, FileMode.Open, FileAccess.Read);
                //compose response and include file as content in it
                response = new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StreamContent(fStream)
                };
                response.Content.Headers.ContentDisposition =
                                new ContentDispositionHeaderValue("attachment")
                                {
                                    FileName = Path.GetFileName(fStream.Name)
                                };
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            }
            return response;
        }

        //public async Task<FileStreamResult> DownloadFile([FromQuery] string file, ControllerBase? controller)
        //{
        //    string uploadfolder = "Files";
        //    var filePath = Path.Combine(uploadfolder, "");
        //    if (!System.IO.File.Exists(filePath))
        //        return null;

        //    var memory = new MemoryStream();
        //    using (var stream = new FileStream(filePath, FileMode.Open))
        //    {
        //        await stream.CopyToAsync(memory);
        //    }

        //    return controller.File(memory, GetContentType(filePath), file);
        //}

        private string GetContentType(string path)
        {
            var provider = new FileExtensionContentTypeProvider();
            string contentType;
            if (!provider.TryGetContentType(path, out contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }


    }
}
