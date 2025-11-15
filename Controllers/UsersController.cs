using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PreTrainee_Month2.ApplicationLayer.ServiceInterfaces;
using PreTrainee_Month2.CoreLayer;
using PreTrainee_Month2.CoreLayer.Entities.Static_Entities;
using PreTrainee_Month2.CoreLayer.Entities.User_Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PreTrainee_Month2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/<Users>
        [HttpGet]
        [Authorize]

        public async Task<IActionResult> Get()
        {
            return Ok(await _userService.GetAllUsersAsync());
        }

        [HttpGet("UsersWithProducts")]
        public async Task<IActionResult> GetUsersWithProducts()
        {
            return Ok(await _userService.GetAllUsersWithProductsAsync());
        }

        // GET api/<Users>/5
        [HttpGet("{id}")]
        [Authorize]

        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _userService.GetUserAsync(id));
        }
        [HttpGet("UserWithProducts/{id}")]
        public async Task<IActionResult> GetWithProducts(int id)
        {
            return Ok(await _userService.GetUserWithProductsAsync(id));
        }
       
        // PUT api/<Users>/5
        [HttpPut("{id}")]
        [Authorize]

        public async Task<IActionResult> Put(int id, [FromBody] UserRegisterDTO userRegisterDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            User user = new User(userRegisterDTO)
            {
                HasVerifiedEmail = true
            };
            await _userService.UpdateUserAsync(id, user);
            return Ok();
        }

        // DELETE api/<Users>/5
        [HttpDelete("{id}")]
       // [Authorize]
        public async Task Delete(int id)
        {
            await _userService.DeleteUserAsync(id);
        }
    }
}
