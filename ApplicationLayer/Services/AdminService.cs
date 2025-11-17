using Microsoft.IdentityModel.Tokens;
using PreTrainee_Month2.ApplicationLayer.ServiceInterfaces;

namespace PreTrainee_Month2.ApplicationLayer.Services
{
    public class AdminService : IAdminService
    {
        private readonly IUserService _userService;

        public AdminService(IUserService userService)
        {
            _userService = userService;
        }

        public async Task ActivateUserAsync(int id)
        {
            var user = await _userService.GetUserAsync(id);
            user.HasVerifiedEmail = true;
            await _userService.UpdateUserAsync(id, user);
        }

        public async Task DeactivateUserAsync(int id)
        {
            var user = await _userService.GetUserAsync(id);
            user.HasVerifiedEmail = false;
            await _userService.UpdateUserAsync(id, user);
        }
    }
}
