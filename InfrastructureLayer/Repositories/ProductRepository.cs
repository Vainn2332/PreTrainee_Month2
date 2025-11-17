using Microsoft.EntityFrameworkCore;
using PreTrainee_Month2.CoreLayer.Product_Entities;
using PreTrainee_Month2.CoreLayer.Repository_Interfaces;
using PreTrainee_Month2.InfrastructureLayer.DataBaseContext;

namespace PreTrainee_Month2.InfrastructureLayer.Repositories
{

    public class ProductRepository : IRepository<Product>
    {
        private DBContext _dbContext;

        public ProductRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _dbContext.Products.AsNoTracking().ToListAsync();
        }
        public async Task<Product?> GetAsync(int id)
        {
            return await _dbContext.Products
                .FirstOrDefaultAsync(p => p.ID == id);
        }
        public async Task AddAsync(Product product)
        {
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();
        }

        public async  Task DeleteAsync(int id)
        {
            await _dbContext.Products
                .Where(p => p.ID == id)
                .ExecuteDeleteAsync();
        }


        public async Task UpdateAsync(int id, Product newProduct)
        {
            await _dbContext.Products
                .Where(p => p.ID == id)
                .ExecuteUpdateAsync(p => p
                .SetProperty(s => s.UserId, newProduct.UserId)
                .SetProperty(s => s.Price, newProduct.Price)
                .SetProperty(s => s.DateOfCreation, newProduct.DateOfCreation)
                .SetProperty(s => s.Description, newProduct.Description)
                .SetProperty(s => s.Name, newProduct.Name));
        }
    }
}
