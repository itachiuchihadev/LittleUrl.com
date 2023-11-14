using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace LittleUrl
{
    public class AuthService : IAuthService
    {
         private readonly JwtConfig _jwtSettings;
         public AuthService(IOptions<JwtConfig> jwtSettings)
         {
            _jwtSettings = jwtSettings.Value;
         }


         public virtual async Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress)
        {
            string responseMessage = string.Empty;
            AuthenticationResponse response = new AuthenticationResponse();
            var user = UserData.GetUsersData().FirstOrDefault(x => x.UserName == request.UserName && x.Password == request.Password);

            if (user == null)
            {
                responseMessage = $"Invalid User";
            }
            else
            {
                JwtSecurityToken jwtSecurityToken = await GenerateJWToken(user);
                var JwtToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

                var userToken = GenerateRefreshToken(ipAddress);
                userToken.JwtToken = JwtToken;
                userToken.UserId = user.UserId;

                response = new AuthenticationResponse()
                {
                    UserId = user.UserId.ToString(),
                    Token = JwtToken,
                    Email = user.Email,
                    UserName = user.UserName,
                    UserStatus = "Active",
                    RefreshToken = userToken.Token,
                    RoleName = user.Role
                };
                responseMessage = $"Successfully Logged In {response.UserName}";
            }
            return new Response<AuthenticationResponse>(response, responseMessage);
        }
         private Task<JwtSecurityToken> GenerateJWToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim("uid", user.UserId.ToString()),
            };

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);


            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMonths(3),
                signingCredentials: signingCredentials);
            return Task.FromResult(jwtSecurityToken);
        }

        private UserToken GenerateRefreshToken(string ipAddress)
        {
            using (var rngCryptoServiceProvider = new RSACryptoServiceProvider())
            {
                var randomBytes = new byte[64];
                return new UserToken
                {
                    Token = Convert.ToBase64String(randomBytes),
                    Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.TokenLifetimeInMin),
                    CreatedDate = DateTime.UtcNow,
                    CreatedByIp = ipAddress,
                    RevokedByIp = "NONE",
                    ReplacedByToken = "NONE"
                };
            }
        }
    }
}