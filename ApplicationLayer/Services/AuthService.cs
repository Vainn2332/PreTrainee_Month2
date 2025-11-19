using Microsoft.IdentityModel.Tokens;
using PreTrainee_Month2.ApplicationLayer.ServiceInterfaces;
using PreTrainee_Month2.CoreLayer.Entities.Static_Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Security.Claims;

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
    }
}
