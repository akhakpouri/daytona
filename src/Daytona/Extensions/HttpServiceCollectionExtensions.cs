using Daytona.User;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Daytona.Extensions
{
    public static class HttpServiceCollectionExtensions
    {
        public static IServiceCollection AddUserAccessor(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUserAccessor, UserAccessor>();
            return services;
        }
    }
}
