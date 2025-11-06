namespace PreTrainee_Month2.CoreLayer.Repository_Interfaces
{
    public interface IUserRepository:IRepository<User>
    {
        public Task<IEnumerable<User>> GetAllWithProducts();
        public Task<User> GetWithProducts(int id);
    }
}
