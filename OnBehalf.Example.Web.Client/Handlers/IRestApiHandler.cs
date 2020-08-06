using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace OnBehalf.Example.Web.Client.Handlers
{
    public interface IRestApiHandler
    {
        Task RequestUserDataOnBehalfOfAuthenticatedUser(HttpContext context);
    }
}