using Huellitas.Data.Entities;
using Huellitas.Web.Tests.Controllers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Huellitas.Web.Tests.Helpers
{
    public class TestAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public TestAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
        : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (BaseControllerTests.CurrentUserAuthenticated.HasValue) // TODO: Decouple here
            {
                var claims = new[] { new Claim(ClaimTypes.Name, "Test user"), new Claim(ClaimTypes.NameIdentifier, BaseControllerTests.CurrentUserAuthenticated.ToString()) };
                var identity = new ClaimsIdentity(claims, "Test");
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, "Test");

                var result = AuthenticateResult.Success(ticket);
                return Task.FromResult(result);
            }
            else
            {
                var result = AuthenticateResult.Fail("Huellitas Unauthorized");
                return Task.FromResult(result);
            }
        }
    }
}