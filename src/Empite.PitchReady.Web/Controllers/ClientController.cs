using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Empite.PitchReady.Web.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Empite.PitchReady.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private IClientService _clientService;
        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public async Task<IActionResult> GetClient()
        {
            return Ok(await _clientService.GetClient());
        }

        [HttpPost]
        public async Task<IActionResult> SaveClient(string firstName, string lastName)
        {

            return Ok(await _clientService.SaveClient(firstName,lastName));
        }
    }
}