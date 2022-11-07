namespace MovieDatabase.Web.Areas.Identity.Pages.Account
{
    using System;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Logging;

    using MovieDatabase.Common;
    using MovieDatabase.Data.Models;
    using MovieDatabase.Services.Identity;
    using MovieDatabase.Web.Areas.Identity.Pages.Account.InputModels;

    [AllowAnonymous]
#pragma warning disable SA1649 // File name should match first type name
    public class RegisterModel : PageModel
#pragma warning restore SA1649 // File name should match first type name
    {
        private readonly ApplicationSignInManager<ApplicationUser> signInManager;
        private readonly ApplicationUserManager<ApplicationUser> userManager;
        private readonly ILogger<RegisterModel> logger;
        private readonly IEmailSender emailSender;

        public RegisterModel(
            ApplicationUserManager<ApplicationUser> userManager,
            ApplicationSignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
            this.emailSender = emailSender;
        }

        [BindProperty]
        public RegisterInputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public void OnGet(string returnUrl = null)
        {
            // this.ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            // returnUrl = returnUrl ?? this.Url.Content("~/");
            if (this.ModelState.IsValid)
            {
                var existingUser = await this.userManager.FindByEmailAsync(this.Input.Email);

                if (existingUser != null)
                {
                    this.ModelState.AddModelError(string.Empty, "Email is already taken");

                    return this.Page();
                }

                var user = new ApplicationUser
                {
                    UserName = this.Input.Email,
                    Email = this.Input.Email,
                    Firstname = this.Input.Firstname,
                    Lastname = this.Input.Lastname,
                    IsActive = false,
                    EmailConfirmed = false,
                    LockoutEnabled = true,
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now,
                };

                var result = await this.userManager.CreateAsync(user, this.Input.Password);
                var roleResult = await this.userManager.AddToRoleAsync(user, GlobalConstants.UserRoleName);

                if (result.Succeeded && roleResult.Succeeded)
                {
                    this.logger.LogInformation("User created a new account with password.");

                    var code = await this.userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = this.Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, code = code },
                        protocol: this.Request.Scheme);

                    await this.emailSender.SendEmailAsync(
                        this.Input.Email,
                        "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    // await this.signInManager.SignInAsync(user, isPersistent: false);
                    // return this.LocalRedirect(returnUrl);
                    return this.RedirectToPage(
                        "/ThankYouForRegistering", new { userId = user.Id });
                }

                this.ModelState.AddModelError(string.Empty, "An error was encountered while registering your account. Please try again.");
            }

            // If we got this far, something failed, redisplay form
            return this.Page();
        }
    }
}
