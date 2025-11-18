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

        public async Task<IEnumerable<Product>> FilterByDateOfCreationAsync()
        {
            var products = await _productService.GetAllProductsAsync();
            var outputProducts = products.OrderBy(p => p.DateOfCreation);
            return outputProducts;
        }

        public async Task<IEnumerable<Product>> FilterByDateOfCreationPriceAsync()
        {
            var products = await _productService.GetAllProductsAsync();
            var outputProducts = products.OrderBy(p => p.DateOfCreation);
            return outputProducts;
        }

        public Task<IEnumerable<Product>> FilterByNameAsync()
        {
            throw new NotImplementedException();
        }

         public async Task<Product> SearchByDateOfCreationAsync(DateTime date)
         {
            throw new NotImplementedException();
         }
         

        public async Task<Product> SearchByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<Product> SearchByPriceAsync(decimal price)
        {
            throw new NotImplementedException();
        }
    }
}
