﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Empite.PitchReady.Entity;
using Empite.PitchReady.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Empite.PitchReady.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailHelper _emailSender;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailHelper emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty(SupportsGet = true)]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Range(typeof(bool), "true", "true", ErrorMessage = "You must accept the Terms of Service.")]
            public bool TOSAgree { get; set; }

            public string Code { get; set; }
        }

        public async Task OnGet(string userId, string code, string returnUrl = null)
        {
            var aaa = User.Identity.IsAuthenticated;
            if (aaa)
            {
                await _signInManager.SignOutAsync();
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ReturnUrl = returnUrl;

                throw new Exception();
            }
            else
            {
                Input.Email = user.Email;
                Input.Code = code;
                ModelState.Clear();
                ReturnUrl = returnUrl;
            }
            
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            var aaa = User.Identity.IsAuthenticated;
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Input.Email);
                if (user == null)
                {
                    return NotFound($"Unable to find user '{Input.Email}'.");
                }
                else
                {
                    user.PasswordHash = _userManager.PasswordHasher.HashPassword(user,Input.Password);
                    //user.EmailConfirmed = true;
                    var pwResponse = await _userManager.UpdateAsync(user);

                    var result = await _userManager.ConfirmEmailAsync(user, Input.Code);
                    if (!result.Succeeded)
                    {
                        throw new InvalidOperationException(
                            $"Error confirming email for user with ID '{Input.Email}':");
                    }

                    //var user = new ApplicationUser { UserName = Input.Email, Email = Input.Email, CreatedAt = DateTime.UtcNow};
                    //var result = await _userManager.CreateAsync(user, Input.Password);
                    //if (result.Succeeded)
                    //{
                    //    _logger.LogInformation("User created a new account with password.");
                    //    await _userManager.AddToRoleAsync(user, "Admin");

                    //    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //    var callbackUrl = Url.Page(
                    //        "/Account/ConfirmEmail",
                    //        pageHandler: null,
                    //        values: new { userId = user.Id, code = code },
                    //        protocol: Request.Scheme);

                    //    var body = new Dictionary<string, string>();
                    //    body.Add("$$message$$", $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    //    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                    //        body, "Activation.html");

                    //    //await _signInManager.SignInAsync(user, isPersistent: false);
                    //    //return LocalRedirect(returnUrl);
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return RedirectToPage("./Login");
                }


               
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
