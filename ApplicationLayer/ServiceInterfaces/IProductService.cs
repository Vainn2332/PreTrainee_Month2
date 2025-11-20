using PreTrainee_Month2.CoreLayer;
using PreTrainee_Month2.CoreLayer.Product_Entities;

namespace PreTrainee_Month2.ApplicationLayer.ServiceInterfaces
{
    public interface IProductService
    {
        public Task<IEnumerable<Product>> GetAllProductsAsync();
        public Task<Product?> GetProductAsync(int id);
        public Task AddProductAsync(Product user);
        public Task DeleteProductAsync(int id);
        public Task UpdateProductAsync(int id, Product newProduct);
        public Task<bool> CheckPossessionAsync(int productId, int userId);
    }
}
