using Microsoft.AspNetCore.Http;

namespace Daytona.User
{
    public class UserAccessor : IUserAccessor
    {
        protected readonly IHttpContextAccessor _httpContextAccessor;
        public string UserName => _httpContextAccessor.HttpContext.User.Identity.Name;

        public UserAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
    }
}
