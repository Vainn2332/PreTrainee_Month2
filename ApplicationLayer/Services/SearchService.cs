using PreTrainee_Month2.ApplicationLayer.ServiceInterfaces;
using PreTrainee_Month2.CoreLayer.Product_Entities;

namespace PreTrainee_Month2.ApplicationLayer.Services
{
    public class SearchService : ISearchService
    {
        private readonly IProductService _productService;
        private readonly IUserService _userService;

        public SearchService(IProductService productService, IUserService userService)
        {
            _productService = productService;
            _userService = userService;
        }


        public async Task<IEnumerable<Product>> FilterByNameAscendingAsync()
        {
            var products = await _productService.GetAllProductsAsync();
            var outputProducts = products.OrderBy(p => p.Name);
            return outputProducts;
        }

        public async Task<IEnumerable<Product>> FilterByNameDescendingAsync()
        {
            var products = await _productService.GetAllProductsAsync();
            var outputProducts = products.OrderByDescending(p => p.Name);
            return outputProducts;
        }

        public async Task<IEnumerable<Product>> FilterByPriceAscendingAsync()
        {
            var products = await _productService.GetAllProductsAsync();
            var outputProducts = products.OrderBy(p => p.Price);
            return outputProducts;
        }

        public  async Task<IEnumerable<Product>> FilterByPriceDescendingAsync()
        {
            var products = await _productService.GetAllProductsAsync();
            var outputProducts = products.OrderByDescending(p => p.Price);
            return outputProducts;
        }

        public async Task<Product?> SearchByNameAsync(string name)
        {
            
        }

        public Task<Product> SearchByPriceAsync(decimal price)
        {
            throw new NotImplementedException();
        }
    }
}
