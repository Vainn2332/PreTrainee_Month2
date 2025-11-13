using PreTrainee_Month2.ApplicationLayer.ServiceInterfaces;
using PreTrainee_Month2.CoreLayer;
using PreTrainee_Month2.CoreLayer.Product_Entities;
using PreTrainee_Month2.CoreLayer.Repository_Interfaces;

namespace PreTrainee_Month2.ApplicationLayer.Services
{
    public class ProductService:IProductService
    {
        private IRepository<Product> _productRepository;
        private IUserRepository _userRepository;

        public ProductService(IRepository<Product> productRepository, IUserRepository userRepository)
        {
            _productRepository = productRepository;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllAsync();
        }
        public async Task<Product?> GetProductAsync(int id)
        {
            if (id < 1)
                throw new ArgumentException("Id не может быть <1");
            return await _productRepository.GetAsync(id);
        }
        public async Task AddProductAsync(Product product)
        {
            var users = await _userRepository.GetAllAsync();
            if(!users.Any(u => u.ID==product.UserId))
            {
                throw new ArgumentException("Такого автора не существует!");
            }    

            await _productRepository.AddAsync(product);
        }
        public async Task DeleteProductAsync(int id)
        {
            if (id < 1)
                throw new ArgumentException("Id не может быть <1");

            await _productRepository.DeleteAsync(id);
        }
        public async Task UpdateProductAsync(int id, Product newProduct)
        {
            if (id < 1)
            {
                throw new ArgumentException("Id не может быть <1");
            }

            var users = await _userRepository.GetAllAsync();
            if (!users.Any(u => u.ID == newProduct.UserId))
            {
                throw new ArgumentException("Такого автора не существует!");
            }

            await _productRepository.UpdateAsync(id, newProduct);
        }
    }
}
