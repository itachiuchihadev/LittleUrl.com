using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LittleUrl
{
    public static class ServiceRegistration
    {
        public static void AddSharedInfrastructure(this IServiceCollection services, IConfiguration _config)
        {
            services.AddScoped<IAuthService, AuthService>();
        }
    }
}