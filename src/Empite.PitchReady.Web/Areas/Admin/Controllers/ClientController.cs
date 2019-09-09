using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Empite.PitchReady.Entity;
using Empite.PitchReady.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

namespace Empite.PitchReady.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class ClientController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<ClientController> _logger;
        private readonly IEmailHelper _emailSender;
        private IClientService _clientService;

        public ClientController(UserManager<ApplicationUser> userManager, ILogger<ClientController> logger, IEmailHelper emailSender, IClientService clientService)
        {
            _userManager = userManager;
            _logger = logger;
            _emailSender = emailSender;
            _clientService = clientService;
        }

        public async Task<IActionResult> List()
        {
            var clients = await _clientService.GetClient();
            List<Client> clientList = new List<Client>();
            Client client;
            foreach (var item in clients)
            {
                client = new Client{FirstName = item.ApplicationUser.FirstName,LastName = item.ApplicationUser.LastName,Email=item.ApplicationUser.Email,IsActive = item.ApplicationUser.EmailConfirmed, UserGuid = item.ApplicationUserId};
                clientList.Add(client);
            }
            return View(clientList);
        }

        public IActionResult Create(string id)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Client client)
        {
            var user = new ApplicationUser { UserName = client.Email, Email = client.Email, CreatedAt = DateTime.UtcNow };
            var result = await _userManager.CreateAsync(user);
            if (result.Succeeded)
            {
                await _clientService.SaveClient(client.FirstName, client.LastName, user);

                _logger.LogInformation("User created a new account with password.");
                await _userManager.AddToRoleAsync(user, "Client");

               

                return View(client);
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View();


        }

        public async Task<IActionResult> SendInvite(string userGuid)
        {
            var user = await _userManager.FindByIdAsync(userGuid);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { area = "Identity", userId = user.Id, code = code },
                protocol: Request.Scheme);

            var body = new Dictionary<string, string>();
            body.Add("$$message$$", $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

            await _emailSender.SendEmailAsync(user.Email, "Confirm your email",
                body, "Activation.html");

            //await _signInManager.SignInAsync(user, isPersistent: false);
            //return LocalRedirect(returnUrl);
            TempData["invitation"] = "Invitation Sent";
            return RedirectToAction("List", "Client",
                new { area = "Admin" });
        }


    }
}