namespace PreTrainee_Month2.CoreLayer.Repository_Interfaces
{
    public interface IUserRepository:IRepository<User>
    {
        public Task<IEnumerable<User>> GetAllWithProductsAsync();
        public Task<User?> GetWithProductsAsync(int id);
    }
}
