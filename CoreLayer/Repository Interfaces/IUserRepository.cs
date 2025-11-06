namespace PreTrainee_Month2.CoreLayer.Repository_Interfaces
{
    public interface IUserRepository:IRepository<User>
    {
        public Task GetAllWithProducts();
        public Task GetWithProducts(int id);
    }
}
