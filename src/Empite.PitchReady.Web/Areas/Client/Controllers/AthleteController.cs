using Empite.PitchReady.Entity;
using Empite.PitchReady.Service;
using Empite.PitchReady.Web.Areas.Admin;
using Empite.PitchReady.Web.Areas.Admin.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Athlete = Empite.PitchReady.Entity.Athlete;

namespace Empite.PitchReady.Web.Areas.Client.Controllers
{
   [Area("Client")]
    public class AthleteController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<ClientController> _logger;
        private readonly IEmailHelper _emailSender;
        private IClientService _clientService;

        public AthleteController(UserManager<ApplicationUser> userManager, ILogger<ClientController> logger, IEmailHelper emailSender, IClientService clientService)
        {
            _userManager = userManager;
            _logger = logger;
            _emailSender = emailSender;
            _clientService = clientService;
        }
        public IActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        public async Task<IActionResult> Create(Athlete athlete)
        {

            await _clientService.SaveAthlete(athlete);

            _logger.LogInformation("User created a new Athlete");
            //return Ok(await _clientService.SaveClient(firstName, lastName, new ApplicationUser()));
            return View(athlete);
        }
    }
}