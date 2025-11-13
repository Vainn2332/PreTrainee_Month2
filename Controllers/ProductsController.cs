using Microsoft.AspNetCore.Mvc;
using PreTrainee_Month2.ApplicationLayer.ServiceInterfaces;
using PreTrainee_Month2.CoreLayer.Product_Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PreTrainee_Month2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        // GET: api/<ProductsController>
        private IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _productService.GetAllProductsAsync());
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _productService.GetProductAsync(id));
        }

        // POST api/<ProductsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductDTO productDTO)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("неверно введены данные!");
            }
            Product product = new Product(productDTO);
            await _productService.AddProductAsync(product);
            return Ok();
        }

        // PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ProductDTO newProductDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("неверно введены данные!");
            }
            Product newProduct = new Product(newProductDTO);//добавить обработку сиротских записей
            await _productService.UpdateProductAsync(id, newProduct);
            return Ok();
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.DeleteProductAsync(id);
            return Ok();
        }
    }
}
