using PreTrainee_Month2.ApplicationLayer.ServiceInterfaces;
using PreTrainee_Month2.CoreLayer;
using PreTrainee_Month2.CoreLayer.Repository_Interfaces;

namespace PreTrainee_Month2.ApplicationLayer.Services
{
    public class UserService:IUserService
    { 
        private IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> GetAllUsersWithProductsAsync()
        {
            return await _userRepository.GetAllWithProductsAsync();
        }
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllAsync();
        }
        public async Task<User?> GetUserWithProductsAsync(int id)
        {
            if (id < 1)
                throw new ArgumentException("id не может быть <1");
            return await _userRepository.GetWithProductsAsync(id);
        }
        public async Task<User?> GetUserAsync(int id)
        {
            if (id < 1)
                throw new ArgumentException("id не может быть <1");
            return await _userRepository.GetAsync(id);
        }
        public async Task AddUserAsync(User user)
        {
            await _userRepository.AddAsync(user);
        }
        public async Task DeleteUserAsync(int id)
        {
            if (id < 1)
                throw new ArgumentException("id не может быть <1");
            await _userRepository.DeleteAsync(id);
        }
        public async Task UpdateUserAsync(int id, User newUser)
        {
            if (id < 1)
                throw new ArgumentException("id не может быть <1");
            await _userRepository.UpdateAsync(id, newUser);
        }
    }
}
