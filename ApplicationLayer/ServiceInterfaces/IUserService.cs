using PreTrainee_Month2.CoreLayer;

namespace PreTrainee_Month2.ApplicationLayer.ServiceInterfaces
{
    public interface IUserService
    {
        public string HashPassword(string password);
        public bool VerifyPassword(string password, string hashedUserPassword);
        public  Task<IEnumerable<User>> GetAllUsersWithProductsAsync();
        public Task<IEnumerable<User>> GetAllUsersAsync();
        public Task<User?> GetUserWithProductsAsync(int id);
        public Task<User?> GetUserAsync(int id);
        public Task<User?> GetUserByEmailAsync(string email);
        public  Task AddUserAsync(User user);
        public Task DeleteUserAsync(int id);
        public Task UpdateUserAsync(int id, User newUser);
    }
}
