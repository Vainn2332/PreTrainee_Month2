using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PreTrainee_Month2.ApplicationLayer.ServiceInterfaces;
using PreTrainee_Month2.CoreLayer;
using PreTrainee_Month2.CoreLayer.Entities.Static_Entities;
using PreTrainee_Month2.CoreLayer.Entities.User_Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
namespace PreTrainee_Month2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private IUserService _userService;

        public AuthenticationController(IUserService userService)
        {
            _userService = userService;
        }
        // POST api/<Users/register>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserPostAndPutDTO userPostAndPutDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            User user = new User(userPostAndPutDTO);
            var target =await  _userService.GetUserByEmailAsync(user.EmailAddress);
            if (target != null)
            {
                return BadRequest("Такой пользователь уже существует!");
            }

            var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.EmailAddress) };
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(5)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
                );
            var encodedJWT = new JwtSecurityTokenHandler().WriteToken(jwt);

            await _userService.AddUserAsync(user);

            return Ok(encodedJWT);
        }

        [HttpGet("login")]
        public async Task<IActionResult> Login([FromBody] UserPostAndPutDTO userPostAndPutDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            User user = new User(userPostAndPutDTO);
            var target =await  _userService.GetUserByEmailAsync(user.EmailAddress);
            if (target == null)//если не существует
            {
                return BadRequest("Такой пользователь не существует!");
            }

            var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.EmailAddress) };
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(5)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
                );
            var encodedJWT = new JwtSecurityTokenHandler().WriteToken(jwt);

            await _userService.AddUserAsync(user);

            return Ok(encodedJWT);
        }
    }
}
