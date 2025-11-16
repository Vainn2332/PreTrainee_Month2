using Microsoft.EntityFrameworkCore;
using PreTrainee_Month2.CoreLayer;
using PreTrainee_Month2.CoreLayer.Repository_Interfaces;
using PreTrainee_Month2.InfrastructureLayer.DataBaseContext;
namespace PreTrainee_Month2.InfrastructureLayer.Repositories
{
    public class UserRepository : IUserRepository
    {
        private DBContext _dbContext;
        public UserRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<User>> GetAllWithProductsAsync()
        {
            return await _dbContext.Users
                .Include(u=>u.Products)
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _dbContext.Users.AsNoTracking().ToListAsync();
        }
        public async Task<User?> GetWithProductsAsync(int id)
        {
            return await _dbContext.Users
                .Include(u=>u.Products)
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.ID == id);
        }

        public async Task<User?> GetAsync(int id)
        {
            return await _dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.ID == id);
        }

        public async Task AddAsync(User user)
        {
           await  _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _dbContext.Users
                .Where(u => u.ID == id)
                .ExecuteDeleteAsync();
        }

        

        public async Task UpdateAsync(int id, User newUser)
        {
            await _dbContext.Users.Where(u => u.ID == id)
                 .ExecuteUpdateAsync(p =>p
                 .SetProperty(s => s.EmailAddress, newUser.EmailAddress)
                 .SetProperty(s => s.Name, newUser.Name)                          
                 .SetProperty(s => s.Role, newUser.Role)
                 .SetProperty(s=>s.HasVerifiedEmail,newUser.HasVerifiedEmail)
                 .SetProperty(s => s.Password,newUser.Password));
        }
    }
}
