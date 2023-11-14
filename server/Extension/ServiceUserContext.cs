using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LittleUrl
{
    public class ServiceUserContext : IServiceUserContext
    {
        private readonly IHttpContextAccessor _httpContext;
        public ServiceUserContext(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }
        // public ClaimsPrincipal User => new ClaimsPrincipal();
        public string UserName { get => _httpContext?.HttpContext?.User?.FindFirstValue(ClaimTypes.Name)??""; }
        public string RoleName { get => _httpContext?.HttpContext?.User?.FindFirstValue(ClaimTypes.Role)??""; }
        public Guid UserId { get => new Guid(_httpContext?.HttpContext?.User?.FindFirstValue("uid")??""); }
        public bool IsAuthenticated { get => !string.IsNullOrWhiteSpace(UserName); }
    }
}