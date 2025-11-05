namespace PreTrainee_Month2.CoreLayer.Repository_Interfaces
{
    public interface IUserRepository
    {
        public Task<IEnumerable<User>> GetUsersAsync();
        public Task<User> GetUser(int id);
        public Task AddUserAsync(User user);
        public Task UpdateUSerAsync(int id,User newUser);
        public Task DeleteUserAsync(int id);
    }
}
