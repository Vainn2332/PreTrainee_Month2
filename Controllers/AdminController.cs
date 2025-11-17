using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PreTrainee_Month2.ApplicationLayer.ServiceInterfaces;

namespace PreTrainee_Month2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly IUserService _userService;
        public AdminController(IAdminService adminService, IUserService userService)
        {
            _adminService = adminService;
            _userService = userService;
        }

        [HttpPost("deactivate")]
        [Authorize]
        public async Task DeactivateUser(int userId)
        {
            var user=await _userService.GetUserAsync(userId);
            if(user == null)
            {
                throw new ArgumentException("Пользователь не найден!");
            }
            await _adminService.DeactivateUserAsync(userId);
        }

        [HttpPost("activate")]
        [Authorize]
        public async Task ActivateUser(int userId)
        {
            var user = await _userService.GetUserAsync(userId);
            if (user == null)
            {
                throw new ArgumentException("Пользователь не найден!");
            }
            await _adminService.ActivateUserAsync(userId);
        }
    }
}
