using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace TodoList.Proj.Services.TokenService;

public class TestAuthHandler: AuthenticationHandler<AuthenticationSchemeOptions>
{
    public TestAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
    {
    }
    

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var Claims = new Claim[] { new Claim(ClaimTypes.Name, "testeUser") };
        var Identity = new ClaimsIdentity(Claims);
        var principals = new ClaimsPrincipal(Identity);
        var Ticket = new AuthenticationTicket(principals, "Test");

        return Task.FromResult(AuthenticateResult.Success(Ticket)); 
    }
}