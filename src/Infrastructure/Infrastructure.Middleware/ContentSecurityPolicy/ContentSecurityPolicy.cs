using Microsoft.AspNetCore.Builder;

namespace Infrastructure.Middleware.ContentSecurityPolicy;

public static class ContenteSecurityPolicy
{
    /// <summary>
    /// Add Content Security Policy
    /// </summary>
    /// <param name="app"></param>
    public static void AddCsp(this IApplicationBuilder app)
    {
        app.Use(async (context, next) =>
        {
            context.Response.Headers.Add("Content-Security-Policy", "default-src 'self'; script-src 'self'; style-src 'self'; font-src 'self'; img-src 'self'; frame-src 'self'");
            context.Response.Headers.Add("X-Frame-options", "DENY");
            await next();
        });
    }
}
