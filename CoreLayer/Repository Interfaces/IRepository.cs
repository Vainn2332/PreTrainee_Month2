namespace PreTrainee_Month2.CoreLayer.Repository_Interfaces
{
    public interface IRepository<T>
    {
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<T?> GetAsync(int id);
        public Task AddAsync(T entity);
        public Task UpdateAsync(int id, T newEntity);
        public Task DeleteAsync(int id);
    }
}
