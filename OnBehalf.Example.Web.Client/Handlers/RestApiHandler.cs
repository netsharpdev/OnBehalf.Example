using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using Microsoft.Identity.Web;

namespace OnBehalf.Example.Web.Client.Handlers
{
    public class RestApiHandler : IRestApiHandler
    {
        private readonly ITokenAcquisition _tokenAcquisition;

        public RestApiHandler(ITokenAcquisition tokenAcquisition)
        {
            _tokenAcquisition = tokenAcquisition;
        }

        public async Task RequestUserDataOnBehalfOfAuthenticatedUser(HttpContext context)
        {
            var scopes = new List<string> { $"642b4ac0-ccc4-4e02-a626-47c24531820e/Weather.Read" };
            try
            {
                var token = await _tokenAcquisition.GetAccessTokenForUserAsync(scopes);
                var httpClient = new HttpClient();

                var defaultRequestHeaders = httpClient.DefaultRequestHeaders;
                if (defaultRequestHeaders.Accept.All(m => m.MediaType != "application/json"))
                    httpClient.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                defaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await httpClient.GetAsync(
                    "https://localhost:5051/WeatherForecast");
            }
            catch (Exception ex)
            {
                //something went wrong
            }
        }
    }
}