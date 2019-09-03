using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Empite.PitchReady.Service;
using Microsoft.AspNetCore.Mvc;
using Empite.PitchReady.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Empite.PitchReady.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly SiteSettings _siteSettings;

        public HomeController(IOptions<SiteSettings> settings)
        {
            _siteSettings = settings.Value;

        }
        public IActionResult Index()
        {
            return View("Index", _siteSettings.Environment);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
