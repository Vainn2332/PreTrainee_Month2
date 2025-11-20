using Microsoft.AspNetCore.Mvc;
using PreTrainee_Month2.ApplicationLayer.ServiceInterfaces;
using PreTrainee_Month2.CoreLayer.Product_Entities;
using PreTrainee_Month2.CoreLayer.Entities.Product_Entities;
using Microsoft.AspNetCore.Authorization;
using PreTrainee_Month2.CoreLayer.Entities.User_Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PreTrainee_Month2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        // GET: api/<ProductsController>
        private IProductService _productService;
        private IAuthService _authService;
        public ProductsController(IProductService productService, IAuthService authService)
        {
            _productService = productService;
            _authService = authService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            return Ok(await _productService.GetAllProductsAsync());
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _productService.GetProductAsync(id));
        }

        // POST api/<ProductsController>
        [HttpPost]
        [Authorize]

        public async Task<IActionResult> Post([FromBody] ProductDTO productDTO)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Product product = new Product(productDTO);
            await _productService.AddProductAsync(product);
            return Ok();
        }

        // PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        [Authorize]

        public async Task<IActionResult> Put(int id, [FromBody] ProductPutDTO newProductPutDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Product newProduct = new Product(newProductPutDTO);
            await _productService.UpdateProductAsync(id, newProduct);
            return Ok();
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{productId}")]
        [Authorize]
        public async Task<IActionResult> Delete(int productId)
        {
            var jwt = _authService.GetJWTFromHeader(Request);
            UserJWTInfo userInfo = _authService.ParseJWT(jwt);

            //если данный продукт не принадлежит текущему пользователю
            if (!await _productService.CheckPossessionAsync(productId, userInfo.UserId))
            {
                return Unauthorized("Вы не можете удалить чужой продукт!");
            }
            await _productService.DeleteProductAsync(productId);
            return Ok();
        }
    }
}
