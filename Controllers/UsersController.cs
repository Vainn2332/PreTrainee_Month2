using Microsoft.AspNetCore.Mvc;
using PreTrainee_Month2.ApplicationLayer.ServiceInterfaces;
using PreTrainee_Month2.CoreLayer;
using PreTrainee_Month2.CoreLayer.Entities.User_Entities;
using PreTrainee_Month2.CoreLayer.User_Entities;
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
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _userService.GetUserAsync(id));
        }
        [HttpGet("UserWithProducts/{id}")]
        public async Task<IActionResult> GetWithProducts(int id)
        {
            return Ok(await _userService.GetUserWithProductsAsync(id));
        }
        // POST api/<Users>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserPostAndPutDTO userPostAndPutDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            User user = new User(userPostAndPutDTO);
            await _userService.AddUserAsync(user);
            return Ok();
        }

        // PUT api/<Users>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UserPostAndPutDTO userPostAndPutDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            User user = new User(userPostAndPutDTO);
            await _userService.UpdateUserAsync(id, user);
            return Ok();
        }

        // DELETE api/<Users>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _userService.DeleteUserAsync(id);
        }
    }
}
