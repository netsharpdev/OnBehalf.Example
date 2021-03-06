﻿using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web.Resource;

namespace OnBehalf.Example.Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private const string scopeClaimType = "http://schemas.microsoft.com/identity/claims/scope";
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private static readonly string scope = "Weather.Read";
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            HttpContext.VerifyUserHasAnyAcceptedScope(scope);
            var rng = new Random();
            return Ok(
                Enumerable.Range(1, 5)
                    .Select(
                        index => new WeatherForecast
                        {
                            Date = DateTime.Now.AddDays(index),
                            TemperatureC = rng.Next(-20, 55),
                            Summary = Summaries[rng.Next(Summaries.Length)]
                        })
                    .ToArray());
        }
    }
}
