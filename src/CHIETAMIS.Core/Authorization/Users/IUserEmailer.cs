using System.Threading.Tasks;
using CHIETAMIS.Chat;
using CHIETAMIS.DiscretionaryProjects;

namespace CHIETAMIS.Authorization.Users
{
    public interface IUserEmailer
    {
        /// <summary>
        /// Send email activation link to user's email address.
        /// </summary>
        /// <param name="user">User</param>
        /// <param name="link">Email activation link</param>
        /// <param name="plainPassword">
        /// Can be set to user's plain password to include it in the email.
        /// </param>
        Task SendEmailActivationLinkAsync(User user, string link, string plainPassword = null);

        /// <summary>
        /// Sends a password reset link to user's email.
        /// </summary>
        /// <param name="user">User</param>
        /// <param name="link">Password reset link (optional)</param>
        Task SendPasswordResetLinkAsync(User user, string link = null);

        /// <summary>
        /// Sends an email for unread chat message to user's email.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="senderUsername"></param>
        /// <param name="senderTenancyName"></param>
        /// <param name="chatMessage"></param>
        Task TryToSendChatMessageMail(User user, string senderUsername, string senderTenancyName, ChatMessage chatMessage);

        Task DGSendSubmissionEmailAsync(string EmailAddress, string SDLNumber, string OrganisationName, string ProjectCode, string ProjectName);
        Task SendGenericEmailAsync(string EmailAddress, string Subject, string Salut, string Body, string Signature);
        Task MGSendSubmissionEmailAsync(string EmailAddress, string SDLNumber, string OrganisationName, string ProjectCode, string ProjectName);
        Task MGSendNotYetSubmittedAsync(string EmailAddress, string SDL_Number, string Organisation_Name);
        Task DGSendBulkOutcomeEmailsAsync (string EmailAddress);
        Task DGSendNotificationEmailsAsync(string EmailAddress);
        Task NotificationEmailsAsync(string EmailAddress);
        Task OfficeMoveEmailsAsync(string EmailAddress);

        Task AssignRSAEmailAsync(string EmailAddress, string SDLNumber, string OrganisationName, string ProjectCode, string ProjectName);
        Task StatusUpdateEmailsAsync(string EmailAddress, string Status, string ContractNumber);
        Task SendBulkTranche1DocRejectionEmailsAsync(string EmailAddress, string Reason, string contractnum);
        Task SendMandatoryRejectionEmailsAsync(string EmailAddress, string Reason);
        Task SendMandatoryExtensionEmailsAsync(string EmailAddress, string SDL_No, string Organisation_Name);

    }
}
