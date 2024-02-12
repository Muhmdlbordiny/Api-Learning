using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace AspNetBeginner.Authentication
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return Task.FromResult(AuthenticateResult.NoResult());
            var authHeader = Request.Headers["Authorization"].ToString();
            if (!authHeader.StartsWith("Basic ", StringComparison.OrdinalIgnoreCase))
                return Task.FromResult(AuthenticateResult.Fail("UnKnown schema"));
            var encodedcredentials = authHeader["Basic ".Length..];
            var decodedCredentials = Encoding.UTF8.GetString( Convert.FromBase64String(encodedcredentials));
            var UserNameAndPassword = decodedCredentials.Split(':');
            if (UserNameAndPassword[0] != "admin" || UserNameAndPassword[1] != "password")
                return Task.FromResult(AuthenticateResult.Fail("Invaild user name or password"));

            var identity = new ClaimsIdentity(new Claim[] 
            {
                new Claim(ClaimTypes.NameIdentifier,"1"),
                new Claim(ClaimTypes.Name,UserNameAndPassword[0])
            },"Basic");// standard in dotnet

            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, "Basic");

            return Task.FromResult(AuthenticateResult.Success(ticket));        
         }
    }
}
