using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace LittleUrl
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        // Dependency Injection
        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            //Reading the AuthHeader which is signed with JWT
            string authHeader = context.Request.Headers["Authorization"];


            if (authHeader != null)
            {
                //Reading the JWT middle part           
                int startPoint = authHeader.IndexOf(".") + 1;
                int endPoint = authHeader.LastIndexOf(".");

                var tokenString = authHeader
                    .Substring(startPoint, endPoint - startPoint).Split(".");
                var token = authHeader.Split(' ')[1].ToString();
            
                var handler = new JwtSecurityTokenHandler();
                var jwtSecurityToken = handler.ReadJwtToken(token);
                var identity = new ClaimsIdentity(jwtSecurityToken.Claims, "basic");
                context.User = new ClaimsPrincipal(identity);
            }
            await _next(context);
        }
    }
}