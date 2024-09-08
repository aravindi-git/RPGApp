using System.Security.Claims;

namespace RPGApp.Utilities
{
    public class UserDetails
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserDetails(IHttpContextAccessor httpContextAccessor) 
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int GetUserId() => int.Parse(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    }
}
