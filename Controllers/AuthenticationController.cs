using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PreTrainee_Month2.ApplicationLayer.ServiceInterfaces;
using PreTrainee_Month2.CoreLayer;
using PreTrainee_Month2.CoreLayer.Entities.Static_Entities;
using PreTrainee_Month2.CoreLayer.Entities.User_Entities;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Security.Claims;
namespace PreTrainee_Month2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private IUserService _userService;
        private IEmailService _emailService;
        public AuthenticationController(IUserService userService, IEmailService emailService)
        {
            _userService = userService;
            _emailService = emailService;
        }
        // POST api/<Users/register>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDTO userRegisterDTO)
        {//отправка токена по ссылке на почту где подтверждаем регистрацию
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var target =await  _userService.GetUserByEmailAsync(userRegisterDTO.EmailAddress);
            if (target != null)
            {
                return BadRequest("Такой пользователь уже существует!");
            }
            var notRegistreduser = new User(userRegisterDTO);
            //добавляем пользователя(не забыть написать сервис который удаляет через время не аутентифицированных)
            await _userService.AddUserAsync(notRegistreduser);

            var confirmLink = Url.Action("ConfirmRegistration", "Authentication"
                , new
                {                    
                    EmailAddress = userRegisterDTO.EmailAddress,
                }, Request.Scheme);

            await _emailService.SendConfirmRegistrationEmailAsync(userRegisterDTO.EmailAddress, confirmLink); 

            return Ok("Подтвердите почту для регистрации");
        }

        [HttpGet("ConfirmRegistration")]
        public async Task<IActionResult> ConfirmRegistration(string EmailAddress)
        {
            if (string.IsNullOrEmpty(EmailAddress))
            {
                return BadRequest("неверная ссылка подтверждения!");
            }

            var claims = new List<Claim> { new Claim(ClaimTypes.Name, EmailAddress) };
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(5)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
                );
            var encodedJWT = new JwtSecurityTokenHandler().WriteToken(jwt);
            User successfullyAuthorizedUser = await _userService.GetUserByEmailAsync(EmailAddress);
            successfullyAuthorizedUser.HasVerifiedEmail = true;
            await _userService.UpdateUserAsync(successfullyAuthorizedUser.ID, successfullyAuthorizedUser); 

            return Ok(encodedJWT);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO userLoginDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var target =await  _userService.GetUserByEmailAsync(userLoginDTO.EmailAddress);
            if (target == null)//если не существует
            {
                return BadRequest("Такого пользователя не существует!");
            }
            if (target.Password != userLoginDTO.Password)
            {
                return BadRequest("Неправильный Пароль!");
            }
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, target.EmailAddress) };
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(5)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
                );
            var encodedJWT = new JwtSecurityTokenHandler().WriteToken(jwt);

            return Ok(encodedJWT);
        }


        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] UserConfirmPassword userConfirmPassword)
        {//такой костыль ибо пока нет времени делать фронтэнд
            if (!ModelState.IsValid)
            {
                return BadRequest("Введённые данные не соответствуют почте!");
            }
            var target = await _userService.GetUserByEmailAsync(userConfirmPassword.EmailAddress);
            if(target == null)
            {
                return BadRequest("Такого пользователя не существует!");
            }
            string? resetPasswordLink = Url.Action("ConfirmNewPassword", "Authentication", new
            {
                UserConfirmPassword = userConfirmPassword
            });
            await _emailService.SendResetPasswordEmailAsync(userConfirmPassword.EmailAddress,resetPasswordLink);
            return Ok("инструкция по сбросу пароля отправлена на почту");
        }

        [HttpGet("ConfirmNewPassword")]
        public async Task<IActionResult> ConfirmNewPassword([FromBody]UserConfirmPassword userConfirmPassword)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, userConfirmPassword.EmailAddress) };
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(5)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
                );
            var encodedJWT = new JwtSecurityTokenHandler().WriteToken(jwt);

            var user = await _userService.GetUserByEmailAsync(userConfirmPassword.EmailAddress);
            user.Password = userConfirmPassword.Password;

            await _userService.UpdateUserAsync(user.ID, user);
            return Ok(encodedJWT);
        }

    }
}
