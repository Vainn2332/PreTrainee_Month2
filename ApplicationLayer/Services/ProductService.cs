using PreTrainee_Month2.CoreLayer.Product_Entities;
using PreTrainee_Month2.CoreLayer.Repository_Interfaces;

namespace PreTrainee_Month2.ApplicationLayer.Services
{
    public class ProductService
    {
        private IRepository<Product> _productRepository;

        public ProductService(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
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
        {//чекнуть создание сиротской записи
            await _productRepository.AddAsync(product);
        }
        public async Task DeleteProductAsync(int id)
        {
            if (id < 1)
                throw new ArgumentException("Id не может быть <1");
            await _productRepository.DeleteAsync(id);
        }
        public async Task UpdateProductAsync(int id, Product newUser)
        {
            if (id < 1)
                throw new ArgumentException("Id не может быть <1");
            await _productRepository.UpdateAsync(id, newUser);
        }
    }
}
