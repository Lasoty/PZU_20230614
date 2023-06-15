using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using PzuCwiczenia.Infrastructure.ServiceInterfaces;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace PzuCwiczenia.WebApi.Authentication;

public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly IUserService userService;

    public BasicAuthenticationHandler(
        IUserService userService,
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory loggerFactory,
        UrlEncoder urlEncoder,
        ISystemClock clock
        ) : base(options, loggerFactory, urlEncoder, clock)
    {
        this.userService = userService;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        string userName = null;

        try
        {
            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            string[] credentials = Encoding.UTF8.GetString(Convert.FromBase64String(authHeader.Parameter)).Split(":");
            userName = credentials.FirstOrDefault();
            string? password = credentials.LastOrDefault();

            if (!userService.ValidateCredentials(userName, password))
                throw new InvalidOperationException("Błędne dane logowania");
        }
        catch (Exception ex)
        {
            return AuthenticateResult.Fail(ex.Message);
        }

        Claim[] claims = new[] {
            new Claim(ClaimTypes.Name, userName),
            //new Claim(ClaimTypes.Role, "Admin"),
            new Claim(ClaimTypes.Role, "User")
        };
        ClaimsIdentity identity = new(claims, Scheme.Name);
        ClaimsPrincipal principal = new(identity);
        AuthenticationTicket ticket = new(principal, Scheme.Name);
        return AuthenticateResult.Success(ticket);
    }
}
