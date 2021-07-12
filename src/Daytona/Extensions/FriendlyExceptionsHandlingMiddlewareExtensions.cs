using Daytona.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Daytona.Extensions
{
    public static class FriendlyExceptionsHandlingMiddlewareExtensions
    {
        public static IServiceCollection AddUseFriendlyExceptionsMiddleware(this IServiceCollection services) =>
            services.AddScoped<FriendlyExceptionsHandlingMiddleware>();

        public static IApplicationBuilder UseFriendlyExceptionsHandling(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<FriendlyExceptionsHandlingMiddleware>();
        }
    }
}
