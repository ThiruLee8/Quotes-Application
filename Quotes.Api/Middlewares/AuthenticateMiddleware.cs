using Microsoft.Extensions.Primitives;
using System.Security.Claims;

namespace Quotes.Api.Middlewares
{
    public class AuthenticateMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthenticateMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {

            if (context.Request.Headers.TryGetValue("UserRole",out StringValues userRole))
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.Role, userRole.ToString())
                };
                var identity = new ClaimsIdentity(claims, "Manual");
                context.User = new ClaimsPrincipal(identity);
            }
            await _next(context);

        }

    }
}
