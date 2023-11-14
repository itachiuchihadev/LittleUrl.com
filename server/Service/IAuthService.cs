using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LittleUrl
{
    public interface IAuthService
    {
        Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress);
    }
}