using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LittleUrl
{
    public class AuthenticationResponse
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserStatus { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public string RoleName { get; set; }
    }
}