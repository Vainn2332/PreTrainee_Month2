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
        public async Task<Product> Get(int id)
        {
            return await _dbContext.Products
                .FirstOrDefaultAsync(p => p.ID == id);

        }
        public async Task AddAsync(Product product)
        {
            await _dbContext.Products.AddAsync(product);//проверить работает ли? без связующего поля
        }

        public async  Task DeleteAsync(int id)
        {
            await _dbContext.Products
                .Where(p => p.ID == id)
                .ExecuteDeleteAsync();
        }


        public async Task UpdateAsync(int id, Product newUser)
        {
            await _dbContext.Products
                .Where(p => p.ID == id)
                .ExecuteUpdateAsync(p => p
                .SetProperty(s => s.UserId, newUser.UserId)
                .SetProperty(s => s.Price, newUser.Price)
                .SetProperty(s => s.DateOfCreation, newUser.DateOfCreation)
                .SetProperty(s => s.Description, newUser.Description));
        }
    }
}
