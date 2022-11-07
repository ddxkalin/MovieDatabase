namespace MovieDatabase.Web.Areas.Identity.Pages
{
    using System;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    using MovieDatabase.Common.Exceptions;
    using MovieDatabase.Data.Models;
    using MovieDatabase.Services.Identity;
    using MovieDatabase.Web.Areas.Identity.Pages.Account.OutputModels;

    [AllowAnonymous]
    public class ThankYouForRegisteringModel : PageModel
    {
        private readonly ApplicationUserManager<ApplicationUser> userManager;
        private readonly IEmailSender emailSender;

        public ThankYouForRegisteringModel(ApplicationUserManager<ApplicationUser> userManager, IEmailSender emailSender)
        {
            this.userManager = userManager;
            this.emailSender = emailSender;
        }

        public ThankYouForRegisteringOutputModel Output { get; set; }

        public async Task OnGetAsync(string userId)
        {
            ApplicationUser user = await this.GetUserAndLoadOutputModel(userId);
        }

        public async Task<IActionResult> OnGetResendAsync(string userId)
        {
            ApplicationUser user = await this.GetUserAndLoadOutputModel(userId);

            if (DateTimeOffset.UtcNow.Subtract(user.EmailConfirmationTokenResentOn).TotalMinutes < 5)
            {
                this.Output.SubsequentResend = true;

                return this.Page();
            }

            var code = await this.userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = this.Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { userId = userId, code = code },
                protocol: this.Request.Scheme);

            await this.emailSender.SendEmailAsync(
                this.Output.Email,
                "Confirm your email",
                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

            this.Output.Resent = true;

            // update the token resent on value, so it can be checked if more resends follow
            var result = await this.userManager.SetEmailConfirmationTokenResentOnAsync(userId);

            return this.Page();
        }

        private async Task<ApplicationUser> GetUserAndLoadOutputModel(string userId)
        {
            ApplicationUser user = await this.userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new EntityNotFoundException("userId");
            }

            if (user.EmailConfirmed)
            {
                throw new InvalidOperationException("email is confirmed");
            }

            this.Output = new ThankYouForRegisteringOutputModel
            {
                UserId = user.Id,
                Email = user.Email,
                Firstname = user.Firstname,
                Lastname = user.Lastname
            };

            return user;
        }
    }
}