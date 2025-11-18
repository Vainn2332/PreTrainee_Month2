using PreTrainee_Month2.ApplicationLayer.ServiceInterfaces;
using PreTrainee_Month2.CoreLayer.Product_Entities;
using System.Xml.Linq;

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
            var products = await _productService.GetAllProductsAsync();
            var target =products.FirstOrDefault(p=>p.Name == name);
            if (target == null)
            {
                throw new ArgumentException("Продукт с таким именем не найден!");
            }
            return target;
        }

        public async Task<IEnumerable<Product>> SearchByPriceAsync(decimal price)
        {
            var products = await _productService.GetAllProductsAsync();
            var target = products.Where(p => p.Price>=(int)(price-1)&&p.Price<=(int)(price+1));
            if (target == null)
            {
                throw new ArgumentException("Продукты с такой ценой не найдены!");
            }
            return target;
        }
    }
}
