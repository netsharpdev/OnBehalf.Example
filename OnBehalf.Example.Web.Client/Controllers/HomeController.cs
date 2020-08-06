using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using OnBehalf.Example.Web.Client.Handlers;
using OnBehalf.Example.Web.Client.Models;

namespace OnBehalf.Example.Web.Client.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRestApiHandler _restApiHandler;

        public HomeController(ILogger<HomeController> logger, IRestApiHandler restApiHandler)
        {
            _logger = logger;
            _restApiHandler = restApiHandler;
        }
        public async Task<IActionResult> Index()
        {
            await _restApiHandler.RequestUserDataOnBehalfOfAuthenticatedUser(HttpContext);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult RequestOnBehalfOfAuthorizedUser()
        {
            return Ok();
        }
        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
