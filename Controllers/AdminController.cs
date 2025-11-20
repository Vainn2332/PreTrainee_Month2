using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PreTrainee_Month2.ApplicationLayer.ServiceInterfaces;
using PreTrainee_Month2.ApplicationLayer.Services;

namespace PreTrainee_Month2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        public AdminController(IAdminService adminService, IUserService userService, IAuthService authService)
        {
            _adminService = adminService;
            _userService = userService;
            _authService = authService;
        }

        [HttpPost("deactivate")]
        [Authorize]
        public async Task<IActionResult> DeactivateUserAsync(int id)
        {
            if (id <= 0)
            {
                return BadRequest("id не может быть отрицательным!");
            }

            var jwt = _authService.GetJWTFromHeader(Request);
            var userInfo = _authService.ParseJWT(jwt);

            var currentUser = await _userService.GetUserAsync(userInfo.UserId);//проверка прав текущего пользователя
            if (currentUser.Role != "admin")
            {
                return Unauthorized("У вас недостаточно прав");
            }

            var target=await _userService.GetUserAsync(id);
            if(target == null)
            {
                throw new ArgumentException("Пользователь не найден!");
            }
            await _adminService.DeactivateUserAsync(id);
            return Ok();
        }

        [HttpPost("activate")]
        [Authorize]
        public async Task<IActionResult> ActivateUserAsync(int id)
        {
            if (id <= 0)
            {
                return BadRequest("id не может быть отрицательным!");
            }

            var jwt = _authService.GetJWTFromHeader(Request);
            var userInfo = _authService.ParseJWT(jwt);

            var currentUser = await _userService.GetUserAsync(userInfo.UserId);//проверка прав текущего пользователя
            if (currentUser.Role != "admin")
            {
                return Unauthorized("У вас недостаточно прав");
            }

            var target = await _userService.GetUserAsync(id);
            if (target == null)
            {
                throw new ArgumentException("Пользователь не найден!");
            }
            await _adminService.ActivateUserAsync(id);
            return Ok();
        }
    }
}
