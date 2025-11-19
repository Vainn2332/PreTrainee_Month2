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
            var users = await _userRepository.GetAllWithProductsAsync();
            //реализация softDelete(выбираем только у активированных пользователей)
            var products = users.Where(u => u.HasVerifiedEmail == true).SelectMany(u => u.Products);

            return products;
        }
        public async Task<Product?> GetProductAsync(int productId)
        {
            if (productId < 1)
            {
                throw new ArgumentException("Id не может быть <1");
            }

            var product = await _productRepository.GetAsync(productId);
            if(product == null)
            {
                throw new ArgumentException("Данный продукт не найден!");
            }

            var user =await _userRepository.GetAsync(product.UserId);
            if (user.HasVerifiedEmail == false)
            {
                throw new ArgumentException("Данный продукт деактивирован!");
            }
            return product;
        }
        public async Task AddProductAsync(Product product)
        {
            char.ToUpper(product.Name[0]);//всегда добавляем продукты с заглавной буквы

            var users = await _userRepository.GetAllAsync();
            if(!users.Any(u => u.ID==product.UserId))
            {
                throw new ArgumentException("Владельца данного продукта не существует!");
            }

            await _productRepository.AddAsync(product);
        }
        public async Task DeleteProductAsync(int id)
        {
            if (id < 1)
                throw new ArgumentException("Id не может быть <1");
            var target = await _productRepository.GetAsync(id);
            if (target == null) 
            {
                throw new ArgumentException("Товар не найден!");
            }

            await _productRepository.DeleteAsync(id);
        }
        public async Task UpdateProductAsync(int id, Product newProduct)
        {
            char.ToUpper(newProduct.Name[0]);//всегда с заглавной буквы

            var products = await _productRepository.GetAllAsync();//////////////
            var product = products.FirstOrDefault(p=>p.ID==id);
            if(product == null)
            {
                throw new ArgumentException("такого продукта не существует!");
            }
            newProduct.UserId = product.UserId;
            

            await _productRepository.UpdateAsync(id, newProduct);
        }
    }
}
