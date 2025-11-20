using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PreTrainee_Month2.ApplicationLayer.ServiceInterfaces;
using PreTrainee_Month2.CoreLayer.Entities.Static_Entities;
using PreTrainee_Month2.CoreLayer.Entities.User_Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace PreTrainee_Month2.ApplicationLayer.Services
{
    public class AuthService : IAuthService
    {
        public string GenerateJWT(int userId,string userEmail)
        {
            var claims = new List<Claim> 
            { 
                new Claim(JwtRegisteredClaimNames.Email, userEmail),
                new Claim(JwtRegisteredClaimNames.Sub,userId.ToString())
            };
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(IAuthService.tokenExpirationTimeInMinutes)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
                );
            var encodedJWT = new JwtSecurityTokenHandler().WriteToken(jwt);
            return encodedJWT;
        }

        public UserJWTInfo ParseJWT(string jwt)
        {
            var parts = jwt.Split('.');
            if(parts.Length!=3||string.IsNullOrEmpty(jwt))
            {
                throw new ArgumentException("Неверный формат jwt токена!");
            }
            var payload = parts[1];
            var payloadBytes=Convert.FromBase64String(payload);
            var payloadJSON=Encoding.UTF8.GetString(payloadBytes);
            return JsonSerializer.Deserialize<UserJWTInfo>(payloadJSON);
        }

        public string GetJWTFromHeader(HttpRequest request)
        {
            request.Headers.TryGetValue("Authorization", out var authHeader);
            var token = authHeader.ToString().Replace("Bearer", "");
            return token;
        }
    }
}
