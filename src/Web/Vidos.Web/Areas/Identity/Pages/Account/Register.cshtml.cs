using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Vidos.Data.Models;
using Vidos.Web.Common;
using Vidos.Web.Common.Constants;

namespace Vidos.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<VidosUser> _signInManager;
        private readonly UserManager<VidosUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<VidosUser> userManager,
            SignInManager<VidosUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = DisplayNames.Email)]
            public string Email { get; set; }

            [Required]
            [StringLength(Constants.MaxFirstNameLength, ErrorMessage = ErrorMessages.StringInvalidLength, MinimumLength = Constants.MinFirstNameLength)]
            [Display(Name = DisplayNames.FirstName)]
            public string FirstName { get; set; }

            [Required]
            [StringLength(Constants.MaxLastNameLength, ErrorMessage = ErrorMessages.StringInvalidLength, MinimumLength = Constants.MinLastNameLength)]
            [Display(Name = DisplayNames.LastName)]
            public string LastName { get; set; }

            [Required]
            [StringLength(Constants.MaxPasswordLength, ErrorMessage = ErrorMessages.StringInvalidLength, MinimumLength = Constants.MinPasswordLength)]
            [DataType(DataType.Password)]
            [Display(Name = DisplayNames.Password)]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = DisplayNames.ConfirmPassword)]
            [Compare(nameof(Password), ErrorMessage = ErrorMessages.PasswordMissMatch)]
            public string ConfirmPassword { get; set; }
        }

        public IActionResult OnGet(string returnUrl = null)
        {
            if (this._signInManager.IsSignedIn(this.User) && !this.User.IsInRole(Constants.GuestRole))
            {
                return RedirectToAction("Index", "Home");
            }

            ReturnUrl = returnUrl;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                var user = new VidosUser
                    {
                        UserName = Input.Email,
                        Email = Input.Email,
                        FirstName = Input.FirstName,
                        LastName = Input.LastName
                    };
                var result = await _userManager.CreateAsync(user, Input.Password);
                await this._userManager.AddToRoleAsync(user, Constants.UserRole);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
