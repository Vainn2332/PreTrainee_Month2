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
        private IAuthService _authService;
       
        public AuthenticationController(IUserService userService, IEmailService emailService, IAuthService authService)
        {
            _userService = userService;
            _emailService = emailService;
            _authService = authService;
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

            var notRegistredUser = new User(userRegisterDTO);
            notRegistredUser.Password = _userService.HashPassword(notRegistredUser.Password);
            await _userService.AddUserAsync(notRegistredUser);

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

            var target = await _userService.GetUserByEmailAsync(EmailAddress);            
            var encodedJWT =_authService.GenerateJWT(target.ID,target.EmailAddress);
           
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
            if (target.HasVerifiedEmail == false)
            {
                var confirmLink = Url.Action("ConfirmRegistration", "Authentication"
                , new
                {
                    EmailAddress = userLoginDTO.EmailAddress,
                }, Request.Scheme);
                await _emailService.SendUserActivationEmailAsync(userLoginDTO.EmailAddress, confirmLink);
                return Ok("Ваш аккаунт был деактивирован.Для активации подтвердите почту");
            }
            
            var encodedJWT = _authService.GenerateJWT(target.ID,target.EmailAddress);

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
            if (target.HasVerifiedEmail == false)
            {
                return BadRequest("Аккаунт ещё не автооризован!");
            }
            string? resetPasswordLink = Url.Action("ConfirmNewPassword", "Authentication", new
            {
                Password = userConfirmPassword.Password,
                EmailAddress=userConfirmPassword.EmailAddress
            },Request.Scheme);
            await _emailService.SendResetPasswordEmailAsync(userConfirmPassword.EmailAddress,resetPasswordLink);
            return Ok("инструкция по сбросу пароля отправлена на почту");
        }

        [HttpGet("ConfirmNewPassword")]
        public async Task<IActionResult> ConfirmNewPassword(string newPassword,[EmailAddress]string EmailAddress)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var target = await _userService.GetUserByEmailAsync(EmailAddress);
           
            var encodedJWT = _authService.GenerateJWT(target.ID,target.EmailAddress);

            target.Password = newPassword;
            await _userService.UpdateUserAsync(target.ID, target);

            return Ok(encodedJWT);
        }

    }
}
